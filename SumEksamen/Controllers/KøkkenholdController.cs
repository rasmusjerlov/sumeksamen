using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace SumEksamen.Controllers
{
    public class KøkkenholdController : Controller
    {
        // GET: KøkkenholdController
        public ActionResult Index()
        {
            return View();
        }
        
        
        [HttpGet]
        [Route("upload")]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Upload a valid file.");

            var elevListe = new List<Elev>();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        string navn = worksheet.Cells[row, 1].Text;
                        Køn køn = (Køn)Enum.Parse(typeof(Køn), worksheet.Cells[row, 2].Text);

                        var elev = new Elev(navn, køn);
                        elevListe.Add(elev);
                    }
                }
            }

            Console.WriteLine(elevListe);

            return Ok(elevListe);
        }

    }
}
