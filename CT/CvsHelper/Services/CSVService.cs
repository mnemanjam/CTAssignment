using CsvHelper;
using CsvHelper.Configuration;
using CT.Core.Common.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CT.CvsHelper.Services
{
    public class CSVService : ICSVService
    {
        public void WriteCSV<T>(List<T> records)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
            };

            using (var writer = new StreamWriter("C:\\Users\\file.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<PearsonDTOMap>();

                csv.WriteRecords(records);
            }
        }
        public byte[] WriteCsvToMemory(IEnumerable<PersonDTO> records)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
            };
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.Context.RegisterClassMap<PearsonDTOMap>();
                csvWriter.WriteRecords(records);
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }
        
    }
}