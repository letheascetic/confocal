#include "core_def.h"
#include "core_cfg.h"

// channel num (laser & pmt)
#define CHAN_NUM (4)
#define LASER_CHAN_NUM CHAN_NUM
#define PMT_CHAN_NUM CHAN_NUM

// default
#define LASER_POWER_DEFAULT     (10.0)
#define PMT_GAIN_DEFAULT        (10.0)
#define CRS_AMPLITUDE_DEFAULT   (3.3f)
#define SCAN_POINTS_DEFAULT     (512)
#define SCAN_PIXEL_TIME_DEFAULT (2)		// 2us
#define HOLE_SIZE_DEFAULT		(50)

/* define laser struct */
typedef struct _LASER_CHAN {
    uint16_t id;
    uint16_t status;
    float power;
} LASER_CHAN[LASER_CHAN_NUM];

typedef struct _LASER {
    char* portName;
    bool isOpen;
    LASER_CHAN channels;
}LASER;

/* define pmt struct */
typedef struct _PMT {
    float gain;
} PMT[PMT_CHAN_NUM];

/* define crs struct */
typedef struct _CRS {
    float amplitude;
} CRS;

/* define scan struct */
typedef struct _SCAN {
    uint16_t mode;
    uint16_t strategy;
    uint16_t x;
    uint16_t y;
    uint16_t flag;
    float pixelTime;
} SCAN;

/* define light hole struct */
typedef struct _PINHOLE {
    uint16_t size;
} PINHOLE;

/* define config struct */
struct _Cfg
{
    LASER m_laser;
    PMT m_pmt;
    CRS m_crs;
    SCAN m_scan;
    PINHOLE m_hole;

    _Cfg();
    void Init();
};

typedef struct _Cfg Cfg, *PCfg;

Cfg m_cfg;
HCONFIG h_cfg = &m_cfg;

_Cfg::_Cfg()
{
    Init();
}

void _Cfg::Init()
{
    m_laser.isOpen = false;
    m_laser.portName = NULL;
    for (int i = 0; i < LASER_CHAN_NUM; i++)
    {
        m_laser.channels[i].id = i;
        m_laser.channels[i].status = LASER_CHAN_SWITCH_ON;
        m_laser.channels[i].power = LASER_POWER_DEFAULT;
    }

    for (int i = 0; i < PMT_CHAN_NUM; i++)
    {
        m_pmt[i].gain = PMT_GAIN_DEFAULT;
    }

    m_crs.amplitude = CRS_AMPLITUDE_DEFAULT;

    m_hole.size = HOLE_SIZE_DEFAULT;

    m_scan.mode = SCAN_MODE_GALVANOMETER;
    m_scan.strategy = SCAN_STRATEGY_Z_UNIDIRECTION;
    m_scan.x = SCAN_POINTS_DEFAULT;
    m_scan.y = SCAN_POINTS_DEFAULT;
    m_scan.pixelTime = SCAN_PIXEL_TIME_DEFAULT;
    m_scan.flag = 0;
}

#pragma region APIs

int Test(int val)
{
    if (val == 0)
    {
        return API_SUCCESS;
    }
    else
    {
        return API_FAILED_ATS_CONFIG_ASYNC_READ_FAILED;
    }
}

HCONFIG CfgGetConfig(void)
{
    return h_cfg;
}

uint16_t CfgGetChannelNum(void)
{
    return CHAN_NUM;
}

int CfgSetLaserSwitch(HCONFIG pConfig, uint16_t id, uint16_t status)
{
    ((PCfg)pConfig)->m_laser[id].status = status;
    return API_SUCCESS;
}

uint16_t CfgGetLaserSwitch(HCONFIG pConfig, uint16_t id)
{
    return ((PCfg)pConfig)->m_laser[id].status;
}

int CfgSetLaserPower(HCONFIG pConfig, uint16_t id, float power)
{
    ((PCfg)pConfig)->m_laser[id].power = power;
    return API_SUCCESS;
}

float CfgGetLaserPower(HCONFIG pConfig, uint16_t id)
{
    return ((PCfg)pConfig)->m_laser[id].power;
}

int CfgSetPmtGain(HCONFIG pConfig, uint16_t id, float gain)
{
    ((PCfg)pConfig)->m_pmt[id].gain = gain;
    return API_SUCCESS;
}

float CfgGetPmtGain(HCONFIG pConfig, uint16_t id)
{
    return ((PCfg)pConfig)->m_pmt[id].gain;
}

int CfgSetCrsAmplitude(HCONFIG pConfig, float amplitude)
{
    ((PCfg)pConfig)->m_crs.amplitude = amplitude;
    return API_SUCCESS;
}

float CfgGetCrsAmplitude(HCONFIG pConfig)
{
    return ((PCfg)pConfig)->m_crs.amplitude;
}

int CfgSetScanMode(HCONFIG pConfig, uint16_t mode)
{
    ((PCfg)pConfig)->m_scan.mode = mode;
    return API_SUCCESS;
}

uint16_t CfgGetScanMode(HCONFIG pConfig)
{
    return ((PCfg)pConfig)->m_scan.mode;
}

int CfgSetScanStartegy(HCONFIG pConfig, uint16_t strategy)
{
    ((PCfg)pConfig)->m_scan.strategy = strategy;
    return API_SUCCESS;
}

uint16_t CfgGetScanStrategy(HCONFIG pConfig)
{
    return ((PCfg)pConfig)->m_scan.strategy;
}

int CfgSetScanPointsX(HCONFIG pConfig, uint16_t x)
{
    ((PCfg)pConfig)->m_scan.x = x;
    return API_SUCCESS;
}

uint16_t CfgGetScanPointsX(HCONFIG pConfig)
{
    return ((PCfg)pConfig)->m_scan.x;
}

int CfgSetScanPointsY(HCONFIG pConfig, uint16_t y)
{
    ((PCfg)pConfig)->m_scan.y = y;
    return API_SUCCESS;
}

uint16_t CfgGetScanPointsY(HCONFIG pConfig)
{
    return ((PCfg)pConfig)->m_scan.y;
}

int CfgSetScanFlag(HCONFIG pConfig, uint16_t flag)
{
    ((PCfg)pConfig)->m_scan.flag = flag;
    return API_SUCCESS;
}

uint16_t CfgGetScanFlag(HCONFIG pConfig)
{
    return ((PCfg)pConfig)->m_scan.flag;
}

int CfgSetScanPixelTime(HCONFIG pConfig, float pixelTime)
{
    ((PCfg)pConfig)->m_scan.pixelTime = pixelTime;
    return API_SUCCESS;
}

float CfgGetScanPixelTime(HCONFIG pConfig)
{
    return ((PCfg)pConfig)->m_scan.pixelTime;
}

int CfgSetHoleSize(HCONFIG pConfig, uint16_t size)
{
    ((PCfg)pConfig)->m_hole.size = size;
    return API_SUCCESS;
}

uint16_t CfgGetHoleSize(HCONFIG pConfig)
{
    return ((PCfg)pConfig)->m_hole.size;
}

#pragma endregion

