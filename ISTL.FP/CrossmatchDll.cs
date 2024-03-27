using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ISTL.FP
{
    class CrossmatchDll
    {
        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getDeviceCount(int hWnd);

        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int initializeDevice(int hWnd);

        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int communicationBreakCallback(int hwnd, IntPtr ptr);

        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int takingResultImage(int hwnd, IntPtr ptr);


        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setCaptureMode(int hWnd);

        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int startCapture(int hWnd, ref Object obj, ref Object obj1);

        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int setVisualization(int hWnd, IntPtr hwnd2);

        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int takeResultImage(int hWnd);

        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int registerCallbackComplete(int hWnd, IntPtr ptr);

        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int takingResultImage(int hWnd, ref Object obj);

        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int registerCallbackResultImage(int hWnd, IntPtr hwnd2);

        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int registerCallbackImageQuality(int hWnd, ref Object obj);

        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int registerCallBackClearObject(int hWnd, ref Object obj);

        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int releaseDevice(int hWnd);

        [System.Runtime.InteropServices.DllImport("FingerprintDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int getBeeper(int hWnd, int patt, int vol);
    }
}
