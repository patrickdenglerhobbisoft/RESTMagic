
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
    
    public partial class SampleDataModel
#if !SDK
        :DataModel
#endif
    {
    
        public SampleDataModel()
        {
            // any casting or changes when setting parameters
            RuleSet = new Dictionary<object, object>()
            {
                {  typeof(MyCustomEnum), new RulesCaster() { CastTypeParameter = typeof(Int32), CastTypeFill = typeof(MyCustomEnum) } },
            };
        }

         
        public  List<SampleDataModel> Get(QueryModel queryModel)
        {
            return this.Get<SampleDataModel>(queryModel);
        }

     
    }
}

