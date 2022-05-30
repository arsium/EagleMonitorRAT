
# Eagle Monitor RAT Reborn | Open Source Remote Access Tool

Fast, lightweight & easily customizable remote access tool written in C# coded from scratch.  
Consider this as an upgrade of HorusEyesRat which was written in Visual Basic .NET.
As in the old good times, I decided to write a client in Visual Basic .NET.
<br>
Issue section removed because of spamming & abusing.





## Why did I choose to rework it ?

As I said, I learnt new things to make my code cleaner and a better networking management.

## New things will be added in the future ?

Of course. I plan to add features when I find them interesting to add. Please don't spam issue section with new features. If you really want a specific feature, you will have to add it by yourself. A wiki section will be written to show you how to add your own features.

## What's been reworked ?

* Whole UI (datagridview instead of listview, dark theme...)
* Packets system
* All plugins
* Server side and packets handling
* Client side and packets handling
* Native Imports
* Settings
* Builder
* Automation tasks
* Mass tasks
* Memory execution
* Asynchronous operations
* Use of threads (when needed)
* Wifi recovery removed

## What's new ?

* Logs system
* Keylogger (offline and realtime)
* Client writtent both in VB and C#
* An installer for deployment
* Notification sound

## Current features

* Clients written in C# and VB (32 and 64 bit) (dlls + exes)
* Passwords recovery (automatically saved)
* History recovery (automatically saved)
* Autofill recovery (automatically saved)
* Keywords recovery (automatically saved)
* Remote camera viewer (+ save pictures)
* Remote microphone (automatically saved)
* Remote desktop control (+ save pictures)
* Remote keyboard
* Remote mouse
* Remote chat
* Remote DotNet Code Execution
* Process manager 
* Kill process (native techniques)
* Suspend process (native techniques)
* Resume process (native techniques)
* (shellcode) Process injection (NtWriteVirtualMemory + NtCreateThreadEx)
* (shellcode) Process injection (NtMapViewOfSection + NtCreateThreadEx)
* Shutdown system
* Reboot system
* Suspend system
* Hibernate system
* Log out user
* BSOD
* Lock workstation
* Offline keylogger (automatically saved)
* Realtime keylogger (automatically saved)
* (current client) Managed pe execution
* (current client) Unmanaged pe execution
* (current client) Managed dll execution
* (current client) Unmanaged dll execution
* (current client) Shellcode execution
* Blur screenlocker
* Audio up
* Audio down
* Hide + show taskbar
* Hide + show desktop icons
* Set wallpaper
* File manager
* Delete file
* Download file
* Rename file
* Upload file
* Get information (CPU, hardware, system)
* Shortcuts (download, desktop and documents paths)
* Logs (automatically saved)
* Mass Tasks

## External depencencies

* MRG.Controls.UI 
* XanderUI 
* GunaUI 
* dnlib 
* Newtonsoft.Json
* BouncyCastle
* NAudio

## Notes

* All saved stuff (logs, passwords...) are saved in csv format except offline keylogger
* All external dlls used are in folder "DLLs + Package"
* This remote access tool can also be used as stealer & payloads loader.
* Build the project in "release mode" only (change x64 bit for Server and clients)
* If you update with installer, you will have to backup your configs + logs (.dat, .json, Logs folder)
* Under license (AGPL)

## Technical information

* Communication encrypted with RSM encryption (https://bhf.im/threads/438711/)
* Packets compressed with QuickLZ (http://www.quicklz.com/download.html)
* Automation tasks saved with binary format
* Settings saved with JSON format
* Server .NET 4.8
* Client .NET 4.5
* Tested on freshly installed W10 & W11 VM

## DLLs

Dlls are same as exe. You have to change the config class.
The entrypoint should be called as follows :

```csharp
Client.EntryClass.Main()
```
## Inspirations

* Webcam plugins : [AsyncRat](https://github.com/NYAN-x-CAT/AsyncRAT-C-Sharp/blob/master/AsyncRAT-C%23/Plugin/RemoteCamera/RemoteCamera/Packet.cs)
* Keylogger (modded) : [AsyncRat](https://github.com/NYAN-x-CAT/AsyncRAT-C-Sharp/blob/master/AsyncRAT-C%23/Plugin/LimeLogger/LimeLogger/Packet.cs)
* Unmanaged pe and dlls : [schellingb](https://github.com/schellingb/DLLFromMemory-net)
* Remote mouse & keyboard : [Quasar](https://github.com/quasar/Quasar/)

## TODO

* Encryption/Decryption for folders and files
* Rework installation method(s)
* Wiki to make your own plugins
* Some code improvements and refractoring

## Known bugs

* Self destruct method (currently disabled)

## Interface 

![PIC2](https://github.com/arsium/EagleMonitorRAT/blob/main/IMG/2.png?raw=true)
![PIC3](https://github.com/arsium/EagleMonitorRAT/blob/main/IMG/6.png?raw=true)
![PIC4](https://github.com/arsium/EagleMonitorRAT/blob/main/IMG/7.png?raw=true)

I, the creator and all those associated with the development and production of this program are not responsible for any actions and or damages caused by this software. You bear the full responsibility of your actions and acknowledge that this software was created for educational purposes only. This software's intended purpose is NOT to be used maliciously, or on any system that you do not have own or have explicit permission to operate and use this program on. By using this software, you automatically agree to the above.

Want to buy a coffee ? BTC: 1JpBNGLNmYR6MANK7wcY3h1YF2vG92BM4r
