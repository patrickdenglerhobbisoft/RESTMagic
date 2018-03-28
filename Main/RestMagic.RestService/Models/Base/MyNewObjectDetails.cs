
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

   
    public partial class MyNewObjectDetails
#if !SDK
        : DataModel 
#endif
    {
         
        		public int ChildName{ get; set;}
		public int LastUpdated{ get; set;}

#if !SDK
		public override string GetSqlText()
        {
            return @"
	SELECT	
			<FieldList />
			 
	FROM
					LegacyTableDetails,

	WHERE
					[LegacyTableDetails].[C0NN1] = IIF(@ChildName is null, [LegacyTableDetails].[C0NN1], @ChildName) AND
		[LegacyTableDetails].[BadSchemaNameDate] = IIF(@LastUpdated is null, [LegacyTableDetails].[BadSchemaNameDate], @LastUpdated) AND

			
			--Id =IIF (@Id is null, Id  ,@Id) AND
			--TextData1 =IIF (@TextData1 is null, TextData1  ,@TextData1) AND
			--IntData2 =IIF (@IntData2 is null, IntData2  ,@IntData2) AND
			--CustomEnum =IIF (@CustomEnum is null, CustomEnum  ,@CustomEnum) 

";
        }
#endif   

    }
}

