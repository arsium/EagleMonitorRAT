
# Eagle Monitor RAT Reborn | Open Source & Modern Remote Access Tool
Fast, lightweight & easily customizable remote access tool written in C# coded from scratch.  
<br>

## What's new ?

* Ransomware (RSA 4096 + AES)
* Stub automatically obfuscated
* Multiple hosts for client (+ support hostname)
* Directory size in file manager (only top files)
* Auto Save Recovery Option
* Information retrieves windows activation key
* New UI
* ...

## Current features

* Clients written in C# and VB (32 and 64 bit) (dlls + exes)
* Passwords recovery (automatically saved)
* History recovery (automatically saved)
* Autofill recovery (automatically saved)
* Keywords recovery (automatically saved)
* Remote camera viewer (+ save pictures)
* Remote microphone (automatically saved)
* Remote desktop control (+ save pictures)
* Remote chat
* Remote DotNet Code Execution
* Process manager 
* Kill process (native)
* Suspend process (native)
* Resume process (native)
* (shellcode) Process injection (NtWriteVirtualMemory + NtCreateThreadEx)
* (shellcode) Process injection (NtMapViewOfSection + NtCreateThreadEx)
* Shutdown
* Reboot
* Suspend
* Hibernate
* Log out
* BSOD
* Lock workstation
* Keylogger (fully offline & realtime)
* (current process) Managed pe execution
* (current process) Unmanaged pe execution
* (current process) Managed dll execution
* (current process) Unmanaged dll execution
* (current process) Shellcode execution
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
* Logs system
* Notification sound

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

* Wiki to make your own plugins
* Rewrite the VB Client
* Themes
* New persistence methods

## Known bugs

* Self destruct method (currently disabled)

## Interface 

![PIC2](https://github.com/arsium/EagleMonitorRAT/blob/main/IMG/2.png?raw=true)
![PIC3](https://github.com/arsium/EagleMonitorRAT/blob/main/IMG/6.png?raw=true)
![PIC4](https://github.com/arsium/EagleMonitorRAT/blob/main/IMG/7.png?raw=true)

I, the creator and all those associated with the development and production of this program are not responsible for any actions and or damages caused by this software. You bear the full responsibility of your actions and acknowledge that this software was created for educational purposes only. This software's intended purpose is NOT to be used maliciously, or on any system that you do not have own or have explicit permission to operate and use this program on. By using this software, you automatically agree to the above.

<a href="https://www.paypal.com/donate/?hosted_button_id=D83FCLVFMMHAA"><img src="https://raw.githubusercontent.com/andreostrovsky/donate-with-paypal/925c5a9e397363c6f7a477973fdeed485df5fdd9/blue.svg" height="40"></a>  

BTC: 1JpBNGLNmYR6MANK7wcY3h1YF2vG92BM4r
