# Eagle Monitor
Fast, lightweight & easily customizable remote access tool written in C#. Consider this as an upgrade of HorusEyesRat which was written in VB.NET. 

Under MIT License [LICENSE](https://github.com/arsium/EagleMonitor/blob/main/LICENSE).

Depencencies : 

* MRG.Controls.UI (UI)
* XanderUI (UI)
* Siticone.UI (UI)
* dnlib (builder)
* Newtonsoft.Json (configuration)
* BouncyCastle (file manager encryption/decryption) : https://www.bouncycastle.org/
* Compression : http://www.quicklz.com/download.html
* RSM : https://bhf.im/threads/438711/
* Load native dll and pe : https://github.com/schellingb/DLLFromMemory-net
* WebCam : inspired by AsyncRat : https://github.com/NYAN-x-CAT/AsyncRAT-C-Sharp/blob/master/AsyncRAT-C%23/Plugin/RemoteCamera/RemoteCamera/Packet.cs

Features :
* Supports DNS (No-IP for example)
* Encrypted & Compressed Communication with RSM
* Packet Serialization
* Stub only 28KB and easily encryptable
* Installable in task scheduler
* Confirurable with JSON
* Multi listeners
* MultiThreaded & Parallel
* Mass & On connected tasks
* Passords Recovery
* History Recovery
* Wifi passwords recovery
* Automatic saves recovery
* Process manager
* Kill Proc
* Suspend Proc
* Resume Proc
* Change Windows Title
* Minimize Window
* Maximize Window
* Show Window 
* Hide Window
* Delete File
* Download File
* Launch File
* Upload File
* Rename File
* Encrypt File -> +- 25 algo (symmetric)
* Decrypt File
* Remote Desktop
* Hide & Show Desktop Icons
* Hide & Show Taskbar
* Change Wallpaper
* Execute Dll native in memory
* Execute native PE in memory
* Execute Shellcode in memory
* Execute Managed dll in memory
* ScreenLocker
* BSOD
* Disable Mouse
* Disable Keyboard
* Get Privileges [Se] -> see logs to check if the privilege has been gotten.
* WebCam Capture
* Shutdown
* Reboot
* Log out
* Suspend
* Hibernate

Notes : 
* UI Bugs
* Dark theme in construction
* Need to improve & finish logs system
* No more options will be added to the builder. If you want more, you will have to do it yourself.
* If you want to build the solution, please use only release mode. I configured it to directly build plugins next to Eagle Monitor.
* For stub and some plugins, depencies are put inside with CosturaFody.
* New features I will probably add : keyloggers, get system details, set client's priority....

Preview :

![Image description](https://i.postimg.cc/yYyQb2cD/Capture-d-cran-82.png)
![Image description](https://i.postimg.cc/rsCJ7tdB/Capture-d-cran-83.png)
![Image description](https://i.postimg.cc/vHbXfXkb/Capture-d-cran-84.png)

Edit :
Many stupid people said it can't connect to server but if you don't add port it's logic :
Right Click on listview in startform (And if it does not work , disable your firewall or add an exception) : 

![Image description](https://i.postimg.cc/kgX5YwdT/Capture-d-cran-85.png)


I, the creator and all those associated with the development and production of this program are not responsible for any actions and or damages caused by this software. You bear the full responsibility of your actions and acknowledge that this software was created for educational purposes only. This software's intended purpose is NOT to be used maliciously, or on any system that you do not have own or have explicit permission to operate and use this program on. By using this software, you automatically agree to the above.

Want to buy a coffee ? BTC: 1JpBNGLNmYR6MANK7wcY3h1YF2vG92BM4r
