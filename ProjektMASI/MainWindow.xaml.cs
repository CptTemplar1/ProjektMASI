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

            // Przypisujemy obsługę zdarzenia ValueChanged po inicjalizacji komponentów
            ScaleSlider.ValueChanged += ScaleSlider_ValueChanged;
        }

        //Metody obsługi wyświetlania zdjęcia diagramów w instrukcji
        private void Icon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Ustawia nakładkę jako widoczną i ukrywa inne elementy
            PreviewOverlay.Visibility = Visibility.Visible;
            TopPanel.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Collapsed;

            // Dopasowanie rozmiaru obrazu do okna, zachowanie proporcji
            var maxWidth = this.ActualWidth;
            var maxHeight = this.ActualHeight;
            var image = PreviewOverlay.Children.OfType<Image>().FirstOrDefault();
            if (image != null)
            {
                double aspectRatio = image.Source.Width / image.Source.Height;
                if (maxWidth / maxHeight > aspectRatio)
                {
                    image.Width = maxHeight * aspectRatio;
                    image.Height = maxHeight;
                }
                else
                {
                    image.Width = maxWidth;
                    image.Height = maxWidth / aspectRatio;
                }
            }
        }

        private void Overlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Ukrywa nakładkę i przywraca widoczność pozostałych elementów
            PreviewOverlay.Visibility = Visibility.Collapsed;
            TopPanel.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Visible;
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Pobranie minimalnej szerokości z kontrolki TextBox
                double minWidth = textBox.MinWidth;

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

                // Obliczanie nowej szerokości na podstawie tekstu
                double newWidth = formattedText.Width + 10; // Dodajemy 10 dla lepszego odstępu

                // Ustawianie szerokości TextBox z zachowaniem minimalnej szerokości
                textBox.Width = Math.Max(newWidth, minWidth);
            }
        }



        private void HUTextPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (HUTextPanel != null && TopPath != null)
            {
                // Ustawienie szerokości TopPath na szerokość HUTextPanel
                TopPath.Width = HUTextPanel.ActualWidth;

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

        private void VUTextPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (VUTextPanel != null && LeftPath != null)
            {
                // Ustawienie wysokości LeftPath na wysokość VUTextPanel
                //LeftPath.Height = VUTextPanel.ActualHeight / 2; //modyfikacja wysokości jest niepotrzebna, bo wtedy robi się za bardzo wypukła linia

                // Ustawienie szerokości LeftPath na szerokość VUTextPanel
                LeftPath.Width = VUTextPanel.ActualHeight;

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

        //Wyczyść wszystkie pola tekstowe w kontenerze MainContent
        private void ClearFieldsButton_Click(object sender, RoutedEventArgs e)
        {
            // Wywołujemy metodę czyszczenia dla MainContent
            ClearTextFields(MainContent);
        }

        private void ClearTextFields(Panel container)
        {
            // Iteracja po wszystkich dzieciach kontenera
            foreach (var child in container.Children)
            {
                // Jeśli dziecko jest TextBox, to czyścimy jego zawartość
                if (child is TextBox textBox)
                {
                    textBox.Clear();
                }
                // Jeśli dziecko jest kontenerem (np. StackPanel, Grid), rekurencyjnie wywołujemy metodę
                else if (child is Panel childPanel)
                {
                    ClearTextFields(childPanel); // Rekurencja na zagnieżdżonym kontenerze
                }
            }
        }

        //Metoda skalująca wielkość głównego okna za pomocą Slidera
        private void ScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Sprawdzamy, czy MainContentScaleTransform został zainicjalizowany
            if (MainContentScaleTransform != null)
            {
                double scale = e.NewValue;
                MainContentScaleTransform.ScaleX = scale;
                MainContentScaleTransform.ScaleY = scale;

                // Usuń ustawianie szerokości i wysokości kontenera
                MainContent.Width = MainGrid.ActualWidth * scale;
                MainContent.Height = MainGrid.ActualHeight * scale;
            }
        }


        // Metoda sprawdzająca, czy wszystkie pola tekstowe są wypełnione
        private bool AreAllFieldsFilled()
        {
            return !string.IsNullOrWhiteSpace(HUValue1TextField.Text) &&
                   !string.IsNullOrWhiteSpace(HUValue2TextField.Text) &&
                   !string.IsNullOrWhiteSpace(VUValue1TextField.Text) &&
                   !string.IsNullOrWhiteSpace(VUValue2TextField.Text);
        }

        // Metoda zamieniająca miejscami unitermy
        private void SwapElements(object sender, RoutedEventArgs e)
            {
                // Sprawdzenie, czy wszystkie pola tekstowe są wypełnione
                if (!AreAllFieldsFilled())
                {
                    MessageBox.Show("Wszystkie pola tekstowe muszą być wypełnione przed wykonaniem zamiany!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool isLeftSelected = LeftRadioButton.IsChecked ?? false;

                // Określenie elementów źródłowych i docelowych
                var sourcePanel = isLeftSelected ? HUValue1TextField : HUValue2TextField;
                var targetPanel = VerticalUniterm;

                var sourceParent = (Panel)sourcePanel.Parent;
                var targetParent = (Panel)targetPanel.Parent;

                // Przeniesienie elementów
                sourceParent.Children.Remove(sourcePanel);
                targetParent.Children.Remove(targetPanel);

                sourceParent.Children.Add(targetPanel);
                targetParent.Children.Add(sourcePanel);

                // Zmiana stanu przycisków
                SwapButton.IsEnabled = false;
                UndoButton.IsEnabled = true;

                // Wyłączenie widoczności panelu tekstowego
                sourcePanel.Visibility = Visibility.Hidden;
            }

            // Cofnięcie zamiany
            private void UndoButton_Click(object sender, RoutedEventArgs e)
            {
                // Implementacja cofnięcia zmian (odwrócenie operacji zamiany)
                SwapButton.IsEnabled = true;
                UndoButton.IsEnabled = false;
            }



            //koniec klasy 
        }
    }