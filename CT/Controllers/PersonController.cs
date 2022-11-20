using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using CT.Core.BL.Service;
using CT.Core.Common.DTO;
using CT.Core.DAL.Entity;
using CT.CvsHelper.Services;
using CT.SOAP.Demo;
using Microsoft.AspNet.Identity;

namespace CT.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        #region Fields
        public IPersonService PersonService { get; set; }
        private readonly ICSVService CsvService;
        #endregion

        #region "PersonController"
        public PersonController(IPersonService personService, ICSVService csvService)
        {
            this.PersonService = personService;
            this.CsvService = csvService;
        }
        #endregion

        #region Index
        // GET: Person
        public ActionResult Index(string searchString)
        {
            ViewBag.Poruka = TempData["msg"];
            ViewBag.Color = TempData["color"];
            //if user in role Admin, get all customers who added in last month, else get all customers for User CRM
            List<PersonDTO> personList = User.IsInRole("Admin") ? PersonService.GetAllPerson("all", false) : PersonService.GetAllPerson(User.Identity.GetUserId(), false);
            //reached the daily limit of 5
            var i = personList.Count(x => x.RecordTime.Date == DateTime.Now.Date);
            if (i < 5)
            {
                if (searchString != null)
                {
                    return RedirectToAction("Create", new { id = searchString });
                }
            }
            else
            {
                TempData["msg"] = "You have reached the daily limit of 5 customers per agent";
                TempData["color"] = "green";
            }
            return View(personList);
        }
        #endregion

        #region Details
        // GET: Person/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PersonDTO personDTO = new PersonDTO
            {
                IdPerson = id.Value
            };
            personDTO = PersonService.DoesItExistPearson(personDTO);
            if (personDTO == null)
            {
                return HttpNotFound();
            }
            return View(personDTO);
        }
        #endregion

        #region Create     
        // GET: Person/Create
        public ActionResult Create(string id)
        {
            PersonDTO personDTO  = FindPerson(id);
            //Check if exist User with same SSN in same CRM
            var exist = PersonService.GetAllPerson(User.Identity.GetUserId(), true).Where(x => x.SSN == personDTO.SSN).ToList();

            if (exist.Count() == 0)
            {
                if (personDTO.SSN != null)
                {
                    return View(personDTO);
                }
                else
                {
                    TempData["msg"] = "The customer with the entered ID does not exist";
                    TempData["color"] = "red";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["msg"] = "The customer with the entered ID has already been added this month to your CRM";
                TempData["color"] = "red";
                return RedirectToAction("Index");
            }
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonDTO person)
        {
            if (ModelState.IsValid)
            {
                person.IdUser = User.Identity.GetUserId();
                person.RecordTime = DateTime.Now;
                string message = PersonService.AddPerson(person);
                if (message == "Ok")
                {
                    TempData["msg"] = "You have successfully created a customer";
                    TempData["color"] = "green"; ;
                }
                else
                {
                    TempData["msg"] = message;
                    TempData["color"] = "red";
                }
                return RedirectToAction("Index");
            }

            return View(person);
        }
        #endregion

        #region Edit
        // GET: Person/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonDTO personDTO = new PersonDTO
            {
                IdPerson = id.Value
            };
            personDTO = PersonService.DoesItExistPearson(personDTO);
            return View(personDTO);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonDTO personDTO)
        {
            if (ModelState.IsValid)
            {
                string message =PersonService.UpdatePerson(personDTO);
                if (message == "Ok")
                {
                    TempData["msg"] = "You have successfully changed the customer";
                    TempData["color"] = "green"; ;
                }
                else
                {
                    TempData["msg"] = message;
                    TempData["color"] = "red";
                }
                return RedirectToAction("Index");
            }
            return View(personDTO);
        }
        #endregion

        #region Delete
        // GET: Person/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonDTO personDTO = new PersonDTO
            {
                IdPerson = id.Value
            };
            personDTO = PersonService.DoesItExistPearson(personDTO);
            return View(personDTO);
        }
        #endregion

        #region DeleteConfirmed
        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PersonDTO personDTO = new PersonDTO
            {
                IdPerson = id
            };
            string message = PersonService.RemovePerson(personDTO = PersonService.DoesItExistPearson(personDTO));
            if (message == "Ok")
            {
                TempData["msg"] = "You have successfully deleted the customer";
                TempData["color"] = "green"; ;
            }
            else
            {
                TempData["msg"] = message;
                TempData["color"] = "red";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region ExportCustomers  
        /// <summary>
        /// Export customer in local file
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public FileStreamResult ExportCustomers(string model)
        {
            //if user in role Admin, get all customers who added in last month, else get all customers for User CRM
            var result = CsvService.WriteCsvToMemory(User.IsInRole("Admin") ? PersonService.GetAllPerson("all", false) : PersonService.GetAllPerson(User.Identity.GetUserId(), true));
            var memoryStream = new MemoryStream(result);
            return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = "Customers.csv" };
        }
        #endregion

        #region FindPerson      
        /// <summary>
        /// FindPerson from SOAPDemo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private PersonDTO FindPerson(string id)
        {
            SOAPDemo sOAPDemo = new SOAPDemo();
            var personSoap = sOAPDemo.FindPerson(id);
            PersonDTO personDTO = new PersonDTO();
            if (personSoap != null)
            {
                if (personSoap.FavoriteColors == null)
                    personSoap.FavoriteColors = new string[] { "/" };
                
                personDTO = new PersonDTO
                {
                    Name = personSoap.Name,
                    SSN = personSoap.SSN,
                    DOB = personSoap.DOB,
                    Age = personSoap.Age,

                    StreetHome = personSoap.Home.Street,
                    CityHome = personSoap.Home.City,
                    StateHome = personSoap.Home.State,
                    ZipHome = int.Parse(personSoap.Home.Zip),

                    StreetOffice = personSoap.Office.Street,
                    CityOffice = personSoap.Office.City,
                    StateOffice = personSoap.Office.State,
                    ZipOffice = int.Parse(personSoap.Office.Zip),
                    FavoriteColors = string.Join(",", personSoap.FavoriteColors),
                };
            }
            return personDTO;
        }
        #endregion
    }
}
