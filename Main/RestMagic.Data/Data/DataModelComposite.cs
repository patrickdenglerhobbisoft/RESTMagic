using RestMagic.Lib.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestMagic.Lib.Data
{
    public class DataModelComposite:DataModel
    {

        public virtual T GetComposite<T>(QueryModel[] queryModel)
        {
            DataFactory dbLayer = new DataFactory();
            var objectTypeInstance = Activator.CreateInstance<T>();
            try
            {
                foreach (QueryModel qm in queryModel)
                {
                    bool queryModelIsComposite = (qm.DataModel == typeof(T).Name.ToString());
                    PropertyInfo propertyInfo = objectTypeInstance.GetType().GetProperty(qm.DataModel);
                    if (!queryModelIsComposite && propertyInfo == null)
                    {
                        throw new RestMagicExcpetion(qm.DataModel + " not found as property on " + objectTypeInstance.GetType().Name);
                    }
                    else
                    {
                        dynamic dataModel = ReflectionHelper.GetObjectTypeInstance(qm);
                        MethodInfo method = ReflectionHelper.GetMethodForGetListGeneric(dataModel.GetType());
                        // ISSUE: TODO  Need to worry about ordering
                        if (queryModelIsComposite)
                        {
                            objectTypeInstance = method.Invoke(dataModel, new object[] { });
                        }
                        else
                        {
                            dynamic result = method.Invoke(dataModel, new object[] { });
                            propertyInfo.SetValue(objectTypeInstance, result);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new RestMagicExcpetion("Unexpected Error in GetComposite.", ex);
            }

            return (T)objectTypeInstance;
        }
    }
}
