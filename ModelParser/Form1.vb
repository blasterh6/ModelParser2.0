Imports System.Collections.Specialized
Imports System.IO
Imports System.Reflection

Public Class PartidaOC
    Public Cantidad As String
    Public Producto As String
    Public Descripcion As String
    Public Costo As String
    Public IvaText As String
End Class

Public Class OrdenCompra
    Public ClaveOrden As String
    Public Proveedor As String
    Public Fecha As String
    Public Moneda As String
    Public Observaciones As String
    Public Solicitante As String
    Public EntregarEn As String
    Public EsquemaBruto As String
    
    Public Partidas As New List(Of PartidaOC)
End Class

Public Class Form1
    Dim claveorden As String
    Dim ListaOrdenes As New List(Of OrdenCompra)
    
    Friend WithEvents ComboBoxOrdenes As ComboBox
    Friend WithEvents ButtonGuardarTodas As Button
    
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
        claveorden = Nothing
        RichTextBox1.Text = ""
        Label1.Text = "Orden"
        Label2.Text = "Proveedor"
        Label3.Text = "Moneda"
        Label4.Text = "Fecha"
        Label5.Text = "Esquema"
        Label6.Text = "Almacen"
        Label7.Text = "Solicitante"
        Label8.Text = "Observaciones"
        Label9.Text = "Archivo"
        Label10.Text = "Destino"
    End Sub
    
    Private Sub MostrarOrden(orden As OrdenCompra)
        Label1.Text = "Orden: " & orden.ClaveOrden
        Label2.Text = "Proveedor: " & orden.Proveedor
        Label3.Text = "Moneda: " & orden.Moneda
        Label4.Text = "Fecha: " & orden.Fecha
        Label5.Text = "Esquema: " & orden.EsquemaBruto
        Label6.Text = "Almacen: 1"
        Label7.Text = "Solicitante: " & orden.Solicitante
        Label8.Text = "Observaciones: " & orden.Observaciones
        Label10.Text = "Guardar como: " & orden.ClaveOrden
        claveorden = orden.ClaveOrden
        
        RichTextBox1.Text = GenerarXML(orden)
    End Sub

    Private Function GenerarXML(orden As OrdenCompra) As String
        Dim proveedor As String = orden.Proveedor
        Dim almacen As String = "1"
        Dim esquema As String
        If orden.EsquemaBruto = "16.0" Then
            esquema = "9"
        ElseIf orden.EsquemaBruto = "8.0" Then
            esquema = "13"
        ElseIf orden.EsquemaBruto = "-4.0" Then
            esquema = "14"
        Else
            esquema = "12"
        End If
        Dim moneda As String
        If orden.Moneda = "MXN" Then
            moneda = "1"
        Else
            moneda = "2"
        End If
        Dim obs As String = orden.Observaciones & " SOLICITADO POR: " & orden.Solicitante

        Dim xml As String = "<?xml version=""1.0"" standalone=""yes""?>  " & vbCrLf &
