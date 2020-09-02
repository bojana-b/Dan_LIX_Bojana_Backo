using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        // Boolean called allowClick and set to false
        bool allowClick = false;

        // We are creating an instance of a picture box object in a variable and naming it first guess
        PictureBox firstGuess;

        // A new instance of the Random number generator class
        Random rnd = new Random();

        // An instances of the timer class
        Timer clickTimer = new Timer();

        // This will be used to calculate the countdown timer in the game
        int time = 60;

        // This is another timer we are adding to the game, this will have an Interval activated from the beginning which will run 1500 milliseconds or 1.5 second
        Timer timer = new Timer { Interval = 1500 };

        public Form1()
        {
            InitializeComponent();
        }

        //  Add all the pictureboxes to an Array
        private PictureBox[] pictureBoxes
        {
            get { return Controls.OfType<PictureBox>().ToArray(); }
        }

        //  Create an array of Image class 
        private static IEnumerable<Image> images
        {
            get
            {
                return new Image[]
                {
                    Properties.Resources.bart_image,
                    Properties.Resources.homer_image,
                    Properties.Resources.krasti_image,
                    Properties.Resources.Lisa_image,
                    Properties.Resources.Mardz_image,
                    Properties.Resources.Megi_image,
                    Properties.Resources.Mo_image,
                    Properties.Resources.Skiner_image
                };
            }
        }

        // This functions purpose is to start the timer and display the remaining time on the label 
        private void startGameTimer()
        {
            timer.Start();
            timer.Tick += delegate
            {
                time--;
                if (time < 0)
                {
                    timer.Stop();
                    MessageBox.Show("Out of time");
                    ResetImages();
                }
                var ssTime = TimeSpan.FromSeconds(time);
                lblTime.Text = "00: " + time.ToString();
            };
        }

        // This function will be resetting the picture boxes
        private void ResetImages()
        {
            foreach (var pic in pictureBoxes)
            {
                pic.Tag = null;
                pic.Visible = true;
            }
            HideImages();
            setRandomImages();
            time = 60;
            timer.Start();
        }

        // This function will run a foreach loop through the form looking for picture box components and it will mask them with the question mark image
        private void HideImages()
        {
            foreach (var pic in pictureBoxes)
            {
                pic.Image = Properties.Resources.question;
            }
        }

        // Loop through all of the picture boxes randomly
        private PictureBox getFreeSlot()
        {
            int num;
            do
            {
                num = rnd.Next(0, pictureBoxes.Count());
            }
            while (pictureBoxes[num].Tag != null);
            return pictureBoxes[num];
        }

        //  This loop will look for images and try to find a pair of slots where both can be tagged with the same name
        private void setRandomImages()
        {
            foreach (var image in images)
            {
                getFreeSlot().Tag = image;
                getFreeSlot().Tag = image;
            }
        }
        private void CLICKTIMER_TICK(object sender, EventArgs e)
        {
            HideImages();
            allowClick = true;
            clickTimer.Stop();
        }
        private void clickImage(object sender, EventArgs e)
        {
            if (!allowClick) return;
            var pic = (PictureBox)sender;
            if (firstGuess == null)
            {
                firstGuess = pic;
                pic.Image = (Image)pic.Tag;
                return;
            }
            pic.Image = (Image)pic.Tag;
            if (pic.Image == firstGuess.Image && pic != firstGuess)
            {
                pic.Visible = firstGuess.Visible = false;
                {
                    firstGuess = pic;
                }
                HideImages();
            }
            else
            {
                allowClick = false;
                clickTimer.Start();
            }
            firstGuess = null;
            if (pictureBoxes.Any(p => p.Visible)) return;
            MessageBox.Show("You Win Now Try Again");
            writeToFile();
            ResetImages();
        }
        private void startGame(object sender, EventArgs e)
        {
            allowClick = true;
            setRandomImages();
            HideImages();
            startGameTimer();
            clickTimer.Interval = 1000;
            clickTimer.Tick += CLICKTIMER_TICK;
            button1.Enabled = false;
        }
        private void writeToFile()
        {
            try
            {
                using (StreamWriter sw = File.AppendText(@"\..\IgraPamcenja.txt"))
                {
                    sw.WriteLine(DateTime.Now);
                }
            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLine($"The file was not found: '{e}'");
            }
            catch (IOException e)
            {
                Debug.WriteLine($"The file could not be opened: '{e}'");
            }
        }
    }
}

