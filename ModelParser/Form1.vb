Imports System.Collections.Specialized
Imports System.IO
Imports System.Reflection

Public Class Form1
    Dim datosorden As String
    Dim claveorden As String
    'copiar al portapapeles
    Private Sub copiar(ByVal file As String)
        If System.IO.File.Exists(file) Then
            My.Computer.Clipboard.Clear()
            Dim paths As StringCollection = New StringCollection()
            paths.Add(file)
            Clipboard.SetFileDropList(paths)
            MsgBox("Se copio el modelo al portapapeles: " & file)
        Else
            MsgBox("No se encontro el archivo: " & file)
        End If
    End Sub

    'iniciar el modelo
    Private Sub iniciarmodelo()
        datosorden = Nothing
        claveorden = Nothing
        RichTextBox1.Text = ""
        Label1.Text = "Orden" 'orden de compra
        Label2.Text = "Proveedor" 'proveedor
        Label3.Text = "Moneda" 'moneda
        Label4.Text = "Fecha" 'fecha
        Label5.Text = "Esquema" 'esquema impuestos
        Label6.Text = "Almacen" 'almacen
        Label7.Text = "Solicitante" 'solicitante
        Label8.Text = "Observaciones" 'observaciones
        Label9.Text = "Archivo"
        Label10.Text = "Destino"
        RichTextBox1.AppendText("<?xml version=""1.0"" standalone=""yes""?>  
<DATAPACKET Version=""2.0"">
    <METADATA>
        <FIELDS>
            <FIELD attrname=""CVE_CLPV"" fieldtype=""string"" WIDTH=""10""/>
            <FIELD attrname=""NUM_ALMA"" fieldtype=""i4""/>
            <FIELD attrname=""CVE_PEDI"" fieldtype=""string"" WIDTH=""20""/>
            <FIELD attrname=""ESQUEMA"" fieldtype=""i4""/>
            <FIELD attrname=""DES_TOT"" fieldtype=""r8""/>
            <FIELD attrname=""DES_FIN"" fieldtype=""r8""/>
            <FIELD attrname=""CVE_VEND"" fieldtype=""string"" WIDTH=""5""/>
            <FIELD attrname=""COM_TOT"" fieldtype=""r8""/>
            <FIELD attrname=""NUM_MONED"" fieldtype=""i4""/>
            <FIELD attrname=""TIPCAMB"" fieldtype=""r8""/>
            <FIELD attrname=""STR_OBS"" fieldtype=""string"" WIDTH=""255""/>
            <FIELD attrname=""ENTREGA"" fieldtype=""string"" WIDTH=""25""/>
            <FIELD attrname=""SU_REFER"" fieldtype=""string"" WIDTH=""20""/>
            <FIELD attrname=""TOT_IND"" fieldtype=""r8""/>
            <FIELD attrname=""MODULO"" fieldtype=""string"" WIDTH=""4""/>
            <FIELD attrname=""CONDICION"" fieldtype=""string"" WIDTH=""25""/>
            <FIELD attrname=""dtfield"" fieldtype=""nested"">
                <FIELDS>
                    <FIELD attrname=""CANT"" fieldtype=""r8""/>
                    <FIELD attrname=""CVE_ART"" fieldtype=""string"" WIDTH=""20""/>
                    <FIELD attrname=""DESC1"" fieldtype=""r8""/>
                    <FIELD attrname=""DESC2"" fieldtype=""r8""/>
                    <FIELD attrname=""DESC3"" fieldtype=""r8""/>
                    <FIELD attrname=""IMPU1"" fieldtype=""r8""/>
                    <FIELD attrname=""IMPU2"" fieldtype=""r8""/>
                    <FIELD attrname=""IMPU3"" fieldtype=""r8""/>
                    <FIELD attrname=""IMPU4"" fieldtype=""r8""/>
                    <FIELD attrname=""COMI"" fieldtype=""r8""/>
                    <FIELD attrname=""PREC"" fieldtype=""r8""/>
                    <FIELD attrname=""NUM_ALM"" fieldtype=""i4""/>
                    <FIELD attrname=""STR_OBS"" fieldtype=""string"" WIDTH=""255""/>
                    <FIELD attrname=""REG_GPOPROD"" fieldtype=""i4""/>
                    <FIELD attrname=""REG_KITPROD"" fieldtype=""i4""/>
                    <FIELD attrname=""NUM_REG"" fieldtype=""i4""/>
                    <FIELD attrname=""COSTO"" fieldtype=""r8""/>
                    <FIELD attrname=""TIPO_PROD"" fieldtype=""string"" WIDTH=""1""/>
                    <FIELD attrname=""TIPO_ELEM"" fieldtype=""string"" WIDTH=""1""/>
                    <FIELD attrname=""MINDIRECTO"" fieldtype=""r8""/>
                    <FIELD attrname=""TIP_CAM"" fieldtype=""r8""/>
                    <FIELD attrname=""FACT_CONV"" fieldtype=""r8""/>
                    <FIELD attrname=""UNI_VENTA"" fieldtype=""string"" WIDTH=""10""/>
                    <FIELD attrname=""IMP1APLA"" fieldtype=""i4""/>
                    <FIELD attrname=""IMP2APLA"" fieldtype=""i4""/>
                    <FIELD attrname=""IMP3APLA"" fieldtype=""i4""/>
                    <FIELD attrname=""IMP4APLA"" fieldtype=""i4""/>
                    <FIELD attrname=""PREC_SINREDO"" fieldtype=""r8""/>
                    <FIELD attrname=""COST_SINREDO"" fieldtype=""r8""/>
                    <FIELD attrname=""LOTE"" fieldtype=""string"" WIDTH=""16""/>
                    <FIELD attrname=""PEDIMENTO"" fieldtype=""string"" WIDTH=""16""/>
                    <FIELD attrname=""FECHCADUC"" fieldtype=""dateTime""/>
                    <FIELD attrname=""FECHADUANA"" fieldtype=""dateTime""/>
                </FIELDS>
                <PARAMS/>
            </FIELD>
        </FIELDS>
        <PARAMS/>
    </METADATA>
    <ROWDATA>")
    End Sub
    'cargar archivo
    Private Sub cargararchivo(ByVal archivo As String)
        Dim file As String = archivo
        Using MyReader As New Microsoft.VisualBasic.
                      FileIO.TextFieldParser(
                        file)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            Dim cline As Integer = 1
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    'Dim currentField As String 'para debuguear campo por campo

                    If Not currentRow.Length = 12 Then
                        MsgBox("Formato de importacion incorrecto")
                        iniciarmodelo()
                        Exit Sub
                    End If

                    If cline > 1 Then '1 son los headers, a partir de 2 ya son datos
                        'agregar directamente segun el indice

                        Label1.Text = Label1.Text + ": " + currentRow(0).ToString() 'orden de compra


                        Label2.Text = Label2.Text + ": " + currentRow(1).ToString() 'proveedor
                        Label3.Text = Label3.Text + ": " + currentRow(3).ToString() 'moneda
                        Label4.Text = Label4.Text + ": " + currentRow(2).ToString() 'fecha
                        Label5.Text = Label5.Text + ": " + currentRow(9).ToString() 'esquema impuestos
                        Label6.Text = Label6.Text + ": 1" 'almacen
                        Label7.Text = Label7.Text + ": " + currentRow(5).ToString() 'solicitante
                        Label8.Text = Label8.Text + ": " + currentRow(4).ToString() 'observaciones


                        Dim proveedor As String = currentRow(1).ToString() 'proveedor
                        Dim almacen As String = "1"
                        Dim esquema As String
                        If currentRow(9).ToString() = "16.0" Then
                            esquema = "9"
                        ElseIf currentRow(9).ToString() = "8.0" Then
                            esquema = "13"
                        ElseIf currentRow(9).ToString() = "-4.0" Then
                            esquema = "14"
                        Else
                            esquema = "12"
                        End If
                        Dim moneda As String
                        If currentRow(3).ToString() = "MXN" Then
                            moneda = "1"
                        Else
                            moneda = "2"
                        End If

                        Dim observaciones As String = currentRow(4).ToString() & " SOLICITADO POR: " & currentRow(5).ToString() 'observaciones
                        Dim entregaren As String = currentRow(11).ToString() 'entregar en

                        If cline = 2 Then ' aqui nomas se llena una vez la informacion de la orden
                            claveorden = currentRow(0).ToString()
                            Label10.Text = "Guardar como: " & claveorden
                            datosorden = "<ROW 
                                            CVE_CLPV=""" & proveedor & """ 
                                            NUM_ALMA=""" & almacen & """ 
                                            ESQUEMA=""" & esquema & """ 
                                            DES_TOT=""0"" 
                                            DES_FIN=""0"" 
                                            NUM_MONED=""" & moneda & """ 
                                            TIPCAMB=""1"" 
                                            STR_OBS=""" & observaciones & """ 
                                            ENTREGA=""" & entregaren & """ 
                                            SU_REFER="""" 
                                            TOT_IND=""0"" 
                                            MODULO=""COMP"">
                                            <dtfield>"
                            RichTextBox1.AppendText(datosorden)
                        End If

                        'aqui se tienen que llenar las partidas
                        Dim cant As Integer = currentRow(8).ToString()
                        Dim prod As String = currentRow(7).ToString()
                        Dim iva As Integer = 0
                        If currentRow(9).ToString().Contains(".") Then
                            iva = currentRow(9)
                        End If
                        Dim ret As Integer = 0 'currentRow(9).ToString()
                        Dim desc As String = currentRow(10).ToString()
                        Dim costo As String = currentRow(6).ToString()




                        Dim lineaoc As String = "<ROWdtfield 
                    CANT=""" & cant & """ 
                    CVE_ART=""" & prod & """ 
                    DESC1=""0"" 
                    IMPU1=""0"" 
                    IMPU2=""0"" 
                    IMPU3=""" & ret & """ 
                    IMPU4=""" & iva & """ 
                    PREC=""0"" 
                    NUM_ALM=""" & almacen & """ 
                    STR_OBS=""" & desc & """ 
                    REG_GPOPROD=""0"" 
                    COSTO=""" & costo & """ 
                    TIPO_PROD=""P"" 
                    TIPO_ELEM=""N"" 
                    MINDIRECTO=""0"" 
                    TIP_CAM=""1"" 
                    FACT_CONV=""1"" 
                    UNI_VENTA=""pz"" 
                    IMP1APLA=""6"" 
                    IMP2APLA=""6"" 
                    IMP3APLA=""6"" 
                    IMP4APLA=""1"" 
                    PREC_SINREDO=""0"" 
                    COST_SINREDO=""" & costo & """/>"

                        RichTextBox1.AppendText(lineaoc)

                    End If

                    'For Each currentField In currentRow
                    ' MsgBox(currentField & "index: " & Array.IndexOf(currentRow, currentField))
                    ' Next

                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    MsgBox("Linea " & ex.Message &
                    "no valida, se saltara.")
                End Try
                cline = cline + 1
            End While
        End Using
        'cerrar el modelo
        Dim cierre As String = " </dtfield>
        </ROW>
    </ROWDATA>
</DATAPACKET>"
        RichTextBox1.AppendText(cierre)
    End Sub
    'boton cargar informacion
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim file As String
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            file = OpenFileDialog1.FileName
            iniciarmodelo()
            Label9.Text = "Archivo: " & file
        Else
            Exit Sub
        End If
        If Not IsNothing(file) Then
            MsgBox(file)
            cargararchivo(file)
        End If



    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iniciarmodelo()
    End Sub
    'guardar modelo
    Private Sub guardarmodelo(ByVal modelo As String)
        Dim sw As New StreamWriter(modelo)
        sw.Write(RichTextBox1.Text)
        sw.Close()
        If CheckBox1.Checked = False Then
            If MsgBox("Quieres abrir el archivo guardado?", MsgBoxStyle.YesNo, "Modelo Guardado") = MsgBoxResult.Yes Then
                Try
                    Process.Start("notepad.exe", modelo)
                Catch ex As Exception
                    MsgBox(ex.Message.ToString())
                End Try
            End If
            copiar(modelo)
        End If
        iniciarmodelo()
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SaveFileDialog1.Filter = "TextFile (*.mod)|*.mod"
        If Not claveorden Is Nothing Then
            SaveFileDialog1.FileName = Trim(claveorden.ToUpper)
        End If
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Dim modelo As String = SaveFileDialog1.FileName
            guardarmodelo(modelo)
        End If
    End Sub
    Private Sub procesarlote(ByVal folder As String)
        For Each item In ListBox1.Items()
            'MsgBox(item)
            cargararchivo(item)
            If Not claveorden Is Nothing Then

                Dim model As String = folder & "\" & Trim(claveorden.ToUpper()) & ".mod"
                'MsgBox(model)
                guardarmodelo(model)
            End If
            'guardarmodelo(item) 'falta parsear el nom,bre con el que se guardara el archivo mod
        Next
        MsgBox("Se proceso el lote")
        iniciarmodelo()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
            ListBox1.Items.Clear()
            Dim f As String = FolderBrowserDialog1.SelectedPath
            Dim files As String() = Directory.GetFiles(f, "*.csv")
            For Each file In files
                ListBox1.Items.Add(file)
            Next
            'empezar el cagadero
            procesarlote(f)
        End If
    End Sub
End Class
