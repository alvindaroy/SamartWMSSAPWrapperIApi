using B1SLayer;
using Newtonsoft.Json;
using SamartWMSSAPWrapperApi.Model.Transaction;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamartWMSSAPWrapperApi.Model
{
    public class DeliveryDataModel
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

                    Deliveries model = JsonConvert.DeserializeObject<Deliveries>(document, settings);

                    Log.Logger.Information("Payload: " + JsonConvert.SerializeObject(model));

                    var createdStockTransfer = await servicelayer.Request("DeliveryNotes").PostAsync<dynamic>(model);

                    serviceResponse.Success = true;
                    serviceResponse.Data = System.Text.Json.JsonSerializer.Serialize(createdStockTransfer);
                    await servicelayer.LogoutAsync();
                }
                else
                {
                    throw new Exception("Invalid SL session.");
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error("Error Delivery creation: " + e.ToString());

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
                id = id.Replace("B2B", "").Replace("B2C", "");
                if (string.IsNullOrEmpty(document))
                {
                    throw new Exception("Invalid or Empty document payload.");
                }
                else
                {
                    Log.Logger.Information("Request payload: " + document);
                }

                SLConnection servicelayer = ServiceLayer.Connection;
                var loginRes = await servicelayer.LoginAsync();
                if (!string.IsNullOrEmpty(loginRes.SessionId))
                {
                    var settings = new JsonSerializerSettings
                    {
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    };
                    Deliveries model = JsonConvert.DeserializeObject<Deliveries>(document, settings);

                    await servicelayer.Request($"Orders({id})").PatchAsync(model);

                    dynamic updatesOrders = await servicelayer.Request($"Orders({id})").GetAsync<dynamic>();

                    serviceResponse.Success = true;
                    serviceResponse.Data = System.Text.Json.JsonSerializer.Serialize(updatesOrders); 
                    await servicelayer.LogoutAsync();
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
        public async Task<ServiceResponse<string>> Cancel(string id)
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            try
            {
                id = id.Replace("B2B", "").Replace("B2C", "");
                SLConnection servicelayer = ServiceLayer.Connection;
                var loginRes = await servicelayer.LoginAsync();
                if (!string.IsNullOrEmpty(loginRes.SessionId))
                {
                    await servicelayer.Request($"Orders({id})/Cancel").PostAsync();

                    var updatesOrders = await servicelayer.Request($"Orders({id})").GetAsync<dynamic>();

                    serviceResponse.Success = true;
                    serviceResponse.Data = System.Text.Json.JsonSerializer.Serialize(updatesOrders);
                    await servicelayer.LogoutAsync();
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
}
