using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace ProjektMASI.ServiceClasses
{
    class TextBoxService
    {
        // Metoda obsługująca zmianę szerokości pola tekstowego w zależności od długości wprowadzonego tekstu
        public void ScaleTextBox(object sender, TextChangedEventArgs e)
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


        // Metoda rekurencyjnie czyści zawartość pól tekstowych w kontenerze określonym w parametrze
        public void ClearTextFields(Panel container)
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


        // Metoda sprawdzająca, czy wszystkie pola tekstowe w tablicy są wypełnione
        public bool IsNotEmpty(TextBox[] textFields)
        {
            foreach (var field in textFields)
            {
                if (string.IsNullOrWhiteSpace(field.Text))
                {
                    return false;
                }
            }
            return true;
        }

        // Metoda blokująca/odblokowująca edytowanie pól tekstowych na podstawie parametru
        public void ToggleTextFields(TextBox[] textFields, bool enable)
        {
            foreach (var field in textFields)
            {
                field.IsEnabled = enable;
            }
        }
    }
}
