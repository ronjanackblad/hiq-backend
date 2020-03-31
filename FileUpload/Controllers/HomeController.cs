using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FileUpload.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace FileUpload.Controllers
{
    public class HomeController : Controller
    {

        private IHostEnvironment _env;

        public HomeController(IHostEnvironment env)
        {
            _env = env; 
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public string SingleFile(IFormFile file)
        {
            // POST method that accepts a file, 
            StringBuilder result = new StringBuilder();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }

            var text = result.ToString();

            var frequencies = new Dictionary<string, int>();
            string highestWord = null;
            int highestFreq = 0;

            var message = string.Join(" ", text);
            char[] splichar = new char[] { ' ', '.', ',', '\n', '_' };
            string[] single = message.Split(splichar);
            foreach (var item in single)
            {
                if (item == "")
                {

                }
                else
                {
                    int freq;
                    frequencies.TryGetValue(item, out freq);
                    freq += 1;

                    if (freq > highestFreq)
                    {
                        highestFreq = freq;
                        highestWord = item.Trim();
                    }
                    frequencies[item] = freq;
                }
            }

            string test = string.Join(" ", text);

            string modified = test.Replace(highestWord, "foo" + highestWord + "bar");

            return modified;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
