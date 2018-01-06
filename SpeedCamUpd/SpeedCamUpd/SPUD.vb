Imports MbUnit.Framework

Public Class SPUD
  ' Version 2007.7.05.1
  ' (c) Amesdp 2007
  ' amesdp@gmail.com
  ' Free to use for non-commercial purposes

    Option Compare Text

  Const epsilon = 0.00000011920929  'conversion factor to lat/long

  ' Record types
  Const rt_new = 0
  Const rt_deleted = 1
  Const rt_edited = 2

  ' Camera types
  Const ct_fixedspeedcam = 0
  Const ct_mobilespeedcam = 1
  Const ct_trafficlightspeedcam = 2
  Const ct_sectionspeedcam = 3
  Const ct_redlightcam = 4

  ' Camera direction
  Const cd_unidirectional = 0
  Const cd_bidirectional = 1
  Const cd_alldirections = 2

  Const camfilename = "SpeedcamUpdates.spud"
  Const newcamfilename = "SpeedcamUpdatesNew.spud"
  Const oldcamfilename = "SpeedcamUpdatesOld.spud"

  Const WarnColor = 6     'Yellow
  Const SupersededColor = 37      'Light blue

  Public Sub Workbook_Open(ByVal launchdir As String)

    'if first time load, do startup processing
    If launchdir = "" Then
      ' Get program path
      Dim a = Application.ExecutablePath
      Dim i
      For i = Len(a) To 1 Step -1
        If Mid$(a, i, 1) = "\" Then Exit For
      Next i
      launchdir = Left$(a, i)
      ChDir(launchdir$)
      ChDrive(Left$(launchdir$, 2))
      'initialize ShowEdit from Options sheet
      Dim maxcol = 11         'last is Description column
      Dim ShowEdit = (Range("OptionShowEdit") = Range("YesNo").Cells(1))
      r = Start_row
      notvalid = 0
      Do Until Cells(r, 1) = ""
        If Cells(r, 9) <> 0 Then notvalid = notvalid + 1
        r = r + 1
      Loop
      totalcams = r - Start_row
      totalvalidcams = totalcams - notvalid
      Worksheets(1).RecCount.Caption = "Cams:" + Str$(totalcams) + Chr$(13) + "Valid Cams:" + Str$(totalvalidcams)
    End If

    'RefreshInProgress = True            'Prevent status update to Edited
    'refresh_spudfile
    'RefreshInProgress = False

  End Sub
  Public Sub import_camfile()

    Dim spCam1 As spCam
    Dim r As Integer, c As Integer, rstart As Integer
    Dim L As Long
    Dim notvalid As Integer, warns As Integer

    ' Assume that this routine has been invoked from the List spreadsheet

    'Find first blank row, because we're going to add to existing list of cams
    r = Start_row
    Do Until Cells(r, 1) = ""
      r = r + 1
    Loop
    rstart = r

    'get file name to open
    cfn = Application.GetOpenFilename("MioMap (*.spud), *.spud, GPX (*.gpx), (*.gpx), Google Earth (*.kml), *.kml", 1, "Import Speedcams")

    If cfn = False Then Exit Sub 'user clicked Cancel

    If Right$(cfn, 3) = "gpx" Then      'GPX import selected
      cfname$ = cfn
      Import_GPX(cfname$, rstart)
      Exit Sub
    End If

    If Right$(cfn, 3) = "kml" Then      'Google Earth import selected
      cfname$ = cfn
      Import_GoogleEarth(cfname$, rstart)
      Exit Sub
    End If

    ' Import MioMap spud file

    Open cfn For Binary As 1

    rowdeleted = Rows.Count
    recordsadded = 0
    addednotvalid = 0
    warncount = 0

    RefreshInProgress = True        'Prevent content-change event processing

    ' Read the file into the spreadsheet
    Do Until EOF(1)         'not sure why, but having this here makes a critical difference
        Get #1, , spCam1
      If EOF(1) Then Exit Do
      recordsadded = recordsadded + 1
      FillRow(r, spCam1, notvalid, warns)
      warncount = warncount + warns
      addednotvalid = addednotvalid + notvalid
      r = r + 1
    Loop

    Close(1)

    ' Clear any non-blank rows after last record found in file
    '    Do
    '        If Cells(r, 1) = "" Then Exit Do    'blank row, our job is done
    '        For c = 1 To maxcol  'clear non-blank rows
    '            Cells(r, c).Interior.ColorIndex = 0     'no color
    '            Cells(r, c) = ""
    '        Next c
    '        r = r + 1
    '    Loop Until r > Rows.Count
    Rows(r & ":" & Rows.Count).Select()
    Selection.ClearContents()
    Selection.Interior.ColorIndex = 0
    Cells(rstart, 1).Select()

    ' Fix up cam no.s if some rows were deleted
    If rowdeleted < Rows.Count Then
      r = rowdeleted
      Do Until Cells(r, 1) = "" Or r >= Rows.Count
        Cells(r, 1) = Str(r - Start_row + 1)
        r = r + 1
      Loop
    End If

    Range("OptionUnits") = Range("Units").Cells(2)      'Units in spud file always mph

    RefreshInProgress = False

    totalcams = totalcams + recordsadded
    totalvalidcams = totalvalidcams + recordsadded - addednotvalid

    a$ = "Imported" & Str$(recordsadded) & " cams"
    If warncount = 0 Then
      MsgBox(a$, vbInformation, "Import Speedcams")
    Else
      MsgBox(a$ + Chr$(13) + Str$(warncount) + " warnings", vbExclamation, "Import Speedcams")
    End If

  End Sub
  Sub FillRow(ByVal r As Integer, ByVal spCam1 As spCam, ByVal notvalid As Integer, ByVal warncount As Integer)

    ' Can be called by either Refresh or Import routines
    ' spCam1 - cam data
    ' r - row to fill
    ' notvalid - count of records deleted or superseded by this entry
    ' warncount - number of warnings generated

    ' Assume that List sheet is the current sheet

    For c = 1 To maxcol
      Cells(r, c).Interior.ColorIndex = 0     'set normal cell background color
      Cells(r, c) = ""
    Next c

    Cells(r, 1) = Str$(r - Start_row + 1)       'set Cam no. (reference only, not used)

    notvalid = 0
    warncount = 0

    c = 2
    Select Case spCam1.spFlag
      Case rt_new
        'use named range to allow modification/translation of strings
        Cells(r, c) = Range("Statusvalues").Cells(1) 'New
      Case rt_deleted
        Cells(r, c) = Range("Statusvalues").Cells(2) 'Deleted
        For c1 = 1 To maxcol
          Cells(r, c1).Interior.ColorIndex = SupersededColor
        Next c1
        notvalid = notvalid + 1
      Case rt_edited
        Cells(r, c) = Range("Statusvalues").Cells(3) 'Edited"
      Case Else
        Cells(r, c) = spCam1.spFlag
        Cells(r, c).Interior.ColorIndex = WarnColor     'yellow warning
        warncount = warncount + 1
        Cells(r, 10) = "Invalid Flag field"
    End Select

    ' If people want to see the deleted value, ok
    '    If spCam1.spFlag = rt_deleted And Range(Names("CheckShowDeleted")) = Range(Names("YesNo")).Cells(2) Then
    '        For c = 3 To 8
    '            Cells(r, c) = ""
    '        Next c
    '        Return
    '    End If

    c = 3
    Cells(r, c) = spCam1.spLong * epsilon
    If spCam1.spLong * epsilon > 180 Or spCam1.spLong * epsilon < -180 Then
      Cells(r, c).Interior.ColorIndex = WarnColor     'yellow warning
      Cells(r, 10) = "Invalid Longitude"
      warncount = warncount + 1
    End If

    c = 4
    Cells(r, c) = spCam1.spLat * epsilon
    If spCam1.spLat * epsilon > 180 Or spCam1.spLat * epsilon < -180 Then
      Cells(r, c).Interior.ColorIndex = WarnColor     'yellow warning
      Cells(r, 10) = "Invalid Latitude"
      warncount = warncount + 1
    End If

    'Byte value 0-255 can't be wrong
    Cells(r, 5) = spCam1.spLimit

    c = 6
    Select Case spCam1.spType
      Case ct_fixedspeedcam
        'use named range to allow modification/translation of strings
        camtype$ = Range("TypeValues").Cells(1)  'Fixed Speed Cam
      Case ct_mobilespeedcam
        camtype$ = Range("TypeValues").Cells(2)  'Mobile Speed Cam
      Case ct_trafficlightspeedcam
        camtype$ = Range("TypeValues").Cells(3)  'Traffic Light Speed Cam
      Case ct_sectionspeedcam
        camtype$ = Range("TypeValues").Cells(4)  'Section Control Speed Cam
      Case ct_redlightcam
        camtype$ = Range("TypeValues").Cells(5)  'Red Light Cam
      Case Else
        camtype$ = Str$(spCam1.spType)
        Cells(r, c).Interior.ColorIndex = WarnColor     'yellow warning
        Cells(r, 10) = "Invalid cam type"
        warncount = warncount + 1
    End Select
    Cells(r, c) = camtype$

    'hi bit of Angle is in the low bit of the Direction field
    Cells(r, 7) = spCam1.spAngle + (spCam1.spDirection And 1) * &H100

    c = 8
    Select Case spCam1.spDirection \ 2
      Case cd_unidirectional
        'use named range to allow modification/translation of strings
        camdir$ = Range("DirectionValues").Cells(1)  'Unidirectional
      Case cd_bidirectional
        camdir$ = Range("DirectionValues").Cells(2)  'Bi-directional
      Case cd_alldirections
        camdir$ = Range("DirectionValues").Cells(3)  'All directions
      Case Else
        camdir$ = Str$(spCam1.spDirection)
        Cells(r, c).Interior.ColorIndex = WarnColor     'yellow warning
        Cells(r, 10) = "Invalid Direction"
        warncount = warncount + 1
    End Select
    Cells(r, c) = camdir$

    ' Check for superseded records (if Edited or Deleted)
    If spCam1.spFlag = rt_deleted Or spCam1.spFlag = rt_edited Or totalcams > 0 Then 'if Deleted or Edited or not first import
      For r1 = r - 1 To Start_row Step -1     'Look backwards from current row for matching coordinates
        'if match found
        If Cells(r1, 3) = Cells(r, 3) And Cells(r1, 4) = Cells(r, 4) Then
          'if showing deleted records
          If Range("CheckShowDeleted") = Range("YesNo").Cells(1) Then  'Yes
            'invalidate record if it was previously valid
            If Cells(r1, 9) = "" Then notvalid = notvalid + 1
            Cells(r1, 9) = Cells(r, 1)       'mark that row as superseded and insert reference to superseding record
            For c = 1 To maxcol
              Cells(r1, c).Interior.ColorIndex = SupersededColor
            Next c
          Else    'removing deleted records
            Rows(r1).Delete()
            If r1 < rowdeleted Then rowdeleted = r1
            r = r - 1
            notvalid = notvalid + 1
          End If
          Exit For            'no need to go back further
        End If
      Next r1
      If spCam1.spFlag = rt_deleted Then   'if Deleted
        ' Erase current deleted row if not showing deleted rows
        ' We need to go through the process of creating the row to ensure that the coordinates come out exactly the same
        If Range("CheckShowDeleted") = Range("YesNo").Cells(2) Then  'No
          For c = 1 To maxcol
            Cells(r, c) = ""
            Cells(r, c).Interior.ColorIndex = 0
          Next c
          r = r - 1
        Else
          Cells(r, 9) = "Deleted"
        End If
      End If
    End If

  End Sub

  Public Sub export_camfile(ByVal checkflag As Boolean)

    'Checkflag - True if only checking data consistency, not writing

    ' Export the speedcam list to a file

    Dim spCam1 As spCam
    Dim doublecalc As Double, savelong As Double, savelat As Double
    Dim savedeleted As Boolean
    Dim fd As CommonDialog

    If checkflag Then

      minsep! = Range("Minsep")

      RefreshInProgress = True        'Prevent content-change event processing

    Else

      'get file name to save
      cfn = Application.GetSaveAsFilename(camfilename, "MioMap (*.spud), *.spud, Google Earth (*.kml), (*.kml)", 1, "Export")

      If cfn = False Then Exit Sub 'user clicked Cancel

      If Right$(cfn, 3) = "kml" Then      'Google Earth export selected
        cfname$ = cfn
        Export_GoogleEarth(cfname$)
        Exit Sub
      End If

    End If

    ' Export to MioMap spud file

    If Not checkflag Then

      If Range("OptionUnits") <> Range("Units").Cells(2) Then 'if not required Mph
        RefreshInProgress = True        'Prevent pop-ups
        Range("OptionUnits") = Range("Units").Cells(2)
        DoEvents()
        RefreshInProgress = False
      End If

      ' Get rid of any existing file (Open Binary won't delete existing file)
      On Error Resume Next
      Kill(cfn)
      On Error GoTo 0

        Open cfn For Binary As 1
      '    Open newcamfilename For Binary As 1

    End If

    warningcount = 0

    savedeleted = Range("OptionSaveDeleted") = Range("YesNo").Cells(1)

    ' Assume that this routine has been invoked from the List spreadsheet

    r = Start_row
    totalrecords = 0

    'Write the spreadsheet contents to the speedcamupdates file
    Do
        GoSub Getrow
      If spCam1.spFlag = &HFF Then Exit Do
      If checkflag Then
        totalrecords = totalrecords + 1
      Else
        'if not deleted and not supersded, or allowed to write deleted records
        If (spCam1.spFlag <> rt_deleted And Cells(r, 9) = "") Or savedeleted Then
                Put #1, , spCam1
          totalrecords = totalrecords + 1
        End If
      End If
      r = r + 1
    Loop

    Close(1)

    If checkflag Then
      a$ = "Checked" + Str$(totalrecords) + " records"
      RefreshInProgress = False
      t$ = "Data Check"
    Else
      a$ = "Exported" + Str$(totalrecords) + " cams to " & cfn
      t$ = "Export"
    End If
    If warningcount > 0 Then
      a$ = a$ + Chr$(13) + Str$(warningcount) + " warnings"
      i = vbExclamation
    Else
      i = vbInformation
    End If
    MsgBox(a$, i, t$)

    '    If Errorflag Then
    '        MsgBox "Created " + CurDir$ + newcamfilename
    '        MsgBox "Errors detected"
    '    Else
    '        On Error Resume Next
    '          oldcamfilename$ = camfilename
    '          If Right$(oldcamfilename$, 5) = ".spud" Then oldcamfilename$ = Left$(oldcamfilename$, Len(oldcamfilename$) - 5)
    '          oldcamfilename$ = oldcamfilename$ + "Old.spud"
    '        Kill oldcamfilename
    '        Name camfilename As oldcamfilename
    '        Name newcamfilename As camfilename
    '        On Error GoTo 0
    '        MsgBox "Updated " + CurDir$ + camfilename
    '    End If

    Exit Sub

