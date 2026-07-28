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
        TableLayoutPanel1 = New TableLayoutPanel()
        GroupBox1 = New GroupBox()
        CheckBox1 = New CheckBox()
        GroupBox4 = New GroupBox()
        ListBox1 = New ListBox()
        Label11 = New Label()
        Button3 = New Button()
        Label5 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        Label8 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        GroupBox2 = New GroupBox()
        Label10 = New Label()
        Label9 = New Label()
        TableLayoutPanel2 = New TableLayoutPanel()
        Button1 = New Button()
        Button2 = New Button()
        GroupBox3 = New GroupBox()
        RichTextBox1 = New RichTextBox()
        SaveFileDialog1 = New SaveFileDialog()
        OpenFileDialog1 = New OpenFileDialog()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        TableLayoutPanel1.SuspendLayout()
        GroupBox1.SuspendLayout()
        GroupBox4.SuspendLayout()
        GroupBox2.SuspendLayout()
        TableLayoutPanel2.SuspendLayout()
        GroupBox3.SuspendLayout()
        SuspendLayout()
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 76.8278961F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 23.1721039F))
        TableLayoutPanel1.Controls.Add(GroupBox1, 1, 1)
        TableLayoutPanel1.Controls.Add(GroupBox2, 0, 0)
        TableLayoutPanel1.Controls.Add(TableLayoutPanel2, 1, 0)
        TableLayoutPanel1.Controls.Add(GroupBox3, 0, 1)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 2
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 14.7766323F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 85.2233658F))
        TableLayoutPanel1.Size = New Size(889, 582)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(CheckBox1)
        GroupBox1.Controls.Add(GroupBox4)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Controls.Add(Label8)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Dock = DockStyle.Fill
        GroupBox1.Location = New Point(686, 89)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(200, 490)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "ℹ️ Info Panel"
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Checked = True
        CheckBox1.CheckState = CheckState.Checked
        CheckBox1.Location = New Point(6, 223)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(114, 19)
        CheckBox1.TabIndex = 9
        CheckBox1.Text = "🤫 Modo Silencioso"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(ListBox1)
        GroupBox4.Controls.Add(Label11)
        GroupBox4.Controls.Add(Button3)
        GroupBox4.Dock = DockStyle.Bottom
        GroupBox4.Location = New Point(3, 248)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(194, 239)
        GroupBox4.TabIndex = 8
        GroupBox4.TabStop = False
        GroupBox4.Text = "🔄 Archivos en Serie"
        ' 
        ' ListBox1
        ' 
        ListBox1.Dock = DockStyle.Bottom
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(3, 82)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(188, 154)
        ListBox1.TabIndex = 2
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(6, 52)
        Label11.Name = "Label11"
        Label11.Size = New Size(64, 15)
        Label11.TabIndex = 1
        Label11.Text = "Ruta e info"
        ' 
        ' Button3
        ' 
        Button3.Dock = DockStyle.Top
        Button3.Location = New Point(3, 19)
        Button3.Name = "Button3"
        Button3.Size = New Size(188, 30)
        Button3.TabIndex = 0
        Button3.Text = "📁 Seleccionar Carpeta"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(7, 194)
        Label5.Name = "Label5"
        Label5.Size = New Size(55, 15)
        Label5.TabIndex = 7
        Label5.Text = "Esquema"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(7, 170)
        Label6.Name = "Label6"
        Label6.Size = New Size(54, 15)
        Label6.TabIndex = 6
        Label6.Text = "Almacen"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(7, 145)
        Label7.Name = "Label7"
        Label7.Size = New Size(62, 15)
        Label7.TabIndex = 5
        Label7.Text = "Solicitante"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(7, 121)
        Label8.Name = "Label8"
        Label8.Size = New Size(84, 15)
        Label8.TabIndex = 4
        Label8.Text = "Observaciones"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(6, 95)
        Label3.Name = "Label3"
        Label3.Size = New Size(51, 15)
        Label3.TabIndex = 3
        Label3.Text = "Moneda"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(6, 71)
        Label4.Name = "Label4"
        Label4.Size = New Size(38, 15)
        Label4.TabIndex = 2
        Label4.Text = "Fecha"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(6, 46)
        Label2.Name = "Label2"
        Label2.Size = New Size(61, 15)
        Label2.TabIndex = 1
        Label2.Text = "Proveedor"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(6, 22)
        Label1.Name = "Label1"
        Label1.Size = New Size(40, 15)
        Label1.TabIndex = 0
        Label1.Text = "Orden"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(Label10)
        GroupBox2.Controls.Add(Label9)
        GroupBox2.Dock = DockStyle.Fill
        GroupBox2.Location = New Point(3, 3)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(677, 80)
        GroupBox2.TabIndex = 1
        GroupBox2.TabStop = False
        GroupBox2.Text = "📍 Origen - Destino"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(20, 50)
        Label10.Name = "Label10"
        Label10.Size = New Size(47, 15)
        Label10.TabIndex = 1
        Label10.Text = "Destino"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(19, 26)
        Label9.Name = "Label9"
        Label9.Size = New Size(48, 15)
        Label9.TabIndex = 0
        Label9.Text = "Archivo"
        ' 
        ' TableLayoutPanel2
        ' 
        TableLayoutPanel2.ColumnCount = 1
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.Controls.Add(Button1, 0, 0)
        TableLayoutPanel2.Controls.Add(Button2, 0, 1)
        TableLayoutPanel2.Location = New Point(686, 3)
        TableLayoutPanel2.Name = "TableLayoutPanel2"
        TableLayoutPanel2.RowCount = 2
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.Size = New Size(200, 76)
        TableLayoutPanel2.TabIndex = 2
        ' 
        ' Button1
        ' 
        Button1.Dock = DockStyle.Fill
        Button1.Location = New Point(3, 3)
        Button1.Name = "Button1"
        Button1.Size = New Size(194, 32)
        Button1.TabIndex = 0
        Button1.Text = "📂 Cargar Archivo"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Dock = DockStyle.Fill
        Button2.Location = New Point(3, 41)
        Button2.Name = "Button2"
        Button2.Size = New Size(194, 32)
        Button2.TabIndex = 1
        Button2.Text = "💾 Guardar Modelo"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(RichTextBox1)
        GroupBox3.Dock = DockStyle.Fill
        GroupBox3.Location = New Point(3, 89)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(677, 490)
        GroupBox3.TabIndex = 3
        GroupBox3.TabStop = False
        GroupBox3.Text = "👁️ Previa"
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.Dock = DockStyle.Fill
        RichTextBox1.Location = New Point(3, 19)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.ReadOnly = True
        RichTextBox1.Size = New Size(671, 468)
        RichTextBox1.TabIndex = 0
        RichTextBox1.Text = ""
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "Abrir CSV"
        OpenFileDialog1.Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*"
        OpenFileDialog1.RestoreDirectory = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(889, 582)
        Controls.Add(TableLayoutPanel1)
        Name = "Form1"
        Text = "Model Parser"
        TableLayoutPanel1.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        TableLayoutPanel2.ResumeLayout(False)
        GroupBox3.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog

End Class
