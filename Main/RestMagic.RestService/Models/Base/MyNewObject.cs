
#if !SDK
using RestMagic.Lib.Data;
#endif
using RestMagic.Lib; 
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
         
        public long Id { get; set; }

        public string FullName { get; set; }

        public string Title { get; set; }


        public List<MyNewObjectDetails> Details { get; set; } = new List<MyNewObjectDetails>();

    }
}