Getrow:

    If Cells(r, 1) = "" Or Cells(r, 1) = " " Then
      spCam1.spFlag = &HFF                'end of data
      Return
    End If

    c = 2                                   'record Status
    spStatus$ = Cells(r, c)
    'use named range to allow modification/translation of strings
    Select Case spStatus$
      Case Range("Statusvalues").Cells(1)  'New
        spCam1.spFlag = rt_new
      Case Range("Statusvalues").Cells(2)  'Deleted
        spCam1.spFlag = rt_deleted
      Case Range("Statusvalues").Cells(3)  'Edited
        spCam1.spFlag = rt_edited
      Case Else
        Cells(r, 10) = "Invalid Flag field"
        Cells(r, c).Interior.ColorIndex = WarnColor     'yellow warning
        warningcount = warningcount + 1
        spCam1.spFlag = Cells(r, c)         'Try to preserve the value
    End Select

    c = 3                   'Longitude
    doublecalc = Cells(r, c)    'make sure calc is done at full accuracy
    If doublecalc < -180 Or doublecalc > 180 Then
      doublecalc = 0
      Cells(r, c).Interior.ColorIndex = WarnColor     'yellow warning
      warningcount = warningcount + 1
      Cells(r, 10) = "Invalid Longitude field"
    End If
    If checkflag Then savelong = doublecalc
    doublecalc = doublecalc / epsilon
    spCam1.spLong = doublecalc      'auto-converted to 4-byte integer

    c = 4                   'Latitude
    doublecalc = Cells(r, c)    'make sure calc is done at full accuracy
    If doublecalc < -180 Or doublecalc > 180 Then
      doublecalc = 0
      Cells(r, 10) = "Invalid Latitude field"
      Cells(r, c).Interior.ColorIndex = WarnColor     'yellow warning
      warningcount = warningcount + 1
    End If
    If checkflag Then savelat = doublecalc
    doublecalc = doublecalc / epsilon
    spCam1.spLat = doublecalc      'auto-converted to 4-byte integer

    If checkflag Then GoSub check_close

    c = 5                   'Speed Limit
    i = Cells(r, c)
    If i < 0 Or i > 255 Then
      i = i And &HFF
      Cells(r, c).Interior.ColorIndex = WarnColor     'yellow warning
      Cells(r, 10) = "Invalid Speed Limit"
      warningcount = warningcount + 1
    End If
    spCam1.spLimit = i

    c = 6                   'Camtype
    camtype = Cells(r, c)
    'use named range to allow modification/translation of strings
    Select Case camtype
      Case Range("TypeValues").Cells(1)    'Fixed Speed Cam
        spCam1.spType = ct_fixedspeedcam
      Case Range("TypeValues").Cells(2)    'Mobile Speed Cam
        spCam1.spType = ct_mobilespeedcam
      Case Range("TypeValues").Cells(3)    'Traffic Light Speed Cam
        spCam1.spType = ct_trafficlightspeedcam
      Case Range("TypeValues").Cells(4)    'Section Control Speed Cam
        spCam1.spType = ct_sectionspeedcam
      Case Range("TypeValues").Cells(5)    'Red Light Cam
        spCam1.spType = ct_redlightcam
      Case Else
        spCam1.spType = Cells(r, c)     'try to preserve the value
        Cells(r, 10) = "Invalid cam type"
        Cells(r, c).Interior.ColorIndex = WarnColor     'yellow warning
        warningcount = warningcount + 1
    End Select

    c = 7           'Angle
    iAngle = Cells(r, c)
    If iAngle < 0 Or iAngle > 359 Then
      iAngle = Abs(iAngle) Mod 360
      Cells(r, 10) = "Invalid Angle"
      Cells(r, c).Interior.ColorIndex = WarnColor     'yellow warning
      warningcount = warningcount + 1
    End If
    spCam1.spAngle = iAngle And &HFF

    c = 8           'Direction
    camdir$ = Cells(r, c)
    Select Case camdir$
      'use named range to allow modification/translation of strings
      Case Range("DirectionValues").Cells(1)   'Unidirectional
        Direction = cd_unidirectional
      Case Range("DirectionValues").Cells(2)   'Bi-directional
        Direction = cd_bidirectional
      Case Range("DirectionValues").Cells(3)   'All directions
        Direction = cd_alldirections
      Case Else
        Direction = Cells(r, c)         'try to preserve the value
        Cells(r, 10) = "Invalid Direction"
        Cells(r, c).Interior.ColorIndex = WarnColor     'yellow warning
        warningcount = warningcount + 1
    End Select
    spCam1.spDirection = (iAngle And &H100) \ &H100 + Direction * 2  'shift bits left by 1

    Return

