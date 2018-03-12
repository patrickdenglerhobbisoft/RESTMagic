﻿using System;
using System.Net;
using System.Web.Http;
using RestMagic.Lib.Data;
using System.Data;
using RestMagic.RestService.Services;
using System.Configuration;
using RestMagic.RestService.Logging;
using RestMagic.Lib;

namespace RestMagic.RestService.Controllers
{
    [RoutePrefix(Constants.RoutePrefix)]
    public class DomainController : ApiController
    {
        protected static IDataModelService DataModelService = null;
        protected static ILogger Logger;
        public DomainController() : base( )
        {
            if (DataModelService == null)
            {
                string connectingString = ConfigurationManager.ConnectionStrings["Sample"].ConnectionString;
                DataModelService = new SqlDataModelService(connectingString);
            }
            Logger = new ConsoleLogger();
        }


        [Route("{PrimaryModelName}")]
        [HttpPost]
      
        public object FindDataByModel( [FromBody] QueryModel queryModel)
        {
            object result = string.Empty;
            try
            {
                if (queryModel.Parameters == null || queryModel.Parameters.Count == 0)
                {
                    throw new RestMagicExcpetion("Call to FindDataByModel contained no parameters.");
                }
                result = DataModelService.GetData( queryModel);
                
            }
            catch (Exception e)
            {
                Logger.LogError( e);
                result = e.Message;
            }

            return result;
        }




        
    }

}
