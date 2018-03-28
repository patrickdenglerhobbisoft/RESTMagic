using RestMagic.DataManager.Data;
using RestMagic.Lib.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestMagic.DataManager
{
    public class Processor
    {
        public void Generate(string[] dataModels, MetaData metaData,TextBox textBox = null)
        {

            var metaDataDetails = metaData.DataModelDetails.ToList();
            foreach (var dataModel in dataModels)
            {
                GenerateCodeForModel(dataModel, metaDataDetails.Where(m => m.DataModelName == dataModel), textBox);
            }
        }

        private void GenerateCodeForModel(string dataModel, IEnumerable<MetaData.DataModelDetailsRow> modelDetails, TextBox textBox = null)
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
                CreateFiles(codeMetaData, textBox);

            }

        }

        private void CreateFiles(CodeMetaData codeMetaData, TextBox textBox = null)
        {
            string modelName = codeMetaData.ModelName;
            string dataModelText = LoadTemplate("datamodel.txt");
            string dataModelBaseText = LoadTemplate("datamodelbase.txt");
            string sql = LoadTemplate("sql.txt");
            string sprocWrapper = LoadTemplate("sprocwrapper.txt");

            sql = ReplaceTokens(codeMetaData, sql);
            codeMetaData.AllSql = sql;
            dataModelText = ReplaceTokens(codeMetaData, dataModelText);
            dataModelBaseText = ReplaceTokens(codeMetaData, dataModelBaseText);
          
            sprocWrapper = ReplaceTokens(codeMetaData, sprocWrapper);

            string serviceDirectory = Helpers.GetRestServiceDirectory();

            GenerateFiles(dataModelText, modelName + ".cs", serviceDirectory + @"Models\", textBox);
            GenerateFiles(dataModelBaseText, modelName + ".cs", serviceDirectory + @"Models\Base\", textBox);
            GenerateFiles(sprocWrapper, modelName + ".sql", serviceDirectory + @"Sql\", textBox);
 
        }

        private void GenerateFiles(string dataModelText, string fileName, string location, TextBox textBox =null)
        {
            File.WriteAllText(location + fileName, dataModelText);
            if (textBox != null)
            {
                textBox.Text += "File " + location + fileName + " written." + Environment.NewLine; 
            }
        }

        private string ReplaceTokens(CodeMetaData codeMetaData, string dataModelText)
        {
            var result = dataModelText.Replace("<DataModelName />", codeMetaData.ModelName)
                    .Replace("<Parameters />", codeMetaData.FormatListToString(codeMetaData.Parameters, ""))
                    .Replace("<Sql />", codeMetaData.AllSql)
                    .Replace("<Properties />", codeMetaData.FormatListToString(codeMetaData.Properties, ""))
                    .Replace("<WhereClause />", codeMetaData.FormatListToString(codeMetaData.WhereClause, ""))
                    .Replace("<TableList />", codeMetaData.FormatListToString(codeMetaData.TableList, ""));
            return result;
        }

     
        private string LoadTemplate(string templateName)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Templates\";
            return File.ReadAllText(path + templateName);

        }
        private List<string> GenerateCodeByType(string codeType, IEnumerable<MetaData.DataModelDetailsRow> modelDetails)
        {
            List<string> result = new List<string>();
            switch (codeType)
            {
                case "Sql":
                    foreach (MetaData.DataModelDetailsRow row in modelDetails)
                    {
                        result.Add(row.SourceTableName.ToString() + "." + row.SourceFieldName.ToString() + " AS [" + row.DataFieldName + "],");
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
                    result.Add(ds.Tables[0].Rows[0][0].ToString());
                }
            }
            return result;
        }

    }


    class CodeMetaData
    {
        internal string AllSql;

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
