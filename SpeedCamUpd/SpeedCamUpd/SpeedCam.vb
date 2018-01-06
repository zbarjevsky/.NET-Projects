Public Class SpeedCam
    spLong As Long
    spLat As Long
    spLimit As Byte         'speed limit, may be mph or kmh?
    spType As Byte          'see ct_ codes
    spAngle As Byte
    spDirection As Byte     'low bit contains hi bit of Angle (0-359 deg)
    spFlag As Byte          'record status, see rt_ codes
End Class
