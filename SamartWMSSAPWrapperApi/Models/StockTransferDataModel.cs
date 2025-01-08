using B1SLayer; 
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SamartWMSSAPWrapperApi.Model.Transaction;
using Newtonsoft.Json;

namespace SamartWMSSAPWrapperApi.Model
{
    public class StockTransferDataModel
    {
        public CompanyInfo companyInfo { get; set; }
        public string document { get; set; } 
        public async Task<ServiceResponse<string>> Post()
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            try
            {
                if (string.IsNullOrEmpty(document))
                {
                    throw new Exception("Invalid or Empty document payload.");
                }
                 
                SLConnection servicelayer = ServiceLayer.Connection;
                var loginRes = await servicelayer.LoginAsync();
                if (!string.IsNullOrEmpty(loginRes.SessionId))
                {

                    var settings = new JsonSerializerSettings
                    {
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    };
                    StockTransfers model = JsonConvert.DeserializeObject<StockTransfers>(document, settings);

                    var createdStockTransfer = await servicelayer.Request("StockTransfers").PostAsync<System.Dynamic.ExpandoObject>(model);

                    serviceResponse.Success = true;
                    serviceResponse.Data = JsonConvert.SerializeObject(createdStockTransfer);
                }
                else
                {
                    throw new Exception("Invalid SL session.");
                }
            }
            catch(Exception e)
            {
                Log.Logger.Error("Error Stock Transfer creation: " + e.ToString());

                serviceResponse.Message = e.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<string>> Patch(string id)
        { 
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            try
            {
                id = id.Replace("STO", "");
                if (string.IsNullOrEmpty(document))
                {
                    throw new Exception("Invalid or Empty document payload.");
                }

                SLConnection servicelayer = ServiceLayer.Connection;
                var loginRes = await servicelayer.LoginAsync();
                if (!string.IsNullOrEmpty(loginRes.SessionId))
                {
                    var settings = new JsonSerializerSettings
                    {
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    };
                    StockTransfers model = JsonConvert.DeserializeObject<StockTransfers>(document, settings);

                    await servicelayer.Request($"InventoryTransferRequests({id})").PatchAsync(model);

                    var updatedTransferRequest = await servicelayer.Request($"InventoryTransferRequests({id})").GetAsync<System.Dynamic.ExpandoObject>();

                    serviceResponse.Success = true;
                    serviceResponse.Data = JsonConvert.SerializeObject(updatedTransferRequest);
                }
                else
                {
                    throw new Exception("Invalid SL session.");
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error("Error Stock Transfer creation: " + e.ToString());

                serviceResponse.Message = e.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    }
    public class CompanyInfo
    {
        public string database { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
