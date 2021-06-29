using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using MySql.Data.Common;

using MySql.Data.Types;

namespace localHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service2" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service2.svc or Service2.svc.cs at the Solution Explorer and start debugging.
    public class Service2 : IService2
    {
        public HanbiroDemo HanbiroGetInfo(string email)
        {
            string query = string.Format("SELECT DISTINCT start_date, end_date, type FROM view_vacation_request WHERE email = '{0}';", email);
            HanbiroDemo hanbiro = new HanbiroDemo();
            string connectionString = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand cmd = new MySqlCommand(query, connection);

            DataSet dt = new DataSet();
            MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
            mda.Fill(dt);
            hanbiro.HanbiroGetDemo = dt;

            return hanbiro;
        }
    }
}
