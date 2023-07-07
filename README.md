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

