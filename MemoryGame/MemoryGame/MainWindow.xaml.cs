using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Image = System.Windows.Controls.Image;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool allowClick = false;
        Image firstGuess;
        Random rnd = new Random();
        

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        //private Image[] images
        //{
        //    get { return new Image[] { bart_image, }}
        //}

        private static IEnumerable<Bitmap> images
        {
            get
            {
                return new Bitmap[]
                {
                    Properties.Resources.bart_image,
                    Properties.Resources.homer_image,
                    Properties.Resources.krasti_image,
                    Properties.Resources.Lisa_image,
                    Properties.Resources.Mardz_image,
                    Properties.Resources.Megi_image,
                    Properties.Resources.Mo_image,
                    Properties.Resources.Skiner_image,
                    Properties.Resources.question
                };
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }
    }
}
