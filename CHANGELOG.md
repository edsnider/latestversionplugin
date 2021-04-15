# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [2.1.0] - 2021-04-15

### Added
- Added optional `CountryCode` property. GitHub issues [#15](https://github.com/edsnider/LatestVersionPlugin/issues/15), [#25](https://github.com/edsnider/LatestVersionPlugin/issues/25)

## [2.0.0] - 2020-02-25

### Removed
- Removed parameterized method overloads:
  - Removed `GetLatestVersionNumber(string appName)` - use `GetLatestVersionNumber()` instead
  - Removed `OpenAppInStore(string appName)` - use `OpenAppInStore()` instead

### Fixed
- [iOS, macOS] Fixed `OpenAppInStore()` implementation to use the app URL provided by the iTunes lookup API instead of the appstore.com URL which was problematic. GitHub issue [#6](https://github.com/edsnider/latestversionplugin/issues/6)

## [1.1.2] - 2018-12-28

### Fixed
- [Android] Fixed pattern matching logic in `GetLatestVersionNumber()` GitHub issue [#10](https://github.com/edsnider/LatestVersionPlugin/issues/10)

## [1.1.1] - 2018-04-26

### Fixed
- [Android] Fixed pattern matching logic in `GetLatestVersionNumber()` based on changes to play.google.com. GitHub issue [#5](https://github.com/edsnider/LatestVersionPlugin/issues/5)

## [1.1.0] - 2018-02-07

### Added
- Added `InstalledVersionNumber` property. GitHub issue [#2](https://github.com/edsnider/LatestVersionPlugin/issues/2)

## [1.0.1] - 2018-02-01

### Added
- NuGet icon

## [1.0.0] - 2018-01-16

### Added
- Initial release
  - Suppored platforms: Xamarin.iOS, Xamarin.Android, Xamarin.Mac, UWP.

### Changed
Breaking Changes (from 1.0.0-beta releases):
- Changed `void OpenAppInStore()` to `Task OpenAppInStore()`
- Changed `void OpenAppInStore(string appName)` to `Task OpenAppInStore(string appName)`
