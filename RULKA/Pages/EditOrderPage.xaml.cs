using RULKA.Entities;
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

namespace RULKA.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditOrderPage.xaml
    /// </summary>
    public partial class EditOrderPage : Page
    {
        public EditOrderPage()
        {
            InitializeComponent();
            var orders = CarServiceEntities.GetContext().Order.ToList();
            LViewOrder.ItemsSource = orders;
            DataContext = this;
            UpdateData();
        }
        public void UpdateData()
        {
            var result = CarServiceEntities.GetContext().Order.ToList();

            if (cmbFilter.SelectedIndex == 1) result = result.Where(p => p.OrderStatusID == 1).ToList();
            if (cmbFilter.SelectedIndex == 2) result = result.Where(p => p.OrderStatusID == 2).ToList();
     
           // result = result.Where(p => p.ClientID.ToBoolean();
            LViewOrder.ItemsSource = result;
        }
        public string[] FilterList { get; set; } =
        {
            "Все заказы",
            "Новый",
            "Завершен"
        };

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void txtSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void btnEditOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditCurrentOrderPage((sender as Button).DataContext as Order));
        }
        private void LViewOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