check_close:

    'check how close cam is to previous cams, look for duplicates and cams placed too close together

    'Cells(r, 10).Interior.ColorIndex = Cells(r, 9).Interior.ColorIndex
    'Cells(r, 11).Interior.ColorIndex = Cells(r, 9).Interior.ColorIndex

    For r1 = r - 1 To Start_row Step -1

      c = 3                   'Longitude
      doublecalc = Cells(r1, c)    'make sure calc is done at full accuracy
      If doublecalc < -180 Or doublecalc > 180 Then doublecalc = 0
      If doublecalc - savelong < 0.1 Then

        'calculate approximate longitudinal separation distance in meters (this is a simplified approximation, but close enough for our purposes)
        kmsep! = 4.0E+7! / 360 * Abs(doublecalc - savelong) * savelat / 90

        c = 4                   'Latitude
        doublecalc = Cells(r1, c)    'make sure calc is done at full accuracy
        If doublecalc < -180 Or doublecalc > 180 Then doublecalc = 0
        If doublecalc - savelat < 0.1 Then
          If kmsep! = 0 And doublecalc = savelat And Cells(r1, 9) = "" Then
            ' coordinate duplication found, mark earlier record superseded
            Cells(r1, 9) = Str$(r - Start_row + 1)
            For c1 = 1 To 9
              Cells(r1, c1).Interior.ColorIndex = SupersededColor
            Next c1
            totalvalidcams = totalvalidcams - 1
            warningcount = warningcount + 1
          Else
            'calculate approximate separation distance in meters
            kmsep! = Sqr(kmsep! ^ 2 + (4.0E+7! / 360 * (doublecalc - savelat)) ^ 2)
            If kmsep! < minsep! Then
              Cells(r, 10) = "Too close to cam" + Str$(r1 - Start_row + 1) + "," + Str$(Int(kmsep!)) + " meters"
              Cells(r, 10).Interior.ColorIndex = WarnColor
              warningcount = warningcount + 1
              'found one camera close by, but keep looking for superseded
            End If
          End If
        End If
      End If

    Next r1

    Return

  End Sub
  Public Sub ConvertUnits(ByVal u As String)

    ' Need to refer specifically to Sheet 1 here because this is invoked from the Options worksheet
    ' Caution: this refers to Worksheet 1 rather than Worksheet "List" to allow name changes
    r = Start_row
    Do
      If Worksheets(1).Cells(r, 1) = "" Then Exit Do
      u1 = Worksheets(1).Cells(r, 5)
      If u = "Kph" Then
        Worksheets(1).Cells(r, 5) = Int(u1 * 1.609 + 0.5)
      Else 'If u = "Mph" Then
        Worksheets(1).Cells(r, 5) = Int(u1 / 1.609 + 0.5)
      End If
      r = r + 1
    Loop

  End Sub
  Public Sub UpdateStatus(ByVal Target As Range)

    ' This routine called to validate changes when the user edits a cell in the speedcams list

    r = Target.Row
    'Ok to edit Deleted records, just change them to Edited
    If Cells(r, 2) = Range("Statusvalues").Cells(2) And Target.Column = 2 Then 'If record marked Deleted
      For c = 1 To 9
        Cells(r, c).Interior.ColorIndex = SupersededColor
      Next c
      Cells(r, 9) = "Deleted"

    ElseIf Cells(r, 2) <> Range("Statusvalues").Cells(2) And Target.Column = 2 Then 'If record marked New or Edited
      If Cells(r, 9) = "Deleted" Then Cells(r, 9) = ""
      For c = 1 To 9
        Cells(r, c).Interior.ColorIndex = 0
      Next c

    ElseIf Cells(r, 2) = Range("Statusvalues").Cells(2) Then 'Deleted record has been edited
      Cells(r, 2) = Range("Statusvalues").Cells(3) 'Change to Edited
      For c = 1 To maxcol
        Cells(r, c).Interior.ColorIndex = 0
      Next c

    Else  'if record was Edited or New, it's a problem if it has been superseded
      If Cells(r, 9) <> "" Then
        MsgBox("This record has been superseded by record " & Cells(r, 9) & ".", , "Warning")
      End If
    End If

    'if the user has changed the lat/long coordinates, they may have broken the link to
    '  previous New or Edited versions of the record
    If (Cells(r, 2) <> "" And Cells(r, 2) <> Range("Statusvalues").Cells(1)) Or Cells(r, 9) <> "" Then 'Record is chained to previous records
      If Target.Column = 3 Or Target.Column = 4 Then
        MsgBox("Changing coordinates has broken the link to previous superseded records!", , "Warning")
      End If
    End If

  End Sub
  Public Function IfYes(ByVal testval As String) As Boolean
    'Not hard-coding the words Yes and No to allow for easy translation
    IfYes = testval = Range("YesNo").Cells(1)
  End Function
  Public Sub Export_GoogleEarth(ByVal GEfname As String)

    ' Export the speedcams list in Google Earth kml (xml) format
    ' Needs the SpeedcamsTemplate.kml file

    Dim start_placemark As Long
    Dim savedeleted As Boolean

    savedeleted = (Range("OptionSaveDeleted") = Range("YesNo").Cells(1))

    r = Start_row
    Do      'find first valid record
      If Cells(r, 1) = "" Then Exit Sub 'no valid speedcams
      'if allowed to write deleted and superseded records
      If savedeleted Then Exit Do 'if saving all records, go ahead
        GoSub getrecordstatus           'if not saving deleted or superseded, must check
      If recstatus <> rt_deleted And Cells(r, 9) = "" Then Exit Do
      r = r + 1
    Loop

    On Error Resume Next
    Open "SpeedcamsTemplate.kml" For Input As 1
    If Err() <> 0 Then
      MsgBox("Can't open SpeedcamsTemplate.kml" + Chr$(13) + Error$, , "Error")
      Exit Sub
    End If
    On Error GoTo 0
    Open GEfname For Output As 2

    totalrecords = 0

    'Export all records
    found_placemark = False
    Do While Not EOF(1)
      If Not found_placemark Then start_placemark = Seek(1)
        Line Input #1, a$
      If Not found_placemark Then
        found_placemark = (a$ Like "*<Placemark>*")
      ElseIf a$ Like "*<name>*" Then
        a$ = "   <name>Cam No." & Cells(r, 1) & "</name>"
      ElseIf a$ Like "*<description>*" Then
        a$ = "   <description>"
        a$ = a$ & Cells(r, 2) & Chr$(13)     'Status
        a$ = a$ & "Limit: " & Cells(r, 5) & Chr$(13)   'Speed Limit
        a$ = a$ & Cells(r, 6) & Chr$(13)     'Cam Type
        a$ = a$ & "Angle: " & Cells(r, 7) & Chr$(13)   'Angle
        a$ = a$ & Cells(r, 8)       'Direction
        If Cells(r, 11) <> "" Then a$ = a$ & Chr$(13) & Cells(r, 11)
        a$ = a$ & "</description>"
      ElseIf a$ Like "*<coordinates>*" Then
        a$ = "    <coordinates>" & Cells(r, 3) & "," & Cells(r, 4) & ",0</coordinates>"
      ElseIf a$ Like "*</Placemark>*" Then
        totalrecords = totalrecords + 1
        Do      'find next valid record
          r = r + 1
          If Cells(r, 1) = "" Then Exit Do 'no more speedcams to go
          'if allowed to write deleted and superseded records
          If savedeleted Then
            Seek(1, start_placemark)     'write another placemark record
            Exit Do
          End If
                GoSub getrecordstatus
          If (recstatus <> rt_deleted And Cells(r, 9) = "") Or savedeleted Then
            Seek(1, start_placemark)     'write another placemark
            Exit Do
          End If
        Loop
      End If
        Print #2, a$
    Loop
    Close(2)
    Close(1)
    MsgBox("Exported" & Str$(totalrecords) & " cams to " & GEfname, vbInformation, "Export Speedcams")

    Exit Sub

