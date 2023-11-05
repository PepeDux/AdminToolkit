using AdminToolkit.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminToolkit
{
    /// <summary>
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text.Length > 0 && password.Password.Length > 0)
            {
                if (login.Text == Encoding.UTF8.GetString(Convert.FromBase64String(Settings.Default["Login"].ToString())) &&
                    password.Password == Encoding.UTF8.GetString(Convert.FromBase64String(Settings.Default["Password"].ToString())))
                {
                    NavigationService.Navigate(new MainFunctionalPage());
                }
            }
            else MessageBox.Show("Введите логин и пэроль");
        }

        private void signIn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (login.Text != Encoding.UTF8.GetString(Convert.FromBase64String(Settings.Default["Login"].ToString())) ||
               password.Password != Encoding.UTF8.GetString(Convert.FromBase64String(Settings.Default["Password"].ToString())))
            {
                var random = new Random();
                FrameworkElement pnlClient = this.Content as FrameworkElement;

                signIn.Margin = new Thickness(random.Next(0, (int)pnlClient.ActualWidth - 70), random.Next(0, (int)pnlClient.ActualHeight - 70), 0, 0);
            }
        }
    }
}
