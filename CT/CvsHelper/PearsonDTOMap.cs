using CsvHelper.Configuration;
using CT.Core.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CT.CvsHelper
{
    public class PearsonDTOMap : ClassMap<PersonDTO>
    {
        public PearsonDTOMap()
        {
            Map(m => m.Name).Index(0).Name("Name");
            Map(m => m.SSN).Index(1).Name("SSN");
            Map(m => m.DOB).Index(2).Name("DOB");
            Map(m => m.FavoriteColors).Index(3).Name("FavoriteColors");
            Map(m => m.Age).Index(4).Name("Age");
            Map(m => m.StreetOffice).Index(5).Name("StreetOffice");
            Map(m => m.CityOffice).Index(6).Name("CityOffice");
            Map(m => m.StateOffice).Index(7).Name("StateOffice");
            Map(m => m.ZipOffice).Index(8).Name("ZipOffice");
            Map(m => m.StreetHome).Index(9).Name("StreetHome");
            Map(m => m.CityHome).Index(10).Name("CityHome");
            Map(m => m.StateHome).Index(11).Name("StateHome");
            Map(m => m.ZipHome).Index(12).Name("ZipHome");
            Map(m => m.Discount).Index(13).Name("Discount");
            Map(m => m.RecordTime).Index(14).Name("RecordTime");
        }
    }
}