using BulkInsertData.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;

using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Azure;
using static System.Net.WebRequestMethods;
using static System.Net.Mime.MediaTypeNames;

namespace BulkInsertData.Controllers
{
    public class PatientInfoController : Controller
    {

        private readonly AppDbContext _appDbContext;
        private DateTime Start;
        private TimeSpan TimeSpan;

        public PatientInfoController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<TimeSpan> AddBulkDataAsync(string filepath)
        {
            //Upload and save the file.
          
            List<PatientInfo> PatientInfos = System.IO.File.ReadAllLines(filepath)
                                        .Skip(1)
                                        .Select(v => PatientInfo.FromCsv(v))
                                        .ToList();
            await _appDbContext.BulkInsertAsync(PatientInfos);
            TimeSpan = DateTime.Now - Start;
            return TimeSpan;
        }


        [HttpGet]
        public IActionResult Index()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile formFile)
        {
            try {
            string filePath = Path.GetFullPath(formFile.FileName);
            filePath.Replace("\"","\\");
            await AddBulkDataAsync(filePath);
            ViewBag.Message = "Data Update Successfully";
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Data not updated !";
            }
            return View();
        }




    }

}
