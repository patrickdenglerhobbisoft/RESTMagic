using RestMagic.Lib;
using RestMagic.Lib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestMagic.Lib.Language
{
    
    public static class ReflectionHelper
    {
        public const string SDK_ASSEMBLY_NAMESPACE = "RestMagic.SDK";
        public const string RESTSERVICE_ASSEMBLY_NAMESPACE = "RestMagic.RestService";
        public const string RESTSERVICE_MODEL_NAMESPACE = RESTSERVICE_ASSEMBLY_NAMESPACE + ".Models";

        public static MethodInfo GetMethodForGetListGeneric(Type type)
        {
            MethodInfo result = type
             .GetMethods()
             .Single(m => m.Name == "Get" && m.IsGenericMethodDefinition);

            if (result == null)
            {
                throw new RestMagicExcpetion("Couldn't create GetList<T> for  " + type.Name);
            }
            return result.MakeGenericMethod(type);
        }
        public static MethodInfo GetMethodForGetListGeneric(string className)
        {
            
            Type type = Type.GetType(className); 
            if (type == null)
            {
                throw new RestMagicExcpetion("Couldn't resolve type " + className);
                
            }
            return GetMethodForGetListGeneric(type);
        }

        public static List<Type> GetDataModelTypesFromAssemblyName(string assemblyName)
        {
            List<Type> result = new List<Type>();
            Assembly assembly = Assembly.Load(assemblyName);

            return assembly.GetTypes().ToList<Type>().Where(t => t.Name.Contains("DataModel")).ToList<Type>();

           
        }
        public static DataModel GetObjectTypeInstance(QueryModel queryModel)
        {
            string dataModelTypeName = RESTSERVICE_MODEL_NAMESPACE + "." + queryModel.DataModel;

            return (DataModel)Activator.CreateInstance(RESTSERVICE_ASSEMBLY_NAMESPACE, dataModelTypeName).Unwrap();
        }


    }
}
