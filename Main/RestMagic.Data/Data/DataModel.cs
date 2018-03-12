using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;
using RestMagic.Lib.Language;

namespace RestMagic.Lib.Data
{

    public class DataModel
    {
        private string parameterizedQuery;
        public string ParameterizedQuery
        {
            get { return parameterizedQuery; }
            set { parameterizedQuery = value; }
        }
        private string ModelName
        {
            get { return this.GetType().Name; }
        }
        internal Type ModelType
        {
            get { return this.GetType(); }
        }

        private string DBAccessText = null;

        public virtual List<T> Get<T>(QueryModel queryModel)
        {
            string dataModel = queryModel.DataModel;

            var objectTypeInstance = Activator.CreateInstance<T>();
            DataFactory dbLayer = new DataFactory();
            DataSet ds = null;

            if (DBAccessText == null)
            {
                DBAccessText = "Get" + ModelName;
            }
            ds = dbLayer.GetDataSet(DBAccessText, DataUtilities.GetParameterListForFetch(objectTypeInstance as DataModel, queryModel.Parameters));
            dbLayer.Dispose();

            return DataUtilities.CreateObjectListFromDataSet<T>(ds, objectTypeInstance);
        }

    

    
 
    public virtual bool Upsert(QueryModel queryModel)
    {
        DataFactory dbLayer = new DataFactory();
        long returnValue = 0;
        dbLayer.ExecuteNonQuery("Upsert" + queryModel.DataModel, DataUtilities.GetParameterListForUpsert(this), ref returnValue);
        dbLayer.Dispose();
        return (returnValue > 0);
    }


    [ExcludeAsSqlParam]
    [JsonIgnore]
    public Dictionary<object, object> RuleSet { get; set; } = new Dictionary<object, object>();

}

    //public virtual List<T> GetFromQuery<T>(Dictionary<string, object> parameters, string queryString)
    //{
    //    var objectTypeInstance = Activator.CreateInstance<T>();
    //    DataFactory dbLayer = new DataFactory();

    //    DataSet ds = dbLayer.GetDataSet(queryString, DataUtilities.GetParameterListForFetch(objectTypeInstance as DataModel, parameters));
    //    dbLayer.Dispose();

    //    return DataUtilities.CreateObjectListFromDataSet<T>(ds, objectTypeInstance);
    //}

    //public virtual T Get<T>(QueryModel queryModel)
    //{
    //    var objectTypeInstance = Activator.CreateInstance<T>();
    //    DataFactory dbLayer= new DataFactory();

    //    DataSet ds = dbLayer.GetDataSet("spGet" + ModelName, DataUtilities.GetParameterListForFetch(objectTypeInstance as DataModel,queryModel.Parameters));
    //    dbLayer.Dispose();

    //    return DataUtilities.CreateObjectFromDataRow<T>(DataFactory.TopRow(ds) as DataRow, objectTypeInstance );

    //}
    //public virtual T GetFromQuery<T>(QueryModel queryModel)
    //{
    //    var objectTypeInstance = Activator.CreateInstance<T>();
    //    DataFactory dbLayer = new DataFactory();

    //    string queryString = string.Empty; // TODO

    //    DataSet ds = dbLayer.GetDataSet(queryString, DataUtilities.GetParameterListForFetch(objectTypeInstance as DataModel, queryModel.Parameters));
    //    dbLayer.Dispose();

    //    return DataUtilities.CreateObjectFromDataRow<T>(DataFactory.TopRow(ds) as DataRow, objectTypeInstance);
    //}


}
