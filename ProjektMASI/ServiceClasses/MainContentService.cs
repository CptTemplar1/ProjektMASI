using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProjektMASI.ServiceClasses
{
    class MainContentService
    {
        // Metoda obsługująca slider, skalująca zawartość okna w zależności od wartości suwaka
        public void ScaleMainContentWindow(object sender, RoutedPropertyChangedEventArgs<double> e, ScaleTransform mainContentScaleTransform, StackPanel mainContent, Grid mainGrid)
        {
            // Sprawdzamy, czy MainContentScaleTransform został zainicjalizowany
            if (mainContentScaleTransform != null)
            {
                double scale = e.NewValue;
                mainContentScaleTransform.ScaleX = scale;
                mainContentScaleTransform.ScaleY = scale;

                // Usuń ustawianie szerokości i wysokości kontenera
                mainContent.Width = mainGrid.ActualWidth * scale;
                mainContent.Height = mainGrid.ActualHeight * scale;
            }
        }
    }
}
