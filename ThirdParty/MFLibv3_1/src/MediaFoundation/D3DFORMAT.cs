using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaFoundation
{
    //Compression formats.
    [Flags()]
    public enum DxFourCC
    {
        D3DFMT_DXT1 = 0x31545844,
        D3DFMT_DXT2 = 0x32545844,
        D3DFMT_DXT3 = 0x33545844,
        D3DFMT_DXT4 = 0x34545844,
        D3DFMT_DXT5 = 0x35545844,
        DX10 = 0x30315844,
        DXGI_FORMAT_BC4_UNORM = 0x55344342,
        DXGI_FORMAT_BC4_SNORM = 0x53344342,
        DXGI_FORMAT_BC5_UNORM = 0x32495441,
        DXGI_FORMAT_BC5_SNORM = 0x53354342,

        //DXGI_FORMAT_R8G8_B8G8_UNORM
        D3DFMT_R8G8_B8G8 = 0x47424752,

        //DXGI_FORMAT_G8R8_G8B8_UNORM
        D3DFMT_G8R8_G8B8 = 0x42475247,

        //DXGI_FORMAT_R16G16B16A16_UNORM
        D3DFMT_A16B16G16R16 = 36,

        //DXGI_FORMAT_R16G16B16A16_SNORM
        D3DFMT_Q16W16V16U16 = 110,

        //DXGI_FORMAT_R16_FLOAT
        D3DFMT_R16F = 111,

        //DXGI_FORMAT_R16G16_FLOAT
        D3DFMT_G16R16F = 112,

        //DXGI_FORMAT_R16G16B16A16_FLOAT
        D3DFMT_A16B16G16R16F = 113,

        //DXGI_FORMAT_R32_FLOAT
        D3DFMT_R32F = 114,

        //DXGI_FORMAT_R32G32_FLOAT
        D3DFMT_G32R32F = 115,

        //DXGI_FORMAT_R32G32B32A32_FLOAT
        D3DFMT_A32B32G32R32F = 116,

        D3DFMT_UYVY = 0x59565955,
        D3DFMT_YUY2 = 0x32595559,
        D3DFMT_CxV8U8 = 117,

        //This is set only by the nvidia exporter, it is not set by the dx texture tool
        //,it is ignored by the dx texture tool but it returns the ability to be opened in photoshop so I decided to keep it.
        D3DFMT_Q8W8V8U8 = 63,
    }

    public enum D3DFORMAT
    {
        D3DFMT_UNKNOWN = 0,

        D3DFMT_R8G8B8 = 20,
        D3DFMT_A8R8G8B8 = 21,
        D3DFMT_X8R8G8B8 = 22,
        D3DFMT_R5G6B5 = 23,
        D3DFMT_X1R5G5B5 = 24,
        D3DFMT_A1R5G5B5 = 25,
        D3DFMT_A4R4G4B4 = 26,
        D3DFMT_R3G3B2 = 27,
        D3DFMT_A8 = 28,
        D3DFMT_A8R3G3B2 = 29,
        D3DFMT_X4R4G4B4 = 30,
        D3DFMT_A2B10G10R10 = 31,
        D3DFMT_A8B8G8R8 = 32,
        D3DFMT_X8B8G8R8 = 33,
        D3DFMT_G16R16 = 34,
        D3DFMT_A2R10G10B10 = 35,
        D3DFMT_A16B16G16R16 = 36,

        D3DFMT_A8P8 = 40,
        D3DFMT_P8 = 41,

        D3DFMT_L8 = 50,
        D3DFMT_A8L8 = 51,
        D3DFMT_A4L4 = 52,

        D3DFMT_V8U8 = 60,
        D3DFMT_L6V5U5 = 61,
        D3DFMT_X8L8V8U8 = 62,
        D3DFMT_Q8W8V8U8 = 63,
        D3DFMT_V16U16 = 64,
        D3DFMT_A2W10V10U10 = 67,

        D3DFMT_UYVY = DxFourCC.D3DFMT_UYVY, // MAKEFOURCC('U', 'Y', 'V', 'Y'),
        //D3DFMT_R8G8_B8G8 = FourCC.D3DFMT_RGBG, // MAKEFOURCC('R', 'G', 'B', 'G'),
        D3DFMT_YUY2 = DxFourCC.D3DFMT_YUY2, // MAKEFOURCC('Y', 'U', 'Y', '2'),
        //D3DFMT_G8R8_G8B8 = DxFourCC.D3DFMT_GRGB, // MAKEFOURCC('G', 'R', 'G', 'B'),
        D3DFMT_DXT1 = DxFourCC.D3DFMT_DXT1, // MAKEFOURCC('D', 'X', 'T', '1'),
        D3DFMT_DXT2 = DxFourCC.D3DFMT_DXT2, // MAKEFOURCC('D', 'X', 'T', '2'),
        D3DFMT_DXT3 = DxFourCC.D3DFMT_DXT3, // MAKEFOURCC('D', 'X', 'T', '3'),
        D3DFMT_DXT4 = DxFourCC.D3DFMT_DXT4, // MAKEFOURCC('D', 'X', 'T', '4'),
        D3DFMT_DXT5 = DxFourCC.D3DFMT_DXT5, // MAKEFOURCC('D', 'X', 'T', '5'),

        D3DFMT_D16_LOCKABLE = 70,
        D3DFMT_D32 = 71,
        D3DFMT_D15S1 = 73,
        D3DFMT_D24S8 = 75,
        D3DFMT_D24X8 = 77,
        D3DFMT_D24X4S4 = 79,
        D3DFMT_D16 = 80,

        D3DFMT_D32F_LOCKABLE = 82,
        D3DFMT_D24FS8 = 83,

#if !D3D_DISABLE_9EX
        D3DFMT_D32_LOCKABLE = 84,
        D3DFMT_S8_LOCKABLE = 85,
#endif // !D3D_DISABLE_9EX

        D3DFMT_L16 = 81,

        D3DFMT_VERTEXDATA = 100,
        D3DFMT_INDEX16 = 101,
        D3DFMT_INDEX32 = 102,

        D3DFMT_Q16W16V16U16 = 110,

        //D3DFMT_MULTI2_ARGB8 = FourCC.D3DFMT_MET1, //  D3DFORMAT_Tools.MAKEFOURCC('M', 'E', 'T', '1'),

        D3DFMT_R16F = 111,
        D3DFMT_G16R16F = 112,
        D3DFMT_A16B16G16R16F = 113,

        D3DFMT_R32F = 114,
        D3DFMT_G32R32F = 115,
        D3DFMT_A32B32G32R32F = 116,

        D3DFMT_CxV8U8 = 117,

#if !D3D_DISABLE_9EX
        D3DFMT_A1 = 118,
        D3DFMT_A2B10G10R10_XR_BIAS = 119,
        D3DFMT_BINARYBUFFER = 199,
#endif // !D3D_DISABLE_9EX

        D3DFMT_FORCE_DWORD = 0x7fffffff
    }

    public static class D3DFORMAT_Tools
    {
        public static int MAKEFOURCC(char c1, char c2, char c3, char c4)
        {
            return c1 >> 24 + c2 >> 16 + c3 >> 8 + c4;
        }
    }

    ///* standard four character codes */
    //public class FOURCC
    //{
    //    public const UInt32 RIFF = 0x46464952;
    //    public static UInt32 LIST = MakeFourCC('L', 'I', 'S', 'T');

    //    // four character codes used to identify standard built-in I/O procedures
    //    public static UInt32 DOS = MakeFourCC('D', 'O', 'S', ' ');
    //    public static UInt32 MEM = MakeFourCC('M', 'E', 'M', ' ');

    //    public static UInt32 vidc = MakeFourCC('v', 'i', 'd', 'c');
    //    public static UInt32 vcap = MakeFourCC('v', 'c', 'a', 'p');

    //    // For .wav files
    //    public const UInt32 fmt = 0x20746d66;
    //    public const UInt32 fact = 0x74636166;
    //    public const UInt32 data = 0x61746164;
    //    public const UInt32 WAVE = 0x45564157;

    //    public static UInt32 MakeFourCC(byte[] bytes)
    //    {
    //        UInt32 result = ((UInt32)bytes[0] | ((UInt32)bytes[1] << 8) | ((UInt32)bytes[2] << 16) | ((UInt32)bytes[3] << 24));

    //        return result;
    //    }

    //    public static UInt32 MakeFourCC(char ch0, char ch1, char ch2, char ch3)
    //    {
    //        UInt32 result = ((UInt32)(byte)(ch0) | ((UInt32)(byte)(ch1) << 8) | ((UInt32)(byte)(ch2) << 16) | ((UInt32)(byte)(ch3) << 24));

    //        return result;
    //    }

    //    public static string FourCCToString(uint fourcc)
    //    {
    //        char c1 = (char)(fourcc & 0x000000ff);
    //        char c2 = (char)((fourcc & 0x0000ff00) >> 8);
    //        char c3 = (char)((fourcc & 0x00ff0000) >> 16);
    //        char c4 = (char)((fourcc & 0xff000000) >> 24);

    //        StringBuilder builder = new StringBuilder(4);
    //        builder.AppendFormat("{0}{1}{2}{3}", c1, c2, c3, c4);

    //        return builder.ToString();
    //    }
    //}
}

