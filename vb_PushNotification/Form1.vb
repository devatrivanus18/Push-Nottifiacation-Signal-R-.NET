Imports System.Data.SqlClient
Imports System.Net.Http
Imports Microsoft.AspNetCore.SignalR.Client

Public Class Form1
    Dim Conn As SqlConnection
    Dim Da As SqlDataAdapter
    Dim Ds As DataSet
    Dim Cmd As SqlCommand
    Dim Title As String = "Data Karyawan"
    Dim App As String = "WFA"
    Dim RD As SqlDataReader
    Dim LokasiDB As String
    Private WithEvents _connection As HubConnection
    Public ListsKaryawan As IList(Of DataKaryawan) = New List(Of DataKaryawan)()

    Sub Koneksi()
        LokasiDB = "Data Source = tcp:fortiscentral.database.windows.net,1433; Initial Catalog = dbArachne_Tes; User ID = gwr; Password = gw123456_; MultipleActiveResultSets=True"
        Conn = New SqlConnection(LokasiDB)
        If Conn.State = ConnectionState.Closed Then Conn.Open()
    End Sub

    Sub KondisiAwal()
        Koneksi()
        Da = New SqlDataAdapter("select * from T1Karyawan ", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "T1Karyawan")
        dvT0Karyawan.DataSource = (Ds.Tables("T1Karyawan"))
        txtNama.Text = Nothing
    End Sub

    Sub GetKaryawanById(IdKaryawan As Long)
        Koneksi()
        Da = New SqlDataAdapter("select * from T1Karyawan where id= '" & IdKaryawan & "'", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "T1Karyawan")
        dvT0Karyawan.DataSource = (Ds.Tables("T1Karyawan"))
        txtNama.Text = Nothing
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KondisiAwal()
        SignalR()
    End Sub

    Private Sub btnInput_Click(sender As Object, e As EventArgs) Handles btnInput.Click
        Dim MyValue As Integer
        MyValue = Int((6 * Rnd()) + 1)    ' Generate random value between 1 and 6.
        Call Koneksi()
        Dim Simpan As String = "Insert into T1Karyawan (Idkaryawan,NamaLengkap) values('" & MyValue & "' ,'" & txtNama.Text & "')"
        Cmd = New SqlCommand(Simpan, Conn)
        Cmd.ExecuteNonQuery()
        Call Send(App, "Notifikasi", Title, MyValue)
        MessageBox.Show("Data Berhasil Disimpan")
        Call KondisiAwal()
    End Sub



    Private Sub dvT0Karyawan_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dvT0Karyawan.CellDoubleClick
        txtNama.Text = dvT0Karyawan.Rows(e.RowIndex).Cells(4).Value
        txtId.Text = dvT0Karyawan.Rows(e.RowIndex).Cells(0).Value
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Call Koneksi()
        Dim Edit As String = "update T1Karyawan set NamaLengkap = '" & txtNama.Text & "' where IdKaryawan = '" & txtId.Text & "'"
        Cmd = New SqlCommand(Edit, Conn)
        Cmd.ExecuteNonQuery()
        Call Send(App, "Notifikasi", Title, txtId.Text)
        MessageBox.Show("Data '" & txtNama.Text & "' Berhasil Di Edit")
        Call KondisiAwal()
    End Sub

    Private Async Function Send(ByVal app As String, ByVal method As String, ByVal NamaTabel As String, ByVal IdKaryawan As Long) As Task
        Try
            Dim msg = $"{app}_{NamaTabel} {method}"
            Await _connection.SendAsync("Broadcast", msg, method, IdKaryawan)
        Catch e As Exception
            Dim ex = e.Message.ToString()
        End Try
    End Function
    Async Sub SignalR()
        Dim client As HttpClient = New HttpClient()
        client.BaseAddress = New Uri("https://fcsignalrserver.azurewebsites.net/chatHub")
        _connection = New HubConnectionBuilder().WithUrl(client.BaseAddress).Build()
        _connection.[On](Of String, String, Long)("Broadcast", AddressOf BroadcastMessage)
        Await _connection.StartAsync()
    End Sub

    Private Sub BroadcastMessage(msg As String, method As String, idKaryawan As Long)
        NotifyIcon1.Icon = SystemIcons.Information
        NotifyIcon1.Visible = True
        NotifyIcon1.ShowBalloonTip(5000, method, msg, ToolTipIcon.Info)
        KondisiAwal()

    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Call Koneksi()
        Dim CMD As SqlCommand
        Dim hapus As String = "delete From T1Karyawan  where IdKaryawan='" & txtId.Text & "'"
        CMD = New SqlCommand(hapus, Conn)
        CMD.ExecuteNonQuery()
        Call Send(App, "Notifikasi", Title, txtId.Text)
        MessageBox.Show("Data '" & txtNama.Text & "' Berhasil diHapus")
        Call KondisiAwal()
    End Sub
End Class
