using System.Windows;

namespace KyrsovyaPARAMON
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            // Задаем жесткие данные для входа
            string correctLogin = "ivanov";
            string correctPassword = "1111";

            // Проверяем введенные данные
            if (txtLogin.Text == correctLogin && txtPassword.Password == correctPassword)
            {
                // Если всё верно - открываем главное окно
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Закрываем окно авторизации
                this.Close();
            }
            else
            {
                // Если ошибка - показываем красный текст
                txtError.Visibility = Visibility.Visible;
            }
        }
    }
}