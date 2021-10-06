Imports System.Data.SqlClient
Imports Microsoft.AspNetCore.SignalR.Client
Public Class Form1
    Dim Conn As SqlConnection
    Dim Da As SqlDataAdapter
    Dim Ds As DataSet
    Dim Cmd As SqlCommand
    Public _divisi As String = "GWR"
    Dim Title As String = "Data Karyawan"
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

    'Method untuk memulai koneksi ke signalR server
    Async Sub ConnectSignalR()
        _signalRService.TerimaPesan(AddressOf OnDataBerubah)
        Await _signalRService.MulaiKoneksi(_divisi)
    End Sub

    'Method untuk menangani perubahan data
    Private Sub OnDataBerubah(obj As SignalRService.ClientMessage)
        NotifyIcon1.Icon = SystemIcons.Information
        NotifyIcon1.Visible = True
        NotifyIcon1.ShowBalloonTip(5000, obj.JenisPesan, obj.IsiPesan, ToolTipIcon.Info)
        KondisiAwal()
    End Sub

    'Deteksi Koneksi Internet
    Private Async Sub NetworkChange_NetworkAvailabilityChanged(ByVal sender As Object, ByVal e As System.Net.NetworkInformation.NetworkAvailabilityEventArgs)
        Try
            If e.IsAvailable Then
                Koneksi()
                Await _signalRService.MulaiKoneksi(_divisi)
                MessageBox.Show("Anda kembali terhubung ke internet")
            Else
                MessageBox.Show("Koneksi anda tidak stabil, anda mungkin tidak dapat menerima notifikasi dan pembaruan data. Mohon Periksa kembali koneksi internet anda.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Method Insert
    Private Async Sub btnInput_Click(sender As Object, e As EventArgs) Handles btnInput.Click
        Dim MyValue = New Random()
        Dim id = MyValue.Next(1, 10000) 'Id yang dikirim ke SignalR untuk reload data by id di grpc
        Call Koneksi()
        Dim Simpan As String = "Insert into T1Karyawan (Idkaryawan,NamaLengkap) values('" & id & "' ,'" & txtNama.Text & "')"
        Cmd = New SqlCommand(Simpan, Conn)
        Cmd.ExecuteNonQuery()
        Await _signalRService.KirimPesan(Title, "insert", False, id) 'SendMessage ke signalR server
        MessageBox.Show("Data Berhasil Disimpan")
        Call KondisiAwal()
    End Sub

    Private Sub dvT0Karyawan_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dvT0Karyawan.CellClick
        txtNama.Text = dvT0Karyawan.Rows(e.RowIndex).Cells(1).Value
        txtId.Text = dvT0Karyawan.Rows(e.RowIndex).Cells(0).Value
    End Sub

    'Method untuk update data
    Private Async Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Call Koneksi()
        Dim Edit As String = "update T1Karyawan set NamaLengkap = '" & txtNama.Text & "' where IdKaryawan = '" & txtId.Text & "'"
        Cmd = New SqlCommand(Edit, Conn)
        Cmd.ExecuteNonQuery()
        Await _signalRService.KirimPesan(Title, "update", False, txtId.Text) 'SendMessage ke signalR server
        MessageBox.Show("Data '" & txtNama.Text & "' Berhasil Di Edit")
        Call KondisiAwal()
    End Sub

    'Method untuk delete data
    Private Async Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Call Koneksi()
        Dim CMD As SqlCommand
        Dim hapus As String = "delete From T1Karyawan  where IdKaryawan='" & txtId.Text & "'"
        CMD = New SqlCommand(hapus, Conn)
        CMD.ExecuteNonQuery()
        Await _signalRService.KirimPesan(Title, "delete", False, txtId.Text) 'SendMessage ke signalR server
        MessageBox.Show("Data '" & txtNama.Text & "' Berhasil diHapus")
        Call KondisiAwal()
    End Sub

    Private Sub btn_Batal_Click(sender As Object, e As EventArgs) Handles btn_Batal.Click
        txtNama.Clear()
        Dim id = New Random
        txtId.Text = id.Next(1, 1000)
    End Sub
End Class