getrecordstatus:

    c = 2                                   'record Status
    spStatus$ = Cells(r, c)
    'use named range to allow modification/translation of strings
    Select Case spStatus$
      Case Range("Statusvalues").Cells(1)  'New
        recstatus = rt_new
      Case Range("Statusvalues").Cells(2)  'Deleted
        recstatus = rt_deleted
      Case Range("Statusvalues").Cells(3)  'Edited
        recstatus = rt_edited
      Case Else
        recstatus = 0
    End Select

    Return

  End Sub
  Public Sub Import_GPX(ByVal cfname As String, ByVal rstart As Integer)

    ' Import speedcams from a GPX file

    ' Must have Option Compare Text at start of module containing this routine

    Dim r As Integer, c As Integer
    Dim spCam1 As spCam
    Dim notvalid As Integer, warns As Integer

    Open cfname For Input As 1

    r = rstart
    recordsadded = 0
    addednotvalid = 0
    warncount = 0

    RefreshInProgress = True        'Prevent content-change event processing

    ' Read the file into the spreadsheet
    Do
        GoSub GetGPXRecord
      '        If EOF(1) Then Exit Do
      If fb$ = "" Then Exit Do 'no more data
      recordsadded = recordsadded + 1
      FillRow(r, spCam1, notvalid, warns)
      warncount = warncount + warns
      addednotvalid = addednotvalid + notvalid
      Cells(r, 11) = camdescr$
      r = r + 1
    Loop

    Close(1)

    Rows(r & ":" & Rows.Count).Select()
    Selection.ClearContents()
    Selection.Interior.ColorIndex = 0
    Cells(rstart, 1).Select()

    RefreshInProgress = False

    totalcams = totalcams + recordsadded
    totalvalidcams = totalvalidcams + recordsadded - addednotvalid

    a$ = "Imported" & Str$(recordsadded) & " cams"
    If warncount = 0 Then
      MsgBox(a$, vbInformation, "Import Speedcams")
    Else
      MsgBox(a$ + Chr$(13) + Str$(warncount) + " warnings", vbExclamation, "Import Speedcams")
    End If

    Exit Sub

