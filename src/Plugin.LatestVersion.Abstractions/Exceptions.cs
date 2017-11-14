using System;

namespace Plugin.LatestVersion
{
    /// <summary>
    /// Store access exception.
    /// </summary>
    public class StoreAccessException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Plugin.LatestVersion.StoreAccessException"/> class.
        /// </summary>
        public StoreAccessException()
            : base("Error connecting to the app store.")
        {}
    }

    /// <summary>
    /// Store content exception.
    /// </summary>
    public class StoreContentException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Plugin.LatestVersion.StoreContentException"/> class.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="innerException">Inner exception.</param>
        public StoreContentException(string propertyName, Exception innerException)
            : base($"Error getting content from app store: {propertyName}.", innerException)
        {}
    }

    /// <summary>
    /// App not found exception.
    /// </summary>
    public class AppNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Plugin.LatestVersion.AppNotFoundException"/> class.
        /// </summary>
        /// <param name="appName">App name.</param>
        public AppNotFoundException(string appName)
            : base($"Error opening app: {appName}.")
        {}
    }

    /// <summary>
    /// App version comparison exception.
    /// </summary>
    public class AppVersionComparisonException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Plugin.LatestVersion.AppVersionComparisonException"/> class.
        /// </summary>
        /// <param name="innerException">Inner exception.</param>
        public AppVersionComparisonException(Exception innerException)
            : base($"Error comparing current app version number with latest.", innerException)
        {}
    }
}
