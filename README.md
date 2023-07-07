# DiSHLoader
## The new way of installing and loading DiSH

## Installation
For now, precompiled builds are not available in alpha stage.

### Building
You'll need Visual Studio 2022 with the **.NET desktop development** workflow and git.

1. Open the repo in visual studio, by cloning it with VS "Open Repository"
2. Build the projects. The first time, it may take a while.
3. You can now configure DiSH Core

### Configuration

DiSH Loader needs manual configuration for now. To have a working envoirnement, you need to:
1. Create a folder in %appdata% with the following directory tree:
```
%appdata%\Roaming
└───LDevs
    └───DiSHLoader
        └───versions
```
2. Get a working DiSH installation, by downloading a release >= v11.0 on [the DiSH repo](https://github.com/LDevs-Team/DiSH)
3. Open regedit.exe and go to Compuer\HKEY_CURRENT_USER\SOFTWARE. Create a key with the following tree:
```
Compuer\HKEY_CURRENT_USER\SOFTWARE
└───LDevs
    └───DiSHLoader
```
4. Create a string key called DefaultVersion, with the content of the release name you downloaded (without the v). For example, v11.0 will be DefaultVersion = 11.0
5. Return to %APPDATA%\ROAMING\LDevs\DiSHLoader\versions and create a folder with the same name as the content of the DefaultVersion key
6. Copy all the contents of the folder DiSH-Windows of the archive you downloaded from DiSH releases
7. Create a Discord bot and copy its token.
8. Enable ALL INTENTS in the Discord bot developer portal
9. Create a new discord server or use one you already have
10. Invite there your new bot
11. Create:

 - A category
 - A channel for the logs
 - A channel for global commands

12. Create a file named .env in %APPDATA%\ROAMING\LDevs\DiSHLoader\ and fill it with
``` env
TOKEN=(your discord bot token)
GUILD_ID=(your discord server ID)
CATEGORY_ID=(your category ID)
LOGS_ID=(your logs channel ID)
GLOBAL_ID=(your global channel ID)
```

## How this works

> **Warning**
> The following explaination is pretty technical, and is only used to better explain the badly-documented code in the projects

### DiSHLoader
> **Note**
> TL;DR: the code is contained in Program.cs in the DiSHLoader folder. Its use is loading DiSH and making sure it does not crash

- the `logMessages` function, logs messages from DiSHCore, used below.
- The program gets the path to %appdata%
- The program gets the basic keys: 
  - the path to the .env file (with %appdata%\LDevs\DiSHLoader\\.env as default)
  - the default version (required, no default)
  - the path to the versions folder (with %appdata%\LDevs\DiSHLoader\versions as default)
- The program loads the .env file specified by the registry key mentioned above
- The program creates the start info for DiSH. It provides only the path to the executable. It is derived by: `[versions path]\[default version name]\DiSH.exe`
- The program starts DiSH
- The program waits for DiSH to exit and saves the exit codeto a variable
- It then uses a switch to determine what caused DiSH to exit.
  - Code 0: do nothing - DiSH probably exited on its own
  - Code 2: restart the program - this is used by DiSH to indicate a change in the configuration (still not implemented) and therefore the requirement to restart th program with new configuration data
  - Code 1: DiSH crashed. Start DiSHCore (see below)

### DiSHCore

> **Note**
> TL;DR It works similarly to the main DiSH repo, which is better documented, check that one out for now