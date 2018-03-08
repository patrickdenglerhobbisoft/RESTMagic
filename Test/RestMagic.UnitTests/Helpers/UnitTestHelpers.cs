using System;
using System.Collections.Generic;
using System.Linq;
using RestMagic.Lib.Data;
using RestMagic.RestService.Models;

namespace RestMagic.Platform.UnitTests.Helpers
{
    public static class UnitTestHelpers
    {


        private static List<SampleDataModel> allSampleDataResults = null;



        private static Dictionary<string, BasicSortingPagingTestData> sortingTestList = null;
        public static Dictionary<string, BasicSortingPagingTestData> SortingTestList
        {
            get
            {
                if (sortingTestList == null) InitSortingTestList();
                return sortingTestList;
            }
        }

        private static void InitSortingTestList()
        {
            // To generate these, use the xpGenerateUnitTestData stored procedure int he Sample.df
            // then replace the MyCustomEnums as appropriate

            allSampleDataResults = new List<SampleDataModel>(){
                new SampleDataModel() {Id = 1,TextData1 = "Test",IntData2 = 3,CustomEnum = MyCustomEnum.Foo, LastUpdateDate = DateTime.Parse("2017-12-12 09:16:07.4533333"), },
                new SampleDataModel() {Id = 2,TextData1 = "Test2",IntData2 = 1,CustomEnum = MyCustomEnum.Bar, LastUpdateDate = DateTime.Parse("2017-12-12 09:16:11.1733333"), },
                new SampleDataModel() {Id = 3,TextData1 = "Test3",IntData2 = 0,CustomEnum = MyCustomEnum.Bar, LastUpdateDate = DateTime.Parse("2017-12-10 09:16:15.0000000"), },
                new SampleDataModel() {Id = 4,TextData1 = "Test4",IntData2 = 5,CustomEnum = MyCustomEnum.Foo, LastUpdateDate = DateTime.Parse("2017-12-12 09:16:25.4600000"), },
                new SampleDataModel() {Id = 5,TextData1 = "Test6",IntData2 = 4,CustomEnum = MyCustomEnum.Foo, LastUpdateDate = DateTime.Parse("2017-12-12 09:16:29.6600000"), },
                new SampleDataModel() {Id = 6,TextData1 = "Test11",IntData2 = 3,CustomEnum = MyCustomEnum.Foo, LastUpdateDate = DateTime.Parse("2017-12-12 09:16:34.6000000"), },
                new SampleDataModel() {Id = 7,TextData1 = "Test211",IntData2 = 1,CustomEnum = MyCustomEnum.Bar, LastUpdateDate = DateTime.Parse("2017-12-11 00:00:00.0000000"), },
                new SampleDataModel() {Id = 8,TextData1 = "Test311",IntData2 = 0,CustomEnum = MyCustomEnum.Foo, LastUpdateDate = DateTime.Parse("2017-12-12 09:16:34.6100000"), },
                new SampleDataModel() {Id = 9,TextData1 = "Test411",IntData2 = 5,CustomEnum = MyCustomEnum.Bar, LastUpdateDate = DateTime.Parse("2017-12-12 09:16:34.6100000"), },
                new SampleDataModel() {Id = 10,TextData1 = "Test611",IntData2 = 4,CustomEnum = MyCustomEnum.Foo, LastUpdateDate = DateTime.Parse("2017-12-12 09:16:34.6133333"), },
                new SampleDataModel() {Id = 11,TextData1 = "Test22",IntData2 = 3,CustomEnum = MyCustomEnum.Foo, LastUpdateDate = DateTime.Parse("2017-12-12 09:16:37.9866667"), },
                new SampleDataModel() {Id = 12,TextData1 = "Test222",IntData2 = 1,CustomEnum = MyCustomEnum.Bar, LastUpdateDate = DateTime.Parse("2017-12-12 09:16:37.9900000"), },
                new SampleDataModel() {Id = 13,TextData1 = "Test322",IntData2 = 0,CustomEnum = MyCustomEnum.Foo, LastUpdateDate = DateTime.Parse("2017-01-01 00:00:00.0000000"), },
                new SampleDataModel() {Id = 14,TextData1 = "Test422",IntData2 = 5,CustomEnum = MyCustomEnum.Foo, LastUpdateDate = DateTime.Parse("2017-12-12 09:16:37.9966667"), },
                new SampleDataModel() {Id = 15,TextData1 = "tEST422",IntData2 = 4,CustomEnum = MyCustomEnum.Foo, LastUpdateDate = DateTime.Parse("2017-12-12 09:16:37.9966667"), },
            };
            sortingTestList = new Dictionary<string, BasicSortingPagingTestData>();

            // SORT INT / ASCENDING

            Queue<SampleDataModel> expectedResult = new Queue<SampleDataModel>();
            foreach (SampleDataModel model in allSampleDataResults.Where(m => m.IntData2 == 1).OrderBy(o => o.Id))
            {
                expectedResult.Enqueue(model);
            }
            sortingTestList.Add("FilterOnInt2_SortById", new BasicSortingPagingTestData()
            {
                // express your expected result as the key (int)
                ExpectedSortedDataModelResult = expectedResult,
                QueryToUse = new QueryModel()
                {
                    Parameters = new Dictionary<string, object>()
                     {
                         {  "IntData2",1 },
                     },
                    SortOrder = SortOrder.ASC,
                    SortField = "Id",
                },

            });

            // SORT INT / DESCENDING
            expectedResult = new Queue<SampleDataModel>();
            foreach (SampleDataModel model in allSampleDataResults.Where(m => m.IntData2 == 1).OrderByDescending(o => o.Id))
            {
                expectedResult.Enqueue(model);
            }
            sortingTestList.Add("FilterOnInt2_SortByIdDesc", new BasicSortingPagingTestData()
            {
                // express your expected result as the key (int)
                ExpectedSortedDataModelResult = expectedResult,
                QueryToUse = new QueryModel()
                {
                    Parameters = new Dictionary<string, object>()
                     {
                         {  "IntData2",1 },
                     },
                    SortOrder = SortOrder.DESC,
                    SortField = "Id",
                },

            });



            // Paging / Use default sort
            expectedResult = new Queue<SampleDataModel>();
            foreach (SampleDataModel model in allSampleDataResults.OrderBy(o => o.Id).Take(5))
            {
                expectedResult.Enqueue(model);

            }
            sortingTestList.Add("Paging_NoOrder_Page5", new BasicSortingPagingTestData()
            {
                // express your expected result as the key (int)
                ExpectedSortedDataModelResult = expectedResult,
                QueryToUse = new QueryModel()
                {
                    //Parameters = new Dictionary<string, object>()
                    // {
                    //     {  "IntData2",1 },
                    // },
                    SortOrder = SortOrder.ASC,
                    PageSize = 5,
                    PageNumber = 0,
                },

            });


        }


