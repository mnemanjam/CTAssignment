using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace CT.Core.Common.DTO
{
    public class PersonDTO
    {
        [Key]
        public long IdPerson { get; set; }
        [Name("Name")]
        [JsonProperty(PropertyName = "Name", Order = 0)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "SSN", Order = 1)]
        [Name("SSN")]
        public string SSN { get; set; }
        [JsonProperty(PropertyName = "DOB", Order = 2)]
        [Name("DOB")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DOB { get; set; }
        [JsonProperty(PropertyName = "FavoriteColors", Order = 4)]
        [Name("FavoriteColors")]
        public string FavoriteColors { get; set; }
        [JsonProperty(PropertyName = "Age", Order = 5)]
        [Name("Age")]
        public Nullable<long> Age { get; set; }
        [JsonProperty(PropertyName = "StreetOffice", Order = 6)]
        [Name("StreetOffice")]

        public string StreetOffice { get; set; }
        [JsonProperty(PropertyName = "CityOffice", Order = 7)]
        [Name("CityOffice")]
        public string CityOffice { get; set; }
        [JsonProperty(PropertyName = "StateOffice", Order = 8)]
        [Name("StateOffice")]
        public string StateOffice { get; set; }
        [JsonProperty(PropertyName = "ZipOffice", Order = 9)]
        [Name("ZipOffice")]
        public Nullable<int> ZipOffice { get; set; }
        [JsonProperty(PropertyName = "StreetHome", Order = 10)]
        [Name("StreetHome")]
        public string StreetHome { get; set; }
        [JsonProperty(PropertyName = "CityHome", Order = 11)]
        [Name("CityHome")]
        public string CityHome { get; set; }
        [JsonProperty(PropertyName = "StateHome", Order = 12)]
        [Name("StateHome")]
        public string StateHome { get; set; }
        [JsonProperty(PropertyName = "ZipHome", Order = 13)]
        [Name("ZipHome")]
        public Nullable<int> ZipHome { get; set; }
        [JsonProperty(PropertyName = "Discount", Order = 14)]
        [Name("Discount")]
        [DisplayName("Discount (%)")]
        public string Discount { get; set; }
        public string IdUser { get; set; }
        [JsonProperty(PropertyName = "RecordTime", Order = 15)]
        [Name("RecordTime")]
        public DateTime RecordTime { get; set; }
        public string CRM { get; set; }
    }
}
