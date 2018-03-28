
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
    
      
        public static List<MyNewObjectDetails> Get(QueryModel queryModel)
        {
             
            return new  MyNewObjectDetails().Get<MyNewObjectDetails>(queryModel);
        }

     
    }
}