GetGPXRecord:

    camdescr$ = ""
    coords$ = ""
    Do 'While Not EOF(1)
      '        Line Input #1, a$
      '        GoSub GetlineGPX
      Getline(1, fb$, a$)
      If a$ = "" Then Exit Do
      If a$ Like "*<wpt*" Then       'waypoint with coordinates
        camtext$ = ""
        coords$ = a$
      ElseIf a$ Like "*<name>*" Then  'waypoint name
            GoSub GetText
      ElseIf a$ Like "*<cmt>*" Then   'waypoint comment
            GoSub GetText
        i1 = InStr(camtext$, "<cmt>") + 5
        i2 = InStr(i1, camtext$, "</")
        If i2 < i1 Then i2 = Len(camtext$)
        camdescr$ = camdescr$ + Mid$(camtext$, i1, i2 - i1)
      ElseIf a$ Like "*<desc>*" Then  'waypoint description
            GoSub GetText
        i1 = InStr(camtext$, "<desc>") + 6
        i2 = InStr(i1, camtext$, "</")
        If i2 < i1 Then i2 = Len(camtext$)
        camdescr$ = Mid$(camtext$, i1, i2 - i1) + " " + camdescr$
      ElseIf a$ Like "*<label_text>*" Then  'waypoint label
            GoSub GetText
        i1 = InStr(camtext$, "<label_text>") + Len("<label_text>")
        i2 = InStr(i1, camtext$, "</")
        If i2 < i1 Then i2 = Len(camtext$)
        camdescr$ = camdescr$ + " " + Mid$(camtext$, i1, i2 - i1)
      ElseIf a$ Like "*</wpt>*" Then  'end of waypoint
            GoSub MakeCam
        Exit Do
      End If
    Loop

    Return

