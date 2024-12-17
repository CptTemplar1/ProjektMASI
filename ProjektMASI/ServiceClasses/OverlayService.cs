using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace ProjektMASI.ServiceClasses
{
    class OverlayService
    {
        // Metoda obsługująca kliknięcie ikony w górnym panelu wyświetlającej podgląd zdjęcia diagramu na cały ekran
        public void IconClicked(object sender, MouseButtonEventArgs e, Grid previewOverlay, Grid topPanel, Grid mainGrid, MainWindow mainWindow)
        {
            // Ustawia nakładkę jako widoczną i ukrywa inne elementy
            previewOverlay.Visibility = Visibility.Visible;
            topPanel.Visibility = Visibility.Collapsed;
            mainGrid.Visibility = Visibility.Collapsed;

            // Dopasowanie rozmiaru obrazu do okna, zachowanie proporcji
            var maxWidth = mainWindow.ActualWidth;
            var maxHeight = mainWindow.ActualHeight;
            var image = previewOverlay.Children.OfType<Image>().FirstOrDefault();
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

        // Metoda obsługująca kliknięcie na nakładkę z podglądem zdjęcia diagramu, wyłaczająca podgląd
        public void OverlayClicked(object sender, MouseButtonEventArgs e, Grid previewOverlay, Grid topPanel, Grid mainGrid)
        {
            // Ukrywa nakładkę i przywraca widoczność pozostałych elementów
            previewOverlay.Visibility = Visibility.Collapsed;
            topPanel.Visibility = Visibility.Visible;
            mainGrid.Visibility = Visibility.Visible;
        }
    }
}
