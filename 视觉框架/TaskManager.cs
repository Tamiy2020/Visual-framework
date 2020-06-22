using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 视觉框架
{

    public class TaskManager
    {
        public Camera Camera;

        public TaskManager(Camera camera)
        {
            this.Camera = camera;

            camera.StartDevice();
        }

        /// <summary>
        /// 切换触发模式
        /// </summary>
        /// <param name="live"></param>
        public void Live(bool live)
        {
            Camera.ChangeTriggerMode(live);
        }

        /// <summary>
        /// 发送软触发命令
        /// </summary>
        public void Grad()
        {
            Camera.SoftTrigger();
        }


    }
}
