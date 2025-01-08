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
    public class StockTransferController : ApiController
    {
        [HttpGet]
        [ActionName("Get")]
        public string Get()
        {
            return "success";
        }
        [HttpPost]
        [ActionName("Create")]
        public async Task<ServiceResponse<string>> Post(StockTransfers payload)
        {
            StockTransferDataModel dataModel = new StockTransferDataModel
            {
                companyInfo = new CompanyInfo
                {
                    database = GlobalVar.CompanyDB,
                    username = GlobalVar.Username,
                    password = GlobalVar.Password
                },
                document = JsonConvert.SerializeObject(payload)
            };
            return await dataModel.Post();
        } 
    }
}
