using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 视觉框架
{
    public partial class Frm_FirstStart : Form
    {
        Form1 form;
        RegistryKey regkey;
        string str1 = "";
        string str2 = "";
        public Frm_FirstStart()
        {
            InitializeComponent();
        }

        //窗体加载时
        private void Frm_FirstStart_Load(object sender, EventArgs e)
        {
            form = new Form1();

            try
            {
                form.cameraManager.EnumDevice();
            }
            catch (Exception)
            {
                MessageBox.Show("未发现设备");
                Close();
            }
            foreach (var item in form.cameraManager.listCamera)
            {
                comboBox1.Items.Add(item.strDisplayName);
            }
            form.cameraManager.OpenAll();

        }

        //选择相机
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("视觉框架") == null)
            {
                //创建注册表
                regkey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("视觉框架");
            }
            if (lbl_c1.Text == string.Empty)
            {
                str1 = comboBox1.SelectedItem.ToString();
                lbl_c1.Text = "相机1：" + str1;

                comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
                Program.ChangeList(form.cameraManager.listCamera, str1, 0);
                regkey.SetValue("相机1", str1);
            }
            else if (lbl_c1.Text != string.Empty && lbl_c2.Text == string.Empty)
            {
                str2 = comboBox1.SelectedItem.ToString();
                lbl_c2.Text = "相机2：" + str2;
                comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
                Program.ChangeList(form.cameraManager.listCamera, str2, 1);
                regkey.SetValue("相机2", str2);
            }
        }

        //设置窗体样式
        private void SetWindows()
        {
            if (regkey.GetValue("相机数量").ToString() == "1")
            {
                form.cameraManager.listCamera[0].SetWindow(str1, (form.cameraForm as Frm_Cameras1).dpWins[0]);

            }
            if (regkey.GetValue("相机数量").ToString() == "2")
            {
                form.cameraManager.listCamera[0].SetWindow(str1, (form.cameraForm as Frm_Cameras2).dpWins[0]);
                form.cameraManager.listCamera[1].SetWindow(str2, (form.cameraForm as Frm_Cameras2).dpWins[1]);

            }
        }

        //配置完成
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                regkey.SetValue("相机数量", form.cameraManager.listCamera.Count, RegistryValueKind.DWord);
                form.SetCameraWindows(form.cameraManager.listCamera.Count);
                SetWindows();
            }
            catch (Exception)
            {
                MessageBox.Show("请选择相机");
                return;
            }
            Thread.Sleep(500);
            this.Hide();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
