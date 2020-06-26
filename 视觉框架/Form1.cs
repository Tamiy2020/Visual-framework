using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 视觉框架
{
    public partial class Form1 : Form
    {
        // public CameraManager cameraManager = new CameraManager();
        public HalconCameraManager cameraManager = new HalconCameraManager();

        public ExecutionManager executionManager;

        public Form cameraForm;

        public Form1()
        {
            InitializeComponent();
        }

       // 设置窗体样式
        public void SetCameraWindows(int count)
        {
            switch (count)
            {
                case 1:
                    cameraForm = new Frm_Cameras1(panel1);
                    break;
                case 2:
                    cameraForm = new Frm_Cameras2(panel1);
                    break;
                default:
                    return;
            }
            cameraForm.Show();

        }

        //窗体关闭时
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cameraManager.CloseAll();
            Environment.Exit(0);
        }

        //窗体加载时
        private void Form1_Load(object sender, EventArgs e)
        {
            executionManager = new ExecutionManager(cameraManager);
            executionManager.GradAll();
        }

        //实时标志
        bool live=true;
        //实时画面
        private void button2_Click(object sender, EventArgs e)
        {
            /*if (live)
            {
                executionManager.LiveAll(live);
                button1.Enabled = true;
            }
            else
            {
                executionManager.LiveAll(live);
                button1.Enabled = false;
            }
            live = !live;*/

        }

        //单张采集
        private void button1_Click(object sender, EventArgs e)
        {
            executionManager.GradAll();
           /* for (int i = 0; i < executionManager.vs.Count; i++)
            {
                textBox1.AppendText($"相机{i + 1}采图时间:" + executionManager.vs[i].ToString() + "ms" + "\r\n");


            }
            textBox1.AppendText("============\r\n");*/

        }
    }


}
