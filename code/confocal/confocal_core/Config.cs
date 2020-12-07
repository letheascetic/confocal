using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace confocal_core
{
    /// <summary>
    /// 代码执行结果
    /// </summary>
    public enum API_RETURN_CODE : int
    {
        API_SUCCESS = 0x00000000,
        API_FAILED = 0x00000001,
        /* error for config */
        API_FAILED_CONFIG_HANDLE_INVALID = 0x00000100,
        API_FAILED_CONFIG_SET_LASER_SWITCH_FAILED,
        API_FAILED_CONFIG_SET_LASER_POWER_FAILED,
        /* error for ats9440 */
        API_FAILED_ATS_HANDLE_INVALID = 0x00000200,
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
        /* error for laser */
        API_FAILED_LASER_LOAD_DLL_FAILED = 0x00000400,
        API_FAILED_LASER_CONNECT_FAILED,
        API_FAILED_LASER_RELEASE_FAILED,
        API_FAILED_LASER_OPEN_CHANNEL_FAILED,
        API_FAILED_LASER_CLOSE_CHANNEL_FAILED,
        API_FAILED_LASER_SET_POWER_FAILED,
        API_FAILED_LASER_UP_POWER_FAILED,
        API_FAILED_LASER_DOWN_POWER_FAILED,
        /* error for ni card */
        API_FAILED_NI_CONFIG_AO_TASK_EXCEPTION = 0x00000800,
        API_FAILED_NI_CONFIG_DO_TASK_EXCEPTION,
        API_FAILED_NI_CONFIG_AI_TASK_EXCEPTION,
        API_FAILED_NI_CONFIG_CI_TASK_EXCEPTION,
        API_FAILED_NI_NO_AI_CHANNEL_ACTIVATED,
        API_FAILED_NI_START_TASK_EXCEPTION,
        /* error for scan task */
        API_FAILED_SCAN_TASK_INVALID = 0x00001000,
        API_FAILED_SCAN_TASK_NOT_FOUND,
        API_FAILED_SCAN_TASK_START_FAILED,
        API_FAILED_SCAN_TASK_STOP_FAILED,
        /* error for usb dac */
        API_FAILED_USB_DAC_OPEN_FAILED = 0x00002000,
        API_FAILED_USB_DAC_RELEASE_FAILED,
        API_FAILED_USB_DAC_SET_ALL_OUT_FAILED,
        API_FAILED_USB_DAC_SET_CHANNEL_OUT_FAILED,
        API_FAILED_USB_DAC_SET_ZERO_CALIBRATION_FAILED,
        API_FAILED_USB_DAC_SET_GAIN_CALIBRATION_FAILED,
        API_FAILED_USB_DAC_WRITE_CONFIG_FAILED,
        API_FAILED_USB_DAC_READ_CONFIG_FAILED,
        API_FAILED_USB_DAC_SET_REGISTER_FAILED
    }

    /// <summary>
    /// 通道ID
    /// </summary>
    public enum CHAN_ID : int
    {
        WAVELENGTH_405_NM = 0,
        WAVELENGTH_488_NM = 1,
        WAVELENGTH_561_NM = 2,
        WAVELENGTH_640_NM = 3
    }

    /// <summary>
    /// 激光开关
    /// </summary>
    public enum LASER_SWITCH : int
    {
        OFF = 0,
        ON = 1
    }

    /// <summary>
    /// 扫描模式：Galvano 或 Resonant
    /// </summary>
    public enum SCAN_MODE : int
    {
        RESONANT = 0,
        GALVANO = 1
    }

    /// <summary>
    /// 扫描方向：单向 or 双向
    /// </summary>
    public enum SCAN_DIRECTION : int
    {
        BIDIRECTION = 0,          // 双向
        UNIDIRECTION = 1          // 单向
    }

    /// <summary>
    /// 扫描振镜：双镜 or 三镜
    /// </summary>
    public enum SCANNER_SYSTEM
    {
        TWO_SCANNERS = 0,
        THREE_SCANNERS = 1
    };

    /// <summary>
    /// 扫描采集模式：实时 捕获 发现
    /// </summary>
    public enum SCAN_ACQUISITION_MODE
    {
        LIVE = 0,
        CAPTURE = 1,
        FIND = 2
    };

    /// <summary>
    /// 扫描线跳过
    /// </summary>
    public enum SCAN_LINE_SKIPPING
    {
        NONE = 0,
        EVERY2 = 2,
        EVERY4 = 4,
        EVERY8 = 8,
        EVERY16 = 16,
    };

    /// <summary>
    /// 扫描线操作模式
    /// </summary>
    public enum SCAN_LINE_OPTION_MODE
    {
        NONE = 0,
        AVERAGE = 1,
        INTEGRATE = 2
    };

    /// <summary>
    /// 扫描线选项参数
    /// </summary>
    public enum SCAN_LINE_OPTION_PARA
    {
        REPEAT2 = 2,
        REPEAT4 = 4,
        REPEAT8 = 8,
        REPEAT16 = 16
    };


    public enum SCAN_PIXELS
    { 
        X64 = 0,
        X128 = 1
    };


    public class Config
    {

    }
}
