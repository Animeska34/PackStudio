using System.Runtime.InteropServices;

namespace PackUI.Pack.V2
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PackHeader
    {
        public uint magic;
        public byte versionMajor;
        public byte versionMinor;
        public byte versionPatch;
        public byte isBigEndian;
        public ulong itemCount;
    }
}
