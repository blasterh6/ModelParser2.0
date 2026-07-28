Imports System.Collections.Specialized
Imports System.IO
Imports System.Reflection

#Region "Clases de Datos"

' <summary>
' Representa una partida individual (un producto/artículo) dentro de una orden de compra.
' </summary>
Public Class PartidaOC
    Public Cantidad As String
    Public Producto As String
    Public Descripcion As String
    Public Costo As String
    Public IvaText As String
End Class

' <summary>
' Representa una Orden de Compra completa, la cual puede contener una o varias partidas.
' </summary>
Public Class OrdenCompra
    Public ClaveOrden As String
    Public Proveedor As String
    Public Fecha As String
    Public Moneda As String
    Public Observaciones As String
    Public Solicitante As String
    Public EntregarEn As String
    Public EsquemaBruto As String
    
    ' Lista que almacena todos los productos que pertenecen a esta orden
    Public Partidas As New List(Of PartidaOC)
End Class

#End Region

Public Class Form1

#Region "Variables Globales y Controles Dinámicos"

    ' Variable para almacenar temporalmente la clave de la orden que se está visualizando
    Dim claveorden As String
    
    ' Lista principal que almacena todas las órdenes de compra extraídas del CSV en memoria
    Dim ListaOrdenes As New List(Of OrdenCompra)
    
    ' Controles dinámicos que se crean en tiempo de ejecución para seleccionar órdenes
    Friend WithEvents ComboBoxOrdenes As ComboBox
    Friend WithEvents LabelCombo As Label

#End Region

#Region "Inicialización y UI (Interfaz de Usuario)"

    ' <summary>
    ' Evento que se dispara al abrir el programa.
    ' Aquí se inicializan los componentes visuales que no estaban en el diseñador originalmente.
    ' </summary>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 0. Ajustar las etiquetas existentes para que se trunquen si el texto es muy largo (AutoEllipsis)
        Label9.AutoSize = False
        Label9.Width = 360
        Label9.AutoEllipsis = True
        
        Label10.AutoSize = False
        Label10.Width = 360
        Label10.AutoEllipsis = True

        ' 1. Crear y configurar la etiqueta (Label) del menú desplegable
        LabelCombo = New Label()
        LabelCombo.Text = "Seleccionar Previa:"
        LabelCombo.Location = New Point(400, 29)
        LabelCombo.AutoSize = True
        LabelCombo.Visible = False
        GroupBox2.Controls.Add(LabelCombo)
        
        ' 2. Crear y configurar el menú desplegable (ComboBox) de órdenes
        ComboBoxOrdenes = New ComboBox()
        ComboBoxOrdenes.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBoxOrdenes.Location = New Point(515, 26)
        ComboBoxOrdenes.Size = New Size(150, 23)
        ComboBoxOrdenes.Visible = False
        GroupBox2.Controls.Add(ComboBoxOrdenes)
        
        ' Vincular el evento de cambio de selección al método correspondiente
        AddHandler ComboBoxOrdenes.SelectedIndexChanged, AddressOf ComboBoxOrdenes_SelectedIndexChanged

        ' 3. Limpiar y preparar la interfaz
        iniciarmodelo()
    End Sub

    ' <summary>
    ' Restablece la interfaz a su estado original, limpia los campos de texto, memoria y oculta los selectores.
    ' </summary>
    Private Sub iniciarmodelo()
        ' Limpiar variables en memoria
        claveorden = Nothing
        ListaOrdenes.Clear()
        
        ' Limpiar la caja de texto de previsualización
        RichTextBox1.Text = ""
        
        ' Restablecer las etiquetas (Labels) con sus valores por defecto
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
        Button2.Text = "Guardar Modelo"
        
        ' Ocultar el selector de órdenes dinámico si ya fue inicializado
        If ComboBoxOrdenes IsNot Nothing Then
            ComboBoxOrdenes.Items.Clear()
            ComboBoxOrdenes.Visible = False
            LabelCombo.Visible = False
        End If
    End Sub
    
    ' <summary>
    ' Actualiza el panel lateral y la caja de texto central (RichTextBox) con la información de una orden específica.
    ' </summary>
    ' <param name="orden">La orden de compra a mostrar en pantalla</param>
    Private Sub MostrarOrden(orden As OrdenCompra)
        ' Actualizar las etiquetas con los datos de la orden seleccionada
        Label1.Text = "Orden: " & orden.ClaveOrden
        Label2.Text = "Proveedor: " & orden.Proveedor
        Label3.Text = "Moneda: " & orden.Moneda
        Label4.Text = "Fecha: " & orden.Fecha
        Label5.Text = "Esquema: " & orden.EsquemaBruto
        Label6.Text = "Almacen: 1"
        Label7.Text = "Solicitante: " & orden.Solicitante
        Label8.Text = "Observaciones: " & orden.Observaciones
        Label10.Text = "Destino: " & orden.ClaveOrden & ".mod"
        
        ' Guardar la clave en la variable global
        claveorden = orden.ClaveOrden
        
        ' Generar el XML (.mod) y mostrarlo en la previsualización
        RichTextBox1.Text = GenerarXML(orden)
    End Sub

