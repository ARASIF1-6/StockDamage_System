using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockDamage.Models
{
    public class StockDamageModel
    {
        public string WarehouseName { get; set; }
        public string SubItemName { get; set; }
        public string SubItemCode { get; set; }
        public string BatchNo { get; set; }
        public string Currency { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal AmountIn { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal TotalAmount { get; set; }
        public string EmployeeName { get; set; }
        public string Comments { get; set; }

    }
}