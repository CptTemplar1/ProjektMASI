using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ProjektMASI.ServiceClasses
{
    class UnitermService
    {
        // Metoda obsługująca dopasowanie wielkości lini unitermu poziomego w zależności od zmiany wielkości panelu tekstowego
        public void ScaleHUTextPanel(object sender, SizeChangedEventArgs e, StackPanel hUTextPanel, Path topPath)
        {
            if (hUTextPanel != null && topPath != null)
            {
                // Ustawienie szerokości TopPath na szerokość HUTextPanel
                topPath.Width = hUTextPanel.ActualWidth;

                // Dostosowanie danych geometrycznych Path
                topPath.Data = new PathGeometry(new PathFigureCollection
        {
            new PathFigure
            {
                StartPoint = new Point(0, 0),
                Segments = new PathSegmentCollection
                {
                    new QuadraticBezierSegment
                    {
                        Point1 = new Point(topPath.Width / 2, -topPath.Height / 2),
                        Point2 = new Point(topPath.Width, 0)
                    }
                }
            }
        });
            }
        }

        // Metoda obsługująca dopasowanie wielkości lini unitermu pionowego w zależności od zmiany wielkości panelu tekstowego
        public void ScaleVUTextPanel(object sender, SizeChangedEventArgs e, StackPanel vUTextPanel, Path leftPath)
        {
            if (vUTextPanel != null && leftPath != null)
            {
                // Ustawienie szerokości LeftPath na szerokość VUTextPanel
                leftPath.Width = vUTextPanel.ActualHeight;

                // Dostosowanie danych geometrycznych Path
                leftPath.Data = new PathGeometry(new PathFigureCollection
        {
            new PathFigure
            {
                StartPoint = new Point(0, 0),
                Segments = new PathSegmentCollection
                {
                    new QuadraticBezierSegment
                    {
                        Point1 = new Point(leftPath.Width / 2, -leftPath.Height / 2),
                        Point2 = new Point(leftPath.Width, 0)
                    }
                }
            }
        });
            }
        }

        // Metoda zamieniająca miejscami unitermy
        public void Swap(object sender, RoutedEventArgs e, RadioButton leftRadioButton, RadioButton rightRadioButton, TextBox hUValue1TextField, TextBox hUValue2TextField, StackPanel verticalUniterm, StackPanel horizontalUniterm,Button swapButton, Button undoButton, Button clearFieldsButton, TextBox[] textFields)
        {
            TextBoxService textBoxService = new TextBoxService();

            // Sprawdzenie, czy wszystkie pola tekstowe są wypełnione
            if (!textBoxService.IsNotEmpty(textFields))
            {
                MessageBox.Show("Wszystkie pola tekstowe muszą być wypełnione przed wykonaniem zamiany!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool isLeftSelected = leftRadioButton.IsChecked ?? false;

            // Określenie elementów źródłowych i docelowych
            var sourcePanel = isLeftSelected ? hUValue1TextField : hUValue2TextField;
            var targetPanel = verticalUniterm;

            var sourceParent = (Panel)sourcePanel.Parent;
            var targetParent = (Panel)targetPanel.Parent;

            // Przeniesienie elementów
            sourceParent.Children.Remove(sourcePanel);
            targetParent.Children.Remove(targetPanel);

            sourceParent.Children.Add(targetPanel);
            targetParent.Children.Add(sourcePanel);

            // Rzutowanie sender na Button, ponieważ sender to obiekt, który wywołał metodę
            Button? clickedButton = sender as Button;

            // Sprawdzamy, czy rzutowanie powiodło się
            if (clickedButton != null)
            {
                // Uzyskujemy nazwę przycisku
                string buttonName = clickedButton.Name;

                // Ustawiamy odpowiednie opcje dla pozostałych elementów ekranu w zależności od tego, któy przycisk był kliknięty
                if (buttonName == "SwapButton")
                {
                    MainWindow.isSwapped = true;

                    swapButton.IsEnabled = false;
                    undoButton.IsEnabled = true;

                    leftRadioButton.IsEnabled = false;
                    rightRadioButton.IsEnabled = false;

                    clearFieldsButton.IsEnabled = false;

                    sourcePanel.Visibility = Visibility.Hidden;

                    textBoxService.ToggleTextFields(textFields, false);
                }
                else if (buttonName == "UndoButton" || buttonName == "ResetButton")
                {
                    MainWindow.isSwapped = false;

                    swapButton.IsEnabled = true;
                    undoButton.IsEnabled = false;

                    leftRadioButton.IsEnabled = true;
                    rightRadioButton.IsEnabled = true;

                    clearFieldsButton.IsEnabled = true;

                    sourcePanel.Visibility = Visibility.Visible;

                    textBoxService.ToggleTextFields(textFields, true);
                }
            }
        }

        // Metoda obsługująca kliknięcie przycisku Reset
        public void Reset(object sender, RoutedEventArgs e, StackPanel mainContent, Slider scaleSlider, RadioButton leftRadioButton, RadioButton rightRadioButton, TextBox hUValue1TextField, TextBox hUValue2TextField, StackPanel verticalUniterm, StackPanel horizontalUniterm, Button swapButton, Button undoButton, Button clearFieldsButton, TextBox[] textFields)
        {
            TextBoxService textBoxService = new TextBoxService();
            UnitermService unitermService = new UnitermService();

            // Najpierw sprawdzamy stan zmiennej isSwapped, aby sprawdzić, czy elementy zostały zamienione
            if (MainWindow.isSwapped)
            {
                // Jeśli elementy zostały zamienione, przywracamy je do pierwotnego stanu
                unitermService.Swap(sender, e, leftRadioButton, rightRadioButton, hUValue1TextField, hUValue2TextField, verticalUniterm, horizontalUniterm, swapButton, undoButton, clearFieldsButton, textFields);
            }

            // Wyczyść wszystkie pola tekstowe, niezależnie od stanu isSwapped
            textBoxService.ClearTextFields(mainContent);

            // Ustawienie wartości suwaka na początkową wartość
            scaleSlider.Value = 1.0;

            MainWindow.isSwapped = false;
        }
    }
}
