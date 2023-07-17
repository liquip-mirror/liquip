﻿namespace Liquip.FileSystems.NTFS.Model;

public interface ISaveableObject
{
    int GetSaveLength();
    void Save(byte[] buffer, int offset);
}