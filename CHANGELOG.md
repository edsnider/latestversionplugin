# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

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