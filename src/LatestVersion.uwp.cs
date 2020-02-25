using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Services.Store;
using Plugin.LatestVersion.Abstractions;

namespace Plugin.LatestVersion
{
    /// <summary>
    /// <see cref="ILatestVersion"/> implementation for UWP.
    /// </summary>
    public class LatestVersionImplementation : ILatestVersion
    {
        string _packageVersion => Package.Current.Id.Version.ToVersionString();

        /// <inheritdoc />
        public string InstalledVersionNumber
        {
            get => _packageVersion;
        }

        /// <inheritdoc />
        public async Task<bool> IsUsingLatestVersion()
        {
            try
            {
                var context = StoreContext.GetDefault();
                var updates = await context.GetAppAndOptionalStorePackageUpdatesAsync();

                return updates.Count <= 0;
            }
            catch (Exception e)
            {
                throw new LatestVersionException($"Error checking for updates available in the Store.", e);
            }
        }

        /// <inheritdoc />
        public async Task<string> GetLatestVersionNumber()
        {
            try
            {
                var version = _packageVersion;
                var context = StoreContext.GetDefault();
                var updates = await context.GetAppAndOptionalStorePackageUpdatesAsync();
                
                if (updates.Count > 0)
                {
                    version = updates[0]?.Package?.Id?.Version.ToVersionString();
                }

                return version;
            }
            catch (Exception e)
            {
                throw new LatestVersionException($"Error getting the latest version number available in the Store.", e);
            }
        }

        /// <inheritdoc />
        public async Task OpenAppInStore()
        {
            try
            {
                var context = StoreContext.GetDefault();
                var product = await context.GetStoreProductForCurrentAppAsync();
                var storeId = product.Product.StoreId;

                if (Guid.TryParse(storeId, out Guid appId))
                {
                    await Windows.System.Launcher.LaunchUriAsync(new Uri($"ms-windows-store://pdp/?AppId={storeId}"));
                }
                else
                {
                    await Windows.System.Launcher.LaunchUriAsync(new Uri($"ms-windows-store://pdp/?ProductId={storeId}"));
                }
            }
            catch (Exception e)
            {
                throw new LatestVersionException($"Unable to open the current app in the Store.", e);
            }
        }
    }
}
