using CT.Core.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.BL.Service
{
    public interface IPersonService : IService
    {
        PersonDTO DoesItExistPearson(PersonDTO personFind);
        string AddPerson(PersonDTO personDTO);
        string UpdatePerson(PersonDTO personDTO);
        string RemovePerson(PersonDTO personDTO);
        List<PersonDTO> GetAllPerson(string idUser, bool CRM);
    }
}
