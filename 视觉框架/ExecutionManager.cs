using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 视觉框架
{
    public class ExecutionManager
    {
        /* public List<TaskManager> taskManagers = new List<TaskManager>();
         public List<double> vs = new List<double>();

         public ExecutionManager(CameraManager cameraManager)
         {
             foreach (var item in cameraManager.listCamera)
             {
                 TaskManager taskManager = new TaskManager(item);
                 taskManagers.Add(taskManager);
             }
         }

         /// <summary>
         /// 实时画面
         /// </summary>
         /// <param name="live"></param>
         public void LiveAll(bool live)
         {
             foreach (var item in taskManagers)
             {
                 item.Live(live);
             }
         }

         /// <summary>
         /// 单张采集所有
         /// </summary>
         public void GradAll()
         {
             vs.Clear();
             foreach (var item in taskManagers)
             {
                 item.Grad();

                 vs.Add(item.Camera.dElapsedtime);
             }
         }*/



        public List<TaskManager> taskManagers = new List<TaskManager>();
        public ExecutionManager(HalconCameraManager halconCameraManager)
        {
            foreach (var item in halconCameraManager.halconCameras)
            {
                TaskManager task = new TaskManager(item);
                taskManagers.Add(task);
            }
        }

        public void GradAll()
        {
            foreach (var item in taskManagers)
            {
                item.Grad();
            }
        }
    }
}
