<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNama = New System.Windows.Forms.TextBox()
        Me.btnInput = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.dvT0Karyawan = New System.Windows.Forms.DataGridView()
        Me.txtId = New System.Windows.Forms.Label()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.btnHapus = New System.Windows.Forms.Button()
        Me.btn_Batal = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        CType(Me.dvT0Karyawan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Label1.Location = New System.Drawing.Point(13, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nama"
        '
        'txtNama
        '
        Me.txtNama.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtNama.Location = New System.Drawing.Point(100, 19)
        Me.txtNama.Name = "txtNama"
        Me.txtNama.Size = New System.Drawing.Size(392, 34)
        Me.txtNama.TabIndex = 1
        '
        'btnInput
        '
        Me.btnInput.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.btnInput.Location = New System.Drawing.Point(100, 58)
        Me.btnInput.Name = "btnInput"
        Me.btnInput.Size = New System.Drawing.Size(80, 46)
        Me.btnInput.TabIndex = 2
        Me.btnInput.Text = "Input"
        Me.btnInput.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.btnEdit.Location = New System.Drawing.Point(203, 59)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(76, 46)
        Me.btnEdit.TabIndex = 2
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'dvT0Karyawan
        '
        Me.dvT0Karyawan.AllowUserToOrderColumns = True
        Me.dvT0Karyawan.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dvT0Karyawan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dvT0Karyawan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dvT0Karyawan.GridColor = System.Drawing.SystemColors.ButtonShadow
        Me.dvT0Karyawan.Location = New System.Drawing.Point(13, 110)
        Me.dvT0Karyawan.Name = "dvT0Karyawan"
        Me.dvT0Karyawan.RowHeadersWidth = 51
        Me.dvT0Karyawan.RowTemplate.Height = 29
        Me.dvT0Karyawan.Size = New System.Drawing.Size(503, 770)
        Me.dvT0Karyawan.TabIndex = 3
        '
        'txtId
        '
        Me.txtId.AutoSize = True
        Me.txtId.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtId.Location = New System.Drawing.Point(13, 66)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(29, 28)
        Me.txtId.TabIndex = 4
        Me.txtId.Text = "id"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'btnHapus
        '
        Me.btnHapus.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.btnHapus.Location = New System.Drawing.Point(304, 58)
        Me.btnHapus.Name = "btnHapus"
        Me.btnHapus.Size = New System.Drawing.Size(80, 47)
        Me.btnHapus.TabIndex = 2
        Me.btnHapus.Text = "Hapus"
        Me.btnHapus.UseVisualStyleBackColor = True
        '
        'btn_Batal
        '
        Me.btn_Batal.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.btn_Batal.Location = New System.Drawing.Point(412, 57)
        Me.btn_Batal.Name = "btn_Batal"
        Me.btn_Batal.Size = New System.Drawing.Size(80, 47)
        Me.btn_Batal.TabIndex = 5
        Me.btn_Batal.Text = "Batal"
        Me.btn_Batal.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1175, 893)
        Me.Controls.Add(Me.btn_Batal)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.dvT0Karyawan)
        Me.Controls.Add(Me.btnHapus)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnInput)
        Me.Controls.Add(Me.txtNama)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.dvT0Karyawan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtNama As TextBox
    Friend WithEvents btnInput As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents dvT0Karyawan As DataGridView
    Friend WithEvents txtId As Label
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents btnHapus As Button
    Friend WithEvents btn_Batal As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
End Class
