using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestMagic.Lib.Data
{
    public enum SortOrder {  ASC = 1, DESC =2}
    public class QueryModel
    {

        public Dictionary<string,object> Parameters { get; set; }

        public string DataModel { get;  set; }
        public SortOrder SortOrder { get; set; }
        public string SortField { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
