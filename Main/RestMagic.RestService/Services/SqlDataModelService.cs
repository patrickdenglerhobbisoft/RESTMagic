using System;
using System.Collections.Generic;
using RestMagic.RestService.Models;
 
using System.Data.SqlClient;
using RestMagic.Lib.Data;
using System.Reflection;

using RestMagic.Lib;
using RestMagic.Lib.Language;

namespace RestMagic.RestService.Services
{
    public class SqlDataModelService : IDataModelService
    {

        public SqlDataModelService(string connectionString)
        {
            DataFactory.PrimaryConnectionString = connectionString;
        }
        public object GetData( QueryModel queryModel)
        {
            object result = null;
            string classString = "RestMagic.RestService.Models." + queryModel.DataModel;
            var dataObject = Activator.CreateInstance(null, classString).Unwrap();
            Type type = Type.GetType(classString);
            MethodInfo methodInfo = ReflectionHelper.GetMethodForGetListGeneric(type);
            try
            {
                result = methodInfo.Invoke(dataObject, new object[] { queryModel });
            }
            catch   (Exception ex)
            {
                throw new RestMagicExcpetion("Unable to invoke method on " + queryModel.DataModel,ex.InnerException);
            }
            return result;
        }


     
    }
    public interface IDataModelService
    {
        object GetData( QueryModel queryModel);
        

    }
}