#End Region

#Region "Lógica de Importación CSV"

    ' <summary>
    ' Lee un archivo CSV línea por línea, extrae los datos, los valida y los agrupa en objetos 'OrdenCompra'.
    ' </summary>
    ' <param name="archivo">Ruta completa del archivo CSV a leer</param>
    Private Sub cargararchivo(ByVal archivo As String)
        ' 1. Limpiar cualquier dato previo en memoria y en la interfaz
        ListaOrdenes.Clear()
        ComboBoxOrdenes.Items.Clear()
        
        ' 2. Iniciar el lector del archivo CSV
        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(archivo)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            
            Dim currentRow As String()
            Dim cline As Integer = 1
            Dim ordenActual As OrdenCompra = Nothing
            
            ' 3. Leer hasta que se acaben las líneas
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    
                    ' Validar que la línea tenga exactamente 12 columnas
                    If Not currentRow.Length = 12 Then
                        MsgBox("Formato de importacion incorrecto en linea " & cline)
                        Continue While
                    End If
                    
                    ' Omitir la primera línea porque son los encabezados del CSV
                    If cline > 1 Then 
                        Dim refPedido As String = currentRow(0).ToString().Trim()
                        
                        ' Si la columna de 'Referencia de pedido' no está vacía, estamos ante una NUEVA orden
                        If Not String.IsNullOrEmpty(refPedido) Then
                            ordenActual = New OrdenCompra()
                            ordenActual.ClaveOrden = refPedido
                            ordenActual.Proveedor = currentRow(1).ToString().Trim()
                            
                            ' VALIDACIÓN CRÍTICA: La orden debe tener un proveedor
                            If String.IsNullOrWhiteSpace(ordenActual.Proveedor) Then
                                MsgBox("Advertencia: La orden de compra " & refPedido & " no tiene Proveedor. Se detendrá el proceso de importación.")
                                iniciarmodelo() ' Limpiar todo
                                Exit Sub ' Abortar el proceso inmediatamente
                            End If
                            
                            ordenActual.Fecha = currentRow(2).ToString()
                            ordenActual.Moneda = currentRow(3).ToString()
                            ordenActual.Observaciones = currentRow(4).ToString()
                            ordenActual.Solicitante = currentRow(5).ToString()
                            ordenActual.EntregarEn = currentRow(11).ToString()
                            ordenActual.EsquemaBruto = currentRow(9).ToString()
                            
                            ' Agregar esta nueva orden a la lista principal
                            ListaOrdenes.Add(ordenActual)
                        End If
                        
                        ' Si ya tenemos una orden activa, agregar los detalles de la partida actual
                        If ordenActual IsNot Nothing Then
                            Dim partida As New PartidaOC()
                            partida.Costo = currentRow(6).ToString()
                            partida.Producto = currentRow(7).ToString()
                            partida.Cantidad = currentRow(8).ToString()
                            partida.IvaText = currentRow(9).ToString()
                            partida.Descripcion = currentRow(10).ToString()
                            
                            ' Agregar la partida a la lista interna de esta orden
                            ordenActual.Partidas.Add(partida)
                        End If
                    End If
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    ' Si hay una línea malformada que el parser no puede leer
                    MsgBox("Linea " & ex.Message & " no valida, se saltara.")
                End Try
                
                cline = cline + 1
            End While
        End Using
        
        ' 4. Configurar la interfaz si se encontraron órdenes válidas
        If ListaOrdenes.Count > 0 Then
            ' Llenar el ComboBox con las claves de las órdenes
            For Each o In ListaOrdenes
                ComboBoxOrdenes.Items.Add(o.ClaveOrden)
            Next
            
            ' Mostrar el ComboBox y seleccionar el primer elemento
            ComboBoxOrdenes.Visible = True
            LabelCombo.Visible = True
            ComboBoxOrdenes.SelectedIndex = 0
            
            ' Modificar el texto del botón Guardar dependiendo de si es 1 orden o varias
            If ListaOrdenes.Count = 1 Then
                Button2.Text = "Guardar Orden"
            Else
                Button2.Text = "Guardar Todas (" & ListaOrdenes.Count & ")"
            End If
        End If
    End Sub

    ' <summary>
    ' Procesa automáticamente múltiples archivos CSV ubicados dentro de una carpeta.
    ' </summary>
    ' <param name="folder">Ruta de la carpeta que contiene los archivos CSV</param>
    Private Sub procesarlote(ByVal folder As String)
        For Each item In ListBox1.Items()
            cargararchivo(item)
            If ListaOrdenes.Count > 0 Then
                ' Guardar todas las órdenes extraídas de este archivo
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

