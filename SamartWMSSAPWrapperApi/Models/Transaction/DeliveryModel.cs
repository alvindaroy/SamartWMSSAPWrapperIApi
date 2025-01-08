using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamartWMSSAPWrapperApi.Model.Transaction
{

    public class Deliveries
    {
        public string CardCode { get; set; }
        public string DocDate { get; set; }
        public string DocDueDate { get; set; }
        public string TaxDate { get; set; }
        public string NumAtCard { get; set; }
        public string U_OrderType { get; set; }
        public string U_Carrier { get; set; }
        public string U_ShipStatus { get; set; }
        public Documentline[] DocumentLines { get; set; }
        public string U_ConsignmentNote { get; set; } 
        public string U_FirstName { get; set; }
        public string U_LastName { get; set; }
        public string U_Phone { get; set; }
        public string Comments { get; set; } 
        public AddressExtension AddressExtension { get; set; }
    }

    public class AddressExtension
    { 
        public string ShipToStreet { get; set; }
        public string ShipToBlock { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToZipCode { get; set; }
        public string ShipToCountry { get; set; }
        public string BillToStreet { get; set; }
        public string BillToBlock { get; set; }
        public string BillToCity { get; set; }
        public string BillToZipCode { get; set; }
        public string BillToCountry { get; set; }
    }

    public class Documentline
    {
        private string _baseEntry;
        public string BaseEntry 
        {
            get => !string.IsNullOrEmpty(_baseEntry) ? _baseEntry.Replace("B2B", "").Replace("B2C", "") : _baseEntry;
            set => _baseEntry = value;
        }
        public string BaseType { get; set; }
        public string BaseLine { get; set; }
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
        public string WarehouseCode { get; set; }
        public Binallocation[] BinAllocations { get; set; }
        public Serialnumber[] SerialNumbers { get; set; }
    }

    public class Binallocation
    {
        public int BinActionType { get; set; }
        public string BinAbsEntry { get; set; }
        public int SerialAndBatchNumbersBaseLine { get; set; }
    }  
}
