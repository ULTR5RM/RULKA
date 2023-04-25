using Microsoft.Win32;
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
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        Product product = new Product();
        private bool _editProduct = false;
        public AddEditProductPage(Product currentProduct)
        {
            InitializeComponent();
            if (currentProduct != null)
            {
                _editProduct = true;
                product = currentProduct;
                btnDeleteProduct.Visibility = Visibility.Visible;
                txtArticle.IsEnabled = false;
            }

            DataContext = product;
            cmbCategory.ItemsSource = CarServiceEntities.GetContext().ProductCategory.ToList();
            cmbSupplier.ItemsSource = CarServiceEntities.GetContext().Suppler.ToList();

        }
        private void btnEnterImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog GetImageDialog = new OpenFileDialog();

                GetImageDialog.Filter = "Файлы изображений: (*.png, *.jpg, *jpeg)| *.png; *.jpg; *jpeg";
                GetImageDialog.InitialDirectory = "AutoService.Rule.Antuh\\Resourse\\Product";
                if (GetImageDialog.ShowDialog() == true) product.ProductImage = GetImageDialog.SafeFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы жействительно хотите удалить {product.ProductName}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    CarServiceEntities.GetContext().Product.Remove(product);
                    CarServiceEntities.GetContext().SaveChanges();
                    MessageBox.Show("Запись удалена!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (product.ProductCost < 0) errors.Append("Стоимость не может быть отрицательной");
            if (product.MinCount < 0) errors.AppendLine("Минимальное количество не может быть отрицательным");
            if (product.ProductDiscountAmount > product.MaxDiscountAmount) errors.AppendLine("Действующая скидка не может быть больше максимальной");
            if (product.ProductDescription == null) errors.AppendLine("Зполните обязательные поля");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_editProduct == false)
                CarServiceEntities.GetContext().Product.Add(product);
            try
            {
                CarServiceEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
