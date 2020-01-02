using ImageMagick;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XCodeDemoWpfClient.Utils;

namespace XCodeDemoWpfClient.CustomControl
{
    /// <summary>
    /// CusWindowHeader.xaml 的交互逻辑
    /// </summary>
    public partial class CusWindowHeader : UserControl
    {
        /// <summary>
        /// 此控件所在父级window
        /// </summary>
        private Window _Window;

        public CusWindowHeader()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //获取主题深色
            SolidColorBrush PrimaryHueDarkBrush = FindResource("PrimaryHueLightBrush") as SolidColorBrush;

            using (MagickImage image = new MagickImage(BitmapUtils.ConvertImageSourceToBitmap(Image_trackLogo.Source)))
            {
                image.Evaluate(Channels.Red, EvaluateOperator.Set, PrimaryHueDarkBrush.Color.R);
                image.Evaluate(Channels.Green, EvaluateOperator.Set, PrimaryHueDarkBrush.Color.G);
                image.Evaluate(Channels.Blue, EvaluateOperator.Set, PrimaryHueDarkBrush.Color.B);

                // 重新给Image控件赋值新图像
                Image_trackLogo.Source = image.ToBitmapSource();
            }

            //获取窗体句柄
            IntPtr hwnd = ((HwndSource)PresentationSource.FromVisual(this)).Handle;

            //通过视觉树，向上查找到Window
            var result = CusVisualTreeHelper.FindVisualParent<Window>(this);
            if (result.Count == 1)
                _Window = result[0];
        }

        /// <summary>
        /// 标题栏移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleBar_Move(object sender, MouseEventArgs e)
        {
            if (_Window != null && e.LeftButton == MouseButtonState.Pressed)
            {
                _Window.DragMove();
            }
        }

        /// <summary>
        /// 最小按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMinWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //左键单击
            if (_Window != null && e.ClickCount == 1 && e.ChangedButton == MouseButton.Left)
            {
                _Window.WindowState = WindowState.Minimized;
            }
        }

        /// <summary>
        /// 最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMaxWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //左键单击
            if (_Window != null && e.ClickCount == 1 && e.ChangedButton == MouseButton.Left)
            {
                if (_Window.WindowState == WindowState.Maximized)
                    _Window.WindowState = WindowState.Normal;
                else
                    _Window.WindowState = WindowState.Maximized;
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCloseWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //左键单击
            if (_Window != null && e.ClickCount == 1 && e.ChangedButton == MouseButton.Left)
            {
                _Window.Close();
            }
        }
    }
}
