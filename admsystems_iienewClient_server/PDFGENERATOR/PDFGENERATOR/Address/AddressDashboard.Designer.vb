<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddressDashboard
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
        Me.btnAddressForm = New System.Windows.Forms.Button()
        Me.btnCourier = New System.Windows.Forms.Button()
        Me.btnAddCategory = New System.Windows.Forms.Button()
        Me.btnAddSubCategory = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnAddressForm
        '
        Me.btnAddressForm.Location = New System.Drawing.Point(85, 70)
        Me.btnAddressForm.Name = "btnAddressForm"
        Me.btnAddressForm.Size = New System.Drawing.Size(230, 191)
        Me.btnAddressForm.TabIndex = 0
        Me.btnAddressForm.Text = "Address Form"
        Me.btnAddressForm.UseVisualStyleBackColor = True
        '
        'btnCourier
        '
        Me.btnCourier.Location = New System.Drawing.Point(358, 291)
        Me.btnCourier.Name = "btnCourier"
        Me.btnCourier.Size = New System.Drawing.Size(230, 191)
        Me.btnCourier.TabIndex = 1
        Me.btnCourier.Text = "Courier"
        Me.btnCourier.UseVisualStyleBackColor = True
        '
        'btnAddCategory
        '
        Me.btnAddCategory.Location = New System.Drawing.Point(358, 70)
        Me.btnAddCategory.Name = "btnAddCategory"
        Me.btnAddCategory.Size = New System.Drawing.Size(230, 191)
        Me.btnAddCategory.TabIndex = 2
        Me.btnAddCategory.Text = "Address Category"
        Me.btnAddCategory.UseVisualStyleBackColor = True
        '
        'btnAddSubCategory
        '
        Me.btnAddSubCategory.Location = New System.Drawing.Point(85, 291)
        Me.btnAddSubCategory.Name = "btnAddSubCategory"
        Me.btnAddSubCategory.Size = New System.Drawing.Size(230, 191)
        Me.btnAddSubCategory.TabIndex = 3
        Me.btnAddSubCategory.Text = "Address SubCategory"
        Me.btnAddSubCategory.UseVisualStyleBackColor = True
        '
        'AddressDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.btnAddSubCategory)
        Me.Controls.Add(Me.btnAddCategory)
        Me.Controls.Add(Me.btnCourier)
        Me.Controls.Add(Me.btnAddressForm)
        Me.Name = "AddressDashboard"
        Me.Text = "AddressDashboard"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAddressForm As System.Windows.Forms.Button
    Friend WithEvents btnCourier As System.Windows.Forms.Button
    Friend WithEvents btnAddCategory As System.Windows.Forms.Button
    Friend WithEvents btnAddSubCategory As System.Windows.Forms.Button
End Class
