using System;
using System.Collections.Generic;
using RestMagic.RestService.Models;
 
using System.Data.SqlClient;
using RestMagic.Lib.Data;
using System.Reflection;
using RestMagic.Data.Language;
using RestMagic.Lib;

namespace RestMagic.RestService.Services
{
    public class SqlDataModelService : IDataModelService
    {

        public SqlDataModelService(string connectionString)
        {
            DataFactory.PrimaryConnectionString = connectionString;
        }
        public object GetData(string primaryModelName, QueryModel queryModel)
        {
            object result = null;
            string classString = "RestMagic.RestService.Models." + primaryModelName;
            var dataObject = Activator.CreateInstance(null, classString).Unwrap();
            Type type = Type.GetType(classString);
            MethodInfo methodInfo = Reflection.GetMethodForGetListGeneric(type);
            try
            {
                result = methodInfo.Invoke(dataObject, new object[] { queryModel });
            }
            catch   (Exception ex)
            {
                throw new RestMagicExcpetion("Unable to invoke method on " + primaryModelName,ex.InnerException);
            }
            return result;
        }


     
    }
    public interface IDataModelService
    {
        object GetData(string primaryModelName, QueryModel queryModel);
        

    }
}