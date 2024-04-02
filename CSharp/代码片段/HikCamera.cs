using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MvCamCtrl.NET;
using MvCamCtrl.NET.CameraParams;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

using System.Drawing.Imaging;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace BasicDemo
{
    public partial class Form1 : Form
    {
        private CCamera m_MyCamera = new CCamera();
        List<CCameraInfo> m_ltDeviceList = new List<CCameraInfo>();

        bool m_bGrabbing = false;
        bool m_bRecord = false;
        Thread m_hReceiveThread = null;

        // ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
        private static Object BufForDriverLock = new Object();
        CImage m_pcImgForDriver  = null;        // 图像信息
        CFrameSpecInfo m_pcImgSpecInfo = null; // 图像的水印信息
        CSaveImageParam m_pcSaveParam = new CSaveImageParam(); // 保存到缓存的参数信息

        // ch:Bitmap | en:Bitmap
        bool m_bRenderByBitmap = false;
        Graphics m_pcGraphic = null;

        UInt32 m_nCurWidth = 0;
        UInt32 m_nCurHeight = 0;
        MvGvspPixelType m_emPixelType = MvGvspPixelType.PixelType_Gvsp_Mono8;

        public Form1()
        {
            InitializeComponent();
            DeviceListAcq();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        // ch:显示错误信息 | en:Show error message
        private void ShowErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg;
            if (nErrorNum == 0)
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            switch (nErrorNum)
            {
                case CErrorDefine.MV_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                case CErrorDefine.MV_E_SUPPORT: errorMsg += " Not supported function "; break;
                case CErrorDefine.MV_E_BUFOVER: errorMsg += " Cache is full "; break;
                case CErrorDefine.MV_E_CALLORDER: errorMsg += " Function calling order error "; break;
                case CErrorDefine.MV_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                case CErrorDefine.MV_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                case CErrorDefine.MV_E_NODATA: errorMsg += " No data "; break;
                case CErrorDefine.MV_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                case CErrorDefine.MV_E_VERSION: errorMsg += " Version mismatches "; break;
                case CErrorDefine.MV_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                case CErrorDefine.MV_E_UNKNOW: errorMsg += " Unknown error "; break;
                case CErrorDefine.MV_E_GC_GENERIC: errorMsg += " General error "; break;
                case CErrorDefine.MV_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                case CErrorDefine.MV_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                case CErrorDefine.MV_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                case CErrorDefine.MV_E_NETER: errorMsg += " Network error "; break;
            }

            MessageBox.Show(errorMsg, "PROMPT");
        }

        private void DebugInfo(string strInfo)
        {
            string strRes = "BasicDemo";
            System.Diagnostics.Debug.WriteLine(strRes + strInfo);
        }

        private Boolean IsMonoData(MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;

                default:
                    return false;
            }
        }

        /************************************************************************
         *  @fn     IsColorData()
         *  @brief  判断是否是彩色数据
         *  @param  enGvspPixelType         [IN]           像素格式
         *  @return 成功，返回0；错误，返回-1 
         ************************************************************************/
        private Boolean IsColorData(MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                    return true;

                default:
                    return false;
            }
        }

        private void bnEnum_Click(object sender, EventArgs e)
        {
            DeviceListAcq();
        }

        private void DeviceListAcq()
        {
            // ch:创建设备列表 | en:Create Device List
            System.GC.Collect();
            cbDeviceList.Items.Clear();
            m_ltDeviceList.Clear();
            int nRet = CSystem.EnumDevices(CSystem.MV_GIGE_DEVICE | CSystem.MV_USB_DEVICE, ref m_ltDeviceList);
            if (0 != nRet)
            {
                ShowErrorMsg("Enumerate devices fail!", 0);
                return;
            }

            // ch:在窗体列表中显示设备名 | en:Display device name in the form list
            for (int i = 0; i < m_ltDeviceList.Count; i++)
            {
                if (m_ltDeviceList[i].nTLayerType == CSystem.MV_GIGE_DEVICE)
                {
                    CGigECameraInfo gigeInfo = (CGigECameraInfo)m_ltDeviceList[i];

                    if (gigeInfo.UserDefinedName != "")
                    {
                        cbDeviceList.Items.Add("GEV: " + gigeInfo.UserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                    else
                    {
                        cbDeviceList.Items.Add("GEV: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                }
                else if (m_ltDeviceList[i].nTLayerType == CSystem.MV_USB_DEVICE)
                {
                    CUSBCameraInfo usbInfo = (CUSBCameraInfo)m_ltDeviceList[i];
                    if (usbInfo.UserDefinedName != "")
                    {
                        cbDeviceList.Items.Add("U3V: " + usbInfo.UserDefinedName + " (" + usbInfo.chSerialNumber + ")");
                    }
                    else
                    {
                        cbDeviceList.Items.Add("U3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                    }
                }
            }

            // ch:选择第一项 | en:Select the first item
            if (m_ltDeviceList.Count != 0)
            {
                cbDeviceList.SelectedIndex = 0;
            }
        }

        private void SetCtrlWhenOpen()
        {
            bnOpen.Enabled = false;

            bnClose.Enabled = true;
            bnStartGrab.Enabled = true;
            bnStopGrab.Enabled = false;
            bnContinuesMode.Enabled = true;
            bnContinuesMode.Checked = true;
            bnTriggerMode.Enabled = true;
            cbSoftTrigger.Enabled = false;
            bnTriggerExec.Enabled = false;

            tbExposure.Enabled = true;
            tbGain.Enabled = true;
            tbFrameRate.Enabled = true;
            bnGetParam.Enabled = true;
            bnSetParam.Enabled = true;
            tbPixelFormat.Enabled = false;
        }

        private void bnOpen_Click(object sender, EventArgs e)
        {
            if (m_ltDeviceList.Count == 0 || cbDeviceList.SelectedIndex == -1)
            {
                ShowErrorMsg("No device, please select", 0);
                return;
            }

            // ch:获取选择的设备信息 | en:Get selected device information
            CCameraInfo device = m_ltDeviceList[cbDeviceList.SelectedIndex];

            // ch:打开设备 | en:Open device
            if (null == m_MyCamera)
            {
                m_MyCamera = new CCamera();
                if (null == m_MyCamera)
                {
                    return;
                }
            }

            int nRet = m_MyCamera.CreateHandle(ref device);
            if (CErrorDefine.MV_OK != nRet)
            {
                return;
            }

            nRet = m_MyCamera.OpenDevice();
            if (CErrorDefine.MV_OK != nRet)
            {
                m_MyCamera.DestroyHandle();
                ShowErrorMsg("Device open fail!", nRet);
                return;
            }

            // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
            if (device.nTLayerType == CSystem.MV_GIGE_DEVICE)
            {
                int nPacketSize = m_MyCamera.GIGE_GetOptimalPacketSize();
                if (nPacketSize > 0)
                {
                    nRet = m_MyCamera.SetIntValue("GevSCPSPacketSize", (uint)nPacketSize);
                    if (nRet != CErrorDefine.MV_OK)
                    {
                        ShowErrorMsg("Set Packet Size failed!", nRet);
                    }
                }
            }

            // ch:控件操作 | en:Control operation
            SetCtrlWhenOpen();

            // ch:获取参数 | en:Get parameters
            bnGetParam_Click(null, null);
        }

        private void SetCtrlWhenClose()
        {
            bnOpen.Enabled = true;

            bnClose.Enabled = false;
            bnStartGrab.Enabled = false;
            bnStopGrab.Enabled = false;
            bnContinuesMode.Enabled = false;
            bnTriggerMode.Enabled = false;
            cbSoftTrigger.Enabled = false;
            bnTriggerExec.Enabled = false;

            bnSaveBmp.Enabled = false;
            bnSaveJpg.Enabled = false;
            bnSaveTiff.Enabled = false;
            bnSavePng.Enabled = false;
            tbExposure.Enabled = false;
            tbGain.Enabled = false;
            tbFrameRate.Enabled = false;
            bnGetParam.Enabled = false;
            bnSetParam.Enabled = false;
            tbPixelFormat.Enabled = false;
            bnStartRecord.Enabled = false;
            bnStopRecord.Enabled = false;
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            // ch:取流标志位清零 | en:Reset flow flag bit
            if (m_bGrabbing == true)
            {
                bnStopGrab_Click(sender, e);
            }

            // ch:关闭设备 | en:Close Device
            m_MyCamera.CloseDevice();
            m_MyCamera.DestroyHandle();

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenClose();
        }

        private void bnContinuesMode_CheckedChanged(object sender, EventArgs e)
        {
            if (bnContinuesMode.Checked)
            {
                m_MyCamera.SetEnumValue("TriggerMode", (uint)MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
                cbSoftTrigger.Enabled = false;
                bnTriggerExec.Enabled = false;
            }
        }

        private void bnTriggerMode_CheckedChanged(object sender, EventArgs e)
        {
            // ch:打开触发模式 | en:Open Trigger Mode
            if (bnTriggerMode.Checked)
            {
                m_MyCamera.SetEnumValue("TriggerMode", (uint)MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);

                // ch:触发源选择:0 - Line0; | en:Trigger source select:0 - Line0;
                //           1 - Line1;
                //           2 - Line2;
                //           3 - Line3;
                //           4 - Counter;
                //           7 - Software;
                if (cbSoftTrigger.Checked)
                {
                    m_MyCamera.SetEnumValue("TriggerSource", (uint)MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
                    if (m_bGrabbing)
                    {
                        bnTriggerExec.Enabled = true;
                    }
                }
                else
                {
                    m_MyCamera.SetEnumValue("TriggerSource", (uint)MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
                }
                cbSoftTrigger.Enabled = true;
            }
        }

        private void SetCtrlWhenStartGrab()
        {
            bnStartGrab.Enabled = false;
            bnStopGrab.Enabled = true;

            if (bnTriggerMode.Checked && cbSoftTrigger.Checked)
            {
                bnTriggerExec.Enabled = true;
            }

            bnSaveBmp.Enabled = true;
            bnSaveJpg.Enabled = true;
            bnSaveTiff.Enabled = true;
            bnSavePng.Enabled = true;
            bnStartRecord.Enabled = true;
            bnStopRecord.Enabled = false;
        }

        public void ReceiveThreadProcess()
        {
            int nRet = CErrorDefine.MV_OK;

            while (m_bGrabbing)
            {
                CFrameout pcFrameInfo = new CFrameout();
                CDisplayFrameInfo pcDisplayInfo = new CDisplayFrameInfo();
                CPixelConvertParam pcConvertParam = new CPixelConvertParam();

                nRet = m_MyCamera.GetImageBuffer(ref pcFrameInfo, 1000);
                if (CErrorDefine.MV_OK == nRet)
                {
                    if (m_bRecord)
                    {
                        CInputFrameInfo pcInputFrameInfo = new CInputFrameInfo();
                        pcInputFrameInfo.InImage = pcFrameInfo.Image;
                        m_MyCamera.InputOneFrame(ref pcInputFrameInfo);
                    }

                    lock (BufForDriverLock)
                    {
                        if (null != m_pcImgForDriver)
                        {
                            m_pcImgForDriver.Destory();
                            m_pcImgForDriver = null;
                        }

                        m_pcImgForDriver = pcFrameInfo.Image.Clone() as CImage;
                        m_pcImgSpecInfo = pcFrameInfo.FrameSpec;
                    }

                    if (m_bRenderByBitmap)
                    {
                        Bitmap pcBitmap = m_MyCamera.ImageToBitmap(ref pcFrameInfo);
                        if (null != pcBitmap)
                        {
                            if (null == m_pcGraphic)
                            {
                                m_pcGraphic = pictureBox1.CreateGraphics();
                            }

                            Rectangle pcDstRect = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
                            Rectangle pcSrcRect = new Rectangle(0, 0, pcBitmap.Width, pcBitmap.Height);
                            m_pcGraphic.DrawImage(pcBitmap, pcDstRect, pcSrcRect, GraphicsUnit.Pixel);
                            pcBitmap.Dispose();
                        }
                    }
                    else
                    {
                        pcDisplayInfo.WindowHandle = pictureBox1.Handle;
                        pcDisplayInfo.Image = pcFrameInfo.Image;
                        m_MyCamera.DisplayOneFrame(ref pcDisplayInfo);
                    }
                    m_MyCamera.FreeImageBuffer(ref pcFrameInfo);
                }
                else
                {
                    if (bnTriggerMode.Checked)
                    {
                        Thread.Sleep(5);
                    }
                }
            }
        }

        // ch:像素类型是否为Mono格式 | en:If Pixel Type is Mono 
        private Boolean IsMono(MvGvspPixelType enPixelType)
        {
            switch (enPixelType)
            {
                case MvGvspPixelType.PixelType_Gvsp_Mono1p:
                case MvGvspPixelType.PixelType_Gvsp_Mono2p:
                case MvGvspPixelType.PixelType_Gvsp_Mono4p:
                case MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MvGvspPixelType.PixelType_Gvsp_Mono8_Signed:
                case MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono14:
                case MvGvspPixelType.PixelType_Gvsp_Mono16:
                    return true;
                default:
                    return false;
            }
        }

        // ch:取图前的必要操作步骤 | en:Necessary operation before grab
        private Int32 NecessaryOperBeforeGrab()
        {
            // ch:取图像宽 | en:Get Iamge Width
            CIntValue pcWidth = new CIntValue();
            int nRet = m_MyCamera.GetIntValue("Width", ref pcWidth);
            if (CErrorDefine.MV_OK != nRet)
            {
                ShowErrorMsg("Get Width Info Fail!", nRet);
                return nRet;
            }
            m_nCurWidth = (UInt32)pcWidth.CurValue;


            // ch:取图像高 | en:Get Iamge Height
            CIntValue pcHeight = new CIntValue();
            nRet = m_MyCamera.GetIntValue("Height", ref pcHeight);
            if (CErrorDefine.MV_OK != nRet)
            {
                ShowErrorMsg("Get Height Info Fail!", nRet);
                return nRet;
            }
            m_nCurHeight = (UInt32)pcHeight.CurValue;

            // ch:取像素格式 | en:Get Pixel Format
            CEnumValue pcPixelFormat = new CEnumValue();
            nRet = m_MyCamera.GetEnumValue("PixelFormat", ref pcPixelFormat);
            if (CErrorDefine.MV_OK != nRet)
            {
                ShowErrorMsg("Get Pixel Format Fail!", nRet);
                return nRet;
            }
            m_emPixelType = (MvGvspPixelType)pcPixelFormat.CurValue;

            return CErrorDefine.MV_OK;
        }

        private void bnStartGrab_Click(object sender, EventArgs e)
        {
            // ch:前置配置 | en:pre-operation
            int nRet = NecessaryOperBeforeGrab();
            if (CErrorDefine.MV_OK != nRet)
            {
                return;
            }

            // ch:标志位置位true | en:Set position bit true
            m_bGrabbing = true;
            //m_bRenderByBitmap = true;
            m_hReceiveThread = new Thread(ReceiveThreadProcess);
            m_hReceiveThread.Start();

            // ch:开始采集 | en:Start Grabbing
            nRet = m_MyCamera.StartGrabbing();
            if (CErrorDefine.MV_OK != nRet)
            {
                m_bGrabbing = false;
                m_hReceiveThread.Join();
                ShowErrorMsg("Start Grabbing Fail!", nRet);
                return;
            }

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenStartGrab();
        }

        private void cbSoftTrigger_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSoftTrigger.Checked)
            {
                // ch:触发源设为软触发 | en:Set trigger source as Software
                m_MyCamera.SetEnumValue("TriggerSource", (uint)MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
                if (m_bGrabbing)
                {
                    bnTriggerExec.Enabled = true;
                }
            }
            else
            {
                m_MyCamera.SetEnumValue("TriggerSource", (uint)MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
                bnTriggerExec.Enabled = false;
            }
        }

        private void bnTriggerExec_Click(object sender, EventArgs e)
        {
            // ch:触发命令 | en:Trigger command
            int nRet = m_MyCamera.SetCommandValue("TriggerSoftware");
            if (CErrorDefine.MV_OK != nRet)
            {
                ShowErrorMsg("Trigger Software Fail!", nRet);
            }
        }

        private void SetCtrlWhenStopGrab()
        {
            bnStartGrab.Enabled = true;
            bnStopGrab.Enabled = false;
            bnTriggerExec.Enabled = false;

            bnSaveBmp.Enabled = false;
            bnSaveJpg.Enabled = false;
            bnSaveTiff.Enabled = false;
            bnSavePng.Enabled = false;
            bnStartRecord.Enabled = false;
            bnStopRecord.Enabled = false;
        }

        private void bnStopGrab_Click(object sender, EventArgs e)
        {
            // ch:标志位设为false | en:Set flag bit false
            m_bGrabbing = false;
            m_hReceiveThread.Join();

            // ch:停止采集 | en:Stop Grabbing
            int nRet = m_MyCamera.StopGrabbing();
            if (nRet != CErrorDefine.MV_OK)
            {
                ShowErrorMsg("Stop Grabbing Fail!", nRet);
            }

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenStopGrab();
        }

        private void bnSaveBmp_Click(object sender, EventArgs e)
        {
            if (false == m_bGrabbing)
            {
                ShowErrorMsg("Not Start Grabbing", 0);
                return;
            }

            CSaveImgToFileParam stSaveFileParam = new CSaveImgToFileParam();

            lock (BufForDriverLock)
            {
                if (null == m_pcImgForDriver || 0 == m_pcImgForDriver.FrameLen)
                {
                    ShowErrorMsg("Save Bmp Fail!", 0);
                    return;
                }

                // 保存到文件
                stSaveFileParam.ImageType = MV_SAVE_IAMGE_TYPE.MV_IMAGE_BMP;
                stSaveFileParam.Image = m_pcImgForDriver;
                stSaveFileParam.MethodValue = 2;
                stSaveFileParam.ImagePath = "Image_w" + stSaveFileParam.Image.Width.ToString() + "_h" + stSaveFileParam.Image.Height.ToString() + "_fn" + m_pcImgSpecInfo.FrameNum.ToString() + ".bmp";
                int nRet = m_MyCamera.SaveImageToFile(ref stSaveFileParam);
                if (CErrorDefine.MV_OK != nRet)
                {
                    ShowErrorMsg("Save Bmp Fail!", nRet);
                    return;
                }

                // 保存图像到缓存
                m_pcSaveParam.InImage = m_pcImgForDriver;
                m_pcSaveParam.ImageType = MV_SAVE_IAMGE_TYPE.MV_IMAGE_BMP;
                m_pcSaveParam.MethodValue = 2;

                nRet = m_MyCamera.SaveImageEx(ref m_pcSaveParam);
                if (nRet != CErrorDefine.MV_OK)
                {
                    DebugInfo("SaveImageEx Failed" + nRet.ToString());
                }
                else
                {
                    String strSaveInfo = String.Format("SavImageEx Success: Height[{0}], Width[{1}], FrameLen[{2}], ImageType[{3}]",
                    m_pcSaveParam.OutImage.Height, m_pcSaveParam.OutImage.Width, m_pcSaveParam.OutImage.FrameLen, m_pcSaveParam.ImageType);
                    DebugInfo(strSaveInfo);
                }
            }

            ShowErrorMsg("Save Succeed!", 0);
        }

        private void bnSaveJpg_Click(object sender, EventArgs e)
        {
            if (false == m_bGrabbing)
            {
                ShowErrorMsg("Not Start Grabbing", 0);
                return;
            }

            CSaveImgToFileParam stSaveFileParam = new CSaveImgToFileParam();

            lock (BufForDriverLock)
            {
                if (null == m_pcImgForDriver || 0 == m_pcImgForDriver.FrameLen)
                {
                    ShowErrorMsg("Save Jpeg Fail!", 0);
                    return;
                }
                stSaveFileParam.ImageType = MV_SAVE_IAMGE_TYPE.MV_IMAGE_JPEG;
                stSaveFileParam.Image = m_pcImgForDriver;
                stSaveFileParam.Quality = 80;
                stSaveFileParam.MethodValue = 2;
                stSaveFileParam.ImagePath = "Image_w" + stSaveFileParam.Image.Width.ToString() + "_h" + stSaveFileParam.Image.Height.ToString() + "_fn" + m_pcImgSpecInfo.FrameNum.ToString() + ".jpg";
                int nRet = m_MyCamera.SaveImageToFile(ref stSaveFileParam);
                if (CErrorDefine.MV_OK != nRet)
                {
                    ShowErrorMsg("Save Jpeg Fail!", nRet);
                    return;
                }
            }

            ShowErrorMsg("Save Succeed!", 0);
        }

        private void bnSavePng_Click(object sender, EventArgs e)
        {
            if (false == m_bGrabbing)
            {
                ShowErrorMsg("Not Start Grabbing", 0);
                return;
            }

            CSaveImgToFileParam stSaveFileParam = new CSaveImgToFileParam();

            lock (BufForDriverLock)
            {
                if (null == m_pcImgForDriver || 0 == m_pcImgForDriver.FrameLen)
                {
                    ShowErrorMsg("Save Png Fail!", 0);
                    return;
                }
                stSaveFileParam.ImageType = MV_SAVE_IAMGE_TYPE.MV_IMAGE_PNG;
                stSaveFileParam.Image = m_pcImgForDriver;
                stSaveFileParam.Quality = 9;
                stSaveFileParam.MethodValue = 2;
                stSaveFileParam.ImagePath = "Image_w" + stSaveFileParam.Image.Width.ToString() + "_h" + stSaveFileParam.Image.Height.ToString() + "_fn" + m_pcImgSpecInfo.FrameNum.ToString() + ".png";
                int nRet = m_MyCamera.SaveImageToFile(ref stSaveFileParam);
                if (CErrorDefine.MV_OK != nRet)
                {
                    ShowErrorMsg("Save Png Fail!", nRet);
                    return;
                }
            }

            ShowErrorMsg("Save Succeed!", 0);
        }

        private void bnSaveTiff_Click(object sender, EventArgs e)
        {
            if (false == m_bGrabbing)
            {
                ShowErrorMsg("Not Start Grabbing", 0);
                return;
            }

            CSaveImgToFileParam stSaveFileParam = new CSaveImgToFileParam();

            lock (BufForDriverLock)
            {
                if (null == m_pcImgForDriver || 0 == m_pcImgForDriver.FrameLen)
                {
                    ShowErrorMsg("Save Tiff Fail!", 0);
                    return;
                }
                stSaveFileParam.ImageType = MV_SAVE_IAMGE_TYPE.MV_IMAGE_TIF;
                stSaveFileParam.Image = m_pcImgForDriver;
                stSaveFileParam.MethodValue = 2;
                stSaveFileParam.ImagePath = "Image_w" + stSaveFileParam.Image.Width.ToString() + "_h" + stSaveFileParam.Image.Height.ToString() + "_fn" + m_pcImgSpecInfo.FrameNum.ToString() + ".tif";
                int nRet = m_MyCamera.SaveImageToFile(ref stSaveFileParam);
                if (CErrorDefine.MV_OK != nRet)
                {
                    ShowErrorMsg("Save Tiff Fail!", nRet);
                    return;
                }
            }

            ShowErrorMsg("Save Succeed!", 0);
        }

        /// <summary>
        /// ch:获取触发模式 | en:Get Trigger Mode
        /// </summary>
        private void GetTriggerMode()
        {
            CEnumValue pcEnumValue = new CEnumValue();
            int nRet = m_MyCamera.GetEnumValue("TriggerMode", ref pcEnumValue);
            if (CErrorDefine.MV_OK == nRet)
            {
                // 触发模式下
                if ((uint)MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON == pcEnumValue.CurValue)
                {
                    bnTriggerMode.Checked = true;
                    bnContinuesMode.Checked = false;

                    // 获取触发源
                    nRet = m_MyCamera.GetEnumValue("TriggerSource", ref pcEnumValue);
                    if (CErrorDefine.MV_OK == nRet)
                    {
                        if ((uint)MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE == pcEnumValue.CurValue)
                        {
                            cbSoftTrigger.Checked = true;
                            if (m_bGrabbing)
                            {
                                bnTriggerExec.Enabled = true;
                            }
                            cbSoftTrigger.Enabled = true;
                        }
                    }
                }
                else
                {
                    // 连续模式下
                    bnContinuesMode.Checked = true;
                    bnTriggerMode.Checked = false;
                }
            }
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnGetParam_Click(object sender, EventArgs e)
        {
            CFloatValue pcFloatValue = new CFloatValue();
            CEnumValue pcEnumValue = new CEnumValue();
            CEnumEntry pcEntryValue = new CEnumEntry();
            int nRet = CErrorDefine.MV_OK;

            // 获取触发模式
            GetTriggerMode();

            // 获取曝光时间
            nRet = m_MyCamera.GetFloatValue("ExposureTime", ref pcFloatValue);
            if (CErrorDefine.MV_OK == nRet)
            {
                tbExposure.Text = pcFloatValue.CurValue.ToString("F1");
            }

            // 获取增益
            nRet = m_MyCamera.GetFloatValue("Gain", ref pcFloatValue);
            if (CErrorDefine.MV_OK == nRet)
            {
                tbGain.Text = pcFloatValue.CurValue.ToString("F1");
            }

            // 获取帧率
            nRet = m_MyCamera.GetFloatValue("ResultingFrameRate", ref pcFloatValue);
            if (CErrorDefine.MV_OK == nRet)
            {
                tbFrameRate.Text = pcFloatValue.CurValue.ToString("F1");
            }

            // 获取像素格式
            nRet = m_MyCamera.GetEnumValue("PixelFormat", ref pcEnumValue);
            if (CErrorDefine.MV_OK == nRet)
            {
                pcEntryValue.Value = pcEnumValue.CurValue;
                nRet = m_MyCamera.GetEnumEntrySymbolic("PixelFormat", ref pcEntryValue);
                if (CErrorDefine.MV_OK == nRet)
                {
                    tbPixelFormat.Text = pcEntryValue.Symbolic;
                }
            }
        }

        private void bnSetParam_Click(object sender, EventArgs e)
        {
            try
            {
                float.Parse(tbExposure.Text);
                float.Parse(tbGain.Text);
                float.Parse(tbFrameRate.Text);
            }
            catch
            {
                ShowErrorMsg("Please enter correct type!", 0);
                return;
            }

            m_MyCamera.SetEnumValue("ExposureAuto", 0);
            int nRet = m_MyCamera.SetFloatValue("ExposureTime", float.Parse(tbExposure.Text));
            if (nRet != CErrorDefine.MV_OK)
            {
                ShowErrorMsg("Set Exposure Time Fail!", nRet);
            }

            m_MyCamera.SetEnumValue("GainAuto", 0);
            nRet = m_MyCamera.SetFloatValue("Gain", float.Parse(tbGain.Text));
            if (nRet != CErrorDefine.MV_OK)
            {
                ShowErrorMsg("Set Gain Fail!", nRet);
            }

            nRet = m_MyCamera.SetFloatValue("AcquisitionFrameRate", float.Parse(tbFrameRate.Text));
            if (nRet != CErrorDefine.MV_OK)
            {
                ShowErrorMsg("Set Frame Rate Fail!", nRet);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bnClose_Click(sender, e);
        }

        private void SetCtrlWhenStartRecord()
        {
            bnStartRecord.Enabled = false;
            bnStopRecord.Enabled = true;
        }

        private void bnStartRecord_Click(object sender, EventArgs e)
        {
            if (false == m_bGrabbing)
            {
                ShowErrorMsg("Not Start Grabbing", 0);
                return;
            }

            CRecordParam pcRecordParam = new CRecordParam();

            pcRecordParam.enPixelType = m_emPixelType;
            pcRecordParam.Height = m_nCurHeight;
            pcRecordParam.Width = m_nCurWidth;
            pcRecordParam.FrameRate = float.Parse(tbFrameRate.Text);
            pcRecordParam.BitRate = 1000;
            pcRecordParam.FilePath = "./Record.avi";
            pcRecordParam.RecordFmtType = MV_RECORD_FORMAT_TYPE.MV_FORMAT_TYPE_AVI;

            Int32 nRet = m_MyCamera.StartRecord(ref pcRecordParam);
            if (nRet != CErrorDefine.MV_OK)
            {
                ShowErrorMsg("Start Record Fail!", nRet);
            }

            m_bRecord = true;

            SetCtrlWhenStartRecord();
        }

        private void SetCtrlWhenStopRecord()
        {
            bnStartRecord.Enabled = true;
            bnStopRecord.Enabled = false;
        }

        private void bnStopRecord_Click(object sender, EventArgs e)
        {
            if (false == m_bGrabbing)
            {
                ShowErrorMsg("Not Start Grabbing", 0);
                return;
            }

            Int32 nRet = m_MyCamera.StopRecord();
            if (nRet != CErrorDefine.MV_OK)
            {
                ShowErrorMsg("Stop Record Fail!", nRet);
            }

            m_bRecord = false;
            SetCtrlWhenStopRecord();
        }
    }
}
