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
                var version = Package.Current.Id.Version.ToVersionString();
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
        public async Task<string> GetLatestVersionNumber(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new ArgumentNullException(nameof(appName));
            }

            // NOTE: This method is currently not support for UWP, so returning string.Empty

            return await Task.FromResult(string.Empty);
        }

        /// <inheritdoc />
        public async Task OpenAppInStore()
        {
            try
            {
                var context = StoreContext.GetDefault();
                var product = await context.GetStoreProductForCurrentAppAsync();
                var storeId = product.Product.StoreId;

                await OpenAppInStore(storeId);
            }
            catch (Exception e)
            {
                throw new LatestVersionException($"Unable to open the current app in the Store.", e);
            }
        }

        /// <inheritdoc />
        public async Task OpenAppInStore(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new ArgumentNullException(nameof(appName));
            }

            try
            {
                if (Guid.TryParse(appName, out Guid appId))
                {
                    await Windows.System.Launcher.LaunchUriAsync(new Uri($"ms-windows-store://pdp/?AppId={appName}"));
                }
                else
                {
                    await Windows.System.Launcher.LaunchUriAsync(new Uri($"ms-windows-store://pdp/?ProductId={appName}"));
                }
            }
            catch (Exception e)
            {
                throw new LatestVersionException($"Unable to open {appName} in the Store.", e);
            }
        }
    }
}
