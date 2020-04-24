using System;
using Plugin.LatestVersion.Abstractions;

namespace Plugin.LatestVersion
{
    /// <summary>
    /// Cross platform LatestVersion Plugin implementations. Use <see cref="Current"/> to access the implementation for the current platform. 
    /// </summary>
    public class CrossLatestVersion
    {
        internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException("This functionality is not implemented in the portable version of this assembly. You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");

        static Lazy<ILatestVersion> _impl = new Lazy<ILatestVersion>(() => CreateLatestVersionImplementation(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        static ILatestVersion CreateLatestVersionImplementation()
        {
#if NETSTANDARD1_1
            return null;
#else
            return new LatestVersionImplementation();
#endif
        }
        
        private static string _IosAppCountryAlpha2Code = "";

        /// <summary>
        /// Needs to be set for IOS application that available only in specific country.
        /// </summary>
        /// <value>Alpha 2 code of IOS Application that available in specific country.</value>
        public static string IosAppCountryAlpha2Code
        {
            set => _IosAppCountryAlpha2Code = value + "/";
            internal get => _IosAppCountryAlpha2Code;
        }
        
        /// <summary>
        /// Checks if the plugin is supported on the current platform.
        /// </summary>
        public static bool IsSupported => _impl.Value != null;

        /// <summary>
        /// Gets the current LatestVersion Plugin implementation.
        /// </summary>
        public static ILatestVersion Current
        {
            get
            {
                if (_impl.Value == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }

                return _impl.Value;
            }
        }
    }
}
