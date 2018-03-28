
CREATE PROC  [ GetMyNewObject] 

	 		BadSchemaName1
		Id
		BadSchemaEnumAsInt1

	--@Id [int]  =NULL,
	--@PageSize [int] = 50,
	--@PageNumber [int] =0,
	--@OrderByModelFieldName nvarchar(255) ='Id', -- must set a default
	--@IsAscending bit = 0

AS

BEGIN

<Sql/>


--ORDER BY (CASE 
	--			WHEN @OrderByModelFieldName = 'Id' and @IsAscending = 1 THEN Id
	--			WHEN @OrderByModelFieldName = 'IntData2' and @IsAscending = 1 THEN IntData2
 --         END) ASC,
	--         (CASE 
	--			WHEN @OrderByModelFieldName = 'Id' and @IsAscending = 0 THEN Id
	--			WHEN @OrderByModelFieldName = 'IntData2' and @IsAscending = 0  THEN IntData2
 --         END) DESC,
	--	  (CASE 
	--			WHEN @OrderByModelFieldName = 'TextData1'  and @IsAscending = 1 THEN TextData1
 --         END) ASC,
	--         (CASE 
	--			WHEN @OrderByModelFieldName = 'TextData1'  and @IsAscending = 0 THEN TextData1
 --         END) DESC
	   
	--ORDER BY ISNULL(STUFF( @OrderByModelFieldName  + ' ' +  IIF(@IsAscending = 1, 'ASC','DESC'),1,0,null),null)

	--OFFSET @PageNumber ROWS FETCH NEXT @PageSize ROWS ONLY
	

END