
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

    public enum MyCustomEnum {  Foo =1, Bar = 2 }
    public partial class SampleDataModel
#if !SDK
        : DataModel
#endif
    {
        [PrimaryKey]
        public long Id { get; set; }

        public string TextData1 { get; set; }

        public int IntData2 { get; set; }

        public MyCustomEnum CustomEnum { get; set; }

        [ExcludeAsSqlParam]
        public DateTime LastUpdateDate { get; set; }

        [ExcludeAsSqlParam]
        public bool IsAuthenticated { get; set; } = false;  // example of field not stored in database

      





    }
}

