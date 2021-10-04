﻿Imports Microsoft.AspNetCore.SignalR.Client

Public Class SignalRService
    Private WithEvents hubConnection As HubConnection
    Public _divisi As String = ""
    Public Sub New() ''construnctor
        hubConnection = New HubConnectionBuilder().WithUrl("https://newfcsignalr.azurewebsites.net/chatHub").Build()
    End Sub
    'CONNECT KE SIGNALR'
    Public Async Function Connect(divisi As String) As Task
        _divisi = divisi
        Await hubConnection.StartAsync()
        Await hubConnection.InvokeAsync("OnConnect", divisi)
    End Function
    Public Async Function Disconnect() As Task
        Await hubConnection.InvokeAsync("OnDisconnect", _divisi)
        Await hubConnection.StopAsync()
    End Function

    Public Async Function SendMessage(ByVal title As String, ByVal method As String, ByVal isBroadcast As Boolean, ByVal Optional id As Long = 0) As Task
        Dim msg = $"xam_{title} {method}"
        Dim message = New ClientMessage With
            {
                .Message = msg,
                .Method = method,
                .Divisi = _divisi,
                .IdKaryawan = id
            }

        If isBroadcast Then
            Await hubConnection.InvokeAsync("BroadcastMessage", message)
        Else
            Await hubConnection.InvokeAsync("SendMessage", message)
        End If
    End Function
    Public Sub ReceiveMessage(ByVal GetMessage As Action(Of ClientMessage), ByVal Optional isBroadcast As Boolean = False)
        If isBroadcast Then
            hubConnection.[On]("BroadcastMessage", GetMessage)
        Else
            hubConnection.[On]("SendMessage", GetMessage)
        End If
    End Sub


    Public Class ClientMessage
        Public Property Message As String
        Public Property Divisi As String
        Public Property Method As String
        Public Property IdKaryawan As Long
    End Class
End Class