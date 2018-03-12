
#if !SDK
using RestMagic.Lib.Data;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestMagic.RestService.Models
{
    
    public partial class MyNewObject
#if !SDK
        : DataModelComposite
#endif
    {
    
      
        public static MyNewObject Get(QueryModel[] queryModel)
        {
            MyNewObject sampleDataModel = new MyNewObject();
            return sampleDataModel.GetComposite<MyNewObject>(queryModel);
        }

     
    }
}

