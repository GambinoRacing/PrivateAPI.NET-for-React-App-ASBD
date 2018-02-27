using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using WebCharts.Models;
using System.Globalization;
using Private_API;

namespace WebCharts.Controllers
{
    [RoutePrefix("API")]
    public class DataController : ApiController
    {
        //Data/date={date}/station={id}/days={days}
        [Route("")]

        public MeteoValues Get(string date, int station, int days)
        {
            MySqlConnection conn = WebApiConfig.conn();

            try
            {
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                string err = "Error: " + ex;
            }

            MySqlCommand executeStations = new MySqlCommand("select Stationsall.station,stationsall.Ime from stationsall join stations_cells using (Station);", conn);

            MySqlCommand executeData = new MySqlCommand($"CALL Get_mod_cell_values_meteogram ('{date}',{station},{days});", conn);

            MySqlDataAdapter da = new MySqlDataAdapter(executeStations);

            MySqlDataAdapter da1 = new MySqlDataAdapter(executeData);

            DataTable dt = new DataTable();

            DataTable dt1 = new DataTable();

            da.Fill(dt);

            da1.Fill(dt1);

            /*
            Result result = new Result();

            foreach (DataRow dr in dt.Rows)
            {
                Stations currentStations = new Stations()
                {
                    Station = dr["station"].ToString(),
                    NameStation = dr["Ime"].ToString()
                };

                result.Stations.Add(currentStations);

            }
            */

            MeteoValues Result = new MeteoValues();

            List<Stations> Stations = new List<Stations>();

            foreach (DataRow dr in dt.Rows)
            {
                Stations getAllStations = new Stations
                {
                    value = dr["station"].ToString(),
                    label = dr["Ime"].ToString()
                };

                Stations.Add(getAllStations);
            }

            List<MeteoData> MeteoData = new List<MeteoData>();

            foreach (DataRow dr1 in dt1.Rows)
            {
                MeteoData getAllData = new MeteoData
                {
                    Date = dr1["DATS"].ToString(),
                    Temp = dr1["TA"].ToString(),
                    Rain = dr1["RH"].ToString(),
                    WindSpeed = dr1["WS"].ToString(),
                    Snow = dr1["SR"].ToString(),
                    Apress = dr1["APRES"].ToString()
                };

                MeteoData.Add(getAllData);
            }

            Result.MeteoData = MeteoData;

            return Result;
        }
    }
}