GetText:

    'read all the text in the element (don't care if we have too much, just looking for keywords
    Do
      camtext$ = camtext$ + a$
      If InStr(a$, "</") <> 0 Then Exit Do
      '        If EOF(1) Then Exit Do
      '        Line Input #1, a$
      'GoSub GetlineGPX
      Getline(1, fb$, a$)
      If a$ = "" Then Exit Do
      camtext$ = camtext$ + " "
    Loop
    '    Loop Until a$ = ""

    Return

MakeCam:

    spCam1.spAngle = 0                  'angle unspecified
    spCam1.spLimit = 0                'no speed limit specified
    spCam1.spFlag = rt_new          'mark record as New
    spCam1.spDirection = cd_alldirections * 2  'shift bits left by 1

    'convert coordinates from Waypoint tag
    On Error Resume Next
    i = InStr(coords$, "lat=")
    doublecalc = 0
    If i <> 0 Then doublecalc = Val(Mid$(coords$, i + Len("lat=") + 1))
    spCam1.spLat = doublecalc / epsilon
    doublecalc = 0
    i = InStr(coords$, "lon=")
    If i <> 0 Then doublecalc = Val(Mid$(coords$, i + Len("lon=") + 1))
    spCam1.spLong = doublecalc / epsilon
    On Error GoTo 0

    'see if we can find any keywords in the text strings to indicate cam type
    '(keywords and corresponding cam types are listed on the Validation sheet)
    camtype$ = ""
    For i = 1 To Range("GPXCamType").Rows.Count
      If camtext$ Like "*" & Range("GPXCamType").Cells(i, 1) & "*" Then
        camtype$ = Range("GPXCamType").Cells(i, 2)
        Exit For
      End If
    Next i

    Select Case camtype$
      Case Range("TypeValues").Cells(1)    'Fixed Speed Cam
        spCam1.spType = ct_fixedspeedcam
      Case Range("TypeValues").Cells(2)    'Mobile Speed Cam
        spCam1.spType = ct_mobilespeedcam
      Case Range("TypeValues").Cells(3)    'Traffic Light Speed Cam
        spCam1.spType = ct_trafficlightspeedcam
      Case Range("TypeValues").Cells(4)    'Section Control Speed Cam
        spCam1.spType = ct_sectionspeedcam
      Case Range("TypeValues").Cells(5)    'Red Light Cam
        spCam1.spType = ct_redlightcam
      Case Else
        spCam1.spType = ct_fixedspeedcam
    End Select

    ' see if there's a speed limit
    If camtext$ Like "*@##*" Then       'look for format @##, e.g. @50
      i = 0
      Do
        i = i + 1
        i = InStr(i, camtext$, "@")
        If i = 0 Then Exit Do
        s = Val(Mid$(camtext$, i + 1))
      Loop Until s > 0
      If s > 255 Then s = 0
      spCam1.spLimit = s
    End If


    Return

  End Sub
  Public Sub Import_GoogleEarth(ByVal cfname As String, ByVal rstart As Integer)

    ' Import speedcams from a Google Earth kml file

    ' Must have Option Compare Text at start of module containing this routine

    Dim r As Integer, c As Integer
    Dim spCam1 As spCam
    Dim notvalid As Integer, warns As Integer

    Open cfname For Input As 1

    r = rstart
    recordsadded = 0
    addednotvalid = 0
    warncount = 0

    RefreshInProgress = True        'Prevent content-change event processing

    ' Read the file into the spreadsheet
    Do
        GoSub GetkmlRecord
      '        If EOF(1) Then Exit Do
      If fb$ = "" Then Exit Do 'no more data
      recordsadded = recordsadded + 1
      FillRow(r, spCam1, notvalid, warns)
      warncount = warncount + warns
      addednotvalid = addednotvalid + notvalid
      If camno <> 0 Then Cells(r, 1) = Str$(camno)
      Cells(r, 11) = camdescr$
      r = r + 1
    Loop

    Close(1)

    Rows(r & ":" & Rows.Count).Select()
    Selection.ClearContents()
    Selection.Interior.ColorIndex = 0
    Cells(rstart, 1).Select()

    RefreshInProgress = False

    totalcams = totalcams + recordsadded
    totalvalidcams = totalvalidcams + recordsadded - addednotvalid

    a$ = "Imported" & Str$(recordsadded) & " cams"
    If warncount = 0 Then
      MsgBox(a$, vbInformation, "Import Speedcams")
    Else
      MsgBox(a$ + Chr$(13) + Str$(warncount) + " warnings", vbExclamation, "Import Speedcams")
    End If

    Exit Sub

