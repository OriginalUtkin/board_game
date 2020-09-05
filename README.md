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

### 3rd party dependencies

* import https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2017-3-32351 to the project. and place it to Assets/3rdparty. So in the end you should have following structure:

```
Assets/
    3rdparty/
        SampleScenes/  # imported
        Standard Assets/  # imported
```