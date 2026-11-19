using System;
using System.Collections.Generic; // Обязательно добавь эту строку для List

namespace KyrsovyaPARAMON.Models
{
    public class Abonent : Person
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsBlocked { get; set; }

        // Список подключенных услуг
        public List<Service> ConnectedServices { get; set; }

        public Abonent() : base()
        {
            PhoneNumber = "+7-000-000-00-00";
            Balance = 0;
            IsBlocked = false;
            ConnectedServices = new List<Service>(); // Инициализируем список
        }

        public Abonent(string name, string lastName, string phone) : base(name, lastName, "")
        {
            PhoneNumber = phone;
            Balance = 0;
            IsBlocked = false;
            ConnectedServices = new List<Service>(); // Инициализируем список
        }

        // Вот тот самый метод, на который ругался Visual Studio:
        public void ConnectService(Service service)
        {
            if (service != null && !ConnectedServices.Contains(service))
            {
                ConnectedServices.Add(service);
                Balance -= service.Price; // Списываем стоимость услуги
            }
        }
    }
}