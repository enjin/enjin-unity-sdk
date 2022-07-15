# Blockchain SDK by Enjin for Unity

Create blockchain projects and video games using the Unity game engine.

[Learn more](https://enjin.io/) about the Enjin blockchain platform.

Sign up to Enjin Cloud: [Goerli (Testnet)](https://goerli.cloud.enjin.io/),
[Mainnet (Production)](https://cloud.enjin.io/), or [JumpNet](https://jumpnet.cloud.enjin.io/).

### Resources

* [Enjin Docs](https://docs.enjin.io/enjin-platform-sdks/introduction-to-enjin-platform-sdks)

### Support

Contact Enjin support through email at support@enjin.io or submit a ticket at https://enjin.io/support.

### Table of Contents

* [Compatibility](#compatibility)
    * [Platform Schemas](#platform-schemas)
    * [Unity](#unity)
    * [Newtonsoft Json](#newtonsoft-json)
* [Usage](#usage)
* [Quick Start](#quick-start)
* [Contributing](#contributing)
    * [Issues](#issues)
    * [Pull Requests](#pull-requests)
* [Copyright and Licensing](#copyright-and-licensing)

## Compatibility

### Platform Schemas

The 2.0 version of the Enjin Unity SDK provides an API compatible with the V.2 schemas used on the Enjin Platform.

### Unity

Unity 2020.3.0f1 is the minimum supported Unity version for this SDK to operate as intended.

This SDK is targeted for .NET Standard 2.0 and supports both Mono and IL2CPP scripting backends.

Known platform(s) this SDK is **not** compatible with are listed below.

* **WebGL** due to restrictions on threads and websocket usage.

### Newtonsoft Json

This SDK comes with its own DLL file for the
[Newtonsoft.Json for Unity](https://github.com/jilleJr/Newtonsoft.Json-for-Unity) library in the event that Unity's
dedicated package, `com.unity.nuget.newtonsoft-json` is unobtainable. However, it is **highly recommended** to use
Unity's own package of the Newtonsoft Json library when possible.

To replace the included Newtonsoft Json library with Unity's own package:

1. Open the **Package Manager** window in the **Unity Editor**
2. Find the **Newtonsoft Json** package provided by *Unity Technologies*
3. Install the latest `3.x.x` version
4. Delete `Newtonsoft.Json.dll` from the `Enjin/Plugins` folder

## Usage

To use this SDK in a project, provide the reference to its assembly definition, `EnjinUnitySdk.asmdef`
such as with a **Assembly Definition** or **Assembly Definition Reference** asset. From there, all entities within the
SDK may be found under namespaces starting with `Enjin.SDK`.

## Quick Start

The below example shows how to setup and authenticate a player client for the player schema.

NOTE: `accessToken` is expected to be retrieved from a game server connected as a project client to the Enjin Platform.
This is to help keep the project's authentication credentials secured.

```c#
using Enjin.SDK;
using Enjin.SDK.Models;
using UnityEngine;

// Attached to a GameObject as a component in a Scene.
public class ExampleScript : MonoBehaviour
{
    void Awake()
    {
        // Builds the player client to run on the Goerli test network.
        // See: https://goerli.cloud.enjin.io to sign up for the test network.
        PlayerClient client = PlayerClient.Builder()
                                          .BaseUri(EnjinHosts.GOERLI)
                                          .Build();

        AccessToken accessToken = /* Retrieve from a game server. */;

        // Authenticates the client with the access token.
        client.Auth(accessToken.Token);

        // Checks if the client was authenticated.
        if (client.IsAuthenticated)
        {
            Debug.Log("Client is now authenticated");
        }
        else
        {
            Debug.Log("Client was not authenticated");
        }

        // Dispose client as part of cleanup and free any resources.
        client.Dispose();
    }
}
```

For immediate setup and testing, a project client that has access to project schema requests may be setup in the game
instance as shown below.

This is **NOT** recommended for anything beyond internal testing, due to possible exposure of the project's credentials
and the game instance ought to be setup to use a player client as soon as possible.

```c#
using Enjin.SDK;
using Enjin.SDK.Graphql;
using Enjin.SDK.Models;
using Enjin.SDK.ProjectSchema;
using UnityEngine;

// Attached to a GameObject as a component in a Scene.
public class ExampleScript : MonoBehaviour
{
    void Awake()
    {
        // Builds the project client to run on the Goerli test network.
        // See: https://goerli.cloud.enjin.io to sign up for the test network.
        ProjectClient client = ProjectClient.Builder()
                                            .BaseUri(EnjinHosts.GOERLI)
                                            .Build();

        // Creates the request to authenticate the client.
        // Replace the appropriate strings with the project's UUID and secret.
        AuthProject req = new AuthProject().Uuid("<the-project's-uuid>")
                                           .Secret("<the-project's-secret>");

        // Sends the request to the platform and gets the response.
        GraphqlResponse<AccessToken> res = client.AuthProject(req).Result;

        // Checks if the request was successful.
        if (!res.IsSuccess)
        {
            Debug.Log("AuthProject request failed");
            client.Dispose();
            return;
        }

        // Authenticates the client with the access token in the response.
        client.Auth(res.Result.Token);

        // Checks if the client was authenticated.
        if (client.IsAuthenticated)
        {
            Debug.Log("Client is now authenticated");
        }
        else
        {
            Debug.Log("Client was not authenticated");
        }

        // Dispose client as part of cleanup and free any resources.
        client.Dispose();
    }
}
```

## Contributing

Contributions to the SDK are appreciated!

### Issues

You may open issues for bugs and enhancement requests.

### Pull Requests

If you make any changes or improvements to the SDK, which you believe are beneficial to others, consider making a pull
request to merge your changes to be included in the next release.

Ensure that tests are passing and add any necessary test classes or test cases for your code.

Be sure to include your name in the list of contributors.

## Copyright and Licensing

The license summary below may be copied.

```text
Copyright 2021 Enjin Pte. Ltd.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
```
