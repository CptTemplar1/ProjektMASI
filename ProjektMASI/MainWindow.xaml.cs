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


        private void TopTextPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (TopTextPanel != null && TopPath != null)
            {
                // Ustawienie szerokości TopPath na szerokość TopTextPanel
                TopPath.Width = TopTextPanel.ActualWidth;

                // Dostosowanie danych geometrycznych Path, aby linia była skalowana poprawnie
                TopPath.Data = new PathGeometry(new PathFigureCollection
        {
            new PathFigure
            {
                StartPoint = new Point(0, 0),
                Segments = new PathSegmentCollection
                {
                    new QuadraticBezierSegment
                    {
                        Point1 = new Point(TopPath.Width / 2, -TopPath.Height / 2),
                        Point2 = new Point(TopPath.Width, 0)
                    }
                }
            }
        });
            }
        }

        private void LeftTextPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (LeftTextPanel != null && LeftPath != null)
            {
                // Ustawienie wysokości LeftPath na wysokość LeftTextPanel
                LeftPath.Height = LeftTextPanel.ActualHeight;

                // Ustawienie szerokości LeftPath na szerokość LeftTextPanel
                LeftPath.Width = LeftTextPanel.ActualWidth / 2;

                // Dostosowanie danych geometrycznych Path, aby linia była skalowana poprawnie
                LeftPath.Data = new PathGeometry(new PathFigureCollection
        {
            new PathFigure
            {
                StartPoint = new Point(0, 0),
                Segments = new PathSegmentCollection
                {
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



        //koniec klasy 
    }
}