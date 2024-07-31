
<p align="center">
  <a href="https://tcno.co/">
    <img src="/img/banner.png"></a>
</p>
<p align="center">
  <img alt="GitHub All Releases" src="https://img.shields.io/github/downloads/TcNobo/osu-cleaner/total?logo=GitHub&style=flat-square">
  <a href="https://tcno.co/">
    <img alt="Website" src="/img/web.svg" height=20"></a>
  <a href="https://s.tcno.co/AccSwitcherDiscord">
    <img alt="Discord server" src="https://img.shields.io/discord/217649733915770880?label=Discord&logo=discord&style=flat-square"></a>
  <a href="https://twitter.com/TcNobo">
    <img alt="Twitter" src="https://img.shields.io/twitter/follow/TcNobo?label=Follow%20%40TcNobo&logo=Twitter&style=flat-square"></a>
  <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/TcNobo/osu-cleaner?logo=GitHub&style=flat-square">
  <img alt="GitHub repo size" src="https://img.shields.io/github/repo-size/TcNobo/osu-cleaner?logo=GitHub&style=flat-square">
</p>
                                                                                                                                  
<p align="center"><a target="_blank" href="https://github.com/TcNobo/osu-cleaner/releases/latest">
  <img alt="Download button" src="/img/btnDownload.png" height=70"></a></p>


Small tool, written in C#, to clean unused elements from beatmaps.
Originally created by [henntix](https://github.com/henntix/osu-cleaner), but completely reskinned, revamped, branded and updated by [TroubleChute](https://tcno.co)

Lots of changes and improvements since the original 1.0 henntix release. Enjoy the revamped software!

If you are using a SSD, you will be happy! Deleting those elements will save your disk space even by few gigabytes. You can choose what to delete: videos, storyboards, beatmap skins, backgrounds and hitsounds. You can also choose to move those files instead of permanent purge.

[Visit the osu! Forum post](https://osu.ppy.sh/community/forums/topics/1235910)

### How does it work?
After either guessing or being told your osu! directory, it will take the options you give it and search for relative files. This lets you pull unused images and sounds out of song packs, as well as other "pointless" files, that you may choose not to use or already ignore because you have a custom skin/sounds active. You'd be surprised how much junk is just lying around in your folder!

## Required runtimes (Download and install):
**- Requires Microsoft .NET Framework 4.8 Runtime:** [Web Installer](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net48-web-installer), [Offline Installer](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net48-offline-installer), [Other languages](https://dotnet.microsoft.com/download/dotnet-framework/net48)

**Running the program:**
After downloading your .zip from the [GitHub Releases](https://github.com/TcNobo/TcNo-osu-Cleaner/releases) page, extract everything to a folder of your choice and run `TcNo-osu-Cleaner.exe`

### Screenshots:
<p>
  <img alt="Main window - Mid-scan" src="/img/screenshot1.png">
  <img alt="main window - Selected files to purge" src="/img/screenshot2.png">
</p>

## License: GNU General Public License v3.0
Copyright for portions of [TcNo-osu-Cleaner](https://github.com/TcNobo/TcNo-osu-Cleaner) are held by [henntix](https://github.com/henntix/), 2016 as part of [osu-cleaner](https://github.com/henntix/osu-cleaner) and under The MIT License. All other copyright for [TcNo-osu-Cleaner](https://github.com/TcNobo/TcNo-osu-Cleaner) are held by [Wesley Pyburn (TroubleChute)](https://github.com/TcNobo/) under The GNU General Public License v3.0.

Additional license information for included NuGet packages and other parts of code can be found in: [HERE](https://github.com/TcNobo/TcNo-osu-Cleaner/blob/master/osu-cleaner/Additional%20Licenses.txt) `Additional Licenses.txt`, and are copied to the build directory, as well as distributed with release versions of this software.

> Contributors are appreciated. If you want to contribute and add something, fork it and send me a pull request.

> If you find issues or bugs, report them via Issues page. Please also report if something has been not deleted but it should be.

## Changelog
* v2.1 (24.04.2021) Added "Missing background" replacer and easy Songs folder moving through Junction Symlinks.
* v2.0 (26.01.2021) Completely reskinned, updated & improved by [TroubleChute](https://tcno.co).
* v1.0 (06.11.2018) Initial release, written in 2016

---

#### Disclaimer
All trademarks and materials are property of their respective owners and their licensors.<br>
This project is not affiliated with "osu!" or "ppy" or "Peppy".<br>
I am not responsible for the contents of external links.<br>
USE THIS SOFTWARE AT YOUR OWN RISK. I AM NOT RESPONSIBLE FOR ANY DAMAGES IF YOU CHOOSE TO USE THIS SOFTWARE. COMPONENTS ARE PROVIDED ON AN "AS IS" AND "AS AVAILABLE" BASIS, WITHOUT ANY WARRANTIES OF ANY KIND TO THE FULLEST EXTENT PERMITTED BY LAW, AND I EXPRESSLY DISCLAIM ANY AND ALL WARRANTIES, WHETHER EXPRESS OR IMPLIED.
