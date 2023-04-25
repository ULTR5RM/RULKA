using System;
using RULKA.Entities;
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
    /// Логика взаимодействия для EditCurrentOrderPage.xaml
    /// </summary>
    public partial class EditCurrentOrderPage : Page
    {
        public EditCurrentOrderPage(Order currentOrder)
        {
            InitializeComponent();
            DataContext = currentOrder;
            cmbStatus.ItemsSource = CarServiceEntities.GetContext().OrderStatus.ToList();
            cmbPickupPoint.ItemsSource = CarServiceEntities.GetContext().PickupPoint.ToList();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CarServiceEntities.GetContext().SaveChanges();
                MessageBox.Show("Изменено");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
