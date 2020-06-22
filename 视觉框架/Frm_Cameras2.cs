using ChoiceTech.Halcon.Control;
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
    public partial class Frm_Cameras2 : Form
    {
        public List<DpWin> dpWins;
        public Frm_Cameras2(Control parent)
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCalls = false;

            dpWins = new List<DpWin>();
            dpWins.Add(dpWin1);
            dpWins.Add(dpWin2);

            TopLevel = false;
            Dock = DockStyle.Fill;
            Parent = parent;


        }
    }
}