"<DATAPACKET Version=""2.0"">" & vbCrLf &
"    <METADATA>" & vbCrLf &
"        <FIELDS>" & vbCrLf &
"            <FIELD attrname=""CVE_CLPV"" fieldtype=""string"" WIDTH=""10""/>" & vbCrLf &
"            <FIELD attrname=""NUM_ALMA"" fieldtype=""i4""/>" & vbCrLf &
"            <FIELD attrname=""CVE_PEDI"" fieldtype=""string"" WIDTH=""20""/>" & vbCrLf &
"            <FIELD attrname=""ESQUEMA"" fieldtype=""i4""/>" & vbCrLf &
"            <FIELD attrname=""DES_TOT"" fieldtype=""r8""/>" & vbCrLf &
"            <FIELD attrname=""DES_FIN"" fieldtype=""r8""/>" & vbCrLf &
"            <FIELD attrname=""CVE_VEND"" fieldtype=""string"" WIDTH=""5""/>" & vbCrLf &
"            <FIELD attrname=""COM_TOT"" fieldtype=""r8""/>" & vbCrLf &
"            <FIELD attrname=""NUM_MONED"" fieldtype=""i4""/>" & vbCrLf &
"            <FIELD attrname=""TIPCAMB"" fieldtype=""r8""/>" & vbCrLf &
"            <FIELD attrname=""STR_OBS"" fieldtype=""string"" WIDTH=""255""/>" & vbCrLf &
"            <FIELD attrname=""ENTREGA"" fieldtype=""string"" WIDTH=""25""/>" & vbCrLf &
"            <FIELD attrname=""SU_REFER"" fieldtype=""string"" WIDTH=""20""/>" & vbCrLf &
"            <FIELD attrname=""TOT_IND"" fieldtype=""r8""/>" & vbCrLf &
"            <FIELD attrname=""MODULO"" fieldtype=""string"" WIDTH=""4""/>" & vbCrLf &
"            <FIELD attrname=""CONDICION"" fieldtype=""string"" WIDTH=""25""/>" & vbCrLf &
"            <FIELD attrname=""dtfield"" fieldtype=""nested"">" & vbCrLf &
"                <FIELDS>" & vbCrLf &
"                    <FIELD attrname=""CANT"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""CVE_ART"" fieldtype=""string"" WIDTH=""20""/>" & vbCrLf &
"                    <FIELD attrname=""DESC1"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""DESC2"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""DESC3"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""IMPU1"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""IMPU2"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""IMPU3"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""IMPU4"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""COMI"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""PREC"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""NUM_ALM"" fieldtype=""i4""/>" & vbCrLf &
"                    <FIELD attrname=""STR_OBS"" fieldtype=""string"" WIDTH=""255""/>" & vbCrLf &
"                    <FIELD attrname=""REG_GPOPROD"" fieldtype=""i4""/>" & vbCrLf &
"                    <FIELD attrname=""REG_KITPROD"" fieldtype=""i4""/>" & vbCrLf &
"                    <FIELD attrname=""NUM_REG"" fieldtype=""i4""/>" & vbCrLf &
"                    <FIELD attrname=""COSTO"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""TIPO_PROD"" fieldtype=""string"" WIDTH=""1""/>" & vbCrLf &
"                    <FIELD attrname=""TIPO_ELEM"" fieldtype=""string"" WIDTH=""1""/>" & vbCrLf &
"                    <FIELD attrname=""MINDIRECTO"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""TIP_CAM"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""FACT_CONV"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""UNI_VENTA"" fieldtype=""string"" WIDTH=""10""/>" & vbCrLf &
"                    <FIELD attrname=""IMP1APLA"" fieldtype=""i4""/>" & vbCrLf &
"                    <FIELD attrname=""IMP2APLA"" fieldtype=""i4""/>" & vbCrLf &
"                    <FIELD attrname=""IMP3APLA"" fieldtype=""i4""/>" & vbCrLf &
"                    <FIELD attrname=""IMP4APLA"" fieldtype=""i4""/>" & vbCrLf &
"                    <FIELD attrname=""PREC_SINREDO"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""COST_SINREDO"" fieldtype=""r8""/>" & vbCrLf &
"                    <FIELD attrname=""LOTE"" fieldtype=""string"" WIDTH=""16""/>" & vbCrLf &
"                    <FIELD attrname=""PEDIMENTO"" fieldtype=""string"" WIDTH=""16""/>" & vbCrLf &
"                    <FIELD attrname=""FECHCADUC"" fieldtype=""dateTime""/>" & vbCrLf &
"                    <FIELD attrname=""FECHADUANA"" fieldtype=""dateTime""/>" & vbCrLf &
"                </FIELDS>" & vbCrLf &
"                <PARAMS/>" & vbCrLf &
"            </FIELD>" & vbCrLf &
"        </FIELDS>" & vbCrLf &
"        <PARAMS/>" & vbCrLf &
"    </METADATA>" & vbCrLf &
"    <ROWDATA>" & vbCrLf &
"<ROW " & vbCrLf &
"    CVE_CLPV=""" & proveedor & """ " & vbCrLf &
"    NUM_ALMA=""" & almacen & """ " & vbCrLf &
"    ESQUEMA=""" & esquema & """ " & vbCrLf &
"    DES_TOT=""0"" " & vbCrLf &
"    DES_FIN=""0"" " & vbCrLf &
"    NUM_MONED=""" & moneda & """ " & vbCrLf &
"    TIPCAMB=""1"" " & vbCrLf &
"    STR_OBS=""" & obs & """ " & vbCrLf &
"    ENTREGA=""" & orden.EntregarEn & """ " & vbCrLf &
"    SU_REFER="""" " & vbCrLf &
"    TOT_IND=""0"" " & vbCrLf &
"    MODULO=""COMP"">" & vbCrLf &
"    <dtfield>" & vbCrLf

        For Each p In orden.Partidas
            Dim cant As String = p.Cantidad
            Dim prod As String = p.Producto
            Dim iva As String = "0"
            If p.IvaText.Contains(".") Then
                Try
                    iva = CInt(Math.Round(Convert.ToDouble(p.IvaText))).ToString()
                Catch ex As Exception
                    iva = "0"
                End Try
            End If
            Dim desc As String = p.Descripcion
            Dim costo As String = p.Costo
            
            xml &= "<ROWdtfield " & vbCrLf &
