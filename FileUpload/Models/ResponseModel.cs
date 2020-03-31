using System;

namespace FileUpload.Models
{
    public class ResponseModel
    {
        public string MostUsedWord { get; set; }
        public string Text { get; set; }
        public int Frequencies { get; set; }
    }
}
