using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace WpfMarket
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindowxaml.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private bool ok = false;
        private string productName;
        private int quantity;
        private decimal price;
        private byte[] binaryImage;

        public bool Ok
        {
            get { return ok; }
        }

        public string ProductName
        {
            get { return productName; }
        }

        public int Quantity
        {
            get { return quantity; }
        }

        public decimal Price
        {
            get { return price; }
        }

        public byte[] BinaryImage
        {
            get { return binaryImage; }
        }

        public AddProductWindow()
        {
            InitializeComponent();
        }

        private void WindowButtonTemplateBorder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Border border = sender as Border;
            ContentPresenter contentPresenter = border.Child as ContentPresenter;

            contentPresenter.Width = e.NewSize.Width - 6;
            contentPresenter.Height = e.NewSize.Height - 6;

            DropShadowEffect dropShadowEffect = new DropShadowEffect();
            dropShadowEffect.Color = Colors.Black;
            dropShadowEffect.Direction = 0;
            dropShadowEffect.ShadowDepth = 0;
            dropShadowEffect.BlurRadius = e.NewSize.Width - 16;
            dropShadowEffect.Opacity = 0.4;

            border.Effect = dropShadowEffect;
        }

        private void ButtonTemplateBorder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Border border = sender as Border;
            ContentPresenter contentPresenter = border.Child as ContentPresenter;

            contentPresenter.Width = e.NewSize.Width - 9;
            contentPresenter.Height = e.NewSize.Height - 9;

            DropShadowEffect dropShadowEffect = new DropShadowEffect();
            dropShadowEffect.Color = Colors.Black;
            dropShadowEffect.Direction = 0;
            dropShadowEffect.ShadowDepth = 0;
            dropShadowEffect.BlurRadius = e.NewSize.Width - 24;
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
            backgroundGradientStopCollection.Add(new GradientStop((FindResource("PeterRiverBrush") as SolidColorBrush).Color, 1));

            RadialGradientBrush backgroundRadialGradientBrush = new RadialGradientBrush(backgroundGradientStopCollection);
            backgroundRadialGradientBrush.Center = new Point(x, y);
            backgroundRadialGradientBrush.GradientOrigin = new Point(x, y);

            GradientStopCollection borderGradientStopCollection = new GradientStopCollection();
            borderGradientStopCollection.Add(new GradientStop(Colors.White, 0.38));
            borderGradientStopCollection.Add(new GradientStop((FindResource("PeterRiverBrush") as SolidColorBrush).Color, 0.62));

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
            backgroundGradientStopCollection.Add(new GradientStop((FindResource("PeterRiverBrush") as SolidColorBrush).Color, 1));

            RadialGradientBrush backgroundRadialGradientBrush = new RadialGradientBrush(backgroundGradientStopCollection);
            backgroundRadialGradientBrush.Center = new Point(x, y);
            backgroundRadialGradientBrush.GradientOrigin = new Point(x, y);

            GradientStopCollection borderGradientStopCollection = new GradientStopCollection();
            borderGradientStopCollection.Add(new GradientStop(Colors.White, 0.38));
            borderGradientStopCollection.Add(new GradientStop((FindResource("PeterRiverBrush") as SolidColorBrush).Color, 0.62));

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

            border.Background = new SolidColorBrush((FindResource("PeterRiverBrush") as SolidColorBrush).Color);
            border.BorderBrush = new SolidColorBrush((FindResource("PeterRiverBrush") as SolidColorBrush).Color);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = string.Empty;

            if (NameTextBox.Text.Equals(string.Empty))
            {
                ErrorTextBlock.Text = "* Name is empty!";
                return;
            }
            else if (QuantityTextBox.Text.Equals(string.Empty))
            {
                ErrorTextBlock.Text = "* Quantity is empty!";
                return;
            }
            else if (PriceTextBox.Text.Equals(string.Empty))
            {
                ErrorTextBlock.Text = "* Price is empty!";
                return;
            }
            else if (binaryImage == null)
            {
                ErrorTextBlock.Text = "* Image is empty!";
                return;
            }

            try { Convert.ToInt32(QuantityTextBox.Text); }
            catch
            {
                ErrorTextBlock.Text = "* Quantity is not in a correct format!";
                return;
            }
            try { Convert.ToDecimal(PriceTextBox.Text); }
            catch
            {
                ErrorTextBlock.Text = "* Price is not in a correct format!";
                return;
            }

            productName = NameTextBox.Text;
            quantity = Convert.ToInt32(QuantityTextBox.Text);
            price = Convert.ToDecimal(PriceTextBox.Text);

            ok = true;
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ok = false;
            Close();
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Filter = "Image (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                ImageTextBox.Text = openFileDialog.SafeFileName;
                FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                binaryImage = new byte[fileStream.Length];
                fileStream.Read(binaryImage, 0, binaryImage.Length);
            }
        }
    }
}
