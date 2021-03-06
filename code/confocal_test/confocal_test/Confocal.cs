﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Confocal
{
    using HCONFIG = System.IntPtr;
    using HATS = System.IntPtr;

    public struct Board_Info
    {
        UInt32 BOARD_MODEL;            // board model
        Byte BOARD_MAJOR_NUMBER;       // board version
        Byte BOARD_MINOR_NUMBER;
        Byte SDK_MAJOR_NUMBER;         // sdk version
        Byte SDK_MINOR_NUMBER;
        Byte SDK_REVISION;
        Byte DRIVER_MAJOR_NUMBER;      // driver version
        Byte DRIVER_MINOR_NUMBER;
        Byte DRIVER_REVISION;
        Byte CPLD_MAJOR_NUMBER;        // CPLD version
        Byte CPLD_MINOR_NUMBER;
    };

    public class Config
    {
        [DllImport("core.dll", EntryPoint = "Test", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Test(int val);

        [DllImport("core.dll", EntryPoint = "CfgGetConfig", CallingConvention = CallingConvention.Cdecl)]
        public static extern HCONFIG GetConfig();

        [DllImport("core.dll", EntryPoint = "CfgGetChannelNum", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 GetChannelNum();

        [DllImport("core.dll", EntryPoint = "CfgSetLaserSwitch", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLaserSwitch(HCONFIG pConfig, UInt16 id, UInt16 status);

        [DllImport("core.dll", EntryPoint = "CfgGetLaserSwitch", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 GetLaserSwitch(HCONFIG pConfig, UInt16 id);

        [DllImport("core.dll", EntryPoint = "CfgSetLaserPower", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetLaserPower(HCONFIG pConfig, UInt16 id, float power);

        [DllImport("core.dll", EntryPoint = "CfgGetLaserPower", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetLaserPower(HCONFIG pConfig, UInt16 id);

        [DllImport("core.dll", EntryPoint = "CfgSetPmtGain", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetPmtGain(HCONFIG pConfig, UInt16 id, float gain);

        [DllImport("core.dll", EntryPoint = "CfgGetPmtGain", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetPmtGain(HCONFIG pConfig, UInt16 id);

        [DllImport("core.dll", EntryPoint = "CfgSetCrsAmplitude", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetCrsAmplitude(HCONFIG pConfig, float amplitude);

        [DllImport("core.dll", EntryPoint = "CfgGetCrsAmplitude", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetCrsAmplitude(HCONFIG pConfig);

        [DllImport("core.dll", EntryPoint = "CfgSetScanMode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetScanMode(HCONFIG pConfig, UInt16 mode);

        [DllImport("core.dll", EntryPoint = "CfgGetScanMode", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 GetScanMode(HCONFIG pConfig);

        [DllImport("core.dll", EntryPoint = "CfgSetScanStartegy", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetScanStartegy(HCONFIG pConfig, UInt16 strategy);

        [DllImport("core.dll", EntryPoint = "CfgGetScanStrategy", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 GetScanStrategy(HCONFIG pConfig);

        [DllImport("core.dll", EntryPoint = "CfgSetScanPointsX", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetScanPointsX(HCONFIG pConfig, UInt16 x);

        [DllImport("core.dll", EntryPoint = "CfgGetScanPointsX", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 GetScanPointsX(HCONFIG pConfig);

        [DllImport("core.dll", EntryPoint = "CfgSetScanPointsY", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetScanPointsY(HCONFIG pConfig, UInt16 y);

        [DllImport("core.dll", EntryPoint = "CfgGetScanPointsY", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 GetScanPointsY(HCONFIG pConfig);

        [DllImport("core.dll", EntryPoint = "CfgSetScanFlag", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetScanFlag(HCONFIG pConfig, UInt16 flag);

        [DllImport("core.dll", EntryPoint = "CfgGetScanFlag", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 GetScanFlag(HCONFIG pConfig);

        [DllImport("core.dll", EntryPoint = "CfgSetScanPixelTime", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetScanPixelTime(HCONFIG pConfig, float pixelTime);

        [DllImport("core.dll", EntryPoint = "CfgGetScanPixelTime", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetScanPixelTime(HCONFIG pConfig);

        [DllImport("core.dll", EntryPoint = "CfgSetHoleSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetHoleSize(HCONFIG pConfig, UInt16 size);

        [DllImport("core.dll", EntryPoint = "CfgGetHoleSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt16 GetHoleSize(HCONFIG pConfig);

    }

    public class Ats
    {
        [DllImport("core.dll", EntryPoint = "AtsTest", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Test(int val);

        [DllImport("core.dll", EntryPoint = "AtsFind", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Find();

        [DllImport("core.dll", EntryPoint = "AtsOpen", CallingConvention = CallingConvention.Cdecl)]
        public static extern HATS Open();

        [DllImport("core.dll", EntryPoint = "AtsClose", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Close();

        [DllImport("core.dll", EntryPoint = "AtsGetBoardInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AtsGetBoardInfo(HATS pAts, ref Board_Info pBoardInfo);
    };

    public enum RETURN_CODE
    {
        API_SUCCESS = 0x00000000,
        API_FAILED,
        API_FAILED_ATS_HANDLE_INVALID,
        API_FAILED_CONFIG_HANDLE_INVALID,
        API_FAILED_ATS_GET_BOARD_VERSION_FAILED,
        API_FAILED_ATS_GET_SDK_VERSION_FAILED,
        API_FIALED_ATS_GET_DRIVER_VERSION_FAILED,
        API_FIALED_ATS_GET_CPLD_VERSION_FAILED,
        API_FAILED_ATS_INFO_INSTANCE_INVALID,
        API_FAILED_ATS_SET_CLOCK_FAILED,
        API_FAILED_ATS_SET_INPUT_CHANNEL_FAILED,
        API_FAILED_ATS_SET_TRIGGER_OPERATION_FAILED,
        API_FAILED_ATS_SET_EXTERNAL_TRIGGER_FAILED,
        API_FAILED_ATS_SET_TRIGGER_TIMEOUT_FAILED,
        API_FAILED_ATS_SET_TRIGGER_DELAY_FAILED,
        API_FAILED_ATS_SET_AUX_IO_FAILED,
        API_FAILED_ATS_GET_CHANNEL_INFO_FAILED,
        API_FAILED_ATS_MALLOC_BUFFER_FAILED,
        API_FAILED_ATS_SET_RECORD_SIZE_FAILED,
        API_FAILED_ATS_CONFIG_ASYNC_READ_FAILED,
        API_FAILED_ATS_START_CAPTURE_FAILED,
        API_FAILED_ATS_ENABLE_CRS_FAILED,
        API_FAILED_ATS_ABORT_ASYNC_READ_FAILED,
        API_FAILED_ATS_FORCE_TRIGGER_ENABLE_FAILED,
        API_FAILED_LASER_LOAD_DLL_FAILED,
        API_FAILED_LASER_CONNECT_FAILED,
        API_FAILED_LASER_RELEASE_FAILED,
        API_FAILED_LASER_OPEN_CHANNEL_FAILED,
        API_FAILED_LASER_CLOSE_CHANNEL_FAILED,
        API_FAILED_LASER_SET_POWER_FAILED,
        API_FAILED_LASER_UP_POWER_FAILED,
        API_FAILED_LASER_DOWN_POWER_FAILED
    };

}
