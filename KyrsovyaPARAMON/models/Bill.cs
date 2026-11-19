using System;

namespace KyrsovyaPARAMON.Models
{
    public class Bill
    {
        public int BillId { get; set; }
        public int AbonentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }

        public Bill()
        {
            Amount = 0;
            Date = DateTime.Now;
            IsPaid = false;
        }
    }
}