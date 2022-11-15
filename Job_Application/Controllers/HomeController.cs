using Job_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SampleCore.Core.IServices;
using SampleCore.Core.Model;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using ExcelDataReader;
using ClosedXML.Excel;

namespace Job_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationServices _ApplicationServices;
        private readonly object JobApplicationContext;
        private IConfiguration configuration;
        private IHostingEnvironment Environment;

        public HomeController(IApplicationServices services, IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            Environment = _environment;
            this.configuration = configuration;
            _ApplicationServices = services;
        }


        public IActionResult Index()
        {
            return View();
        }
       
        [HttpPost]      
        public IActionResult Index(IFormFile postedFile, Application data)
        {
            

            if (postedFile != null)
            {
                //Create a Folder.
               string path = Path.Combine(this.Environment.WebRootPath, "Uploads");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Save the uploaded Excel file.
                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                data.ApplicantResume = filePath;
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        while (reader.Read()) //Each row of the file
                        {
                          
                            data.FullName = reader.GetValue(0).ToString();                          
                            data.HSCMarks = reader.GetValue(1).ToString();
                            data.SSLCMarks = reader.GetValue(2).ToString();
                            data.GraduationPercent = reader.GetValue(3).ToString();
                            data.Interests = reader.GetValue(4).ToString();
                            data.Skills = reader.GetValue(5).ToString();
                        }
                    }
                }
          

            }
            
            _ApplicationServices.CreateApplicationEntry(data);
           
           
            return RedirectToAction("Read");
        }
        #region Read data from table
        public IActionResult Read()
        {
            var value = _ApplicationServices.Reads();
            return View(value);

        }
        #endregion


        [HttpGet]
        public IActionResult Detail(int person_id)
        {
            var value = _ApplicationServices.Detail(person_id);
            return PartialView(value);
        }

       
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var filePath = Path.Combine(
                           Directory.GetCurrentDirectory(),
                          this.Environment.WebRootPath, "Uploads", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }



    }

}
    
