using RestMagic.DataManager.Data;
using RestMagic.Lib.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestMagic.DataManager
{
    public class Processor
    {
        public void Generate(string[] dataModels, MetaData metaData)
        {
            InitializeSql();
            var metaDataDetails = metaData.DataModelDetails.ToList();
            foreach (var dataModel in dataModels)
            {
                GenerateCodeForModel(dataModel, metaDataDetails.Where(m => m.DataModelName == dataModel));
            }
        }

        private void GenerateCodeForModel(string dataModel, IEnumerable<MetaData.DataModelDetailsRow> modelDetails)
        {
            {

                // build code metadata
                CodeMetaData codeMetaData = new CodeMetaData()
                {
                    ModelName = dataModel,
                    Parameters = GenerateCodeByTypeSql("xfnParamCreator", modelDetails),
                    Sql = GenerateCodeByType("Sql", modelDetails),
                    Properties = GenerateCodeByTypeSql("xfnModelCreator", modelDetails),
                    WhereClause = GenerateCodeByTypeSql("xfnIIFCreator", modelDetails),
                    TableList = GenerateCodeByType("TableList", modelDetails),

                };
                // replace templates
                CreateFiles(codeMetaData);

            }

        }

        private void CreateFiles(CodeMetaData codeMetaData)
        {
           
        }

        private List<string> GenerateCodeByType(string codeType, IEnumerable<MetaData.DataModelDetailsRow> modelDetails)
        {
            List<string> result = new List<string>();
            switch (codeType)
            {
                case "Sql":
                    foreach (MetaData.DataModelDetailsRow row in modelDetails)
                    {
                        result.Add(row.SourceTableName.ToString() + "." + row.SourceFieldName.ToString() + " AS [" + row.DataFieldName  +"],");
                    }
                        break;
                case "TableList":
                    foreach (MetaData.DataModelDetailsRow row in modelDetails)
                    {
                        if (!result.Contains(row.SourceTableName.ToString() + ","))
                            result.Add(row.SourceTableName.ToString() + ",");
                    }
                    break;
            }
            return result;
        }
        private List<string> GenerateCodeByTypeSql(string codeType, IEnumerable<MetaData.DataModelDetailsRow> modelDetails)
        {
            List<string> result = new List<string>();
            foreach (MetaData.DataModelDetailsRow row in modelDetails)
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                      new SqlParameter("TableName",row.SourceTableName),
                      new SqlParameter("ColumnName",row.SourceFieldName),
                      new SqlParameter("FieldName",row.DataFieldName),

                };

                DataSet ds = DataFactory.GetDataSetStatic(codeType, parameters);
                if (DataFactory.ValidateHasRows(ds))
                {
                    result.Add(ds.Tables[0].Rows[0].ToString());
                }
            }
            return result;
        }
        private void InitializeSql()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            // C:\Users\patri\source\repos\RestMagic\Test\RestMagic.UnitTests\bin\Debug\RestMagic.UnitTests.dll
            // C:\Users\patri\source\repos\RestMagic\Test\RestMagic.Tools.UnitTests\bin\Debug\RestMagic.DataManager.exe
            string restMagicServiceDirectory = assemblyLocation.Replace(@"Test\RestMagic.UnitTests\bin\Debug\RestMagic.UnitTests.dll", @"Main\RestMagic.RestService");
            restMagicServiceDirectory = restMagicServiceDirectory.Replace(@"Test\RestMagic.Tools.UnitTests\bin\Debug\RestMagic.DataManager.exe", @"Main\RestMagic.RestService");
            var databaseDirectory = restMagicServiceDirectory + @"\App_Data";

            AppDomain.CurrentDomain.SetData("DataDirectory", databaseDirectory);
            DataFactory.PrimaryConnectionString = ConfigurationManager.ConnectionStrings["Sample"].ConnectionString;
        }
    }


    class CodeMetaData
    {
        public string ModelName { get; set; }
        public List<string> Parameters { get; set; } = new List<string>();
        public List<string> Sql { get; set; } = new List<string>();
        public List<string> Properties { get; set; } = new List<string>();
        public List<string> WhereClause { get; set; } = new List<string>();
        public List<string> TableList { get; set; } = new List<string>();

        internal string FormatListToString(List<string> list, string separator, int tabCount = 2)
        {
            string result = string.Empty;
            string tabs = new String('\t', tabCount);
            foreach (var item in list)
            {
                result += tabs + item + separator + "\n";
            }

            // TODO trim end
            return result;
        }
    }

}
