Imports System.Net.Http
Imports Microsoft.AspNetCore.SignalR.Client

Public Class SignalRService
    Private WithEvents hc As HubConnection
    Public _divisi As String = ""
    Public Sub New() 'constructor
        Dim client As HttpClient = New HttpClient()
        client.BaseAddress = New Uri("https://newfcsignalr.azurewebsites.net/chatHub")
        hc = New HubConnectionBuilder().WithUrl(client.BaseAddress).Build()
    End Sub

    Public Async Function MulaiKoneksi(divisi As String) As Task
        _divisi = divisi
        If hc.ConnectionId IsNot Nothing Then
            Await StopKoneksi()
        End If
        Await hc.StartAsync()
        Await hc.InvokeAsync("MulaiKoneksi", divisi)
    End Function

    Public Async Function StopKoneksi() As Task
        Await hc.InvokeAsync("StopKoneksi", _divisi)
        Await hc.StopAsync()
    End Function

    Public Async Function KirimPesan(ByVal namaTabel As String,
                                     ByVal method As String, ByVal isBroadcast As Boolean,
                                     ByVal Optional Id_PrimaryKey As Long = 0) As Task
        Dim isiPesan = $"WFA_{namaTabel} {method}"
        Dim Pesan = New ClientMessage With
            {
                .IsiPesan = isiPesan,
                .JenisPesan = method,
                .Divisi = _divisi,
                .Id_PrimaryKey = Id_PrimaryKey
            }

        If isBroadcast Then
            Await hc.InvokeAsync("KirimPesanBroadcast", Pesan)
        Else
            Await hc.InvokeAsync("KirimPesan", Pesan)
        End If
    End Function

    Public Sub TerimaPesan(ByVal OnDataBerubah As Action(Of ClientMessage), ByVal Optional isBroadcast As Boolean = False)
        If isBroadcast Then
            hc.[On]("KirimPesanBroadcast", OnDataBerubah)
        Else
            hc.[On]("KirimPesan", OnDataBerubah)
        End If
    End Sub


    Public Class ClientMessage
        Public Property Divisi As String
        Public Property IsiPesan As String
        Public Property JenisPesan As String
        Public Property Id_PrimaryKey As Object
        Public Property NamaHalaman As String 'Digunakan di Xamarin, untuk redirect ke halaman tertentu ketika user klik notifikasi di status bar.

    End Class
End Class