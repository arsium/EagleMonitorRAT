
# Eagle Monitor RAT Reborn | Open Source & Modern Remote Access Tool
Fast, lightweight & easily customizable remote access tool written in C# coded from scratch.  
Support project ? <br>
BTC: 1JpBNGLNmYR6MANK7wcY3h1YF2vG92BM4r

<a href="https://www.paypal.com/donate/?hosted_button_id=D83FCLVFMMHAA"><img src="https://raw.githubusercontent.com/andreostrovsky/donate-with-paypal/925c5a9e397363c6f7a477973fdeed485df5fdd9/blue.svg" height="40"></a>  

## Updates

If you wanna follow what I'm working on : [Wiki](https://github.com/arsium/EagleMonitorRAT/wiki/Updates)

## What's new ?

* [Last changelog](https://github.com/arsium/EagleMonitorRAT/releases/tag/3.2.3.0)

## Server features

* Passwords recovery
* History recovery
* Autofill recovery
* Keywords recovery
* Remote Powershell
* Remore CMD
* Remote camera viewer (+ save pictures)
* Remote microphone (auto saved)
* Remote desktop control (+ save pictures)
* Remote keyboard
* Remote mouse
* Remote chat
* Remote DotNet Code Execution (VB + C#)
* Process manager 
* Kill process (native techniques)
* Suspend process (native techniques)
* Resume process (native techniques)
* ShellCode injection (NtWriteVirtualMemory + NtCreateThreadEx)
* ShellCode injection (NtMapViewOfSection + NtCreateThreadEx)
* Shutdown system
* Reboot system
* Suspend system
* Hibernate system
* Log out user
* BSOD
* Lock workstation
* Offline keylogger (automatically saved)
* Realtime keylogger (automatically saved)
* Managed pe execution (current process)
* Unmanaged pe execution (current process)
* Managed dll execution (current process)
* Unmanaged dll execution (current process)
* Shellcode execution (current process)
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

## Client Features :

* Anti-Debug (will cause BSOD if debugged)
* AMSI patching
* ETW patching
* Randomize PE from PEB

## External depencencies

* GunaUI 
* dnlib 
* Newtonsoft.Json
* NAudio

## Notes

* All saved stuff (logs, passwords...) are saved in csv format except offline keylogger
* All external dlls used are in folder "DLLs + Package"
* This remote access tool can also be used as stealer & payloads loader.
* Build the project in "release mode" only (change x64 bit for Server)
* Under license (AGPL)

## Technical information

* Communication encrypted with RSM encryption (https://bhf.im/threads/438711/)
* Packets compressed with QuickLZ (http://www.quicklz.com/download.html)
* Automation tasks saved with binary format
* Settings saved with JSON format
* Server .NET 4.8
* Client .NET 4.5
* Tested on freshly installed W10 & W11 VM

## Inspirations

* Webcam plugins : [AsyncRat](https://github.com/NYAN-x-CAT/AsyncRAT-C-Sharp/blob/master/AsyncRAT-C%23/Plugin/RemoteCamera/RemoteCamera/Packet.cs)

* Keylogger (modded) : [AsyncRat](https://github.com/NYAN-x-CAT/AsyncRAT-C-Sharp/blob/master/AsyncRAT-C%23/Plugin/LimeLogger/LimeLogger/Packet.cs)

* Unmanaged pe and dlls : [schellingb](https://github.com/schellingb/DLLFromMemory-net)

* Remote mouse & keyboard : [Quasar](https://github.com/quasar/Quasar/)

* Remote CMD & Powershell : [Quasar](https://github.com/quasar/Quasar/blob/master/Quasar.Client/IO/Shell.cs)

## Missing features (relative to previous versions)

* Mass tasks
* On connect tasks

## Interface 

![PIC1](https://github.com/arsium/EagleMonitorRAT/blob/main/IMG/1.png?raw=true)
![PIC2](https://github.com/arsium/EagleMonitorRAT/blob/main/IMG/2.png?raw=true)
![PIC3](https://github.com/arsium/EagleMonitorRAT/blob/main/IMG/3.png?raw=true)

I, the creator and all those associated with the development and production of this program are not responsible for any actions and or damages caused by this software. You bear the full responsibility of your actions and acknowledge that this software was created for educational purposes only. This software's intended purpose is NOT to be used maliciously, or on any system that you do not have own or have explicit permission to operate and use this program on. By using this software, you automatically agree to the above.
