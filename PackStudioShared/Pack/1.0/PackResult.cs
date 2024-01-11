namespace Pack.V1
{
    public enum PackResult
    {
        Success = 0,
        FailedToAllocate = 1,
        FailedToCreateZSTD = 2,
        FailedToCreateFile = 3,
        FailedToOpenFile = 4,
        FailedToWriteFile = 5,
        FailedToReadFile = 6,
        FailedToSeekFile = 7,
        FailedToDecompress = 8,
        FailedToGetItem = 9,
        BadDataSize = 10,
        BadFileType = 11,
        BadFileVersion = 12,
        BadFileEndianness = 13,
    }
}