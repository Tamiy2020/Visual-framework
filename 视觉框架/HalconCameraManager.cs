using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 视觉框架
{
    public class HalconCameraManager
    {
        public List<HalconCamera> halconCameras;
        public HalconCameraManager()
        {
            halconCameras = new List<HalconCamera>();
        }

        public void OpenAll()
        {
            foreach (var item in halconCameras)
            {
                item.Open();
            }
        }

        public void CloseAll()
        {
            foreach (var item in halconCameras)
            {
                item.Close();
            }
        }

        public void AddCamera(string path)
        {
            HalconCamera halconCamera = new HalconCamera(
                new HTuple[]
                {
                     new HTuple("File"),//0
                     new HTuple(0),//1
                     new HTuple(1),//2
                     new HTuple(0),//3
                     new HTuple(0),//4
                     new HTuple(0),//5
                     new HTuple(0),//6
                     new HTuple("default"),//7
                     new HTuple(-1),//8
                     new HTuple("default"),//9
                     new HTuple(1),//10
                     new HTuple("false"),//11
                     new HTuple(path),//12
                     new HTuple("default"),//13
                     new HTuple(-1),//14
                     new HTuple(-1)//15
                });
            halconCamera.filename = path;
            halconCameras.Add(halconCamera);
        }
    }
}
