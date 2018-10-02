<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewDailyStock
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmb_Category = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Cmb_RawMaterial = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.dtDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblClosing = New System.Windows.Forms.Label()
        Me.lblTotalOutward = New System.Windows.Forms.Label()
        Me.lblTodayInward = New System.Windows.Forms.Label()
        Me.lblRemainingStock = New System.Windows.Forms.Label()
        Me.lblOpening = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmb_Category
        '
        Me.cmb_Category.FormattingEnabled = True
        Me.cmb_Category.Location = New System.Drawing.Point(149, 18)
        Me.cmb_Category.Margin = New System.Windows.Forms.Padding(2)
        Me.cmb_Category.Name = "cmb_Category"
        Me.cmb_Category.Size = New System.Drawing.Size(289, 21)
        Me.cmb_Category.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(44, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Category"
        '
        'Cmb_RawMaterial
        '
        Me.Cmb_RawMaterial.FormattingEnabled = True
        Me.Cmb_RawMaterial.Location = New System.Drawing.Point(149, 55)
        Me.Cmb_RawMaterial.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmb_RawMaterial.Name = "Cmb_RawMaterial"
        Me.Cmb_RawMaterial.Size = New System.Drawing.Size(289, 21)
        Me.Cmb_RawMaterial.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(44, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Raw Material Name"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(149, 126)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 8
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'dtDate
        '
        Me.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDate.Location = New System.Drawing.Point(149, 87)
        Me.dtDate.Name = "dtDate"
        Me.dtDate.Size = New System.Drawing.Size(116, 20)
        Me.dtDate.TabIndex = 9
        Me.dtDate.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(44, 93)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Date :"
        Me.Label1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.lblTotal)
        Me.Panel1.Controls.Add(Me.lblClosing)
        Me.Panel1.Controls.Add(Me.lblTotalOutward)
        Me.Panel1.Controls.Add(Me.lblTodayInward)
        Me.Panel1.Controls.Add(Me.lblRemainingStock)
        Me.Panel1.Controls.Add(Me.lblOpening)
        Me.Panel1.Location = New System.Drawing.Point(47, 155)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(485, 95)
        Me.Panel1.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(177, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Remaining Stock"
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(209, 57)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(11, 13)
        Me.lblTotal.TabIndex = 9
        Me.lblTotal.Text = " "
        '
        'lblClosing
        '
        Me.lblClosing.AutoSize = True
        Me.lblClosing.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClosing.Location = New System.Drawing.Point(399, 57)
        Me.lblClosing.Name = "lblClosing"
        Me.lblClosing.Size = New System.Drawing.Size(11, 13)
        Me.lblClosing.TabIndex = 8
        Me.lblClosing.Text = " "
        '
        'lblTotalOutward
        '
        Me.lblTotalOutward.AutoSize = True
        Me.lblTotalOutward.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalOutward.Location = New System.Drawing.Point(303, 57)
        Me.lblTotalOutward.Name = "lblTotalOutward"
        Me.lblTotalOutward.Size = New System.Drawing.Size(11, 13)
        Me.lblTotalOutward.TabIndex = 7
        Me.lblTotalOutward.Text = " "
        '
        'lblTodayInward
        '
        Me.lblTodayInward.AutoSize = True
        Me.lblTodayInward.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTodayInward.Location = New System.Drawing.Point(108, 57)
        Me.lblTodayInward.Name = "lblTodayInward"
        Me.lblTodayInward.Size = New System.Drawing.Size(11, 13)
        Me.lblTodayInward.TabIndex = 6
        Me.lblTodayInward.Text = " "
        '
        'lblRemainingStock
        '
        Me.lblRemainingStock.AutoSize = True
        Me.lblRemainingStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemainingStock.ForeColor = System.Drawing.Color.DarkRed
        Me.lblRemainingStock.Location = New System.Drawing.Point(177, 44)
        Me.lblRemainingStock.Name = "lblRemainingStock"
        Me.lblRemainingStock.Size = New System.Drawing.Size(103, 13)
        Me.lblRemainingStock.TabIndex = 2
        Me.lblRemainingStock.Text = "Remaining Stock"
        '
        'lblOpening
        '
        Me.lblOpening.AutoSize = True
        Me.lblOpening.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpening.Location = New System.Drawing.Point(24, 57)
        Me.lblOpening.Name = "lblOpening"
        Me.lblOpening.Size = New System.Drawing.Size(11, 13)
        Me.lblOpening.TabIndex = 1
        Me.lblOpening.Text = " "
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(241, 126)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 12
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'ViewDailyStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(545, 273)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtDate)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.cmb_Category)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Cmb_RawMaterial)
        Me.Controls.Add(Me.Label9)
        Me.Name = "ViewDailyStock"
        Me.Text = "ViewDailyStock"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmb_Category As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Cmb_RawMaterial As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents dtDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblClosing As System.Windows.Forms.Label
    Friend WithEvents lblTotalOutward As System.Windows.Forms.Label
    Friend WithEvents lblTodayInward As System.Windows.Forms.Label
    Friend WithEvents lblRemainingStock As System.Windows.Forms.Label
    Friend WithEvents lblOpening As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
