using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Net = Android.Net;
using Plugin.LatestVersion.Abstractions;

namespace Plugin.LatestVersion
{
    /// <summary>
    /// <see cref="ILatestVersion"/> implementation for Android.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LatestVersionImplementation : ILatestVersion
    {
        string _packageName => Application.Context.PackageName;
        string _versionName => Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, 0).VersionName;

        /// <inheritdoc />
        public string InstalledVersionNumber
        {
            get => _versionName;
        }

        /// <inheritdoc />
        public async Task<bool> IsUsingLatestVersion()
        {
            var latestVersion = string.Empty;

            try
            {
                latestVersion = await GetLatestVersionNumber();

                return Version.Parse(latestVersion).CompareTo(Version.Parse(_versionName)) <= 0;
            }
            catch (Exception e)
            {
                throw new LatestVersionException($"Error comparing current app version number with latest. Version name={_versionName} and lastest version={latestVersion} .", e);
            }
        }

        /// <inheritdoc />
        public async Task<string> GetLatestVersionNumber()
        {
            return await GetLatestVersionNumber(_packageName);
        }

        /// <inheritdoc />
        public async Task<string> GetLatestVersionNumber(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new ArgumentNullException(nameof(appName));
            }

            var version = string.Empty;
            // Use cache parameter to prevent caching
            var url = $"https://play.google.com/store/apps/details?id={appName}&hl=en&cache={Guid.NewGuid()}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                using (var handler = new HttpClientHandler())
                {
                    using (var client = new HttpClient(handler))
                    {
                        using (var responseMsg = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                        {
                            if (!responseMsg.IsSuccessStatusCode)
                            {
                                throw new LatestVersionException($"Error connecting to the Play Store. Url={url}.");
                            }

                            try
                            {
                                var content = responseMsg.Content == null ? null : await responseMsg.Content.ReadAsStringAsync();

                                var versionMatch = Regex.Match(content, "<div[^>]*>Current Version</div><span[^>]*><div[^>]*><span[^>]*>(.*?)<").Groups[1];

                                if (versionMatch.Success)
                                {
                                    version = versionMatch.Value.Trim();
                                }
                            }
                            catch (Exception e)
                            {
                                throw new LatestVersionException($"Error parsing content from the Play Store. Url={url}.", e);
                            }
                        }
                    }
                }
            }

            return version;
        }

        /// <inheritdoc />
        public Task OpenAppInStore()
        {
            return OpenAppInStore(_packageName);
        }

        /// <inheritdoc />
        public Task OpenAppInStore(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new ArgumentNullException(nameof(appName));
            }

            try
            {
                var intent = new Intent(Intent.ActionView, Net.Uri.Parse($"market://details?id={appName}"));
                intent.SetPackage("com.android.vending");
                intent.SetFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(intent);
            }
            catch (ActivityNotFoundException)
            {
                var intent = new Intent(Intent.ActionView, Net.Uri.Parse($"https://play.google.com/store/apps/details?id={appName}"));
                Application.Context.StartActivity(intent);
            }

            return Task.FromResult(true);
        }
    }
}
