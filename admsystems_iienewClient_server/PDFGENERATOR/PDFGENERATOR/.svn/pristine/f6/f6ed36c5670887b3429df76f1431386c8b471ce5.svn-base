
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

''' <summary>
''' Summary description for NewRupeeConvert
''' </summary>
Public Class RupeeConvert
    '
    ' TODO: Add constructor logic here
    '
    Public Sub New()
    End Sub


    '  #region AmountInWords
    Public Shared Function changeToWords(ByVal Num As String, ByVal tr As [Boolean]) As String
        Dim returnValue As String
        'I have created this function for converting amount in indian rupees (INR).
        'You can manipulate as you wish like decimal setting, Doller (any currency) Prefix.


        Dim strNum As String
        Dim strNumDec As String
        Dim StrWord As String
        strNum = Num.ToString()


        If strNum.IndexOf(".") + 1 <> 0 Then
            strNumDec = strNum.Substring(strNum.IndexOf(".") + 2 - 1)


            If strNumDec.Length = 1 Then
                strNumDec = strNumDec & "0"
            End If
            If strNumDec.Length > 2 Then
                strNumDec = strNumDec.Substring(0, 2)
            End If


            strNum = strNum.Substring(0, strNum.IndexOf(".") + 0)
            StrWord = (If((Double.Parse(strNum) = 1), "", "")) & NumToWord(CDec(Double.Parse(strNum))) & (If((Double.Parse(strNumDec) > 0), (" Rupees and " & cWord3(CDec(Double.Parse(strNumDec)))), ""))
        Else
            StrWord = (If((Double.Parse(strNum) = 1), "", "")) & NumToWord(CDec(Double.Parse(strNum)))
        End If
        If Not StrWord.Contains("Rupees") Then
            returnValue = StrWord & " Rupees Only"
        Else
            returnValue = StrWord & " Paise Only"
        End If
        Return returnValue
    End Function
    Public Shared Function NumToWord(ByVal Num As Decimal) As String
        Dim returnValue As String


        'I divided this function in two part.
        '1. Three or less digit number.
        '2. more than three digit number.
        Dim strNum As String
        Dim StrWord As String
        strNum = Num.ToString()


        If strNum.Length <= 3 Then
            StrWord = cWord3(CDec(Double.Parse(strNum)))
        Else
            StrWord = cWordG3(CDec(Double.Parse(strNum.Substring(0, strNum.Length - 3)))) & " " & cWord3(CDec(Double.Parse(strNum.Substring(strNum.Length - 2 - 1))))
        End If
        returnValue = StrWord
        Return returnValue
    End Function
    Public Shared Function cWordG3(ByVal Num As Decimal) As String
        Dim returnValue As String
        '2. more than three digit number.
        Dim strNum As String = ""
        Dim StrWord As String = ""
        Dim readNum As String = ""
        strNum = Num.ToString()
        If strNum.Length Mod 2 <> 0 Then
            readNum = System.Convert.ToString(Double.Parse(strNum.Substring(0, 1)))
            If readNum <> "0" Then
                StrWord = retWord(Decimal.Parse(readNum))
                readNum = System.Convert.ToString(Double.Parse("1" & strReplicate("0", strNum.Length - 1) & "000"))
                StrWord = StrWord & " " & retWord(Decimal.Parse(readNum))
            End If
            strNum = strNum.Substring(1)
        End If
        While Not System.Convert.ToBoolean(strNum.Length = 0)
            readNum = System.Convert.ToString(Double.Parse(strNum.Substring(0, 2)))
            If readNum <> "0" Then
                StrWord = StrWord & " " & cWord3(Decimal.Parse(readNum))
                readNum = System.Convert.ToString(Double.Parse("1" & strReplicate("0", strNum.Length - 2) & "000"))
                StrWord = StrWord & " " & retWord(Decimal.Parse(readNum))
            End If
            strNum = strNum.Substring(2)
        End While
        returnValue = StrWord
        Return returnValue
    End Function
    Public Shared Function cWord3(ByVal Num As Decimal) As String
        Dim returnValue As String
        '1. Three or less digit number.
        Dim strNum As String = ""
        Dim StrWord As String = ""
        Dim readNum As String = ""
        If Num < 0 Then
            Num = Num * -1
        End If
        strNum = Num.ToString()


        If strNum.Length = 3 Then
            readNum = System.Convert.ToString(Double.Parse(strNum.Substring(0, 1)))
            StrWord = retWord(Decimal.Parse(readNum)) & " Hundred"
            strNum = strNum.Substring(1, strNum.Length - 1)
        End If


        If strNum.Length <= 2 Then
            If Double.Parse(strNum) >= 0 AndAlso Double.Parse(strNum) <= 20 Then
                StrWord = StrWord & " " & retWord(CDec(Double.Parse(strNum)))
            Else
                StrWord = StrWord & " " & retWord(CDec(System.Convert.ToDouble(strNum.Substring(0, 1) & "0"))) & " " & retWord(CDec(Double.Parse(strNum.Substring(1, 1))))
            End If
        End If


        strNum = Num.ToString()
        returnValue = StrWord
        Return returnValue
    End Function
    Public Shared Function retWord(ByVal Num As Decimal) As String
        Dim returnValue As String
        'This two dimensional array store the primary word convertion of number.
        returnValue = ""
        Dim ArrWordList As Object(,) = New Object(,) {{0, ""}, {1, "One"}, {2, "Two"}, {3, "Three"}, {4, "Four"}, {5, "Five"}, _
         {6, "Six"}, {7, "Seven"}, {8, "Eight"}, {9, "Nine"}, {10, "Ten"}, {11, "Eleven"}, _
         {12, "Twelve"}, {13, "Thirteen"}, {14, "Fourteen"}, {15, "Fifteen"}, {16, "Sixteen"}, {17, "Seventeen"}, _
         {18, "Eighteen"}, {19, "Nineteen"}, {20, "Twenty"}, {30, "Thirty"}, {40, "Forty"}, {50, "Fifty"}, _
         {60, "Sixty"}, {70, "Seventy"}, {80, "Eighty"}, {90, "Ninety"}, {100, "Hundred"}, {1000, "Thousand"}, _
         {100000, "Lakh"}, {10000000, "Crore"}, {100000000, "Ten Crore"}, {1000000000, "ONE billion"}}


        Dim i As Integer
        For i = 0 To (ArrWordList.Length - 1)
            If Num = System.Convert.ToDecimal(ArrWordList(i, 0)) Then
                returnValue = DirectCast(ArrWordList(i, 1), String)
                Exit For
            End If
        Next
        Return returnValue
    End Function
    Public Shared Function strReplicate(ByVal str As String, ByVal intD As Integer) As String
        Dim returnValue As String
        'This fucntion padded "0" after the number to evaluate hundred, thousand and on....
        'using this function you can replicate any Charactor with given string.
        Dim i As Integer
        returnValue = ""
        For i = 1 To intD
            returnValue = returnValue & str
        Next
        Return returnValue
    End Function

End Class