#End Region

#Region "Generación de XML"

    ' <summary>
    ' Transforma una Orden de Compra y todas sus partidas en el formato XML esperado por el archivo .mod.
    ' </summary>
    ' <param name="orden">El objeto OrdenCompra con los datos a transformar</param>
    ' <returns>Una cadena de texto con el formato XML final (DATAPACKET)</returns>
    Private Function GenerarXML(orden As OrdenCompra) As String
        Dim proveedor As String = orden.Proveedor
        Dim almacen As String = "1"
        Dim esquema As String
        
        ' 1. Conversión de esquema (IVA a ID de esquema interno)
        If orden.EsquemaBruto = "16.0" Then
            esquema = "9"
        ElseIf orden.EsquemaBruto = "8.0" Then
            esquema = "13"
        ElseIf orden.EsquemaBruto = "-4.0" Then
            esquema = "14"
        Else
            esquema = "12"
        End If
        
        ' 2. Conversión de la moneda
        Dim moneda As String
        If orden.Moneda = "MXN" Then
            moneda = "1"
        Else
            moneda = "2"
        End If
        
        ' 3. Concatenar observaciones y solicitante
        Dim obs As String = orden.Observaciones & " SOLICITADO POR: " & orden.Solicitante

        ' 4. Escribir el encabezado del XML y los metadatos (definición de campos)
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

        ' 5. Recorrer cada partida (producto) e insertar su respectivo nodo <ROWdtfield>
        For Each p In orden.Partidas
            Dim cant As String = p.Cantidad
            Dim prod As String = p.Producto
            Dim iva As String = "0"
            
            ' Validar y convertir el IVA a número entero si viene con decimales
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
        
        ' 6. Cerrar etiquetas principales
        xml &= " </dtfield>" & vbCrLf &
"        </ROW>" & vbCrLf &
"    </ROWDATA>" & vbCrLf &
"</DATAPACKET>"

        Return xml
    End Function

#End Region

