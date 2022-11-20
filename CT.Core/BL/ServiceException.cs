using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.BL
{
    public class ServiceException : System.Exception
    {
        #region "Types"

        public enum ErrorType
        {
            TheRecordHasBeenModifiedOrDeletedInTheMeantime = 1,
            UnexpectedError = 2,
            RecordWithIdDoesNotExist = 3,
            RecordMeanwhileDeleted = 4,
            RecordWithIdentifierAlreadyExists = 5,
            InvalidDataInDb = 6,
        }
        
        #endregion

        #region "Fields"

        public ErrorType Code
        {
            get;
            set;
        }

        #endregion

        #region "Constructor"

        public ServiceException()
        {
        }

        public ServiceException(string s, ErrorType code)
            : base(s)
        {
            this.Code = code;
        }

        public ServiceException(string s, System.Exception e, ErrorType code)
            : base(s, e)
        {
            this.Code = code;
        }

        #endregion
    }
}
