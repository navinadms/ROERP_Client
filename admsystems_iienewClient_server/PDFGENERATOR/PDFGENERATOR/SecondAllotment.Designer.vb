﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SecondAllotment
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtEnqNo = New System.Windows.Forms.TextBox()
        Me.cmbUser = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.GvAllotedData = New System.Windows.Forms.DataGridView()
        Me.cmbSearchUser = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnAllot = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GvAllotedData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(188, 102)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(87, 29)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtCustName
        '
        Me.txtCustName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtCustName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtCustName.Enabled = False
        Me.txtCustName.Location = New System.Drawing.Point(99, 36)
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.Size = New System.Drawing.Size(155, 20)
        Me.txtCustName.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Customer Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Enq No "
        '
        'txtEnqNo
        '
        Me.txtEnqNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtEnqNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtEnqNo.Location = New System.Drawing.Point(99, 2)
        Me.txtEnqNo.Name = "txtEnqNo"
        Me.txtEnqNo.Size = New System.Drawing.Size(155, 20)
        Me.txtEnqNo.TabIndex = 0
        '
        'cmbUser
        '
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(99, 67)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(155, 21)
        Me.cmbUser.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "User"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.GvAllotedData)
        Me.GroupBox1.Controls.Add(Me.cmbSearchUser)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 170)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(394, 298)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search User Alloted"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(192, 19)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(87, 29)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'GvAllotedData
        '
        Me.GvAllotedData.AllowUserToAddRows = False
        Me.GvAllotedData.AllowUserToDeleteRows = False
        Me.GvAllotedData.AllowUserToOrderColumns = True
        Me.GvAllotedData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GvAllotedData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GvAllotedData.Location = New System.Drawing.Point(13, 54)
        Me.GvAllotedData.Name = "GvAllotedData"
        Me.GvAllotedData.ReadOnly = True
        Me.GvAllotedData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GvAllotedData.Size = New System.Drawing.Size(375, 238)
        Me.GvAllotedData.TabIndex = 2
        '
        'cmbSearchUser
        '
        Me.cmbSearchUser.FormattingEnabled = True
        Me.cmbSearchUser.Location = New System.Drawing.Point(52, 20)
        Me.cmbSearchUser.Name = "cmbSearchUser"
        Me.cmbSearchUser.Size = New System.Drawing.Size(118, 21)
        Me.cmbSearchUser.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "User"
        '
        'btnAllot
        '
        Me.btnAllot.Location = New System.Drawing.Point(95, 102)
        Me.btnAllot.Name = "btnAllot"
        Me.btnAllot.Size = New System.Drawing.Size(87, 29)
        Me.btnAllot.TabIndex = 3
        Me.btnAllot.Text = "RE-ALLOT"
        Me.btnAllot.UseVisualStyleBackColor = True
        '
        'SecondAllotment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 471)
        Me.Controls.Add(Me.btnAllot)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtCustName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtEnqNo)
        Me.Controls.Add(Me.cmbUser)
        Me.Controls.Add(Me.Label1)
        Me.Name = "SecondAllotment"
        Me.Text = "SecondAllotment"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GvAllotedData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtEnqNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GvAllotedData As System.Windows.Forms.DataGridView
    Friend WithEvents cmbSearchUser As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnAllot As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
End Class