#Region "Manejo de Eventos (Botones y Selectores)"

    ' <summary>
    ' Evento: Clic en el botón "Cargar Archivo".
    ' Abre el explorador para elegir el archivo CSV y llama al procesador.
    ' </summary>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim file As String
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            file = OpenFileDialog1.FileName
            
            ' Limpiar interfaz antes de cargar algo nuevo
            iniciarmodelo()
            Label9.Text = "Archivo: " & file
        Else
            Exit Sub
        End If
        
        If Not IsNothing(file) Then
            ' Cargar el contenido del CSV a memoria
            cargararchivo(file)
        End If
    End Sub
    
    ' <summary>
    ' Evento: Clic en el botón "Guardar Modelo" o "Guardar Todas".
    ' Detecta automáticamente si hay 1 o múltiples órdenes para tomar el flujo adecuado.
    ' </summary>
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Validación por seguridad
        If ListaOrdenes.Count = 0 Then
            MsgBox("No hay órdenes cargadas.")
            Exit Sub
        End If
        
        If ListaOrdenes.Count = 1 Then
            ' ----- FLUJO 1: Archivo Simple -----
            ' Pide ubicación y guarda el archivo con el nombre de la orden
            SaveFileDialog1.Filter = "TextFile (*.mod)|*.mod"
            SaveFileDialog1.FileName = Trim(ListaOrdenes(0).ClaveOrden.ToUpper)
            
            If SaveFileDialog1.ShowDialog = DialogResult.OK Then
                Dim modelo As String = SaveFileDialog1.FileName
                
                ' Crear y escribir el archivo
                Dim sw As New StreamWriter(modelo)
                sw.Write(RichTextBox1.Text)
                sw.Close()
                
                ' Abrir en bloc de notas si el usuario NO habilitó el 'Modo Silencioso'
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
                
                ' Limpiar interfaz después del éxito
                iniciarmodelo()
            End If
            
        Else
            ' ----- FLUJO 2: Archivo Múltiple -----
            ' Pide una carpeta y guarda todos los archivos .mod automáticamente
            If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
                Dim folder As String = FolderBrowserDialog1.SelectedPath
                
                ' Recorrer la memoria e ir guardando cada orden
                For Each orden In ListaOrdenes
                    Dim model As String = folder & "\" & Trim(orden.ClaveOrden.ToUpper()) & ".mod"
                    Dim sw As New StreamWriter(model)
                    sw.Write(GenerarXML(orden))
                    sw.Close()
                Next
                
                MsgBox("Se guardaron " & ListaOrdenes.Count & " archivos .mod correctamente en: " & folder)
                
                ' Limpiar interfaz después del éxito
                iniciarmodelo()
            End If
        End If
    End Sub
    
    ' <summary>
    ' Evento: Clic en el botón "Seleccionar Carpeta" (Procesamiento por lote de carpetas).
    ' </summary>
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
            ListBox1.Items.Clear()
            
            Dim f As String = FolderBrowserDialog1.SelectedPath
            Dim files As String() = Directory.GetFiles(f, "*.csv")
            
            For Each file In files
                ListBox1.Items.Add(file)
            Next
            
            ' Procesa todos los CSV de la carpeta seleccionada
            procesarlote(f)
        End If
    End Sub

    ' <summary>
    ' Evento: Cambio de selección en el menú desplegable de Órdenes.
    ' </summary>
    Private Sub ComboBoxOrdenes_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ComboBoxOrdenes.SelectedIndex >= 0 Then
            ' Muestra en pantalla la orden que el usuario seleccionó
            MostrarOrden(ListaOrdenes(ComboBoxOrdenes.SelectedIndex))
        End If
    End Sub

#End Region

#Region "Utilidades"

    ' <summary>
    ' Copia la ruta del archivo generado al portapapeles del sistema operativo.
    ' </summary>
    ' <param name="file">Ruta completa del archivo generado</param>
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

#End Region

End Class
