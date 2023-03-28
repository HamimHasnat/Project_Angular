using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Project_Angular.Controllers
{
    public class AppointmentService : Controller
    {
        private MyDBContext db;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _HostEnvironment;
        public AppointmentService(MyDBContext _db, Microsoft.AspNetCore.Hosting.IHostingEnvironment HostEnvironment)
        {
            db = _db;
            _HostEnvironment = HostEnvironment;
        }


        [HttpPost]
        public async Task<IActionResult> Post(IFormFile files)
        {
            string filename = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
            filename = this.EnsureCorrectFilename(filename);
            using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                await files.CopyToAsync(output);
            return Ok();
        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }
        private string GetPathAndFilename(string filename)
        {
            return Path.Combine(_HostEnvironment.WebRootPath, "uploads", filename);
        }
        [HttpPost]
        public string UseMe([FromBody] string md)
        {
            return md;
        }
        [HttpPost]
        public string AddAppointmentServiceVm([FromBody] AppointmentServiceVm md)
        {
            RemoveAppointmentServiceVm(md.Appointment.Appointment_ID);
            Appointment m = new Appointment() { Appointment_ID = md.Appointment.Appointment_ID, Appointment_Name = md.Appointment.Appointment_Name, Date = md.Appointment.Date, Phone = md.Appointment.Phone };
            db.Appointment.Add(m);
            db.SaveChanges();
            foreach (var c in md.Service)
            {
                Service d = new Service()
                {
                    Service_ID = c.Service_ID,
                    Service_Name = c.Service_Name,
                    Appointment_ID = c.Appointment_ID,
                    Service_Fee = c.Service_Fee,
                    Picture = c.Picture,
                    //date = DateTime.Parse(c.date.ToShortDateString()),

                };
                db.Service.Add(d);
            }
            db.SaveChanges();
            return "1";
        }



        [HttpPost]
        public string RemoveAppointmentServiceVm(string id)
        {
            List<Service> st5 = db.Service.Where(xx => xx.Appointment_ID == id).ToList();
            db.Service.RemoveRange(st5);
            db.SaveChanges();
            Appointment st6 = db.Appointment.Find(id);
            if (st6 != null)
            {
                db.Appointment.Remove(st6);
            }
            db.SaveChanges();

            return "1";
        }

        public JsonResult GetAllAppointment()
        {
            var a = (from d in db.Appointment select new { d.Appointment_ID, d.Appointment_Name, d.Date, d.Phone });
            return Json(a);
        }

        public JsonResult GetAppointment(string id)
        {
            var a = (from d in db.Appointment where d.Appointment_ID == id select new { d.Appointment_ID, d.Appointment_Name, d.Date, d.Phone });
            return Json(a);
        }
        public JsonResult GetIService(string id)
        {
            var a = (from d in db.Service where d.Appointment_ID == id select new { d.Service_ID, d.Service_Name, d.Service_Fee, d.Picture });
            return Json(a);
        }
        public JsonResult GetAllService()
        {
            var a = (from d in db.Service select new { d.Service_ID, d.Service_Name, d.Service_Fee, d.Picture, d.Appointment_ID });
            return Json(a);
        }
        public ActionResult ShowMe()
        {
            IEnumerable<Appointment> s = db.Appointment.ToList();
            return View(s);
        }

        public ActionResult ShowItems(string sid = "0")
        {
            List<Service> s = db.Service.Where(xx => xx.Appointment_ID == sid).ToList();
            return View(s);
        }

        public ActionResult Create2(string sid = "0")
        {
            ViewBag.sid = sid;
            return View();
        }



        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object
            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object
                    var files = Request.Form.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";
                        //string filename = Path.GetFileName(Request.Files[i].FileName);

                        IFormFile file = files[i];
                        string fname;

                        fname = file.FileName;

                        // Get the complete folder path and store the file inside it.
                        //fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        string webRootPath = _HostEnvironment.WebRootPath;
                        string fname1 = "";
                        fname1 = Path.Combine(webRootPath, "Uploads/" + fname);
                        file.CopyTo(new FileStream(fname1, FileMode.Create));
                    }
                    // Returns message that successfully uploaded
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
    }

}
