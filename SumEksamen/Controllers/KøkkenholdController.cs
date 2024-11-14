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
        VentelisteController ventelisteController = new VentelisteController();
        private static List<Elev> elevListe = new List<Elev>();
        private static List<Køkkenhold> køkkenholdListe = new List<Køkkenhold>();
        
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
                            // Fortæller hvis en enum værdi er ugyldig og hvor man kan finde den i excel arket
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
            køkkenholdListe.Clear();
            var drenge = elevListe.Where(e => e.Køn == Køn.dreng).ToList();
            var piger = elevListe.Where(e => e.Køn == Køn.pige).ToList();

            while (drenge.Count >= 2 && piger.Count >= 2) // Laver køkkenhold med 2 drenge og 2 piger
            {
                var køkkenhold = new Køkkenhold(drenge.Take(2).Concat(piger.Take(2)).ToArray());
                køkkenholdListe.Add(køkkenhold);
                drenge.RemoveRange(0, 2);
                piger.RemoveRange(0, 2);
            }

            // Hvis der er elever tilbage, laves et køkkenhold med de resterende elever
            // Der er et flertal af drenge -> Så der bliver lavet et køkkenhold med 4 drenge
            var remaining = drenge.Concat(piger).ToList();
            for (int i = 0; i < remaining.Count; i += 4)
            {
                if (i + 4 <= remaining.Count)
                {
                    var køkkenhold = new Køkkenhold(remaining.GetRange(i, 4).ToArray());
                    køkkenholdListe.Add(køkkenhold);
                }
            }

            return View("Køkkenhold", køkkenholdListe);
        }


        [HttpPost]
        [Route("exportToExcel")]
        public IActionResult ExportToExcel()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Køkkenhold");

                // Add headers
                worksheet.Cells[1, 1].Value = "Køkkenhold";
                worksheet.Cells[1, 2].Value = "Navn";
                worksheet.Cells[1, 3].Value = "Køn";

                int row = 2;
                foreach (var køkkenhold in køkkenholdListe)
                {
                    foreach (var elev in køkkenhold.GetElevListe())
                    {
                        worksheet.Cells[row, 1].Value = køkkenhold.UgeNr;
                        worksheet.Cells[row, 2].Value = elev.Navn;
                        worksheet.Cells[row, 3].Value = elev.Køn.ToString();
                        row++;
                    }
                    row++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"Køkkenhold-{DateTime.Now.ToString("yyyyMM")}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }

        public void ElevlisteFraVenteliste(string aargang)
        {
            elevListe = ventelisteController.VentelisteTilElevliste(aargang);
        }
        
        
    }
}