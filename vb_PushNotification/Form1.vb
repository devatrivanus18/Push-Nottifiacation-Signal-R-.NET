Imports System.Data.SqlClient
Imports System.Net.Http
Imports Microsoft.AspNetCore.SignalR.Client
Public Class Form1
    Dim Conn As SqlConnection
    Dim Da As SqlDataAdapter
    Dim Ds As DataSet
    Dim Cmd As SqlCommand
    Public _divisi As String = "GWR"
    Dim Title As String = "Data Karyawan"
    'Dim App As String = "WFA"
    Dim RD As SqlDataReader
    Dim _signalRService As SignalRService = New SignalRService()
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
        Da = New SqlDataAdapter("select IdKaryawan,NamaLengkap from T1Karyawan order by IdKaryawan ASC ", Conn)
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
        DataGridView.CheckForIllegalCrossThreadCalls = False
        ConnectSignalR()
        AddHandler System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged, New System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler(AddressOf NetworkChange_NetworkAvailabilityChanged)
        KondisiAwal()
        dvT0Karyawan.RowHeadersVisible = False
        dvT0Karyawan.EnableHeadersVisualStyles = False
        dvT0Karyawan.Columns(0).Width = 100
        dvT0Karyawan.Columns(1).Width = 300
        dvT0Karyawan.Columns(0).HeaderText = "ID"
        dvT0Karyawan.Columns(1).HeaderText = "Nama Lengkap"
        dvT0Karyawan.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
        dvT0Karyawan.ColumnHeadersDefaultCellStyle.BackColor = Color.Yellow
        dvT0Karyawan.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 12)
        With dvT0Karyawan
            .AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke
        End With
    End Sub
    Async Sub ConnectSignalR()
        _signalRService.ReceiveMessage(AddressOf OnDataBerubah)
        Await _signalRService.Connect(_divisi)
    End Sub

    Private Sub OnDataBerubah(obj As SignalRService.ClientMessage)
        NotifyIcon1.Icon = SystemIcons.Information
        NotifyIcon1.Visible = True
        NotifyIcon1.ShowBalloonTip(5000, obj.Method, obj.Message, ToolTipIcon.Info)
        KondisiAwal()
    End Sub

    Private Async Sub NetworkChange_NetworkAvailabilityChanged(ByVal sender As Object, ByVal e As System.Net.NetworkInformation.NetworkAvailabilityEventArgs)
        Try
            If e.IsAvailable Then
                Koneksi()
                Await _signalRService.Connect(_divisi)
                MessageBox.Show("Anda kembali terhubung ke internet")
            Else
                MessageBox.Show("Koneksi anda terputus dari internet")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
    Private Async Sub btnInput_Click(sender As Object, e As EventArgs) Handles btnInput.Click
        Dim MyValue = New Random()
        Dim id = MyValue.Next(1, 10000)
        Call Koneksi()
        Dim Simpan As String = "Insert into T1Karyawan (Idkaryawan,NamaLengkap) values('" & id & "' ,'" & txtNama.Text & "')"
        Cmd = New SqlCommand(Simpan, Conn)
        Cmd.ExecuteNonQuery()
        Await _signalRService.SendMessage(Title, "insert", False, id)
        MessageBox.Show("Data Berhasil Disimpan")
        Call KondisiAwal()
    End Sub
    Private Sub dvT0Karyawan_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dvT0Karyawan.CellClick
        txtNama.Text = dvT0Karyawan.Rows(e.RowIndex).Cells(1).Value
        txtId.Text = dvT0Karyawan.Rows(e.RowIndex).Cells(0).Value
    End Sub
    Private Async Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Call Koneksi()
        Dim Edit As String = "update T1Karyawan set NamaLengkap = '" & txtNama.Text & "' where IdKaryawan = '" & txtId.Text & "'"
        Cmd = New SqlCommand(Edit, Conn)
        Cmd.ExecuteNonQuery()
        Await _signalRService.SendMessage(Title, "update", False, txtId.Text)
        MessageBox.Show("Data '" & txtNama.Text & "' Berhasil Di Edit")
        Call KondisiAwal()
    End Sub
    Private Async Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Call Koneksi()
        Dim CMD As SqlCommand
        Dim hapus As String = "delete From T1Karyawan  where IdKaryawan='" & txtId.Text & "'"
        CMD = New SqlCommand(hapus, Conn)
        CMD.ExecuteNonQuery()
        Await _signalRService.SendMessage(Title, "delete", False, txtId.Text)
        MessageBox.Show("Data '" & txtNama.Text & "' Berhasil diHapus")
        Call KondisiAwal()
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        txtNama.Clear()
        Dim id = New Random
        txtId.Text = id.Next(1, 1000)
    End Sub
End Class
