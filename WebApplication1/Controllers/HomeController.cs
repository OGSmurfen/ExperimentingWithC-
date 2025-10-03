using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Game()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile)
        {
            if(uploadedFile != null && uploadedFile.Length > 0)
            {
                using var reader = new StreamReader(uploadedFile.OpenReadStream());
                var content = await reader.ReadToEndAsync();

                var lines = content.Split('\n');

                
                var xml = XDocument.Parse(content.ToLower());
                    
                var gameData = new GameData();
                var root = xml.Root;
                gameData.X = (int)root.Element("x");
                gameData.Y = (int)root.Element("y");

                ViewBag.FileContent = content;
                ViewBag.LineCount = lines.Length;

                return View("Game", gameData);
                
            }
            else
            {
                ViewBag.FileContent = "No File Selected";
            }

            return View("Game", new GameData { X = 0, Y = 0 });
        }

        [HttpPost]
        public IActionResult SaveGame([FromBody] GameData model)
        {
            var xml = new XDocument(
                new XElement("game",
                    new XElement("x", model.X),
                    new XElement("y", model.Y)
                )
            );

            var xmlString = xml.ToString();
            return File(System.Text.Encoding.UTF8.GetBytes(xmlString), "application/xml", "game.xml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
