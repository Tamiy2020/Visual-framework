using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 视觉框架
{
    public partial class Frm_Cameras1 : Form
    {
        public List<PictureBox> pictureBoxes;
        public Frm_Cameras1(Control parent)
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;

            pictureBoxes = new List<PictureBox>();
            pictureBoxes.Add(pictureBox1);

            TopLevel = false;
            Dock = DockStyle.Fill;
            Parent = parent;


        }
    }
}
