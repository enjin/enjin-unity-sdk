# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- Added `UpdateName` request to `IProjectSchema`.

### Fixed

- Fixed GraphQL template argument type for `BridgeClaimAsset`.

## [2.0.0-beta.1] - 2022-07-18

### Added

- Added logging utilities.
- Added IL2CPP support.
- Added assembly definition `Enjin.Sdk`.

### Changed

- Implemented V.2 schemas and removed V.1 schemas.
- Implemented player and project client classes to handle sending requests to the platform.
- Implemented event service class to handle Enjin Cloud events.
- Changed minimum Unity version to `2020.3`.
- Changed package layout to more closely follow Unity's recommended package layout.
- Updated third party notices for new dependencies.
- Updated internal Pusher client.

### Removed

- Removed Enjin menu from Unity Editor.

[Unreleased]: https://github.com/enjin/enjin-unity-sdk/compare/2.0.0-beta.1...HEAD

[2.0.0-beta.1]: https://github.com/enjin/enjin-unity-sdk/releases/tag/2.0.0-beta.1
