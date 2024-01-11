# PackStudio
Visual packing tool using [pack](https://github.com/cfnptr/pack) library.

## Usage
### Package opening
* Make program default for .pak files
* Use Open with on packages
* Drag&Drop packages on program window

### Package creation
* Drag&Drop Directories in program window and press "Build" button

## Roadmap
* Asset exclusion from build
* Project save and open
* Asset properties display in Inspector
* Image size and format editing in inspector
* Audio transcoding
* Video  transcoding
* Default Asset import settings
* Asset caching

## Project requirements
* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
* [Avalonia UI Extension](https://marketplace.visualstudio.com/items?itemName=AvaloniaTeam.AvaloniaVS)
* Put `pack.dll` to debug build folder _(Required to pack and unpack files)_

## Third Party
* [Pack](https://github.com/cfnptr/pack) (Apache 2.0)
* [Avalonia UI](https://github.com/AvaloniaUI/Avalonia/) (MIT)
