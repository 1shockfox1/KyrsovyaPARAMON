using System;

namespace KyrsovyaPARAMON.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }

        public Service()
        {
            ServiceName = "Определитель номера";
            Price = 50.0m;
        }
    }
}