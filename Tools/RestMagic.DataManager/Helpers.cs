using RestMagic.Lib.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestMagic.DataManager
{
    public static class Helpers
    {
        public static string InitConnectionString()
        {
            string root = @"\RestMagic";
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            assemblyLocation = assemblyLocation.Substring(0,assemblyLocation.IndexOf(root) + root.Length);
            string restMagicServiceDirectory = assemblyLocation+ @"\Main\RestMagic.RestService";
            var databaseDirectory = restMagicServiceDirectory + @"\App_Data";

            AppDomain.CurrentDomain.SetData("DataDirectory", databaseDirectory);
            DataFactory.PrimaryConnectionString = ConfigurationManager.ConnectionStrings["Sample"].ConnectionString;
            return DataFactory.PrimaryConnectionString;
        }
    }
}
