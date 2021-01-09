### Directory structure

```
Assets/
    3rdparty/  # downloadable content, shouldn't be part of repository
        SampleScenes/
        Standard Assets/
    Scripts/  # scripts
    StaticAssets/
        Models/
        Prefabs/
        Scenes/
        Textures/

```

### Setting up development environment

* VS code [link](https://code.visualstudio.com/docs/other/unity)
* Unity debugger [link](https://marketplace.visualstudio.com/items?itemName=Unity.unity-debug#:~:text=You%20can%20now%20debug%20your,should%20hit%20in%20VS%20code.)

VS doesn’t have autocomplete functionality for Unity from the beginning. In order to adjust it do following steps: 
* Install latest version of .NET framework ([link](https://dotnet.microsoft.com/download/dotnet/5.0))
* Install MONO ([link](https://www.mono-project.com/download/stable/#download-mac))
* Install C# extension for VS code ([link](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp))
* Open Extension Setting for installed extension in previous step
* Set `Omnisharp: Use Global Mono` to `always` value (In case there is button with selecting path, add there path to the `settings.json` in project)

### 3rd party dependencies

* import https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2017-3-32351 to the project. and place it to Assets/3rdparty. So in the end you should have following structure:

```
Assets/
    3rdparty/
        SampleScenes/  # imported
        Standard Assets/  # imported
```