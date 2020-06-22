using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GxIAPINET;
using System.Windows.Forms;

namespace 视觉框架
{
    /// <summary>
    /// 相机类
    /// </summary>
    [Serializable]//序列化标志，表示当前类的实例可以被序列化储存

    public class Camera
    {
        [NonSerialized] //不序列化该字段
        /// <summary>
        /// 设备对象
        /// </summary>
        public IGXDevice objIGXDevice = null;

        [NonSerialized] //不序列化该字段
        /// <summary>
        /// 流对象
        /// </summary>
        public IGXStream objIGXStream = null;

        [NonSerialized] //不序列化该字段
        /// <summary>
        /// 远端设备属性控制器对象
        /// </summary>
        public IGXFeatureControl objIGXFeatureControl = null;

        [NonSerialized] //不序列化该字段
        /// <summary>
        /// 流层属性控制器对象
        /// </summary>
        public IGXFeatureControl objIGXStreamFeatureControl = null;

        /// <summary>
        /// 序列号
        /// </summary>
        public string strSN = "";

        /// <summary>
        /// 设备显示名称
        /// </summary>
        public string strDisplayName = "";

        /// <summary>
        /// 设备打开状态
        /// </summary>
        public bool bIsOpen = false;

        /// <summary>
        /// 发送开采命令标识
        /// </summary>
        public bool bIsSnap = false;

        [NonSerialized] //不序列化该字段
        GxBitmap objGxBitmap = null;

        [NonSerialized] //不序列化该字段
        CStopWatch objImageTime = new CStopWatch();

        /// <summary>
        /// 采集时间
        /// </summary>
        public double dElapsedtime;

        /// <summary>
        /// 打开设备打开流
        /// </summary>
        public void OpenDevice()
        {

            // 如果设备已经打开则关闭，保证相机在初始化出错情况下能再次打开
            if (null != objIGXDevice)
            {
                objIGXDevice.Close();
                objIGXDevice = null;
            }

            //打开设备
            objIGXDevice = IGXFactory.GetInstance().OpenDeviceBySN(strSN, GX_ACCESS_MODE.GX_ACCESS_EXCLUSIVE);
            objIGXFeatureControl = objIGXDevice.GetRemoteFeatureControl();

            //打开流
            objIGXStream = objIGXDevice.OpenStream(0);
            objIGXStreamFeatureControl = objIGXStream.GetFeatureControl();

            //初始化相机参数
            objIGXFeatureControl.GetEnumFeature("AcquisitionMode").SetValue("Continuous");//设置采集模式连续采集
            objIGXFeatureControl.GetEnumFeature("TriggerSource").SetValue("Software");//设置触发源软触发
            objIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("Off");//默认实时

            //更新设备打开标识
            bIsOpen = true;

        }

        /// <summary>
        /// 开始采集
        /// </summary>
        public void StartDevice()
        {

            //设置流层Buffer处理模式为OldestFirst
            objIGXStreamFeatureControl.GetEnumFeature("StreamBufferHandlingMode").SetValue("OldestFirst");


            //开启采集流通道
            if (null != objIGXStream)
            {
                //RegisterCaptureCallback第一个参数属于用户自定参数(类型必须为引用类型)，若用户想用这个参数可以在委托函数中进行使用
                objIGXStream.RegisterCaptureCallback(this, CaptureCallbackPro);
                objIGXStream.StartGrab();
            }

            //发送开采命令
            if (null != objIGXFeatureControl)
            {
                objIGXFeatureControl.GetCommandFeature("AcquisitionStart").Execute();
            }

            //开始采集的标识
            bIsSnap = true;
        }



        /// <summary>
        /// 回调函数,用于获取图像信息和显示图像
        /// </summary>
        /// <param name="obj">用户自定义传入参数</param>
        /// <param name="objIFrameData">图像信息对象</param>
        private void CaptureCallbackPro(object objUserParam, IFrameData objIFrameData)
        {
            try
            {
                Camera cam = objUserParam as Camera;
                cam.ImageShowAndSave(objIFrameData);
                dElapsedtime = objImageTime.Stop();
            }
            catch (Exception) { }

        }

        /// <summary>
        /// 设置窗体控件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pictureBox"></param>
        public void SetWindow(string name, PictureBox pictureBox)
        {
            if (name == strDisplayName)
            {
                objGxBitmap = new GxBitmap(objIGXDevice, pictureBox);
            }
        }

        /// <summary>
        ///  图像的显示
        /// </summary>
        /// <param name="objIFrameData"></param>
        void ImageShowAndSave(IFrameData objIFrameData)
        {
            objGxBitmap.Show(objIFrameData);
        }


        /// <summary>
        /// 切换触发模式
        /// </summary>
        /// <param name="isOn"></param>
        public void ChangeTriggerMode(bool isOn)
        {
            if (isOn)
            {
                objIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("On");//单张
            }
            else
            {
                objIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("Off");//实时
            }
        }


        /// <summary>
        /// 发送软触发命令
        /// </summary>
        public void SoftTrigger()
        {
            objImageTime.Start();
            objIGXFeatureControl.GetCommandFeature("TriggerSoftware").Execute();
            objIGXFeatureControl.GetCommandFeature("TriggerSoftware").Execute();

        }

        /// <summary>
        ///  关闭设备关闭流
        /// </summary>
        public void CloseDevice()
        {
            // 如果未停采则先停止采集
            if (bIsSnap)
            {
                objIGXFeatureControl.GetCommandFeature("AcquisitionStop").Execute();
                objIGXFeatureControl = null;
            }
            bIsSnap = false;

            //停止流通道、注销采集回调和关闭流
            try
            {
                if (null != objIGXStream)
                {
                    objIGXStream.StopGrab();
                    //注销采集回调函数
                    objIGXStream.UnregisterCaptureCallback();
                    objIGXStream.Close();
                    objIGXStream = null;
                    objIGXStreamFeatureControl = null;
                }
            }
            catch (Exception) { }

            //关闭设备
            if (null != objIGXDevice)
            {
                objIGXDevice.Close();
                objIGXDevice = null;
            }
            bIsOpen = false;

        }
    }
}
