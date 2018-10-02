Imports System.Collections.Generic
Imports System.Text
Imports EnvDTE
Imports EnvDTE80
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace ConsoleApplication2
    Class Program
        Private Shared Sub Main(ByVal args As String())
            Dim dte As EnvDTE80.DTE2
            Dim obj As Object = Nothing
            Dim t As System.Type = Nothing

            ' Get the ProgID for DTE 8.0.
            t = System.Type.GetTypeFromProgID("VisualStudio.DTE.10.0", True)
            ' Create a new instance of the IDE.
            obj = System.Activator.CreateInstance(t, True)
            ' Cast the instance to DTE2 and assign to variable dte.
            dte = DirectCast(obj, EnvDTE80.DTE2)

            ' Register the IOleMessageFilter to handle any threading 
            ' errors.
            MessageFilter.Register()
            ' Display the Visual Studio IDE.
            dte.MainWindow.Activate()

            ' =====================================
            ' ==Insert your automation code here.==
            ' =====================================
            ' For example, get a reference to the solution2 object
            ' and do what you like with it.
            Dim soln As Solution2 = DirectCast(dte.Solution, Solution2)
            System.Windows.Forms.MessageBox.Show("Solution count: " + soln.Count)
            ' =====================================

            ' All done, so shut down the IDE...
            dte.Quit()
            ' and turn off the IOleMessageFilter.
            MessageFilter.Revoke()

        End Sub
    End Class

    Public Class MessageFilter
        Implements IOleMessageFilter
        '
        ' Class containing the IOleMessageFilter
        ' thread error-handling functions.

        ' Start the filter.
        Public Shared Sub Register()
            Dim newFilter As IOleMessageFilter = New MessageFilter()
            Dim oldFilter As IOleMessageFilter = Nothing
            CoRegisterMessageFilter(newFilter, oldFilter)
        End Sub

        ' Done with the filter, close it.
        Public Shared Sub Revoke()
            Dim oldFilter As IOleMessageFilter = Nothing
            CoRegisterMessageFilter(Nothing, oldFilter)
        End Sub

        '
        ' IOleMessageFilter functions.
        ' Handle incoming thread requests.
        Private Function IOleMessageFilter_HandleInComingCall(ByVal dwCallType As Integer, ByVal hTaskCaller As System.IntPtr, ByVal dwTickCount As Integer, ByVal lpInterfaceInfo As System.IntPtr) As Integer Implements IOleMessageFilter.HandleInComingCall
            'Return the flag SERVERCALL_ISHANDLED.
            Return 0
        End Function

        ' Thread call was rejected, so try again.
        Private Function IOleMessageFilter_RetryRejectedCall(ByVal hTaskCallee As System.IntPtr, ByVal dwTickCount As Integer, ByVal dwRejectType As Integer) As Integer Implements IOleMessageFilter.RetryRejectedCall
            If dwRejectType = 2 Then
                ' flag = SERVERCALL_RETRYLATER.
                ' Retry the thread call immediately if return >=0 & 
                ' <100.
                Return 99
            End If
            ' Too busy; cancel call.
            Return -1
        End Function

        Private Function IOleMessageFilter_MessagePending(ByVal hTaskCallee As System.IntPtr, ByVal dwTickCount As Integer, ByVal dwPendingType As Integer) As Integer Implements IOleMessageFilter.MessagePending
            'Return the flag PENDINGMSG_WAITDEFPROCESS.
            Return 2
        End Function

        ' Implement the IOleMessageFilter interface.
        <DllImport("Ole32.dll")> _
        Private Shared Function CoRegisterMessageFilter(ByVal newFilter As IOleMessageFilter, ByRef oldFilter As IOleMessageFilter) As Integer
        End Function
    End Class

    <ComImport(), Guid("00000016-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
    Interface IOleMessageFilter
        <PreserveSig()> _
        Function HandleInComingCall(ByVal dwCallType As Integer, ByVal hTaskCaller As IntPtr, ByVal dwTickCount As Integer, ByVal lpInterfaceInfo As IntPtr) As Integer

        <PreserveSig()> _
        Function RetryRejectedCall(ByVal hTaskCallee As IntPtr, ByVal dwTickCount As Integer, ByVal dwRejectType As Integer) As Integer

        <PreserveSig()> _
        Function MessagePending(ByVal hTaskCallee As IntPtr, ByVal dwTickCount As Integer, ByVal dwPendingType As Integer) As Integer
    End Interface
End Namespace

