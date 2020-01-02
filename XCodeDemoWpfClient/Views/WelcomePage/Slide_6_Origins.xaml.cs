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

namespace XCodeDemoWpfClient.Views.WelcomePage
{
    /// <summary>
    /// Slide_6_Origins.xaml 的交互逻辑
    /// </summary>
    public partial class Slide_6_Origins : UserControl
    {
        public Slide_6_Origins()
        {
            InitializeComponent();
        }

        private void FirstSlideButton_OnClick(object sender, RoutedEventArgs e)
        {
            Transitioner.SelectedIndex = 0;
        }

        private void SecondSlideButton_OnClick(object sender, RoutedEventArgs e)
        {
            Transitioner.SelectedIndex = 1;
        }

        private void ThreadSlideButton_OnClick(object sender, RoutedEventArgs e)
        {
            Transitioner.SelectedIndex = 2;
        }
    }
}
