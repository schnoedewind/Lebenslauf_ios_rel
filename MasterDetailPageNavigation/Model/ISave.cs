﻿using System.IO;
using System.Threading.Tasks;

namespace Lebenslauf
{
    public interface ISave
    {
        void Save(string filename, string contentType, MemoryStream stream);
    }
    public interface ISaveWindowsPhone
    {
        Task Save(string filename, string contentType, MemoryStream stream);
    }
}