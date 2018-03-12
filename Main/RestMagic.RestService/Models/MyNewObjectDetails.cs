
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
    
    public partial class MyNewObjectDetails
#if !SDK
        : DataModel
#endif
    {
    
      
         
        public static List<MyNewObjectDetails> GetList(QueryModel queryModel)
        {
            MyNewObjectDetails sampleDataModel = new MyNewObjectDetails();
            return sampleDataModel.Get<MyNewObjectDetails>(queryModel);
        }

     
    }
}

