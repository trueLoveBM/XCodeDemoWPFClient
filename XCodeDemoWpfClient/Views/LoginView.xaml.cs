using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;
using XCodeDemoWpfClient.Utils;

namespace XCodeDemoWpfClient.Views
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 界面加载时，赋予图片主题色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //获取主题深色
            SolidColorBrush PrimaryHueDarkBrush = FindResource("PrimaryHueDarkBrush") as SolidColorBrush;

            using (MagickImage image = new MagickImage(BitmapUtils.ConvertImageSourceToBitmap(image_Logo.Source)))
            {
                image.Evaluate(Channels.Red, EvaluateOperator.Set, PrimaryHueDarkBrush.Color.R);
                image.Evaluate(Channels.Green, EvaluateOperator.Set, PrimaryHueDarkBrush.Color.G);
                image.Evaluate(Channels.Blue, EvaluateOperator.Set, PrimaryHueDarkBrush.Color.B);

                // 重新给Image控件赋值新图像
                image_Logo.Source = image.ToBitmapSource();
            }
        }
    }
}
