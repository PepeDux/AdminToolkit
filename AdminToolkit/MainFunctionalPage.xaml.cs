using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для MainFunctionalPage.xaml
    /// </summary>
    public partial class MainFunctionalPage : Page
    {
        public MainFunctionalPage()
        {
            InitializeComponent();
        }
        private void CuttingSub_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CoordinaterSub_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void SFLIBSub_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CuttingSub_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(CuttingSub.Text))
            {
                CuttingSub.Text = "0";
            }
        }

        private void CoordinaterSub_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(CoordinaterSub.Text))
            {
                CoordinaterSub.Text = "0";
            }
        }

        private void SFLIBSub_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SFLIBSub.Text))
            {
                SFLIBSub.Text = "0";
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            AddProductsInfo();

            RegistrationClass registerClass = new RegistrationClass();
            Result.Text = registerClass.Registration(Login.Text, Password.Text, Source.Text);
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Result.Text);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            AddProductsInfo();

            UpdateClass updateClass = new UpdateClass();
            updateClass.Update(Login.Text);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteClass deleteClass = new DeleteClass();
            deleteClass.Delete(Login.Text);
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            AddProductsInfo();

            InfoClass infoClass = new InfoClass();
            Result.Text = infoClass.Info(Login.Text);
        }

        private void Extend_Click(object sender, RoutedEventArgs e)
        {
            AddProductsInfo();

            ExtendClass extendClass = new ExtendClass();
            extendClass.Extend(Login.Text);
        }

        private void AddProductsInfo()
        {
            DataBank.products.Clear();

            DataBank.products.Add(new Product("Cutting", Convert.ToInt16(CuttingSub.Text)));
            DataBank.products.Add(new Product("Coordinater", Convert.ToInt16(CoordinaterSub.Text)));
            DataBank.products.Add(new Product("SFLIB", Convert.ToInt16(SFLIBSub.Text)));
        }
    }
}
