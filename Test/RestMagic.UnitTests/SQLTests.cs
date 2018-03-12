using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using RestMagic.Lib.Data;
using System.IO;
using RestMagic.RestService.Models;
using RestMagic.Platform.UnitTests.Helpers;
using static RestMagic.Platform.UnitTests.Helpers.UnitTestHelpers;
using System.Linq;
using System.Reflection;

namespace RestMagic.Lib.Data.UnitTest
{
    [TestClass]
    public class SqlTests
    {

        [AssemblyInitialize()]
        public static void MyTestInitialize(TestContext testContext)
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            // C:\Users\patri\source\repos\RestMagic\Test\RestMagic.UnitTests\bin\Debug\RestMagic.UnitTests.dll
            string restMagicServiceDirectory = assemblyLocation.Replace(@"Test\RestMagic.UnitTests\bin\Debug\RestMagic.UnitTests.dll", @"Main\RestMagic.RestService");
            var databaseDirectory = restMagicServiceDirectory + @"\App_Data";
           
            AppDomain.CurrentDomain.SetData("DataDirectory", databaseDirectory);
            DataFactory.PrimaryConnectionString = ConfigurationManager.ConnectionStrings["Sample"].ConnectionString;
        }
        

        [TestMethod]
        public void TestGetOne()
        {
            var sampleDataModel = new SampleDataModel().Get(new QueryModel()
            {
                Parameters = new Dictionary<string, object>()
                {
                        { "IntData2",1},
                },
            }).First();
            Debug.WriteLine(sampleDataModel.ToString());
            Assert.IsNotNull(sampleDataModel);
        }
        /// <summary>
        /// Tests placement of related SQL objects required on server for a 
        /// particular model.  Does so by creating a record
        /// </summary>
        [TestMethod]
        public void DatabaseObjectConstructionAndCreateRecord()
        {
            //AddRecord("text1", 2, MyCustomEnum.Bar);
        }

        [TestMethod]
        public void AddGetUpdateInsert()
        {
          
            //SampleDataModel sampleDataModel2;
            SampleDataModel sampleDataModel3;
            SampleDataModel sampleDataModel = new SampleDataModel()
            {
                TextData1 = "my new TextData",
                IntData2 = 3,
                CustomEnum = MyCustomEnum.Foo,
            };

            ///
            ///
            ///  TODO: Upsert Disabled
            ///
            ///

 
            // so let's get it by TextData1 then by IntData2

            // sampleDataModel.Upsert<SampleDataModel>();


            // pass in name value pairs of the columns and search values (using new instance for testing convenience)
            // use TextData1
            //sampleDataModel2 = SampleDataModel.Get(new QueryModel()
            //{
            //    Parameters = new Dictionary<string, object>()
            //    {
            //            { "TextData1",  "my new TextData" },
            //    },
            //});

            //Debug.WriteLine(sampleDataModel2.ToString());
            //Assert.IsNotNull(sampleDataModel2);



            // use int
            sampleDataModel3 = new SampleDataModel().Get(new QueryModel()
            {
                Parameters = new Dictionary<string, object>()
                {
                        { "IntData2",1},
                },
            }).First();
            Debug.WriteLine(sampleDataModel3.ToString());
            Assert.IsNotNull(sampleDataModel3);

            // Disabling Upsert for now
            // change a value
            //sampleDataModel2.IntData2 = 10;
            //sampleDataModel2.Upsert<SampleDataModel>();

            //// fetch back into #3 by id to see if it changed
            //sampleDataModel3 = SampleDataModel.Get(new QueryModel()
            //{
            //    Parameters = new Dictionary<string, object>()
            //{
            //    { "Id", sampleDataModel2.Id }
            //},
            //});
            //Debug.WriteLine(sampleDataModel3.ToString());

            //Assert.AreEqual(sampleDataModel3.IntData2, sampleDataModel2.IntData2);

            //// add a few (haven't handled bulk add yet but that's easy too. So for now, one at a time
            //sampleDataModel = new SampleDataModel()
            //{
            //    TextData1 = "my new TextData2",
            //    IntData2 = 55,
            //    CustomEnum = MyCustomEnum.Foo,
            //};
            //sampleDataModel.Upsert<SampleDataModel>();

            //sampleDataModel = new SampleDataModel()
            //{
            //    TextData1 = "my new TextData3",
            //    IntData2 = 55,
            //    CustomEnum = MyCustomEnum.Foo,
            //};
            //sampleDataModel.Upsert<SampleDataModel>();

            //// now let's get them all

            //List<SampleDataModel> result = SampleDataModel.GetList();

            //foreach (var sample in result)
            //    Debug.WriteLine(sample.ToString());

            //// now lets just get the ones with intData = 3
            //result = SampleDataModel.GetList(new QueryModel()
            //{
            //    Parameters = new Dictionary<string, object>()
            //    {
            //        {"IntData2",55}
            //    },
            //});

            //foreach (var sample in result)
            //    Debug.WriteLine(sample.ToString());
        }

        [TestMethod]
        public void BasicSorting()
        {
            List<SampleDataModel> results = null;

            string filter = null;// "Page";// null;// "DESC";

            try
            {
                foreach (var sortingTestList in UnitTestHelpers.SortingTestList)
                {
                    if (filter != null && filter.Length > 0)
                    {
                        // we can filter lists while debugging
                        if (!sortingTestList.Key.ToUpper().Contains(filter.ToUpper()))
                        {
                            Debug.WriteLine("\nSkipping scenario " + sortingTestList.Key);
                            continue;
                        }
                    }
                    Debug.WriteLine("Testing scenario " + sortingTestList.Key);
                    var testingData = sortingTestList.Value as BasicSortingPagingTestData;
                    results = new SampleDataModel().Get(testingData.QueryToUse);
                    foreach (SampleDataModel resultData in results)
                    {
                        // TODO: Fix ObjectEquals operator override
                        bool areEqual = (resultData == testingData.ExpectedSortedDataModelResult.Dequeue());
                        Assert.IsTrue(areEqual);
                        if (!areEqual)
                            Debugger.Break();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debugger.Break();
            }
        }
        #region Reserved for Future Updates

        //// Upsert not required for Permera
        //// still issues with assigning id back to type when upserting
        //[TestMethod]
        //public void Upsert()
        //{
        //    SampleDataModel sampleDataModel = AddRecord("text2", 3, MyCustomEnum.Foo);
        //    // in this particular design we take advantage of the pattern
        //    // where the return value of the storedprocedure assigns the key to Id

        //    Assert.IsNotNull(sampleDataModel.Id);
        //    long id = sampleDataModel.Id;

        //    SampleDataModel resultSampleDataModel = SampleDataModel.Get(
        //            new Dictionary<string, object>()
        //            {
        //                { "Id",id },
        //            });

        //    Assert.AreEqual<SampleDataModel>(sampleDataModel, resultSampleDataModel);
        //}

       // private SampleDataModel AddRecord(string textValue, int intValue, MyCustomEnum customEnumValue)
       // {
            //SampleDataModel sampleDataModel = new SampleDataModel()
            //{
            //    TextData1 = textValue,
            //    IntData2 = intValue,
            //    CustomEnum = customEnumValue,
            //};
            //sampleDataModel.Upsert<SampleDataModel>();
            //return sampleDataModel;

       // }

        #endregion


    }
}
