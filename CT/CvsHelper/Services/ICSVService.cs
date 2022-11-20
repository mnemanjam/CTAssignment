using CT.Core.Common.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CT.CvsHelper.Services
{
    public interface ICSVService
    {
        void WriteCSV<T>(List<T> records);
        byte[] WriteCsvToMemory(IEnumerable<PersonDTO> records);
    }
}