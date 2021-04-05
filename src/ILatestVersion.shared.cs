using System;
using System.Threading.Tasks;

namespace Plugin.LatestVersion.Abstractions
{
    /// <summary>
    /// LatestVersion plugin
    /// </summary>
    public interface ILatestVersion
    {
        /// <summary>
        /// Gets and sets the country code to use when looking up the app in the public store.
        /// Note: This is currently only needed/used by the Apple implementations (defaults to "us")
        /// </summary>
        string CountryCode { get; set; }

        /// <summary>
        /// Gets the version number of the current app's installed version.
        /// </summary>
        /// <value>The current app's installed version number.</value>
        string InstalledVersionNumber { get; }

        /// <summary>
        /// Checks if the current app is the latest version available in the public store.
        /// </summary>
        /// <returns>True if the current app is the latest version available, false otherwise.</returns>
        Task<bool> IsUsingLatestVersion();

        /// <summary>
        /// Gets the version number of the current app's latest version available in the public store.
        /// </summary>
        /// <returns>The current app's latest version number.</returns>
        Task<string> GetLatestVersionNumber();

        /// <summary>
        /// Opens the current app in the public store.
        /// </summary>
        Task OpenAppInStore();
    }
}
