using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace SumEksamen.Controllers
{
    public class KøkkenholdController : Controller
    {
        private static List<Elev> elevListe = new List<Elev>();

        // GET: KøkkenholdController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("upload")]
        public IActionResult Upload()
        {
            return View(elevListe);
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Upload a valid file.");

            elevListe.Clear();

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
                        if (Enum.TryParse(worksheet.Cells[row, 2].Text, out Køn køn))
                        {
                            var elev = new Elev(navn, køn);
                            elevListe.Add(elev);
                        }
                        else
                        {
                            // Handle the case where the enum value is not valid
                            return BadRequest($"Invalid value for Køn at row {row}");
                        }
                    }
                }
            }

            return View("Upload", elevListe);
        }

        [HttpPost]
        [Route("createKøkkenhold")]
        public IActionResult CreateKøkkenhold()
        {
            var køkkenholdListe = new List<Køkkenhold>();
            for (int i = 0; i < elevListe.Count; i += 4)
            {
                if (i + 4 <= elevListe.Count)
                {
                    var køkkenhold = new Køkkenhold(elevListe.GetRange(i, 4).ToArray());
                    køkkenholdListe.Add(køkkenhold);
                }
            }
            // Hej MAZZA
            return View("Køkkenhold", køkkenholdListe);
        }
    }
}