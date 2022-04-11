
# Eagle Monitor RAT Reborn

Fast, lightweight & easily customizable remote access tool written in C#.  
Consider this as an upgrade of HorusEyesRat which was written in Visual Basic .NET.
As in the old good times, I decided to write the client in Visual Basic .NET.
Also, many people ask for help with installation or errors. I'm not a CSS.
If you have any issue with Eagle, open an issue in the right section explaining the problem and how I can reproduce the issue myself.

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
* Network information (IPV4)
* Client writtent both in VB and C#
* An installer for deployment
* Notification sound

## Current features

* Clients written in C# and VB (32 and 64 bit)
* Passwords recovery (automatically saved)
* History recovery (automatically saved)
* Blur screenlocker
* Remote camera viewer (+ save pictures)
* Remote desktop viewer (+ save pictures)
* Process manager 
* Kill process (native techniques)
* Suspend process
* Resume process
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
* Audio up
* Audio down
* Hide + show taskbar
* Hide + show desktop icons
* Set wallpaper
* File manager
* Delete file
* Download file
* Rename file
* Shortcuts (download, desktop and documents paths)
* Logs ((automatically saved))
* Mass Tasks

## External depencencies

* MRG.Controls.UI 
* XanderUI 
* GunaUI 
* dnlib 
* Newtonsoft.Json
* BouncyCastle

## Notes

* All saved stuff (logs, passwords...) are saved in csv format except offline keylogger
* All external dlls used are in folder "DLLs + Package"
* This remote access tool can also be used as stealer & payloads loader.
* Build the project in "release mode" only (change x64 bit for Server and clients)
* If you update with installer, you will have to backup your configs + logs (.dat, .json, Logs folder)
* Under license (MIT + Commons Clause)

## Technical information

* Communication encrypted with RSM encryption (https://bhf.im/threads/438711/)
* Packets compressed with QuickLZ (http://www.quicklz.com/download.html)
* Automation tasks saved with binary format
* Settings saved with JSON format

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

## TODO

* Encryption/Decryption for folders and files
* Wiki to make your own plugins
* Some code improvements and refractoring

## Known bugs

* Self destruct method (currently disabled)

## Interface 

![PIC1](https://i.postimg.cc/JzYb99xS/PIC1.png)
![PIC2](https://i.postimg.cc/K88PpZPn/PIC2.png)

I, the creator and all those associated with the development and production of this program are not responsible for any actions and or damages caused by this software. You bear the full responsibility of your actions and acknowledge that this software was created for educational purposes only. This software's intended purpose is NOT to be used maliciously, or on any system that you do not have own or have explicit permission to operate and use this program on. By using this software, you automatically agree to the above.

Want to buy a coffee ? BTC: 1JpBNGLNmYR6MANK7wcY3h1YF2vG92BM4r
