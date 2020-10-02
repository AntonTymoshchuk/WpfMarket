using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMarket
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonTemplateBorder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Border border = sender as Border;
            ContentPresenter contentPresenter = border.Child as ContentPresenter;

            contentPresenter.Width = e.NewSize.Width - 12;
            contentPresenter.Height = e.NewSize.Height - 12;

            DropShadowEffect dropShadowEffect = new DropShadowEffect();
            dropShadowEffect.Color = Colors.Black;
            dropShadowEffect.Direction = 0;
            dropShadowEffect.ShadowDepth = 0;
            dropShadowEffect.BlurRadius = e.NewSize.Width - 32;
            dropShadowEffect.Opacity = 0.4;

            border.Effect = dropShadowEffect;
        }

        private void ButtonTemplateBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            double x = e.GetPosition(border).X / border.Width;
            double y = e.GetPosition(border).Y / border.Height;

            GradientStopCollection backgroundGradientStopCollection = new GradientStopCollection();
            backgroundGradientStopCollection.Add(new GradientStop(Colors.White, -3));
            backgroundGradientStopCollection.Add(new GradientStop((FindResource("TurquoiseBrush") as SolidColorBrush).Color, 1));

            RadialGradientBrush backgroundRadialGradientBrush = new RadialGradientBrush(backgroundGradientStopCollection);
            backgroundRadialGradientBrush.Center = new Point(x, y);
            backgroundRadialGradientBrush.GradientOrigin = new Point(x, y);

            GradientStopCollection borderGradientStopCollection = new GradientStopCollection();
            borderGradientStopCollection.Add(new GradientStop(Colors.White, 0.38));
            borderGradientStopCollection.Add(new GradientStop((FindResource("TurquoiseBrush") as SolidColorBrush).Color, 0.62));

            RadialGradientBrush borderRadialGradientBrush = new RadialGradientBrush(borderGradientStopCollection);
            borderRadialGradientBrush.Center = new Point(x, y);
            borderRadialGradientBrush.GradientOrigin = new Point(x, y);

            border.Background = backgroundRadialGradientBrush;
            border.BorderBrush = borderRadialGradientBrush;
        }

        private void ButtonTemplateBorder_MouseMove(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            double x = e.GetPosition(border).X / border.Width;
            double y = e.GetPosition(border).Y / border.Height;

            GradientStopCollection backgroundGradientStopCollection = new GradientStopCollection();
            backgroundGradientStopCollection.Add(new GradientStop(Colors.White, -3));
            backgroundGradientStopCollection.Add(new GradientStop((FindResource("TurquoiseBrush") as SolidColorBrush).Color, 1));

            RadialGradientBrush backgroundRadialGradientBrush = new RadialGradientBrush(backgroundGradientStopCollection);
            backgroundRadialGradientBrush.Center = new Point(x, y);
            backgroundRadialGradientBrush.GradientOrigin = new Point(x, y);

            GradientStopCollection borderGradientStopCollection = new GradientStopCollection();
            borderGradientStopCollection.Add(new GradientStop(Colors.White, 0.38));
            borderGradientStopCollection.Add(new GradientStop((FindResource("TurquoiseBrush") as SolidColorBrush).Color, 0.62));

            RadialGradientBrush borderRadialGradientBrush = new RadialGradientBrush(borderGradientStopCollection);
            borderRadialGradientBrush.Center = new Point(x, y);
            borderRadialGradientBrush.GradientOrigin = new Point(x, y);

            border.Background = backgroundRadialGradientBrush;
            border.BorderBrush = borderRadialGradientBrush;
        }

        private void ButtonTemplateBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            double x = e.GetPosition(border).X;
            double y = e.GetPosition(border).Y;

            if (x > 0 && x < border.Width && y > 0 && y < border.Height)
                return;

            border.Background = new SolidColorBrush((FindResource("TurquoiseBrush") as SolidColorBrush).Color);
            border.BorderBrush = new SolidColorBrush((FindResource("TurquoiseBrush") as SolidColorBrush).Color);
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.ShowDialog();
            if (addProductWindow.Ok == true)
            {
                marketViewModel.AddProduct
                (
                    addProductWindow.ProductName,
                    addProductWindow.Quantity,
                    addProductWindow.Price,
                    addProductWindow.BinaryImage
                );
            }
        }

        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (MarketListBox.SelectedIndex == -1)
                return;

            EditProductWindow editProductWindow = new EditProductWindow();
            editProductWindow.ProductName = marketViewModel.Products[MarketListBox.SelectedIndex].Name;
            editProductWindow.Quantity = marketViewModel.Products[MarketListBox.SelectedIndex].Quantity;
            editProductWindow.Price = marketViewModel.Products[MarketListBox.SelectedIndex].Price;
            
            BitmapImage bitmapImage = marketViewModel.Products[MarketListBox.SelectedIndex].Image as BitmapImage;
            Stream stream = bitmapImage.StreamSource;
            BinaryReader binaryReader = new BinaryReader(stream);
            byte[] binaryImage = new byte[stream.Length];
            binaryReader.Read(binaryImage, 0, binaryImage.Length);

            editProductWindow.ShowDialog();
            if (editProductWindow.Ok == true)
            {
                marketViewModel.EditProduct
                (
                    MarketListBox.SelectedIndex,
                    editProductWindow.ProductName,
                    editProductWindow.Quantity,
                    editProductWindow.Price
                );
            }
        }
    }
}
