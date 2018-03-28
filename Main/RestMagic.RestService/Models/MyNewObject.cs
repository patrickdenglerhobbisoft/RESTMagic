
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
        : DataModel
#endif
    {
    
      
        public static List<MyNewObject> Get(QueryModel queryModel)
        {
             
            return new  MyNewObject().Get<MyNewObject>(queryModel);
        }

     
    }
}

