using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektMASI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        //metoda rozszerzająca pola tekstowe dopasowując je do dłuższego tekstu
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Mierzenie szerokości tekstu w TextBox
                var formattedText = new FormattedText(
                    textBox.Text,
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface(textBox.FontFamily, textBox.FontStyle, textBox.FontWeight, textBox.FontStretch),
                    textBox.FontSize,
                    Brushes.Black,
                    new NumberSubstitution(),
                    1);

                // Dodanie marginesu do szerokości i przypisanie do szerokości TextBox
                textBox.Width = formattedText.Width + 10; // Dodajemy 10 dla lepszego odstępu
            }
        }

        private void UpdatePath(object sender, SizeChangedEventArgs e)
        {
            // Aktualizuje górną linię (nawias) dla pól tekstowych obok siebie
            var path = (Path)((Grid)sender).Children[0];
            double width = e.NewSize.Width - 20;
            path.Data = Geometry.Parse(string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "M 10,0 C 10,0 {0},-20 {1},0",
                width / 2, width + 10));
        }

        private void UpdateVerticalPath(object sender, SizeChangedEventArgs e)
        {
            // Aktualizuje lewą linię (nawias) dla pól tekstowych jedno pod drugim
            var path = (Path)((Grid)sender).Children[0];
            double height = e.NewSize.Height - 20;
            path.Data = Geometry.Parse(string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "M 0,10 C 0,10 -20,{0} 0,{1}",
                height / 2, height + 10));
        }


        private void StackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Sprawdzamy, czy LeftTextPanel jest przypisany, zanim spróbujemy uzyskać jego wysokość.
            if (LeftTextPanel != null && LeftPath != null)
            {
                // Ustawiamy wysokość LeftPath na wysokość LeftTextPanel
                LeftPath.Height = LeftTextPanel.ActualHeight;

                // Szerokość LeftPath może być ustawiona na jakąś stałą wartość lub na podstawie szerokości LeftTextPanel.
                // Na przykład tutaj używamy połowy szerokości LeftTextPanel.
                LeftPath.Width = LeftTextPanel.ActualWidth / 2;

                // Dostosowanie danych geometrycznych Path, aby linia była skalowana poprawnie
                LeftPath.Data = new PathGeometry(new PathFigureCollection
        {
            new PathFigure
            {
                StartPoint = new Point(0, 0),
                Segments = new PathSegmentCollection
                {
                    // Dopasowanie parametrów krzywej, aby obejmowała tekst
                    new QuadraticBezierSegment
                    {
                        Point1 = new Point(LeftPath.Width / 2, -LeftPath.Height / 2),
                        Point2 = new Point(LeftPath.Width, 0)
                    }
                }
            }
        });
            }
        }
    }
}