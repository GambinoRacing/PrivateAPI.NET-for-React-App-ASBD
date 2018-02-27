using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCharts.Models
{
    public class Stations
    {
        public string value { get; set; }

        public string label { get; set; }

       // public string Cell { get; set; }

    }

    public class MeteoData
    {
        public string Date { get; set; }

        public string Temp { get; set; }

        public string Rain { get; set; }

        public string WindSpeed { get; set; }

        public string Snow { get; set; }

        public string Apress { get; set; }

    }

    public class MeteoValues
    {
        public MeteoValues()
        {
            this.Stations = new List<Stations>();

            this.MeteoData = new List<MeteoData>();
        }

        public List<Stations> Stations { get; set; }

        public List<MeteoData> MeteoData { get; set; }

    }
}

