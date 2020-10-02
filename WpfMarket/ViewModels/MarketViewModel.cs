using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfMarket.Commands;
using WpfMarket.DatabaseContext;
using WpfMarket.Models;

namespace WpfMarket.ViewModels
{
    public class MarketViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ProductModel> productModels;
        private string name;
        private string phone;
        private string email;
        private string address;
        private ICommand deleteProductCommand;

        public ObservableCollection<ProductModel> Products
        {
            get { return productModels; }
            set
            {
                productModels = value;
                OnPropertyChanged("Products");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        public ICommand DeleteProductCommand
        {
            get { return deleteProductCommand; }
            set { deleteProductCommand = value; }
        }

        public void DeleteProduct(object productModel)
        {
            if (productModel == null)
                return;
            WpfMarketContext wpfMarketContext = new WpfMarketContext();
            foreach (Product product in wpfMarketContext.Products)
            {
                if (product.Name.Equals((productModel as ProductModel).Name))
                {
                    wpfMarketContext.Products.Attach(product);
                    wpfMarketContext.Products.Remove(product);
                }
            }
            wpfMarketContext.SaveChanges();
            productModels.Remove(productModel as ProductModel);
        }

        public bool CanDeleteProduct(object productModel)
        {
            if (productModel == null)
                return false;
            else return true;
        }

        public MarketViewModel()
        {
            name = "WpfMarket - Swidzerland";
            phone = "+41 066 686 52 59";
            email = "wpfmarket@gmail.com";
            address = "Hottingen, Zürich - Swidzrland";

            productModels = new ObservableCollection<ProductModel>();
            WpfMarketContext wpfMarketContext = new WpfMarketContext();

            foreach (Product product in wpfMarketContext.Products)
            {
                ProductModel productModel = new ProductModel(product.Name, product.Quantity, product.Price, product.Image);
                productModels.Add(productModel);
            }

            deleteProductCommand = new DelegateCommand(DeleteProduct, CanDeleteProduct);
        }

        public void AddProduct(string productName, int quantity, decimal price, byte[] binaryImage)
        {
            WpfMarketContext wpfMarketContext = new WpfMarketContext();
            Product product = new Product()
            {
                Name = productName,
                Quantity = quantity,
                Price = price,
                Image = binaryImage
            };
            wpfMarketContext.Products.Add(product);
            wpfMarketContext.SaveChanges();
            
            ProductModel productModel = new ProductModel(productName, quantity, price, binaryImage);
            productModels.Add(productModel);
        }

        public void EditProduct(int index, string productName, int quantity, decimal price)
        {
            byte[] binaryImage = null;
            WpfMarketContext wpfMarketContext = new WpfMarketContext();

            foreach (Product product in wpfMarketContext.Products)
            {
                if (product.Name.Equals(productModels[index].Name))
                {
                    binaryImage = product.Image;
                    wpfMarketContext.Products.Attach(product);
                    wpfMarketContext.Products.Remove(product);
                }
            }

            Product newProduct = new Product()
            {
                Name = productName,
                Quantity = quantity,
                Price = price,
                Image = binaryImage
            };

            wpfMarketContext.Products.Add(newProduct);
            wpfMarketContext.SaveChanges();

            productModels[index].Name = productName;
            productModels[index].Quantity = quantity;
            productModels[index].Price = price;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

//Csharp   C#
