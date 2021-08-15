
<img src="https://github.com/UnamSanctam/UnamDownloader/blob/master/UnamDownloader.png?raw=true">

# UnamDownloader 1.1.1 - A free silent downloader

A free silent (hidden) open source downloader (binder) that can be built as either a native C or managed .NET C# file. A downloader is essentially the same as a binder although it downloads the files instead of storing them in memory.

## Main Features

* Native (C) - Can choose to build the downloader as a native (C) file, basically no run requirements
* Managed (C#) - Can choose to build the downloader as a managed (.NET C#) file, requires at least .NET 4.0
* Silent - Downloads and executes (if enabled) files without any visible output
* Tiny - Final downloader build is usually less than 10kb
* Multiple files - Supports downloading any amount of files
* Powershell - Does everything through powershell which currently greatly reduces detections
* Compatible - Supports all tested Windows version (Windows 7 to Windows 10) and all file types
* Windows Defender exclusions - Can add exclusions into Windows Defender to ignore any detections from the downloaded files
* Icon/Assembly - Supports adding an icon and assembly data to the built file with building a managed .NET C# build

## Downloads

Pre-Compiled: https://github.com/UnamSanctam/UnamDownloader/releases

## Wiki

You can find the wiki [here](https://github.com/UnamSanctam/UnamDownloader/wiki) or at the top of the page.

## Changelog

### v1.1.1 (15/08/2021)
* Fixed save bug when building native files to another directory than the builder location
* Added the builder location as the standard save location to make it easier to navigate
### v1.1.0 (14/08/2021)
* Added option to build the downloader as a native C file, greatly reduces detections
* Added a TinyCC compiler for native C builds
* Updated required .NET for the builder to .NET 4.5 and the required .NET for the managed .NET C# build to .NET 4.0
* Changed the Run as Administrator option to use a new manifest file
### v1.0.0 (14/08/2021)
* Initial release

[You can view the full Changelog here](CHANGELOG.md)

## Author

* **Unam Sanctam**

## Disclaimer

I, the creator, am not responsible for any actions, and or damages, caused by this software.

You bear the full responsibility of your actions and acknowledge that this software was created for educational purposes only.

This software's main purpose is NOT to be used maliciously, or on any system that you do not own, or have the right to use.

By using this software, you automatically agree to the above.

## License

This project is licensed under the MIT License - see the [LICENSE](/LICENSE) file for details

## Donate

XMR: 8BbApiMBHsPVKkLEP4rVbST6CnSb3LW2gXygngCi5MGiBuwAFh6bFEzT3UTufiCehFK7fNvAjs5Tv6BKYa6w8hwaSjnsg2N

BTC: bc1q26uwkzv6rgsxqnlapkj908l68vl0j753r46wvq

ETH: 0x40E5bB6C61871776f062d296707Ab7B7aEfFe1Cd

ETC: 0xd513e80ECc106A1BA7Fa15F1C590Ef3c4cd16CF3

RVN: RFsUdiQJ31Zr1pKZmJ3fXqH6Gomtjd2cQe

LINK: 0x40E5bB6C61871776f062d296707Ab7B7aEfFe1Cd

DOGE: DNgFYHnZBVLw9FMdRYTQ7vD4X9w3AsWFRv

LTC: Lbr8RLB7wSaDSQtg8VEgfdqKoxqPq5Lkn3