using System;
using System.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Foundation;
using Plugin.LatestVersion.Abstractions;

namespace Plugin.LatestVersion
{
    internal class App
    {
        public string Version { get; set; }
        public string Url { get; set; }
    }

    /// <summary>
    /// <see cref="ILatestVersion"/> implementation for iOS.
    /// </summary>
    public class LatestVersionImplementation : ILatestVersion
    {
        App _app;

        string _bundleIdentifier => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleIdentifier").ToString();
        string _bundleVersion => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();

        /// <inheritdoc />
        public string CountryCode { get; set; } = "us";

        /// <inheritdoc />
        public string InstalledVersionNumber
        {
            get => _bundleVersion;
        }

        /// <inheritdoc />
        public async Task<bool> IsUsingLatestVersion()
        {
            var latestVersion = string.Empty;

            try
            {
                latestVersion = await GetLatestVersionNumber();

                if (!latestVersion.Contains("."))
                    latestVersion += ".0";

                var bundleVersion = _bundleVersion;

                if (!bundleVersion.Contains("."))
                    bundleVersion += ".0";

                return Version.Parse(latestVersion).CompareTo(Version.Parse(bundleVersion)) <= 0;
            }
            catch (Exception e)
            {
                throw new LatestVersionException($"Error comparing current app version number with latest. Bundle version={_bundleVersion} and lastest version={latestVersion} .", e);
            }
        }

        /// <inheritdoc />
        public async Task<string> GetLatestVersionNumber()
        {
            _app = await LookupApp();

            return _app?.Version;
        }

        /// <inheritdoc />
        public async Task OpenAppInStore()
        {
            try
            {
                if (_app == null)
                {
                    _app = await LookupApp();
                }

#if __IOS__
                UIKit.UIApplication.SharedApplication.OpenUrl(new NSUrl($"{_app.Url}"));
#elif __MACOS__
                AppKit.NSWorkspace.SharedWorkspace.OpenUrl(new NSUrl($"{_app.Url}"));
#endif
            }
            catch (Exception e)
            {
                throw new LatestVersionException($"Unable to open app in App Store.", e);
            }
        }

        async Task<App> LookupApp()
        {
            var countryCode = string.IsNullOrWhiteSpace(CountryCode) ? "us" : CountryCode;

            try
            {
                using var http = new HttpClient();
                var response = await http.GetAsync($"http://itunes.apple.com/{countryCode}/lookup?bundleId={_bundleIdentifier}");
                var content = response.Content == null ? null : await response.Content.ReadAsStringAsync();
                var appLookup = JsonValue.Parse(content);

                return new App
                {
                    Version = appLookup["results"][0]["version"],
                    Url = appLookup["results"][0]["trackViewUrl"]
                };
            }
            catch (Exception e)
            {
                throw new LatestVersionException($"Error looking up app details in App Store. BundleIdentifier={_bundleIdentifier}.", e);
            }
        }
    }
}
