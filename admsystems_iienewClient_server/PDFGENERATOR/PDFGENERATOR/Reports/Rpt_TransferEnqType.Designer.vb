<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rpt_TransferEnqType
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
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtMainExec = New System.Windows.Forms.TextBox()
        Me.btnAddExecutive = New System.Windows.Forms.Button()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.txtExec = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtMainStatus = New System.Windows.Forms.TextBox()
        Me.btnADDStatus = New System.Windows.Forms.Button()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtstatus = New System.Windows.Forms.TextBox()
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.txtMainExec)
        Me.Panel3.Controls.Add(Me.btnAddExecutive)
        Me.Panel3.Controls.Add(Me.Label57)
        Me.Panel3.Controls.Add(Me.txtExec)
        Me.Panel3.Location = New System.Drawing.Point(402, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(181, 137)
        Me.Panel3.TabIndex = 30
        '
        'txtMainExec
        '
        Me.txtMainExec.Location = New System.Drawing.Point(8, 91)
        Me.txtMainExec.Multiline = True
        Me.txtMainExec.Name = "txtMainExec"
        Me.txtMainExec.Size = New System.Drawing.Size(164, 39)
        Me.txtMainExec.TabIndex = 2
        '
        'btnAddExecutive
        '
        Me.btnAddExecutive.Location = New System.Drawing.Point(65, 50)
        Me.btnAddExecutive.Name = "btnAddExecutive"
        Me.btnAddExecutive.Size = New System.Drawing.Size(44, 23)
        Me.btnAddExecutive.TabIndex = 1
        Me.btnAddExecutive.Text = "Add"
        Me.btnAddExecutive.UseVisualStyleBackColor = True
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(18, 10)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(108, 15)
        Me.Label57.TabIndex = 20
        Me.Label57.Text = "Sales Executive"
        '
        'txtExec
        '
        Me.txtExec.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtExec.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtExec.Location = New System.Drawing.Point(8, 29)
        Me.txtExec.Name = "txtExec"
        Me.txtExec.Size = New System.Drawing.Size(142, 20)
        Me.txtExec.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtMainStatus)
        Me.Panel2.Controls.Add(Me.btnADDStatus)
        Me.Panel2.Controls.Add(Me.Label56)
        Me.Panel2.Controls.Add(Me.txtstatus)
        Me.Panel2.Location = New System.Drawing.Point(199, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(182, 137)
        Me.Panel2.TabIndex = 29
        '
        'txtMainStatus
        '
        Me.txtMainStatus.Location = New System.Drawing.Point(8, 91)
        Me.txtMainStatus.Multiline = True
        Me.txtMainStatus.Name = "txtMainStatus"
        Me.txtMainStatus.Size = New System.Drawing.Size(159, 39)
        Me.txtMainStatus.TabIndex = 2
        '
        'btnADDStatus
        '
        Me.btnADDStatus.Location = New System.Drawing.Point(62, 50)
        Me.btnADDStatus.Name = "btnADDStatus"
        Me.btnADDStatus.Size = New System.Drawing.Size(44, 23)
        Me.btnADDStatus.TabIndex = 1
        Me.btnADDStatus.Text = "Add"
        Me.btnADDStatus.UseVisualStyleBackColor = True
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(59, 10)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(47, 15)
        Me.Label56.TabIndex = 18
        Me.Label56.Text = "Status"
        '
        'txtstatus
        '
        Me.txtstatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtstatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtstatus.Location = New System.Drawing.Point(29, 29)
        Me.txtstatus.Name = "txtstatus"
        Me.txtstatus.Size = New System.Drawing.Size(99, 20)
        Me.txtstatus.TabIndex = 0
        '
        'dtEndDate
        '
        Me.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEndDate.Location = New System.Drawing.Point(62, 77)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(93, 20)
        Me.dtEndDate.TabIndex = 27
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(93, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 15)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "To :"
        '
        'dtStartDate
        '
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStartDate.Location = New System.Drawing.Point(62, 25)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(93, 20)
        Me.dtStartDate.TabIndex = 25
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(81, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 15)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "From :"
        '
        'Rpt_TransferEnqType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(973, 619)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.dtEndDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtStartDate)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Rpt_TransferEnqType"
        Me.Text = "Rpt_TransferEnqType"
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtMainExec As System.Windows.Forms.TextBox
    Friend WithEvents btnAddExecutive As System.Windows.Forms.Button
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents txtExec As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtMainStatus As System.Windows.Forms.TextBox
    Friend WithEvents btnADDStatus As System.Windows.Forms.Button
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents txtstatus As System.Windows.Forms.TextBox
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
