using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestMagic.Lib.Data
{
    public class SchemaFactory
    {
    
        public  DataTable GetSchemaTable(string connectionString)
        {
            using (OleDbConnection connection = new
            OleDbConnection(connectionString))
            {
                connection.Open();
                DataTable schemaTable = connection.GetOleDbSchemaTable(
                OleDbSchemaGuid.Tables,
                new object[] { null, null, null, "TABLE" });
                return schemaTable;
            }
        }


        public static object TopRow(DataSet ds, string columnName = "")
        {
            if (!ValidateHasRows(ds))
                return null;
            if (columnName.Length == 0)
                return ds.Tables[0].Rows[0];
            else
                return ds.Tables[0].Rows[0][columnName];
        }
        public static bool ValidateHasRows(DataSet ds, int TableCount = 1)
        {
            if (ds == null)
                return false;

            if (ds.Tables.Count < TableCount)
                return false;

            int tableCounter = 0;
            bool hasDataInEachTable = true;
            foreach (DataTable table in ds.Tables)
            {
                if (table.Rows.Count == 0)
                {
                    hasDataInEachTable = false;
                    break;
                }
                tableCounter++;
                if (tableCounter >= TableCount)
                    break;
            }

            return hasDataInEachTable;
        }

    }
}