        public class BasicSortingPagingTestData
        {

            public Queue<SampleDataModel> ExpectedSortedDataModelResult { get; set; }
            public QueryModel QueryToUse { get; set; }
        }


        #region currentSampleDataTableData
        //    1	Test	3	1	12/12/2017 9:16:07 AM
        //  2	Test2	1	3	12/12/2017 9:16:11 AM
        //  3	Test3	0	4	12/12/2017 9:16:15 AM
        //4	Test4	5	6	12/12/2017 9:16:25 AM
        //5	Test6	4	1	12/12/2017 9:16:29 AM
        //6	Test11	3	1	12/12/2017 9:16:34 AM
        //7	Test211	1	3	12/12/2017 9:16:34 AM
        //8	Test311	0	4	12/12/2017 9:16:34 AM
        //9	Test411	5	6	12/12/2017 9:16:34 AM
        //10	Test611	4	1	12/12/2017 9:16:34 AM
        //11	Test22	3	1	12/12/2017 9:16:37 AM
        //12	Test222	1	3	12/12/2017 9:16:37 AM
        //13	Test322	0	4	12/12/2017 9:16:37 AM
        //14	Test422	5	6	12/12/2017 9:16:37 AM
        //15	tEST422	4	1	12/12/2017 9:16:37 AM


        #endregion
    }
}
