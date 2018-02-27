using MySql.Data.MySqlClient;
using Private_API;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using WebCharts.Models;

namespace WebCharts.Controllers
{
    [RoutePrefix("API")]

    public class ValuesController : ApiController
    {

        [Route("AllData")]
        // GET api/Stations

        public IEnumerable<MeteoValues> Get()
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

            MySqlCommand executeData = new MySqlCommand("CALL Get_mod_cell_values_meteogram ('2018-01-27 00:00:00',42020,5);", conn);

            MySqlDataAdapter da = new MySqlDataAdapter(executeStations);

            MySqlDataAdapter da1 = new MySqlDataAdapter(executeData);

            DataTable dt = new DataTable();

            DataTable dt1 = new DataTable();

            da.Fill(dt);

            da1.Fill(dt1);

            List<Stations> Stations = new List<Stations>();

            foreach (DataRow dr in dt.Rows)
            {
                Stations getAllStations = new Stations
                {
                    value = dr["station"].ToString(),
                    label = dr["Ime"].ToString(),
                };

                Stations.Add(getAllStations);
            }

            List<MeteoData> MeteoData = new List<MeteoData>();

            foreach (DataRow dr1 in dt1.Rows)
            {
                MeteoData getMeteoData = new MeteoData
                {
                    Date = dr1["DATS"].ToString(),
                    Temp = dr1["TA"].ToString(),
                    Rain = dr1["RH"].ToString(),
                    WindSpeed = dr1["WS"].ToString(),
                    Snow = dr1["SR"].ToString(),
                    Apress = dr1["APRES"].ToString()
                };

                MeteoData.Add(getMeteoData);
            }

            var getAllValuesList = new List<MeteoValues>()
                {
                    new MeteoValues()
                    {
                        Stations = Stations,
                        MeteoData = MeteoData
                    }
                };

            return getAllValuesList;

        }
    }
}