GetkmlRecord:

    camdescr$ = ""
    coords$ = ""
    intag = False
    Do ' While Not EOF(1)
      'Line Input #1, a$
      'GoSub getlinekml
      Getline(1, fb$, a$)
      If a$ = "" Then Exit Do
      If a$ Like "*<Placemark*" Then       'start of placemark
        intag = True
        camtext$ = ""
      ElseIf Not intag Then
        'keep looking
      ElseIf a$ Like "*<coordinates*" Then       'coordinates of placemark
        coords$ = a$
      ElseIf a$ Like "*<name>*" Then  'waypoint name
            GoSub GetText
      ElseIf a$ Like "*<description>*" Then  'placemark description
            GoSub GetText
        i1 = InStr(camtext$, "<description>") + Len("<description>")
        i2 = InStr(i1, camtext$, "</")
        If i2 < i1 Then i2 = Len(camtext$)
        camdescr$ = Mid$(camtext$, i1, i2 - i1)
      ElseIf a$ Like "*</Placemark>*" Then  'end of placemark
        intag = False
            GoSub MakeCam
        Exit Do
      End If
    Loop

    Return

GetText:

    'read all the text in the element (don't care if we have too much, just looking for keywords
    Do
      camtext$ = camtext$ + a$
      If InStr(a$, "</") <> 0 Then Exit Do
      '        If EOF(1) Then Exit Do
      '        Line Input #1, a$
      '        GoSub getlinekml
      Getline(1, fb$, a$)
      If a$ = "" Then Exit Do
      camtext$ = camtext$ + " "
    Loop
    '    Loop Until a$ = ""

    Return

