# LatestVersion Plugin for Xamarin and Windows apps

[![NuGet](https://img.shields.io/nuget/v/Xam.Plugin.LatestVersion.svg?label=NuGet)](https://www.nuget.org/packages/Xam.Plugin.LatestVersion/)
[![Build status](https://ci.appveyor.com/api/projects/status/sbvvle9doh9k6fkw?svg=true)](https://ci.appveyor.com/project/edsnider/latestversionplugin)

Easily detect if you are running the latest version of your iOS, macOS, Android, or Windows app and open it in the App Store, Play Store, or Microsoft Store to update it. You can also use this plugin to check the latest version number of other apps and to open other apps in the App Store, Play Store, and Microsoft Store.

## Supported platforms and versions

|Platform|Supported|Version|
| ------------------- | :-----------: | :------------------: |
|Xamarin.iOS|Yes|7.0+|
|Xamarin.Android|Yes|10.0+|
|Xamarin.Mac|Yes|10.7+|
|Windows (UWP)|Yes|10.0+<br>(Target 14393+)|

## API Usage

### Check latest version

Check if the current running app is the latest version available in the public store:

```csharp
bool isLatest = await CrossLatestVersion.Current.IsUsingLatestVersion();
```

### Get latest version number

Get the version number of the current running app's latest version available in the public store:

```csharp
string latestVersionNumber = await CrossLatestVersion.Current.GetLatestVersionNumber();
```

Get the version number of any app's latest version available in the public store:

```csharp
string latestVersionNumber = await CrossLatestVersion.Current.GetLatestVersionNumber("appName");
```

- `appName` should be the app's **bundle identifier** (`CFBundleIdentifier`) on iOS/macOS and the app's **package name** on Android.
- This method is not currently supported on UWP. Only `GetLatestVersionNumber()` is supported.

### Open app in public store

Open the current running app in the public store:

```csharp
CrossLatestVersion.Current.OpenAppInStore();
```

Open any app in the public store:

```csharp
CrossLatestVersion.Current.OpenAppInStore("appName");
```

- `appName` should be the app's **bundle name** (`CFBundleName` or `CFBundleDisplayName`) on iOS/macOS, the app's **package name** on Android, and the app's **Store ID** or **App ID** on Windows.

## Example

```csharp
using Plugin.LatestVersion;

var isLatest = await CrossLatestVersion.Current.IsUsingLatestVersion();

if (!isLatest)
{
    var update = await DisplayAlert("New Version", "There is a new version of this app available. Would you like to update now?", "Yes", "No");

    if (update)
    {
        await CrossLatestVersion.Current.OpenAppInStore();
    }
}
```

## License

Licensed under MIT. See [License file](https://github.com/edsnider/LatestVersionPlugin/blob/master/LICENSE)
