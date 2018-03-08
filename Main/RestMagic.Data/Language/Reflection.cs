using RestMagic.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestMagic.Data.Language
{
    
    public static class Reflection
    {
        public static MethodInfo GetMethodForGetListGeneric(Type type)
        {
            MethodInfo result = type
             .GetMethods()
             .Single(m => m.Name == "GetList" && m.IsGenericMethodDefinition);

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
    }
}
