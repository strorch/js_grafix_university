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

namespace Graph_Lab_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var a = radio1.IsChecked;
            var b = radio2.IsChecked;
            var c = radio3.IsChecked;
            var context = (PointsView)this.DataContext;
            //context.RotateVector[1] = 0.1;
            if (a == true)
            {
                switch (e.Key)
                {
                    case Key.W:
                        context.ScalingVector = new double[] { 1, 1.1, 1 };
                        context.Scale();
                        break;
                    case Key.S:
                        context.ScalingVector = new double[] { 1, 0.9, 1 };
                        context.Scale();
                        break;
                    case Key.A:
                        context.ScalingVector = new double[] { 1.1, 1, 1 };
                        context.Scale();
                        break;
                    case Key.D:
                        context.ScalingVector = new double[] { 0.9, 1, 1 };
                        context.Scale();
                        break;
                    case Key.Q:
                        context.ScalingVector = new double[] { 1, 1, 1.1 };
                        context.Scale();
                        break;
                    case Key.E:
                        context.ScalingVector = new double[] { 1, 1, 0.9 };
                        context.Scale();
                        break;
                }
            }
            else if (b == true)
            {
                switch (e.Key)
                {
                    case Key.W:
                        context.TranslationVector = new double[] { 0, 2, 0 };
                        context.Translation();
                        break;
                    case Key.S:
                        context.TranslationVector = new double[] { 0, -2, 0 };
                        context.Translation();
                        break;
                    case Key.A:
                        context.TranslationVector = new double[] { 2, 0, 0 };
                        context.Translation();
                        break;
                    case Key.D:
                        context.TranslationVector = new double[] { -2, 0, 0 };
                        context.Translation();
                        break;
                    case Key.Q:
                        context.TranslationVector = new double[] { 0, 0, 2 };
                        context.Translation();
                        break;
                    case Key.E:
                        context.TranslationVector = new double[] { 0, 0, -2 };
                        context.Translation();
                        break;
                }
            }
            else if (c == true)
            {
                switch (e.Key)
                {
                    case Key.W:
                        context.RotateVector = new double[] { 0, 2, 0 };
                        context.Rotate();
                        break;
                    case Key.S:
                        context.RotateVector = new double[] { 0, -2, 0 };
                        context.Rotate();
                        break;
                    case Key.A:
                        context.RotateVector = new double[] { 2, 0, 0 };
                        context.Rotate();
                        break;
                    case Key.D:
                        context.RotateVector = new double[] { -2, 0, 0 };
                        context.Rotate();
                        break;
                    case Key.Q:
                        context.RotateVector = new double[] { 0, 0, 2 };
                        context.Rotate();
                        break;
                    case Key.E:
                        context.RotateVector = new double[] { 0, 0, -2 };
                        context.Rotate();
                        break;
                }
            }
        }
    }
}
