using System;
using System.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Foundation;
using Plugin.LatestVersion.Abstractions;
using UIKit;

namespace Plugin.LatestVersion
{
    /// <summary>
    /// <see cref="ILatestVersion"/> implementation for iOS.
    /// </summary>
    public class LatestVersionImplementation : ILatestVersion
    {
        string _bundleName => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleName").ToString();
        string _bundleIdentifier => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleIdentifier").ToString();
        string _bundleVersion => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();

        /// <inheritdoc />
        public async Task<bool> IsUsingLatestVersion()
        {
            try
            {
                var latestVersion = await GetLatestVersionNumber();

                return Version.Parse(latestVersion).CompareTo(Version.Parse(_bundleVersion)) <= 0;
            }
            catch (Exception e)
            {
                throw new AppVersionComparisonException(e);
            }
        }

        /// <inheritdoc />
        public async Task<string> GetLatestVersionNumber()
        {
            return await GetLatestVersionNumber(_bundleIdentifier);
        }

        /// <inheritdoc />
        public async Task<string> GetLatestVersionNumber(string appName)
        {
            var version = string.Empty;

            using (var request = new HttpRequestMessage(HttpMethod.Get, $"http://itunes.apple.com/lookup?bundleId={appName}"))
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

                                var appStoreItem = JsonValue.Parse(content);

                                version = appStoreItem["results"][0]["version"];

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
            var appName = _bundleName.Replace(" ", "").ToLower();
            OpenAppInStore(appName);
        }

        /// <inheritdoc />
        public void OpenAppInStore(string appName)
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl($"http://appstore.com/{appName}"));
        }
    }
}
