# LatestVersion Plugin for Xamarin and Windows apps

[![Build Status](https://dev.azure.com/edsnider/latestversionplugin/_apis/build/status/edsnider.latestversionplugin?branchName=master)](https://dev.azure.com/edsnider/latestversionplugin/_build/latest?definitionId=7&branchName=master)
[![MyGet](https://img.shields.io/myget/edsnider/vpre/Xam.Plugin.LatestVersion.svg?label=myget)](https://www.myget.org/feed/edsnider/package/nuget/Xam.Plugin.LatestVersion)
[![NuGet](https://img.shields.io/nuget/v/Xam.Plugin.LatestVersion.svg?label=nuget)](https://www.nuget.org/packages/Xam.Plugin.LatestVersion)

Easily detect if you are running the latest version of your iOS, macOS, Android, or Windows app and open it in the App Store, Play Store, or Microsoft Store to update it.

## Supported platforms and versions

|Platform|Supported|Version|
| ------------------- | :-----------: | :------------------: |
|Xamarin.iOS|Yes|7.0+|
|Xamarin.Android|Yes|10.0+|
|Xamarin.Mac|Yes|10.7+|
|Windows (UWP)|Yes|10.0+<br>(Target 14393+)|

## API Usage

### Check for latest version

Check if the current running app is the latest version available in the public store:

```csharp
bool isLatest = await CrossLatestVersion.Current.IsUsingLatestVersion();
```

### Get latest version number

Get the version number of the current running app's latest version available in the public store:

```csharp
string latestVersionNumber = await CrossLatestVersion.Current.GetLatestVersionNumber();
```

### Get installed version number

Get the version number of the current app's installed version:

```csharp
string installedVersionNumber = CrossLatestVersion.Current.InstalledVersionNumber;
```

### Open app in public store

Open the current running app in the public store:

```csharp
await CrossLatestVersion.Current.OpenAppInStore();
```

### Set country code

Set the country code to be used when looking up the current app in the public store:

```csharp
CrossLatestVersion.Current.CountryCode = "nz";
```

Notes about the `CountryCode` property:
- It is optional; if not provided it will default to "us".
- It is only needed/used on iOS. 
- If used, the value should be an alpha-2 code (ISO 3166-1). 

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
