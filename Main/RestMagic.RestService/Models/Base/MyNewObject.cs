
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
        : DataModel 
#endif
    {
         
        		public int FullName{ get; set;}
		public int Id{ get; set;}
		public int Title{ get; set;}

#if !SDK
		public override string GetSqlText()
        {
            return @"
	SELECT	
			<FieldList />
			 
	FROM
					LegacyTable,

	WHERE
					[LegacyTable].[BadSchemaName1] = IIF(@FullName is null, [LegacyTable].[BadSchemaName1], @FullName) AND
		[LegacyTable].[Id] = IIF(@Id is null, [LegacyTable].[Id], @Id) AND
		[LegacyTable].[BadSchemaEnumAsInt1] = IIF(@Title is null, [LegacyTable].[BadSchemaEnumAsInt1], @Title) AND

			
			--Id =IIF (@Id is null, Id  ,@Id) AND
			--TextData1 =IIF (@TextData1 is null, TextData1  ,@TextData1) AND
			--IntData2 =IIF (@IntData2 is null, IntData2  ,@IntData2) AND
			--CustomEnum =IIF (@CustomEnum is null, CustomEnum  ,@CustomEnum) 

";
        }
#endif   

    }
}

