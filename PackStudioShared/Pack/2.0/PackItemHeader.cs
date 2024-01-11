using System.Runtime.InteropServices;

namespace PackUI.Pack.V2
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PackItemHeader
    {
        public uint zipState;
        public uint dataSize;
        public byte pathSize;
        public byte isReference;
        public ulong dataOffset;
    }
}
