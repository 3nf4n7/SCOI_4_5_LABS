using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba1scoi
{
    public partial class Form2 : Form
    {
        public Form2(Bitmap fura, Bitmap img)
        {
            InitializeComponent();

            pictureBox1.Image = fura;
            pictureBox2.Image = img;
        }
    }
}
