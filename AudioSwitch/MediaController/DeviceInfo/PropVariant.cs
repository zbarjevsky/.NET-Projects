﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.Media.Device
{
    /// <summary>
    /// from Propidl.h.
    /// http://msdn.microsoft.com/en-us/library/aa380072(VS.85).aspx
    /// contains a union so we have to do an explicit layout
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct PropVariant
    {
        /// <summary>
        /// Value type tag.
        /// </summary>
        [FieldOffset(0)] public short vt;
        /// <summary>
        /// Reserved1.
        /// </summary>
        [FieldOffset(2)] public short wReserved1;
        /// <summary>
        /// Reserved2.
        /// </summary>
        [FieldOffset(4)] public short wReserved2;
        /// <summary>
        /// Reserved3.
        /// </summary>
        [FieldOffset(6)] public short wReserved3;
        /// <summary>
        /// cVal.
        /// </summary>
        [FieldOffset(8)] public sbyte cVal;
        /// <summary>
        /// bVal.
        /// </summary>
        [FieldOffset(8)] public byte bVal;
        /// <summary>
        /// iVal.
        /// </summary>
        [FieldOffset(8)] public short iVal;
        /// <summary>
        /// uiVal.
        /// </summary>
        [FieldOffset(8)] public ushort uiVal;
        /// <summary>
        /// lVal.
        /// </summary>
        [FieldOffset(8)] public int lVal;
        /// <summary>
        /// ulVal.
        /// </summary>
        [FieldOffset(8)] public uint ulVal;
        /// <summary>
        /// intVal.
        /// </summary>
        [FieldOffset(8)] public int intVal;
        /// <summary>
        /// uintVal.
        /// </summary>
        [FieldOffset(8)] public uint uintVal;
        /// <summary>
        /// hVal.
        /// </summary>
        [FieldOffset(8)] public long hVal;
        /// <summary>
        /// uhVal.
        /// </summary>
        [FieldOffset(8)] public long uhVal;
        /// <summary>
        /// fltVal.
        /// </summary>
        [FieldOffset(8)] public float fltVal;
        /// <summary>
        /// dblVal.
        /// </summary>
        [FieldOffset(8)] public double dblVal;
        //VARIANT_BOOL boolVal;
        /// <summary>
        /// boolVal.
        /// </summary>
        [FieldOffset(8)] public short boolVal;
        /// <summary>
        /// scode.
        /// </summary>
        [FieldOffset(8)] public int scode;
        //CY cyVal;
        //[FieldOffset(8)] private DateTime date; - can cause issues with invalid value
        /// <summary>
        /// Date time.
        /// </summary>
        [FieldOffset(8)] public System.Runtime.InteropServices.ComTypes.FILETIME filetime;
        //CLSID* puuid;
        //CLIPDATA* pclipdata;
        //BSTR bstrVal;
        //BSTRBLOB bstrblobVal;
        /// <summary>
        /// Binary large object.
        /// </summary>
        [FieldOffset(8)] public Blob blobVal;
        //LPSTR pszVal;
        /// <summary>
        /// Pointer value.
        /// </summary>
        [FieldOffset(8)] public IntPtr pointerValue; //LPWSTR 
        //IUnknown* punkVal;
        /*IDispatch* pdispVal;
        IStream* pStream;
        IStorage* pStorage;
        LPVERSIONEDSTREAM pVersionedStream;
        LPSAFEARRAY parray;
        CAC cac;
        CAUB caub;
        CAI cai;
        CAUI caui;
        CAL cal;
        CAUL caul;
        CAH cah;
        CAUH cauh;
        CAFLT caflt;
        CADBL cadbl;
        CABOOL cabool;
        CASCODE cascode;
        CACY cacy;
        CADATE cadate;
        CAFILETIME cafiletime;
        CACLSID cauuid;
        CACLIPDATA caclipdata;
        CABSTR cabstr;
        CABSTRBLOB cabstrblob;
        CALPSTR calpstr;
        CALPWSTR calpwstr;
        CAPROPVARIANT capropvar;
        CHAR* pcVal;
        UCHAR* pbVal;
        SHORT* piVal;
        USHORT* puiVal;
        LONG* plVal;
        ULONG* pulVal;
        INT* pintVal;
        UINT* puintVal;
        FLOAT* pfltVal;
        DOUBLE* pdblVal;
        VARIANT_BOOL* pboolVal;
        DECIMAL* pdecVal;
        SCODE* pscode;
        CY* pcyVal;
        DATE* pdate;
        BSTR* pbstrVal;
        IUnknown** ppunkVal;
        IDispatch** ppdispVal;
        LPSAFEARRAY* pparray;
        PROPVARIANT* pvarVal;
        */

        /// <summary>
        /// Creates a new PropVariant containing a long value
        /// </summary>
        public static PropVariant FromLong(long value)
        {
            return new PropVariant() { vt = (short)VarEnum.VT_I8, hVal = value };
        }

        /// <summary>
        /// Helper method to gets blob data
        /// </summary>
        private byte[] GetBlob()
        {
            var blob = new byte[blobVal.Length];
            Marshal.Copy(blobVal.Data, blob, 0, blob.Length);
            return blob;
        }

        /// <summary>
        /// Interprets a blob as an array of structs
        /// </summary>
        public T[] GetBlobAsArrayOf<T>()
        {
            var blobByteLength = blobVal.Length;
            var singleInstance = (T)Activator.CreateInstance(typeof(T));
            var structSize = Marshal.SizeOf(singleInstance);
            if (blobByteLength % structSize != 0)
            {
                throw new InvalidDataException(String.Format("Blob size {0} not a multiple of struct size {1}", blobByteLength, structSize));
            }
            var items = blobByteLength / structSize;
            var array = new T[items];
            for (int n = 0; n < items; n++)
            {
                array[n] = (T)Activator.CreateInstance(typeof(T));
                Marshal.PtrToStructure(new IntPtr((long)blobVal.Data + n * structSize), array[n]);
            }
            return array;
        }

        /// <summary>
        /// Gets the type of data in this PropVariant
        /// </summary>
        public VarEnum DataType => (VarEnum)vt;

        /// <summary>
        /// Property value
        /// </summary>
        public object Value
        {
            get
            {
                VarEnum ve = DataType;
                switch (ve)
                {
                    case VarEnum.VT_I1:
                        return bVal;
                    case VarEnum.VT_I2:
                        return iVal;
                    case VarEnum.VT_I4:
                        return lVal;
                    case VarEnum.VT_I8:
                        return hVal;
                    case VarEnum.VT_INT:
                        return iVal;
                    case VarEnum.VT_UI4:
                        return ulVal;
                    case VarEnum.VT_UI8:
                        return uhVal;
                    case VarEnum.VT_LPWSTR:
                        return Marshal.PtrToStringUni(pointerValue);
                    case VarEnum.VT_BLOB:
                    case VarEnum.VT_VECTOR | VarEnum.VT_UI1:
                        return GetBlob();
                    case VarEnum.VT_CLSID:
                        return MarshalHelpers.PtrToStructure<Guid>(pointerValue);
                    case VarEnum.VT_BOOL:
                        switch (boolVal)
                        {
                            case -1:
                                return true;
                            case 0:
                                return false;
                            default:
                                throw new NotSupportedException("PropVariant VT_BOOL must be either -1 or 0");
                        }
                    case VarEnum.VT_FILETIME:
                        return DateTime.FromFileTime((((long)filetime.dwHighDateTime) << 32) + filetime.dwLowDateTime);
                }
                throw new NotImplementedException("PropVariant " + ve);
            }
        }

        ///// <summary>
        ///// allows freeing up memory, might turn this into a Dispose method?
        ///// </summary>
        //[Obsolete("Call with pointer instead")]
        //public void Clear()
        //{
        //    PropVariantNative.PropVariantClear(ref this);
        //}

        ///// <summary>
        ///// Clears with a known pointer
        ///// </summary>
        //public static void Clear(IntPtr ptr)
        //{
        //    PropVariantNative.PropVariantClear(ptr);
        //}
    }
}
