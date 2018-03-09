using System.Collections.Generic;
using System.Data;
using System;
using RestMagic.Lib.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Web;

namespace RestMagic.Lib.Data
{
    public class DataUtilities
    {
        /// <summary>
        /// Converts the data table to a JSON string.
        /// </summary>
        /// <param name="dt">The DataTable.</param>
        /// <returns>JSON formatted string representation of the DataTable</returns>
        public static string ConvertDataTabletoString(DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();

                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }

                rows.Add(row);
            }
            return JsonConvert.SerializeObject(rows);
        }


        internal static List<T> CreateObjectListFromDataSet<T>(DataSet ds, object objectToFill)
        {
            List<T> result = new List<T>();

            // also have to maintain count to expand and copy
            if (DataFactory.ValidateHasRows(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(CreateObjectFromDataRow<T>(row, objectToFill));

                }
            }

            return result;

        }

        public static List<SqlParameter> GetParameterListForFetch(DataModel objectToUse, Dictionary<string, object> parameters)
        {
            List<SqlParameter> result = new List<SqlParameter>();
            try
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        object valueToSet = param.Value;
                        var propInfo = objectToUse.GetType().GetProperty(param.Key);
                        if (propInfo != null)
                        {
                            ProcessRuleIfExists(objectToUse, ref valueToSet, propInfo, true);
                        }
                        result.Add(new SqlParameter("@" + param.Key, valueToSet));
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return result;
        }

        public static string RemoveControlCharacters(string stringToReplace)
        {
            stringToReplace = stringToReplace.Replace("\t", " ").Replace("\r", "").Replace("\n", "");
            while (stringToReplace.Contains("  "))
                stringToReplace = stringToReplace.Replace("  ", " ");
            return stringToReplace;
        }
        public static List<SqlParameter> GetParameterListForUpsert(DataModel objectToUse)
        {
            List<SqlParameter> result = new List<SqlParameter>();
            try
            {
                List<PropertyInfo> propertyInfoList = objectToUse.GetType().GetProperties().ToList<PropertyInfo>();
                foreach (PropertyInfo propertyInfo in propertyInfoList)
                {
                    if (!propertyInfo.GetCustomAttributes().Contains(new ExcludeAsSqlParam()))

                    {
                        object valueToSet;
                        if (!propertyInfo.PropertyType.Name.ToUpper().Contains("ENUM"))
                            valueToSet = propertyInfo.GetValue(objectToUse);
                        else
                            valueToSet = Convert.ChangeType(propertyInfo.GetValue(objectToUse), Enum.GetUnderlyingType(propertyInfo.PropertyType));

                        ProcessRuleIfExists(objectToUse, ref valueToSet, propertyInfo, true);

                        result.Add(new SqlParameter("@" + propertyInfo.Name, valueToSet));
                    }
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return result;
        }


 

        public static T CreateObjectFromDataRow<T>(DataRow row, object objectToFill)
        {
            Dictionary<object, object> getRules = (objectToFill as DataModel).RuleSet;

            if (row != null && row.Table != null)
            {
                foreach (DataColumn dc in row.Table.Columns)
                {
                    if (!dc.ColumnName.ToUpper().Contains("UPDATEDATE"))
                    {
                        var valueToSet = row[dc] == DBNull.Value ? null : row[dc];

                        PropertyInfo propertyInfo = objectToFill.GetType().GetProperty(dc.ColumnName);
                        // if (propertyInfo == null) Debugger.Break();
                        ProcessRuleIfExists(objectToFill, ref valueToSet, propertyInfo, false);
                        propertyInfo.SetValue(objectToFill, valueToSet == null ? null : Convert.ChangeType(valueToSet, propertyInfo.PropertyType), null);


                    }

                }

            }
            return (T)objectToFill;
        }

        public static void ProcessRuleIfExists(object objectToProcess, ref object valueToSet, PropertyInfo propertyInfo, bool IsParameter)
        {
            // see if there is a rule   
            object ruleByName = (objectToProcess as DataModel).RuleSet.ContainsKey(propertyInfo.Name) ? (objectToProcess as DataModel).RuleSet[propertyInfo.Name] : null;
            object ruleByType = (objectToProcess as DataModel).RuleSet.ContainsKey(propertyInfo.PropertyType) ? (objectToProcess as DataModel).RuleSet[propertyInfo.PropertyType] : null;

            valueToSet = DataUtilities.ProcessRules(ruleByName, ruleByType, valueToSet, IsParameter);

        }


        internal static List<T> ConvertDataSetToList<T>(DataSet ds)
        {
            return new List<T>();
        }



        internal static object ProcessRules(object ruleByName, object ruleByType, object valueToSet, bool isParameter)
        {
            object newValue = null;
            var rule = ruleByName != null ? ruleByName : ruleByType; // if it's a named rule, use that first otherwise use the type

            if (rule != null)
            {
                Type castTypeParameter = (rule as RulesCaster).CastTypeParameter;
                Type castTypeFill = (rule as RulesCaster).CastTypeFill;

                if (rule.GetType() == typeof(RulesCaster))
                {
                    if (isParameter && castTypeParameter != null)
                    {
                        newValue = Convert.ChangeType(valueToSet, castTypeParameter);
                    }
                    if (!isParameter && (castTypeFill != null))
                    {
                        if (castTypeFill.Name.ToUpper().Contains("ENUM"))
                            newValue = Enum.ToObject(castTypeFill, valueToSet);
                        else
                            newValue = Convert.ChangeType(valueToSet, (rule as RulesCaster).CastTypeFill);
                    }
                }
            }
            else
            {
                newValue = valueToSet;
            }
            return newValue;
        }

        internal static void RebuildDBObjects(SqlCommand command)
        {
            try
            {
                // clear params for recall
                command.Parameters.Clear();
                switch (command.CommandType)
                {
                    case CommandType.StoredProcedure:
                        // rebuild the sproc
                        string scriptsDirectory = DataFactory.WebDirectory + command.CommandText + ".sql";
                        var sqlText = File.ReadAllText(scriptsDirectory);
                        string[] scripts = sqlText.Split(new string[] { "GO" }, StringSplitOptions.None);
                        foreach (string script in scripts)
                        {
                            DataFactory.ExecuteNonquery(script);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
  
    public class FieldName : Attribute
    {
        private readonly string _field;
        public FieldName(string field)
        {
            _field = field;
        }

        public string Field => _field;
    }

    public class PrimaryKey : Attribute
    {

    }

    public class NullCaster
    {

    }
    public class RulesCaster
    {
        public Type CastTypeParameter { get; set; }
        public Type CastTypeFill { get; set; }

    }

}
