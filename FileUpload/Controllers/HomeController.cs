using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FileUpload.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace FileUpload.Controllers
{
    public class HomeController : Controller
    {

        [HttpPost]
        public ActionResult<ResponseModel> SingleFile(IFormFile file)
        {
            // POST method that accepts a file and returns json 
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

            var response = new ResponseModel();

            response.MostUsedWord = highestWord;
            response.Text = modified;
            response.Frequencies = highestFreq;

            return response;

        }

    }
}
