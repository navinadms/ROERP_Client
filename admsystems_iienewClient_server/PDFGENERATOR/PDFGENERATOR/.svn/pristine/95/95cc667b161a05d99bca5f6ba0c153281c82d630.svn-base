Imports System.Diagnostics
Imports System.Windows.Forms
Class OneInstanceWindow
    Inherits Form
    'your Fields, Methods, Properties, …
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function SetForegroundWindow(ByVal mwh As System.IntPtr) As Boolean
    End Function
    Private Shared Sub Main()
        Dim [me] = Process.GetCurrentProcess()
        Dim arrProcesses = Process.GetProcessesByName([me].ProcessName)
        For i As Integer = 0 To arrProcesses.Length - 1
            If arrProcesses(i).Id <> [me].Id Then
                SetForegroundWindow(arrProcesses(i).MainWindowHandle)
                Return
            End If
        Next
        Application.Run(New OneInstanceWindow())
    End Sub
End Class