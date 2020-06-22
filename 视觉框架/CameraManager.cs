using GxIAPINET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 视觉框架
{
    /// <summary>
    /// 相机管理器类
    /// </summary>
    [Serializable]//序列化标志，表示当前类的实例可以被序列化储存
    public class CameraManager
    {

        [NonSerialized] //不序列化该字段
        /// <summary>
        /// Factory对象
        /// </summary>
        IGXFactory objIGXFactory = null;

        [NonSerialized] //不序列化该字段
        /// <summary>
        /// 设备信息列表
        /// </summary>
        List<IGXDeviceInfo> listIGXDeviceInfo = new List<IGXDeviceInfo>();

        /// <summary>
        /// 相机列表
        /// </summary>
        public List<Camera> listCamera = new List<Camera>();

        //构造函数
        public CameraManager()
        {
            //初始化
            objIGXFactory = IGXFactory.GetInstance();
            objIGXFactory.Init();
        }

        /// <summary>
        /// 枚举设备
        /// </summary>
        public void EnumDevice()
        {
            //listIGXDeviceInfo.Clear();
            objIGXFactory.UpdateDeviceList(200, listIGXDeviceInfo);
            if (listIGXDeviceInfo.Count ==0)
            {
                throw new Exception();
            }
            for (int i = 0; i < listIGXDeviceInfo.Count; i++)
            {
                Camera objCamera = new Camera();
                objCamera.strDisplayName = listIGXDeviceInfo[i].GetDisplayName();
                objCamera.strSN = listIGXDeviceInfo[i].GetSN();

                listCamera.Add(objCamera);//添加相机列队
            }
        }

        /// <summary>
        /// 打开所有设备、流
        /// </summary>
        public void OpenAll()
        {
            foreach (var item in listCamera)
            {
                item.OpenDevice();
            }
        }


        /// <summary>
        /// 关闭所有设备、流
        /// </summary>
        public void CloseAll()
        {
            foreach (var item in listCamera)
            {
                item.CloseDevice();
            }
        }

    }
}
