
using KyrsovyaPARAMON.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace KyrsovyaPARAMON
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Abonent> AbonentsList { get; set; }
        public List<Service> ServicesList { get; set; }
        public List<Bill> BillsList { get; set; }

        private int nextId = 1;

        public MainWindow()
        {
            InitializeComponent();

            // Тестовые данные
            AbonentsList = new ObservableCollection<Abonent>();
            ServicesList = new List<Service>
            {
                new Service { ServiceId = 1, ServiceName = "Определитель номера (50 руб.)", Price = 50 },
                new Service { ServiceId = 2, ServiceName = "Голосовая почта (30 руб.)", Price = 30 },
                new Service { ServiceId = 3, ServiceName = "Безлимитный интернет (200 руб.)", Price = 200 }
            };
            BillsList = new List<Bill>();

            // Добавим одного тестового абонента для красоты
            var testAb = new Abonent("Иван", "Иванов", "+7-999-000-11-22") { Id = nextId++, Balance = 100 };
            AbonentsList.Add(testAb);

            DataContext = this;
            listBoxServices.ItemsSource = ServicesList;
        }

        // --- ЛОГИКА АДМИНИСТРАТОРА ---
        private void ConnectAbonent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Заполните имя и телефон!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newAb = new Abonent(txtName.Text, txtLastName.Text, txtPhone.Text)
            {
                Id = nextId++,
                Balance = 0
            };

            AbonentsList.Add(newAb);

            // Очищаем поля
            txtName.Clear(); txtLastName.Clear(); txtPhone.Clear();

            MessageBox.Show($"Абонент {newAb.GetFIO()} успешно подключен! Его ID: {newAb.Id}", "Успех");
        }

        private void BlockAbonent_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridAbonents.SelectedItem is Abonent selectedAb)
            {
                selectedAb.IsBlocked = !selectedAb.IsBlocked;
                dataGridAbonents.Items.Refresh();
                string status = selectedAb.IsBlocked ? "заблокирован" : "разблокирован";
                MessageBox.Show($"Абонент {selectedAb.GetFIO()} {status}.", "Статус изменен");
            }
            else
            {
                MessageBox.Show("Выберите абонента в таблице!", "Внимание");
            }
        }

        private void ShowBills_Click(object sender, RoutedEventArgs e)
        {
            var unpaid = BillsList.Where(b => !b.IsPaid).ToList();
            if (!unpaid.Any())
            {
                MessageBox.Show("Неоплаченных счетов нет!", "Информация");
                return;
            }

            string result = "Список неоплаченных счетов:\n";
            foreach (var bill in unpaid)
            {
                var ab = AbonentsList.FirstOrDefault(a => a.Id == bill.AbonentId);
                if (ab != null)
                    result += $"Счет №{bill.BillId} | {ab.GetFIO()} | {bill.Amount} руб.\n";
            }
            MessageBox.Show(result, "Должники");
        }

        // --- ЛОГИКА АБОНЕНТА ---
        private void ConnectService_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxServices.SelectedItem is Service selectedService)
            {
                // Ищем абонента, которому подключаем услугу (для простоты берем первого из списка, 
                // в реальной программе тут нужна авторизация)
                var abonent = AbonentsList.FirstOrDefault();

                if (abonent != null)
                {
                    abonent.ConnectService(selectedService);
                    dataGridAbonents.Items.Refresh();
                    MessageBox.Show($"Услуга '{selectedService.ServiceName}' подключена {abonent.GetFIO()}!", "Успех");
                }
            }
            else
            {
                MessageBox.Show("Выберите услугу из списка!", "Внимание");
            }
        }

        private void PayBill_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtAbonentIdPay.Text, out int abId))
            {
                var ab = AbonentsList.FirstOrDefault(a => a.Id == abId);
                if (ab != null)
                {
                    // Имитация оплаты: добавляем 500 рублей
                    ab.Balance += 500;
                    ab.IsBlocked = false; // Автоматически разблокируем

                    // Гасим долги (если есть)
                    var myBills = BillsList.Where(b => b.AbonentId == ab.Id && !b.IsPaid).ToList();
                    foreach (var bill in myBills)
                    {
                        if (ab.Balance >= bill.Amount)
                        {
                            ab.Balance -= bill.Amount;
                            bill.IsPaid = true;
                        }
                    }

                    dataGridAbonents.Items.Refresh();
                    MessageBox.Show($"Счет абонента {ab.GetFIO()} пополнен на 500 руб. Долги погашены.", "Оплата");
                }
                else MessageBox.Show("Абонент с таким ID не найден!", "Ошибка");
            }
            else MessageBox.Show("Введите корректный ID!", "Ошибка");
        }
    }
}