"    CANT=""" & cant & """ " & vbCrLf &
"    CVE_ART=""" & prod & """ " & vbCrLf &
"    DESC1=""0"" " & vbCrLf &
"    IMPU1=""0"" " & vbCrLf &
"    IMPU2=""0"" " & vbCrLf &
"    IMPU3=""0"" " & vbCrLf &
"    IMPU4=""" & iva & """ " & vbCrLf &
"    PREC=""0"" " & vbCrLf &
"    NUM_ALM=""" & almacen & """ " & vbCrLf &
"    STR_OBS=""" & desc & """ " & vbCrLf &
"    REG_GPOPROD=""0"" " & vbCrLf &
"    COSTO=""" & costo & """ " & vbCrLf &
"    TIPO_PROD=""P"" " & vbCrLf &
"    TIPO_ELEM=""N"" " & vbCrLf &
"    MINDIRECTO=""0"" " & vbCrLf &
"    TIP_CAM=""1"" " & vbCrLf &
"    FACT_CONV=""1"" " & vbCrLf &
"    UNI_VENTA=""pz"" " & vbCrLf &
"    IMP1APLA=""6"" " & vbCrLf &
"    IMP2APLA=""6"" " & vbCrLf &
"    IMP3APLA=""6"" " & vbCrLf &
"    IMP4APLA=""1"" " & vbCrLf &
"    PREC_SINREDO=""0"" " & vbCrLf &
"    COST_SINREDO=""" & costo & """/>" & vbCrLf
        Next
        
        xml &= " </dtfield>" & vbCrLf &
"        </ROW>" & vbCrLf &
"    </ROWDATA>" & vbCrLf &
"</DATAPACKET>"

        Return xml
    End Function

    'cargar archivo
    Private Sub cargararchivo(ByVal archivo As String)
        ListaOrdenes.Clear()
        ComboBoxOrdenes.Items.Clear()
        
        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(archivo)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            Dim cline As Integer = 1
            Dim ordenActual As OrdenCompra = Nothing
            
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    
                    If Not currentRow.Length = 12 Then
                        MsgBox("Formato de importacion incorrecto en linea " & cline)
                        Continue While
                    End If
                    
                    If cline > 1 Then '1 son los headers, a partir de 2 ya son datos
                        Dim refPedido As String = currentRow(0).ToString().Trim()
                        If Not String.IsNullOrEmpty(refPedido) Then
                            ordenActual = New OrdenCompra()
                            ordenActual.ClaveOrden = refPedido
                            ordenActual.Proveedor = currentRow(1).ToString()
                            ordenActual.Fecha = currentRow(2).ToString()
                            ordenActual.Moneda = currentRow(3).ToString()
                            ordenActual.Observaciones = currentRow(4).ToString()
                            ordenActual.Solicitante = currentRow(5).ToString()
                            ordenActual.EntregarEn = currentRow(11).ToString()
                            ordenActual.EsquemaBruto = currentRow(9).ToString()
                            
                            ListaOrdenes.Add(ordenActual)
                        End If
                        
                        If ordenActual IsNot Nothing Then
                            Dim partida As New PartidaOC()
                            partida.Costo = currentRow(6).ToString()
                            partida.Producto = currentRow(7).ToString()
                            partida.Cantidad = currentRow(8).ToString()
                            partida.IvaText = currentRow(9).ToString()
                            partida.Descripcion = currentRow(10).ToString()
                            
                            ordenActual.Partidas.Add(partida)
                        End If
                    End If
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Linea " & ex.Message & " no valida, se saltara.")
                End Try
                cline = cline + 1
            End While
        End Using
        
        If ListaOrdenes.Count > 0 Then
            For Each o In ListaOrdenes
                ComboBoxOrdenes.Items.Add(o.ClaveOrden)
            Next
            ComboBoxOrdenes.SelectedIndex = 0
            MsgBox("Se encontraron " & ListaOrdenes.Count & " órdenes en el archivo.")
        End If
    End Sub

    Private Sub ComboBoxOrdenes_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ComboBoxOrdenes.SelectedIndex >= 0 Then
            MostrarOrden(ListaOrdenes(ComboBoxOrdenes.SelectedIndex))
        End If
    End Sub
    
    Private Sub ButtonGuardarTodas_Click(sender As Object, e As EventArgs)
        If ListaOrdenes.Count = 0 Then
            MsgBox("No hay órdenes cargadas.")
            Exit Sub
        End If
        
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
            Dim folder As String = FolderBrowserDialog1.SelectedPath
            For Each orden In ListaOrdenes
                Dim model As String = folder & "\" & Trim(orden.ClaveOrden.ToUpper()) & ".mod"
                Dim sw As New StreamWriter(model)
                sw.Write(GenerarXML(orden))
                sw.Close()
            Next
            MsgBox("Se guardaron " & ListaOrdenes.Count & " archivos .mod correctamente en: " & folder)
        End If
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
            cargararchivo(file)
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBoxOrdenes = New ComboBox()
        ComboBoxOrdenes.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBoxOrdenes.Dock = DockStyle.Top
        GroupBox1.Controls.Add(ComboBoxOrdenes)
        AddHandler ComboBoxOrdenes.SelectedIndexChanged, AddressOf ComboBoxOrdenes_SelectedIndexChanged
        
        ButtonGuardarTodas = New Button()
        ButtonGuardarTodas.Text = "Guardar Todas"
        ButtonGuardarTodas.Dock = DockStyle.Top
        GroupBox1.Controls.Add(ButtonGuardarTodas)
        AddHandler ButtonGuardarTodas.Click, AddressOf ButtonGuardarTodas_Click

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
    End Sub

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
            cargararchivo(item)
            If ListaOrdenes.Count > 0 Then
                For Each orden In ListaOrdenes
                    Dim model As String = folder & "\" & Trim(orden.ClaveOrden.ToUpper()) & ".mod"
                    Dim sw As New StreamWriter(model)
                    sw.Write(GenerarXML(orden))
                    sw.Close()
                Next
            End If
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
            procesarlote(f)
        End If
    End Sub
End Class
