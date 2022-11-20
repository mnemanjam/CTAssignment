using CT.Core.Common.DTO;
using CT.Core.DAL.Repository;
using CT.Core.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.BL.Service.ServiceImpl
{
    public class PersonService : Service, IPersonService
    {

        #region "Constructor"
        public PersonService(IUnitOfWork unitOfWork, IPersonRepository personRepository)
            : base(unitOfWork)
        {
            this.UnitOfWork.PersonRepository = personRepository;
        }
        #endregion        

        #region "DoesItExistPearson"

        public PersonDTO DoesItExistPearson(PersonDTO personFind)
        {
            try
            {
                PersonDTO existsPerson = this.UnitOfWork.PersonRepository.DoesItExistPearson(personFind);
                return existsPerson;
            }
            catch (Exception ex)
            {
                String message = "An unexpected error occurred while determining if a person exists";
                // TODO place for log error
                throw new ServiceException(message, ServiceException.ErrorType.UnexpectedError);
            }
        }
        #endregion

        #region "AddPerson"
        /// <summary>
        /// Add new person  
        /// </summary>
        /// <param name="personDTO"></param>
        public string AddPerson(PersonDTO personDTO)
        {
            try
            {
                this.UnitOfWork.PersonRepository.Add(personDTO.ToEntity());
                this.UnitOfWork.CommitOrFlush();
                return "Ok";
            }
            catch (Exception ex)
            {
                String errorMsg = "An unexpected error occurred while adding person";
                return errorMsg;
                // TODO place for log error
                throw new ServiceException(errorMsg, ServiceException.ErrorType.UnexpectedError);
            }
        }
        #endregion

        #region "UpdatePerson"
        public string UpdatePerson(PersonDTO personDTO)
        {
            try
            {
                this.UnitOfWork.PersonRepository.Update(personDTO.ToEntity());
                this.UnitOfWork.CommitOrFlush();
                return "Ok";
            }
            catch (DbUpdateConcurrencyException)
            {
                String errorMsg = "The person has been modified or deleted in the meantime";
                throw new ServiceException(errorMsg, ServiceException.ErrorType.TheRecordHasBeenModifiedOrDeletedInTheMeantime);
            }
            catch (Exception ex)
            {
                String errorMsg = "An unexpected error occurred while updateing the person";
                return errorMsg;
                // TODO place for log error
                throw new ServiceException(errorMsg, ServiceException.ErrorType.UnexpectedError);
            }
        }
        #endregion

        #region "RemovePerson"
        public string RemovePerson(PersonDTO personDTO)
        {
            try
            {
                this.UnitOfWork.PersonRepository.Remove(personDTO.ToEntity());
                this.UnitOfWork.CommitOrFlush();
                return "Ok";
            }
            catch (DbUpdateConcurrencyException)
            {
                String errorMsg = "The person has been modified or deleted in the meantime";
                return errorMsg;
                throw new ServiceException(errorMsg, ServiceException.ErrorType.TheRecordHasBeenModifiedOrDeletedInTheMeantime);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null
                        && ex.InnerException.InnerException != null
                            && ex.InnerException.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    String errorMsg = "The person cannot be deleted because it is already in use in the application";
                    return errorMsg;
                    throw new ServiceException(errorMsg, ServiceException.ErrorType.UnexpectedError);
                }
                else
                {
                    String errorMsg = "An unexpected error occurred while deleting the person";
                    return errorMsg;
                    // TODO place for log error
                    throw new ServiceException(errorMsg, ServiceException.ErrorType.UnexpectedError);
                }
            }
            catch (Exception ex)
            {
                String errorMsg = "An unexpected error occurred while deleting the person";
                return errorMsg;
                // TODO place for log error
                throw new ServiceException(errorMsg, ServiceException.ErrorType.UnexpectedError);
            }
        }
        #endregion

        #region "GetAllPerson"
        public List<PersonDTO> GetAllPerson(string idUser, bool CRM)
        {
            try
            {
                List<PersonDTO> person = this.UnitOfWork.PersonRepository.GetAllPerson(idUser, CRM);
                return person;
            }
            catch (Exception ex)
            {
                String errorMsg = "An unexpected error occurred while retrieving all persons";
                // TODO place for log error
                throw new ServiceException(errorMsg, ServiceException.ErrorType.UnexpectedError);
            }
        }
        #endregion

    }
}
