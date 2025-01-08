using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamartWMSSAPWrapperApi.Model.Transaction
{

    public class StockTransfers
    {
        public string DocDate { get; set; }
        public string ToWarehouse { get; set; }
        public string FromWarehouse { get; set; }
        public string Comments { get; set; }
        public string U_OrderType { get; set; }
        public string U_Carrier { get; set; }
        public string U_ShipStatus { get; set; }
        public Stocktransferline[] StockTransferLines { get; set; }
        public string U_ConsignmentNoteNo { get; set; }
    }

    public class Stocktransferline
    {
        private string _baseEntry; 
        public string BaseEntry
        {
            get => !string.IsNullOrEmpty(_baseEntry) ? _baseEntry.Replace("STO", "") : _baseEntry;
            set => _baseEntry = value;
        }
        public string BaseType { get; set; }
        public string BaseLine { get; set; }
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
        public string FromWarehouseCode { get; set; }
        public string WarehouseCode { get; set; }
        public Stocktransferlinesbinallocation[] StockTransferLinesBinAllocations { get; set; }
        public Serialnumber[] SerialNumbers { get; set; }
    }

    public class Stocktransferlinesbinallocation
    {
        public string BinActionType { get; set; }
        public string BinAbsEntry { get; set; }
        public int SerialAndBatchNumbersBaseLine { get; set; }
        public int Quantity { get; set; }
    }

    public class Serialnumber
    {
        public string InternalSerialNumber { get; set; }
    }

}
