using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChoiceTech.Halcon.Control;
using HalconDotNet;

namespace 视觉框架
{
    public class HalconCamera
    {
        HTuple[] initpara;  //📕初始化参数

        HTuple hv_AcqHandle;//📕采集句柄HTuple 

        public HalconCamera(HTuple[] initpara)
        {
            this.initpara = initpara;
        }

        public void Open()
        {
            HOperatorSet.OpenFramegrabber(initpara[0], initpara[1], initpara[2], initpara[3], initpara[4],
                    initpara[5], initpara[6], initpara[7], initpara[8], initpara[9], initpara[10], initpara[11],
                    initpara[12], initpara[13], initpara[14], initpara[15], out hv_AcqHandle);
            filename = initpara[12];
        }

        public void Close()
        {
            HOperatorSet.CloseFramegrabber(hv_AcqHandle);
        }


        public void GrabImage()
        {
            HOperatorSet.GrabImage(out HObject ho_Image, hv_AcqHandle);
            windows.HobjectToHimage(ho_Image);
            ho_Image.Dispose();


        }
       public  string filename="";
        DpWin windows;
        public void SetWindow(string name, DpWin windows)
        {

          
            if (name == filename)
            {
                this.windows = windows;
            }
        }
    }
}
