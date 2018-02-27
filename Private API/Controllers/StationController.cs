using MySql.Data.MySqlClient;
using Private_API;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using WebCharts.Models;

namespace WebCharts.Controllers
{
    [RoutePrefix("API")]

    public class StationController : ApiController
    {
        [Route("Stations")]

        public IEnumerable<Models.MeteoValues> Get()
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

            MySqlDataAdapter da = new MySqlDataAdapter(executeStations);

            DataTable dt = new DataTable();

            da.Fill(dt);

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


            var getAllValuesList = new List<MeteoValues>()
                {
                    new MeteoValues()
                    {
                        Stations = Stations,
                    }
                };

            return getAllValuesList;

        }
    }
}