# LatestVersion Plugin for Xamarin apps

[![NuGet](https://img.shields.io/nuget/v/Xam.Plugin.LatestVersion.svg?label=NuGet)](https://www.nuget.org/packages/Xam.Plugin.LatestVersion/)
[![Build status](https://ci.appveyor.com/api/projects/status/sbvvle9doh9k6fkw?svg=true)](https://ci.appveyor.com/project/edsnider/latestversionplugin)

Easily detect if you are running the latest version of your iOS, macOS, or Android app and open it in the App Store or Play Store to update it. You can also use this plugin to check the latest version number of other apps and to open other apps in the App Store and Play Store.

## Supported platforms and versions

_This plugin is currently a beta/pre-release and only supports Xamarin.iOS, Xamarin.Android, and Xamarin.Mac. Support for additional app platforms is intended._

|Platform|Supported|Version|
| ------------------- | :-----------: | :------------------: |
|Xamarin.iOS|Yes|7+|
|Xamarin.Android|Yes|10+|
|Xamarin.Mac|Yes|10.7+|
|Windows (UWP)|No||

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

- `appName` should be the app's **bundle identifier** on iOS/macOS and the app's **package name** on Android.

### Open app in public store

Open the current running app in the public store:

```csharp
CrossLatestVersion.Current.OpenAppInStore();
```

Open any app in the public store:

```csharp
CrossLatestVersion.Current.OpenAppInStore("appName");
```

- `appName` should be the app's **bundle name** with all spaces removed (example: "My App" should be "MyApp") on iOS and the app's **package name** on Android.

**Note about macOS**: `appName` is ignored on macOS in `OpenAppInStore()`. Instead it simply opens the Mac App Store to the updates page because, unlike iOS, you cannot open a specific app page in the Mac App Store using just the app's name.

## Example

```csharp
using Plugin.LatestVersion;

var isLatest = await CrossLatestVersion.Current.IsUsingLatestVersion();

if (!isLatest)
{
    var update = await DisplayAlert("New Version", "There is a new version of this app available. Would you like to update now?", "Yes", "No");

    if (update)
    {
        CrossLatestVersion.Current.OpenAppInStore();
    }
}
```

## License

Licensed under MIT. See [License file](https://github.com/edsnider/LatestVersionPlugin/blob/master/LICENSE)