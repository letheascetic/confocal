#ifndef CORE_DEF_H
#define CORE_DEF_H

/* API Return Code Values */
typedef enum _API_RETURN_CODE
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
} API_RETURN_CODE;


#endif // CORE_DEF_H
