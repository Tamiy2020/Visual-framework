using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 视觉框架
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("视觉框架");
            if (regkey != null)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Form1 form = new Form1();
                try
                {
                    form.cameraManager.EnumDevice();
                }
                catch (Exception)
                {
                    MessageBox.Show("未发现设备");
                    return;
                }
                form.cameraManager.OpenAll();
                form.SetCameraWindows(form.cameraManager.listCamera.Count);
                if (regkey.GetValue("相机数量").ToString() == "1")
                {
                    string str1 = regkey.GetValue("相机1").ToString();
                   
                    if (form.cameraManager.listCamera.Count == 2)
                    {
                        MessageBox.Show("只支持一台设备");
                        return;
                    }
                    if (form.cameraManager.listCamera[0].strDisplayName != str1)
                    {
                        MessageBox.Show("相机异常");
                        return;
                    }
                    ChangeList(form.cameraManager.listCamera, str1, 0);
                    form.cameraManager.listCamera[0].SetWindow(str1, (form.cameraForm as Frm_Cameras1).dpWins[0]);
                }
                if (regkey.GetValue("相机数量").ToString() == "2")
                {
                    if (form.cameraManager.listCamera.Count == 1)
                    {
                        foreach (var item in form.cameraManager.listCamera)
                        {
                            if (item.strDisplayName != regkey.GetValue("相机1").ToString() && item.strDisplayName == regkey.GetValue("相机2").ToString())
                            {
                                MessageBox.Show("相机1采集异常");
                                return;
                            }
                            else
                            {
                                MessageBox.Show("相机2采集异常");
                                return;
                            }
                        }
                    }
                    if (form.cameraManager.listCamera.Count == 2)
                    {
                        string str1 = regkey.GetValue("相机1").ToString();

                        string str2 = regkey.GetValue("相机2").ToString();
                        ChangeList(form.cameraManager.listCamera, str1, 0);
                        ChangeList(form.cameraManager.listCamera, str2, 1);
                        form.cameraManager.listCamera[0].SetWindow(str1, (form.cameraForm as Frm_Cameras2).dpWins[0]);
                        form.cameraManager.listCamera[1].SetWindow(str2, (form.cameraForm as Frm_Cameras2).dpWins[1]);
                    }

                }
                //前面可加启动中的窗体
                Application.Run(form);

            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Frm_FirstStart());
            }
        }

        public static void ChangeList(List<Camera> cameras, string str, int index)
        {
            for (int i = 0; i < cameras.Count; i++)
            {
                if (str == cameras[i].strDisplayName)
                {
                    var temp = cameras[i];
                    cameras[i] = cameras[index];
                    cameras[index] = temp;
                }
            }
        }
    }
}
