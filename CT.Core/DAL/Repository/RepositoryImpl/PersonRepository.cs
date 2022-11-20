using CT.Core.Common.DTO;
using CT.Core.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.DAL.Repository.RepositoryImpl
{
    public class PersonRepository : SqlRepository<Person>, IPersonRepository
    {
        #region "Constructor"
        public PersonRepository(CTEntities context)
            : base(context)
        {
        }
        #endregion

        #region "GetById"
        /// <summary>
        /// Uzima masinu na osnovu vrednosti id - a primarnog kljuca
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Person GetById(Object id)
        {
            Int64 idPerson = Convert.ToInt64(id);

            Person person = this.FindWhere(mas => mas.IdPerson == idPerson).FirstOrDefault();
            return person;
        }
        #endregion

        #region "DoesItExistPearson"

        public PersonDTO DoesItExistPearson(PersonDTO findPerson)
        {
            PersonDTO personDTO = null;

            personDTO = this.FindWhere(pe => pe.IdPerson == findPerson.IdPerson)
                             .Select(pe => new PersonDTO()
                             {
                                 IdPerson = pe.IdPerson,
                                 Name = pe.Name,
                                 SSN = pe.SSN,
                                 DOB = pe.DOB,
                                 StateHome = pe.StateHome,
                                 StreetHome = pe.StreetHome,
                                 CityHome = pe.CityHome,
                                 ZipHome = pe.ZipHome,
                                 StateOffice = pe.StateOffice,
                                 StreetOffice = pe.StreetOffice,
                                 CityOffice = pe.CityOffice,
                                 ZipOffice = pe.ZipOffice,
                                 Discount = pe.Discount,
                                 FavoriteColors = pe.FavoriteColors,
                                 Age = pe.Age,
                                 IdUser = pe.UserId,
                                 RecordTime = pe.RecordTime,
                                 CRM = pe.AspNetUsers.CRMS

                             }).FirstOrDefault();

            return personDTO;
        }
        #endregion

        #region "GetAllPerson"
        public List<PersonDTO> GetAllPerson(string idUser, bool CRM)
        {            
            List<PersonDTO> personList = null;
            if (CRM == true)
            {
                string crm = context.AspNetUsers.Find(idUser).CRMS;
                personList = this.FindWhere(pe => pe.AspNetUsers.CRMS == crm)
                            .Select(pe => new PersonDTO()
                            {
                                IdPerson = pe.IdPerson,
                                Name = pe.Name,
                                SSN = pe.SSN,
                                DOB = pe.DOB,
                                StateHome = pe.StateHome,
                                StreetHome = pe.StreetHome,
                                CityHome = pe.CityHome,
                                ZipHome = pe.ZipHome,
                                StateOffice = pe.StateOffice,
                                StreetOffice = pe.StreetOffice,
                                CityOffice = pe.CityOffice,
                                ZipOffice = pe.ZipOffice,
                                Discount = pe.Discount,
                                FavoriteColors = pe.FavoriteColors,
                                IdUser = pe.UserId,
                                Age = pe.Age,
                                RecordTime = pe.RecordTime,
                                CRM = pe.AspNetUsers.CRMS
                            })
                            .ToList().Where(x => x.RecordTime > DateTime.UtcNow.AddMonths(-1)).ToList();
            }
            else
            if (idUser != "all")
                personList = this.FindWhere(pe => pe.UserId == idUser)
                            .Select(pe => new PersonDTO()
                            {
                                IdPerson = pe.IdPerson,
                                Name = pe.Name,
                                SSN = pe.SSN,
                                DOB = pe.DOB,
                                StateHome = pe.StateHome,
                                StreetHome = pe.StreetHome,
                                CityHome = pe.CityHome,
                                ZipHome = pe.ZipHome,
                                StateOffice = pe.StateOffice,
                                StreetOffice = pe.StreetOffice,
                                CityOffice = pe.CityOffice,
                                ZipOffice = pe.ZipOffice,
                                Discount = pe.Discount,
                                FavoriteColors = pe.FavoriteColors,
                                IdUser = pe.UserId,
                                Age = pe.Age,
                                RecordTime = pe.RecordTime,
                                CRM = pe.AspNetUsers.CRMS
                            })
                            .ToList().Where(x => x.RecordTime > DateTime.UtcNow.AddMonths(-1)).ToList();
            else
                personList = (List<PersonDTO>)this.FindWhere(x => x.AspNetUsers.CRMS == idUser)
                            .Select(pe => new PersonDTO()
                            {
                                IdPerson = pe.IdPerson,
                                Name = pe.Name,
                                SSN = pe.SSN,
                                DOB = pe.DOB,
                                StateHome = pe.StateHome,
                                StreetHome = pe.StreetHome,
                                CityHome = pe.CityHome,
                                ZipHome = pe.ZipHome,
                                StateOffice = pe.StateOffice,
                                StreetOffice = pe.StreetOffice,
                                CityOffice = pe.CityOffice,
                                ZipOffice = pe.ZipOffice,
                                Discount = pe.Discount,
                                FavoriteColors = pe.FavoriteColors,
                                IdUser = pe.UserId,
                                Age = pe.Age,
                                RecordTime = pe.RecordTime,
                                CRM = pe.AspNetUsers.CRMS
                            }).ToList().Where(x => x.RecordTime > DateTime.UtcNow.AddMonths(-1)).ToList();

            return personList;
        }
        #endregion
    }
}
