using Newtonsoft.Json;
using SamartWMSSAPWrapperApi.Model;
using SamartWMSSAPWrapperApi.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SamartWMSSAPWrapperApi.Controllers
{
    public class OrdersController : ApiController
    {
        [HttpPatch]
        public async Task<ServiceResponse<string>> Patch([FromUri] string id, [FromBody] Deliveries payload)
        {
            DeliveryDataModel dataModel = new DeliveryDataModel
            {
                companyInfo = new CompanyInfo
                {
                    database = GlobalVar.CompanyDB,
                    username = GlobalVar.Username,
                    password = GlobalVar.Password
                },
                document = JsonConvert.SerializeObject(payload)
            };
            return await dataModel.Patch(id);
        }
        [HttpPost]
        [ActionName("CancelOrders")]
        public async Task<ServiceResponse<string>> CancelOrders([FromUri] string id, [FromBody] Deliveries payload)
        {
            DeliveryDataModel dataModel = new DeliveryDataModel
            {
                companyInfo = new CompanyInfo
                {
                    database = GlobalVar.CompanyDB,
                    username = GlobalVar.Username,
                    password = GlobalVar.Password
                }
            };
            return await dataModel.Cancel(id);
        }
    }
}
