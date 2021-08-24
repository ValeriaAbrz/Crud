using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DL
{
    public class Conexion
    {
        public static string GetConexion()
        {
            return ConfigurationManager.ConnectionStrings["Estructura"].ConnectionString;
        }
    }
}
