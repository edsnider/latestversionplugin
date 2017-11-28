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
        public async Task<bool> IsUsingLatestVersion()
        {
            try
            {
                var latestVersion = await GetLatestVersionNumber();

                return Version.Parse(latestVersion).CompareTo(Version.Parse(_versionName)) <= 0;
            }
            catch (Exception e)
            {
                throw new AppVersionComparisonException(e);
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
            var version = string.Empty;

            using (var request = new HttpRequestMessage(HttpMethod.Get, $"https://play.google.com/store/apps/details?id={appName}"))
            {
                using (var handler = new HttpClientHandler())
                {
                    using (var client = new HttpClient(handler))
                    {
                        using (var responseMsg = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                        {
                            if (!responseMsg.IsSuccessStatusCode)
                            {
                                throw new StoreAccessException();
                            }

                            try
                            {
                                var content = responseMsg.Content == null ? null : await responseMsg.Content.ReadAsStringAsync();

                                var versionMatch = Regex.Match(content, "<div class=\"content\" itemprop=\"softwareVersion\">(.*?)</div>").Groups[1];

                                if (versionMatch.Success)
                                {
                                    version = versionMatch.Value.Trim();
                                }
                            }
                            catch (Exception e)
                            {
                                throw new StoreContentException("version", e);
                            }
                        }
                    }
                }
            }

            return version;
        }

        /// <inheritdoc />
        public void OpenAppInStore()
        {
            OpenAppInStore(_packageName);
        }

        /// <inheritdoc />
        public void OpenAppInStore(string appName)
        {
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
        }
    }
}