MakeCam:

    camno = 0
    spCam1.spAngle = 0              'assume angle unspecified
    spCam1.spLimit = 0              'assume no speed limit specified
    spCam1.spFlag = rt_new          'mark record as New
    spCam1.spDirection = 0  'assume All directions (must shift bits left)

    On Error Resume Next            'prevent crashes cause by format glitches in text data

    'convert coordinates from coordinates tag <coordinates>long,lat,0</coordinates>
    i = InStr(coords$, ">")
    doublecalc = 0
    If i <> 0 Then doublecalc = Val(Mid$(coords$, i + 1))
    spCam1.spLong = doublecalc / epsilon
    doublecalc = 0
    i = InStr(i + 2, coords$, ",")
    If i <> 0 Then doublecalc = Val(Mid$(coords$, i + 1))
    spCam1.spLat = doublecalc / epsilon

    i = InStr(camtext$, "Cam No.")
    If i <> 0 Then camno = Val(Mid$(camtext$, i + Len("Cam No.")))

    i = InStr(camtext$, "Limit: ")
    If i <> 0 Then spCam1.spLimit = Val(Mid$(camtext$, i + Len("Limit: ")))

    i = InStr(camtext$, "Angle: ")
    If i <> 0 Then
      i = Val(Mid$(camtext$, i + Len("Angle: ")))
      If i > 255 Then
        spCam1.spAngle = i Mod 256
        spCam1.spDirection = i \ 256
      Else
        spCam1.spAngle = i
      End If

    End If

    On Error GoTo 0

    'look for one of the status values
    If InStr(camtext$, Range("Statusvalues").Cells(2)) <> 0 Then 'Deleted
      spCam1.spFlag = rt_deleted
    ElseIf InStr(camtext$, Range("Statusvalues").Cells(3)) <> 0 Then 'Edited
      spCam1.spFlag = rt_edited
      'Else        'else leave as new
    End If

    'look for one of the direction values
    'use named range to allow modification/translation of strings
    If InStr(camtext$, Range("DirectionValues").Cells(1)) <> 0 Then 'Unidirectional
      spCam1.spDirection = spCam1.spDirection + cd_unidirectional * 2
    ElseIf InStr(camtext$, Range("DirectionValues").Cells(2)) <> 0 Then 'Bi-directional
      spCam1.spDirection = spCam1.spDirection + cd_bidirectional * 2
    Else 'If InStr(Range("DirectionValues").Cells(3), camtext$) <> 0 Then 'All directions is the default
      spCam1.spDirection = spCam1.spDirection + cd_alldirections * 2
    End If

    'look for one of the cam type values
    camtype$ = ""
    For i1 = 1 To Range("TypeValues").Rows.Count
      If InStr(camtext$, Range("TypeValues").Cells(i1)) <> 0 Then
        camtype$ = Range("TypeValues").Cells(i1)
        Exit For
      End If
    Next i1
    If i1 > Range("TypeValues").Rows.Count Then         'if exact type value not found, try to match a keyword
      For i = 1 To Range("GPXCamType").Rows.Count
        If camtext$ Like "*" & Range("GPXCamType").Cells(i, 1) & "*" Then
          camtype$ = Range("GPXCamType").Cells(i, 2)      'found a possible match
          Exit For
        End If
      Next i
    End If

    'use named range to allow modification/translation of strings
    Select Case camtype$
      Case Range("TypeValues").Cells(1)    'Fixed Speed Cam
        spCam1.spType = ct_fixedspeedcam
      Case Range("TypeValues").Cells(2)    'Mobile Speed Cam
        spCam1.spType = ct_mobilespeedcam
      Case Range("TypeValues").Cells(3)    'Traffic Light Speed Cam
        spCam1.spType = ct_trafficlightspeedcam
      Case Range("TypeValues").Cells(4)    'Section Control Speed Cam
        spCam1.spType = ct_sectionspeedcam
      Case Range("TypeValues").Cells(5)    'Red Light Cam
        spCam1.spType = ct_redlightcam
      Case Else                            'couldn't find a match, just go with default
        spCam1.spType = ct_fixedspeedcam
    End Select

    Return

  End Sub
  Sub Getline(ByVal fileno As Integer, ByVal fb$, ByVal a$)

    ' Alternative read strategy for badly-formatted XML files that use LFs without CR
    ' Returns a line of text in a$ just like Line Input#1, a$

    ' fileno is the file number to read (must be open for Input)
    ' fb$ is the read buffer

    Static lastpos As Integer               'pointer must be retained between calls

    If (Seek(fileno) = 1) Then              'initialize buffer on first file read
      fb$ = ""
      lastpos = 1
    End If

    'Read 1024 bytes at a time and look for LF marking end of lines
    Do

      If lastpos > Len(fb$) Then          'end of data in this buffer
        i = 0
      Else
        Do
          i = InStr(lastpos, fb$, Chr$(10))   'look for end of line
          If i = 1 Then
            lastpos = lastpos + 1
          Else
            Exit Do
          End If
        Loop
      End If

      If i = 0 Then                       'if no end of line found before end of buffer
        If EOF(1) Then                  'if already at end of file
          fb$ = ""                    'end of data
          a$ = ""                     'no data read
          Exit Do
        End If
        On Error Resume Next
        i1 = 1024                       'read next 1024 characters from file
        If Seek(1) + 1024 > LOF(1) Then 'unless file is shorter
          i1 = LOF(1) + 1 - Seek(1)
        End If
        fb$ = Mid$(fb$, lastpos) + Input$(i1, 1)    'add new data to remainder of old data
        On Error GoTo 0
        lastpos = 1                     'reset current position to start of buffer

      Else            'end of next line found in current buffer
        i1 = 0      'check for CR preceding LF to allow for properly-formatted files
        If Mid$(fb$, i - 1) = Chr$(13) Then
          i1 = 1
          i = i - 1
        End If
        If Mid$(fb$, i - 1) = Chr$(10) Then 'check for double LF at end of line
          i1 = i1 + 1
          i = i - 1
        End If
        a$ = Mid$(fb$, lastpos, i - lastpos)   'get complete line of text
        lastpos = i + 1 + i1            'move pointer to start of next line for next read
        If a$ = "" Then a$ = " " 'don't return a zero-length line, or it will count as end of data (whitespace ok)
        Exit Do
      End If
    Loop

  End Sub
End Class

