Imports MbUnit.Framework


Global Const Start_row = 3       'starting row number for cam list in spreadsheet
Global RefreshInProgress As Boolean
Global launchdir$               'program start directory
Global ShowEdit As Boolean      'True to automatically show edits to cam records
Global totalcams As Integer
Global totalvalidcams As Integer
Global maxcol As Integer

' spCam record consists of 13 bytes
Public Class spCam
    spLong As Long
    spLat As Long
    spLimit As Byte         'speed limit, may be mph or kmh?
    spType As Byte          'see ct_ codes
    spAngle As Byte
    spDirection As Byte     'low bit contains hi bit of Angle (0-359 deg)
    spFlag As Byte          'record status, see rt_ codes
End Class


<TestFixture> _
Public Class Module
  <Test()> _
  Public Sub Test()
    ' TODO: Add test logic here
  End Sub
End Class
