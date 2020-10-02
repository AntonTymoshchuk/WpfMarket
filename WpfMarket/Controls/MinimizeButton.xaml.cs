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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMarket.Controls
{
    /// <summary>
    /// Логика взаимодействия для MinimizeButton.xaml
    /// </summary>
    public partial class MinimizeButton : UserControl
    {
        public MinimizeButton()
        {
            InitializeComponent();
        }

        private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
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

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            double x = e.GetPosition(border).X / border.Width;
            double y = e.GetPosition(border).Y / border.Height;

            GradientStopCollection backgroundGradientStopCollection = new GradientStopCollection();
            backgroundGradientStopCollection.Add(new GradientStop(Colors.White, -3));
            backgroundGradientStopCollection.Add(new GradientStop((FindResource("EmeraldBrush") as SolidColorBrush).Color, 1));

            RadialGradientBrush backgroundRadialGradientBrush = new RadialGradientBrush(backgroundGradientStopCollection);
            backgroundRadialGradientBrush.Center = new Point(x, y);
            backgroundRadialGradientBrush.GradientOrigin = new Point(x, y);

            GradientStopCollection borderGradientStopCollection = new GradientStopCollection();
            borderGradientStopCollection.Add(new GradientStop(Colors.White, 0.38));
            borderGradientStopCollection.Add(new GradientStop((FindResource("EmeraldBrush") as SolidColorBrush).Color, 0.62));

            RadialGradientBrush borderRadialGradientBrush = new RadialGradientBrush(borderGradientStopCollection);
            borderRadialGradientBrush.Center = new Point(x, y);
            borderRadialGradientBrush.GradientOrigin = new Point(x, y);

            border.Background = backgroundRadialGradientBrush;
            border.BorderBrush = borderRadialGradientBrush;
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            double x = e.GetPosition(border).X / border.Width;
            double y = e.GetPosition(border).Y / border.Height;

            GradientStopCollection backgroundGradientStopCollection = new GradientStopCollection();
            backgroundGradientStopCollection.Add(new GradientStop(Colors.White, -3));
            backgroundGradientStopCollection.Add(new GradientStop((FindResource("EmeraldBrush") as SolidColorBrush).Color, 1));

            RadialGradientBrush backgroundRadialGradientBrush = new RadialGradientBrush(backgroundGradientStopCollection);
            backgroundRadialGradientBrush.Center = new Point(x, y);
            backgroundRadialGradientBrush.GradientOrigin = new Point(x, y);

            GradientStopCollection borderGradientStopCollection = new GradientStopCollection();
            borderGradientStopCollection.Add(new GradientStop(Colors.White, 0.38));
            borderGradientStopCollection.Add(new GradientStop((FindResource("EmeraldBrush") as SolidColorBrush).Color, 0.62));

            RadialGradientBrush borderRadialGradientBrush = new RadialGradientBrush(borderGradientStopCollection);
            borderRadialGradientBrush.Center = new Point(x, y);
            borderRadialGradientBrush.GradientOrigin = new Point(x, y);

            border.Background = backgroundRadialGradientBrush;
            border.BorderBrush = borderRadialGradientBrush;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            double x = e.GetPosition(border).X;
            double y = e.GetPosition(border).Y;

            if (x > 0 && x < border.Width && y > 0 && y < border.Height)
                return;

            border.Background = new SolidColorBrush((FindResource("EmeraldBrush") as SolidColorBrush).Color);
            border.BorderBrush = new SolidColorBrush((FindResource("EmeraldBrush") as SolidColorBrush).Color);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
