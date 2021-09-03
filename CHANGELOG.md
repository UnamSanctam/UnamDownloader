### v1.3.0 (03/09/2021)
* Changed obfuscation from reversed string to XOR encryption, reduces detections and file size
* Fixed bug when file path included apostrophes or any other escape characters
### v1.2.0 (21/08/2021)
* Changed it to compile native into 32-bit programs for wider compatibility
* Added random string into the native code to randomize file checksum/hash on each build
* Optimized and improved Windows Defender Exclusions
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