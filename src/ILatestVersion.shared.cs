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
        /// Gets the version number of an app's latest version available in the public store.
        /// </summary>
        /// <returns>The specified app's latest version number</returns>
        /// <param name="appName">Name of the app to get.</param>
        Task<string> GetLatestVersionNumber(string appName);

        /// <summary>
        /// Opens the current app in the public store.
        /// </summary>
        Task OpenAppInStore();

        /// <summary>
        /// Opens an app in the public store.
        /// </summary>
        /// <param name="appName">Name of the app to open.</param>
        Task OpenAppInStore(string appName);
    }
}
