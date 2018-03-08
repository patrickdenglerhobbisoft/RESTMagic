using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;

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
            get  { return this.GetType().Name; }
        }
        internal Type ModelType
        {
            get { return this.GetType(); }
        }

        public virtual DataModel Copy()
        {
            return this.MemberwiseClone() as DataModel;
        }

        public virtual List<T> GetList<T>(QueryModel queryModel)
        {
            string dataModel = queryModel.DataModel;

            var objectTypeInstance = Activator.CreateInstance<T>();
            DataFactory dbLayer= new DataFactory();

            DataSet ds = dbLayer.GetDataSet("spGet" + ModelName, DataUtilities.GetParameterListForFetch(objectTypeInstance as DataModel, queryModel.Parameters));
            dbLayer.Dispose();

            return DataUtilities.CreateObjectListFromDataSet<T> (ds, objectTypeInstance );
        }

        private DataModel GetObjectTypeInstance(QueryModel queryModel)
        {
            string dataModelTypeName = "RestMagic.RestService.Models." + queryModel.DataModel;

            return (DataModel)Activator.CreateInstance("RestMagic.RestService", dataModelTypeName).Unwrap();
        }

        public virtual List<T> GetListFromQuery<T>(Dictionary<string, object> parameters, string queryString)
        {
            var objectTypeInstance = Activator.CreateInstance<T>();
            DataFactory dbLayer = new DataFactory();

            DataSet ds = dbLayer.GetDataSet(queryString, DataUtilities.GetParameterListForFetch(objectTypeInstance as DataModel, parameters));
            dbLayer.Dispose();

            return DataUtilities.CreateObjectListFromDataSet<T>(ds, objectTypeInstance);
        }

        public virtual T Get<T>(QueryModel queryModel)
        {
            var objectTypeInstance = Activator.CreateInstance<T>();
            DataFactory dbLayer= new DataFactory();

            DataSet ds = dbLayer.GetDataSet("spGet" + ModelName, DataUtilities.GetParameterListForFetch(objectTypeInstance as DataModel,queryModel.Parameters));
            dbLayer.Dispose();

            return DataUtilities.CreateObjectFromDataRow<T>(DataFactory.TopRow(ds) as DataRow, objectTypeInstance );
            
        }
        public virtual T GetFromQuery<T>(QueryModel queryModel)
        {
            var objectTypeInstance = Activator.CreateInstance<T>();
            DataFactory dbLayer = new DataFactory();

            string queryString = string.Empty; // TODO

            DataSet ds = dbLayer.GetDataSet(queryString, DataUtilities.GetParameterListForFetch(objectTypeInstance as DataModel, queryModel.Parameters));
            dbLayer.Dispose();

            return DataUtilities.CreateObjectFromDataRow<T>(DataFactory.TopRow(ds) as DataRow, objectTypeInstance);
        }

        public virtual bool Upsert()
        {
            DataFactory dbLayer= new DataFactory();
            long returnValue = 0;
            dbLayer.ExecuteNonQuery("spUpsert" + ModelName, DataUtilities.GetParameterListForUpsert(this), ref returnValue);
            dbLayer.Dispose();
            return (returnValue > 0);
        }

      
        [ExcludeAsSqlParam]
        [JsonIgnore]
        public Dictionary<object, object> RuleSet { get; set; } = new Dictionary<object, object>();
       
    }
}
