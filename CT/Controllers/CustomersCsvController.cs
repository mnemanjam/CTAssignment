using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http;
using CT.Core.BL;
using CT.Core.BL.Service;
using CT.Core.Common.DTO;
using CT.Helpers;
using CT.MessageHandlers;

namespace CT.Controllers
{
    [Authorize]
    public class CustomersCsvController : ApiController
    {
        public IPersonService PersonService { get; set; }

        public CustomersCsvController(IPersonService personService)
        {
            this.PersonService = personService;
        }

        [HttpGet]
        [BasicAuthentication]
        public HttpResponseMessage GetCustomerFile()
        {
            try
            {
                var idUsera = Thread.CurrentPrincipal.Identity.Name;
                var personList = PersonService.GetAllPerson(idUsera, true);

                var data = personList.ToCsv(true);
                var negotiator = this.Configuration.Services.GetContentNegotiator();

                var result = negotiator.Negotiate(typeof(List<PersonDTO>),
                               this.Request,
                               this.Configuration.Formatters);

                // no formatter found
                if (result == null)
                {
                    throw new HttpResponseException(
                          new HttpResponseMessage(HttpStatusCode.NotAcceptable));
                }

                // no data found
                if(data==null)
                {
                    throw new HttpResponseException(
                          new HttpResponseMessage(HttpStatusCode.NotFound));
                }

                var response = new HttpResponseMessage
                {
                    Content = new ObjectContent<string>(
                        data,  // data
                        result.Formatter, // media formatter
                        result.MediaType.MediaType // MIME type
                    )
                };

                // add the `content-disposition` response header
                // to display the "File Download" dialog box 
                response.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "Customers-download.csv"
                    };

                return response;
            }
            catch (ServiceException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format("An unexpected error. Error: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, String.Format("An unexpected error. Error: {0}", ex.Message));
            }
        }
    }
}