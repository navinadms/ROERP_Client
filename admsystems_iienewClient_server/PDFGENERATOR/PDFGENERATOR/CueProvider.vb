Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System


Namespace RavSoft
    ''' <summary>
    ''' Provides textual cues to a text box.
    ''' </summary>
    ''' <summary>
    ''' An object that provides basic logging capabilities.
    ''' Copyright (c) 2008 Ravi Bhavnani, ravib@ravib.com
    '''
    ''' This software may be freely used in any product or work, provided this
    ''' copyright notice is maintained.  To help ensure a single point of release,
    ''' please email and bug reports, flames and suggestions to ravib@ravib.com.
    ''' </summary>
    Public NotInheritable Class CueProvider
        Private Sub New()
        End Sub
        Private Const EM_SETCUEBANNER As Integer = &H1501

        <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
        Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As Int32
        End Function

        ''' <summary>
        ''' Sets a text box's cue text.
        ''' </summary>
        ''' <param name="textBox">The text box.</param>
        ''' <param name="cue">The cue text.</param>
        Public Shared Sub SetCue(ByVal textBox As TextBox, ByVal cue As String)
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, cue)
        End Sub

        ''' <summary>
        ''' Clears a text box's cue text.
        ''' </summary>
        ''' <param name="textBox">The text box</param>
        Public Shared Sub ClearCue(ByVal textBox As TextBox)
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, String.Empty)
        End Sub
    End Class
End Namespace