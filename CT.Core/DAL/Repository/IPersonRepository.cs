using CT.Core.Common.DTO;
using CT.Core.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.DAL.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        PersonDTO DoesItExistPearson(PersonDTO personDTO);
        List<PersonDTO> GetAllPerson(string idUser, bool CRM);
    }
}
