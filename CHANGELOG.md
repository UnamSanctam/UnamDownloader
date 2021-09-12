### v1.4.1 (12/09/2021)
* Worked around windres limitation of not supporting spaces in file paths
### v1.4.0 (12/09/2021)
* Added new custom minimal MinGW64 windres resource compiler
* Added new Icon and Assembly Data options using the new resource compiler
* Removed managed build type since native now has all the features managed had while also being better overall
* Increased key complexity to avoid general key scans
* Improved powershell code
* Fixed general small bugs
* Optimized code
### v1.3.1 (05/09/2021)
* Changed string literal function
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