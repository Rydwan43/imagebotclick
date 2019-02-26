using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace recognize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmpscreenshot = Screenshot();

            Point location;
            bool succes = FindBitmap(Properties.Resources.bmpLogin, bmpscreenshot, out location);

            if (succes == false)
            {
                return;
            }
            if (succes)
            {
                Cursor.Position = location;
            }
            //xd
        }

        private Bitmap Screenshot()
        {
            //zmienna która przechowa screenshota
            //Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Bitmap bmpScreenshot = new Bitmap(50, 50);

            // tworzy obiekt graficzny 
            Graphics g = Graphics.FromImage(bmpScreenshot);

            g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);

            return bmpScreenshot;
        }

        private bool FindBitmap(Bitmap bmpNeedle, Bitmap bmpHaystack, out Point location)
        {
            for (int outer_x = 0; outer_x < bmpHaystack.Width - bmpNeedle.Width; outer_x++)
            {
                for (int outer_y = 0; outer_y < bmpHaystack.Height - bmpNeedle.Height; outer_y++)
                {
                    for (int inner_x = 0; inner_x < bmpNeedle.Width; inner_x++)
                    {
                        for (int inner_y = 0; inner_y < bmpNeedle.Height; inner_y++)
                        {
                            Color cNeedle = bmpNeedle.GetPixel(inner_x, inner_y);
                            Color cHaystack = bmpHaystack.GetPixel(inner_x + outer_x, inner_y + outer_y);

                            if (cNeedle.R != cHaystack.R || cNeedle.G != cHaystack.G || cNeedle.B != cHaystack.B)
                            {
                                goto notFound;
                            }
                        }
                    }
                    location = new Point(outer_x, outer_y);
                    return true;
                notFound:
                    continue;

                }
            }
            location = Point.Empty;
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
