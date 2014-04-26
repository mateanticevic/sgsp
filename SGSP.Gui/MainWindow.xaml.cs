using Microsoft.Win32;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SGSP.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                var model = SGSP.Converter.ModelCreator.Model.CreateChapter(dialog.FileName);

                listScenes.ItemsSource = model.Scenes;
                listObjects.ItemsSource = model.Objects;
                listSlideScenes.ItemsSource = model.SlideScenes;

                chapterItems.Visibility = System.Windows.Visibility.Visible;

                var sf = new Converter.ScriptFactory(dialog.FileName);

                sf.GenerateScript(model);
            }

            
        }

        private void listScenes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem.DataContext = ((ListBox)sender).SelectedItem;
        }

        private void listObjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem.DataContext = ((ListBox)sender).SelectedItem;
        }
    }
}
