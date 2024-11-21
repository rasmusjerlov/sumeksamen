using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SumEksamen.Controllers;
using SumEksamen.Models;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using System.Linq;

namespace SumEksamen.Tests
{
    public class KøkkenholdControllerTest {
        private readonly KøkkenholdController _controller;

        

        [Fact]
        public void Index_ReturnsViewResult()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void UploadGet_ReturnsViewWithElevList()
        {
            var result = _controller.Upload() as ViewResult;
            Assert.NotNull(result);
            Assert.IsType<List<Elev>>(result.Model);
        }

        [Fact]
        public void UploadExcel_NullFile_ReturnsBadRequest()
        {
            var result = _controller.UploadExcel(null) as BadRequestObjectResult;
            Assert.NotNull(result);
            Assert.Equal("Upload a valid file.", result.Value);
        }

        [Fact]
        public void UploadExcel_InvalidGender_ReturnsBadRequest()
        {
            // Mock Excel file with invalid Køn value
            var file = CreateExcelFile(new List<(string Navn, string Køn)>
            {
                ("John", "invalid")
            });

            var result = _controller.UploadExcel(file) as BadRequestObjectResult;
            Assert.NotNull(result);
            Assert.Contains("Invalid value for Køn", result.Value.ToString());
        }

        [Fact]
        public void UploadExcel_ValidFile_PopulatesElevList()
        {
            var file = CreateExcelFile(new List<(string Navn, string Køn)>
            {
                ("John", "dreng"),
                ("Anna", "pige")
            });

            var result = _controller.UploadExcel(file) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(2, ((List<Elev>)result.Model).Count);
            Assert.Equal("John", ((List<Elev>)result.Model)[0].Navn);
        }

        [Fact]
        public void CreateKøkkenhold_ValidData_CreatesKøkkenholdList()
        {
            // Prepopulate elevListe with students
            _controller.UploadExcel(CreateExcelFile(new List<(string Navn, string Køn)>
            {
                ("John", "dreng"),
                ("James", "dreng"),
                ("Anna", "pige"),
                ("Sara", "pige")
            }));

            var result = _controller.CreateKøkkenhold() as ViewResult;

            Assert.NotNull(result);
            var køkkenholdListe = result.Model as List<Køkkenhold>;
            Assert.NotNull(køkkenholdListe);
            Assert.Single(køkkenholdListe);
            Assert.Equal(4, køkkenholdListe[0].GetElevListe().Count());
        }
        
        private IFormFile CreateExcelFile(List<(string Navn, string Køn)> data)
        {
            // Create a memory stream with Excel package data
            using var packageStream = new MemoryStream();
            using var package = new ExcelPackage(packageStream);

            var worksheet = package.Workbook.Worksheets.Add("Elev");
            worksheet.Cells[1, 1].Value = "Navn";
            worksheet.Cells[1, 2].Value = "Køn";

            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = data[i].Navn;
                worksheet.Cells[i + 2, 2].Value = data[i].Køn;
            }

            package.Save();

            // Copy the package stream to a new memory stream
            var fileStream = new MemoryStream(packageStream.ToArray());
            fileStream.Position = 0;  // Reset position to the start of the stream

            // Return a FormFile that uses this new, non-disposed stream
            return new FormFile(fileStream, 0, fileStream.Length, "file", "test.xlsx");
        }
        
        [Fact]
        public void TC1_OpretterKøkkenhold()
        {
            // Arrange
            VentelisteController ventelisteController = new VentelisteController();
            var controller = new KøkkenholdController(ventelisteController);
            ventelisteController.Opretventeliste("2025/2026");
            ventelisteController.TilfoejElev("2025/2026", "mikkel", "dreng");
            ventelisteController.TilfoejElev("2025/2026", "mikkel2", "dreng");
            ventelisteController.TilfoejElev("2025/2026", "mikkel3", "dreng");
            ventelisteController.TilfoejElev("2025/2026", "mikkel4", "dreng");

            // Act
            controller.ElevlisteFraVenteliste("2025/2026");
            var result = controller.CreateKøkkenhold() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var køkkenholdListe = result.Model as List<Køkkenhold>;
            Assert.NotNull(køkkenholdListe);
            Assert.Equal(4, køkkenholdListe[0].GetElevListe().Count());
        }
    }
}