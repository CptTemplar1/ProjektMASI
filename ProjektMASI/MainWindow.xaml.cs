using ProjektMASI.ServiceClasses;
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
    public partial class MainWindow : Window
    {
        public static bool isSwapped = false; // zmienna globalna określająca czy aktualnie unitermy są zamienione miejscami

        private OverlayService overlayService;
        private TextBoxService textBoxService;
        private UnitermService unitermService;
        private MainContentService mainContentService;
        private TextBox[] textFields;

        public MainWindow()
        {
            overlayService = new OverlayService();
            textBoxService = new TextBoxService();
            unitermService = new UnitermService();
            mainContentService = new MainContentService();


            InitializeComponent();

            // Przypisujemy obsługę zdarzenia ValueChanged do suwaka ScaleSlider po inicjalizacji komponentów okna
            ScaleSlider.ValueChanged += ScaleSlider_ValueChanged;

            textFields = new TextBox[] { HUValue1TextField, HUValue2TextField, VUValue1TextField, VUValue2TextField };
        }

        // Metoda obsługująca kliknięcie ikony w górnym panelu wyświetlającej podgląd zdjęcia diagramu na cały ekran
        private void Icon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            overlayService.IconClicked(sender, e, PreviewOverlay, TopPanel, MainGrid, this);
        }

        // Metoda obsługująca kliknięcie na nakładkę z podglądem zdjęcia diagramu, wyłaczająca podgląd
        private void Overlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            overlayService.OverlayClicked(sender, e, PreviewOverlay, TopPanel, MainGrid);
        }
      
        // Metoda obsługująca zmianę szerokości pola tekstowego w zależności od długości wprowadzonego tekstu
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBoxService.ScaleTextBox(sender, e);
        }
       
        // Metoda obsługująca dopasowanie wielkości lini unitermu poziomego w zależności od zmiany wielkości panelu tekstowego
        private void HUTextPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            unitermService.ScaleHUTextPanel(sender, e, HUTextPanel, TopPath);
        }

        // Metoda obsługująca dopasowanie wielkości lini unitermu pionowego w zależności od zmiany wielkości panelu tekstowego
        private void VUTextPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            unitermService.ScaleVUTextPanel(sender, e, VUTextPanel, LeftPath);
        }

        // Metoda obsługująca kliknięcie przycisku ClearFieldsButton
        private void ClearFieldsButton_Clicked(object sender, RoutedEventArgs e)
        {
            textBoxService.ClearTextFields(MainContent);
        }

        // Metoda obsługująca slider, skalująca zawartość okna w zależności od wartości suwaka
        private void ScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mainContentService.ScaleMainContentWindow(sender, e, MainContentScaleTransform, MainContent, MainGrid);
        }
        
        // Metoda zamieniająca miejscami unitermy
        private void SwapUniterms(object sender, RoutedEventArgs e)
        {
            unitermService.Swap(sender, e, LeftRadioButton, RightRadioButton, HUValue1TextField, HUValue2TextField, VerticalUniterm, HorizontalUniterm, SwapButton, UndoButton, ClearFieldsButton, textFields);
        }

        // Metoda obsługująca kliknięcie przycisku Reset
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            unitermService.Reset(sender, e, MainContent, ScaleSlider, LeftRadioButton, RightRadioButton, HUValue1TextField, HUValue2TextField, VerticalUniterm, HorizontalUniterm, SwapButton, UndoButton, ClearFieldsButton, textFields);
        }
    }
}