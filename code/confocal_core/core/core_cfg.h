#ifndef CORE_CFG_H
#define CORE_CFG_H

#include <stdint.h>
#include "core_global.h"

// typedef
typedef void* HCONFIG;

// laser config
// laser channel id
#define LASER_CHAN_ID_405_NM 0x0000
#define LASER_CHAN_ID_488_NM 0x0001
#define LASER_CHAN_ID_561_NM 0x0002
#define LASER_CHAN_ID_640_NM 0x0003
// laser switch
#define LASER_CHAN_SWITCH_OFF 0x0000
#define LASER_CHAN_SWITCH_ON  0x0001

// scan config
// scan mode
#define SCAN_MODE_RESONANT 0x0000
#define SCAN_MODE_GALVANOMETER 0x0001
// scan strategy
#define SCAN_STRATEGY_Z_BIDIRECTION 0x0000
#define SCAN_STRATEGY_Z_UNIDIRECTION 0x0001
#define SCAN_STRATEGY_CIRCLE_EQUI_ANGULAR 0x0002
#define SCAN_STRATEGY_CIRCLE_EQUI_LINEAR 0x0003
// scan flag
#define COSINE_DISTORTION_CORRECTION 0x0001

#pragma region APIs

extern "C" CORE_EXPORT int Test(int val);
extern "C" CORE_EXPORT HCONFIG CfgGetConfig(void);
extern "C" CORE_EXPORT uint16_t CfgGetChannelNum(void);
//extern "C" CORE_EXPORT int CfgSetLaserPortName(char* portName);
//extern "C" CORE_EXPORT char* CfgGetLaserPortName(char* portName);
extern "C" CORE_EXPORT int CfgSetLaserSwitch(HCONFIG pConfig, uint16_t id, uint16_t status);
extern "C" CORE_EXPORT uint16_t CfgGetLaserSwitch(HCONFIG pConfig, uint16_t id);
extern "C" CORE_EXPORT int CfgSetLaserPower(HCONFIG pConfig, uint16_t id, float power);
extern "C" CORE_EXPORT float CfgGetLaserPower(HCONFIG pConfig, uint16_t id);
extern "C" CORE_EXPORT int CfgSetPmtGain(HCONFIG pConfig, uint16_t id, float gain);
extern "C" CORE_EXPORT float CfgGetPmtGain(HCONFIG pConfig, uint16_t id);
extern "C" CORE_EXPORT int CfgSetCrsAmplitude(HCONFIG pConfig, float amplitude);
extern "C" CORE_EXPORT float CfgGetCrsAmplitude(HCONFIG pConfig);
extern "C" CORE_EXPORT int CfgSetScanMode(HCONFIG pConfig, uint16_t mode);
extern "C" CORE_EXPORT uint16_t CfgGetScanMode(HCONFIG pConfig);
extern "C" CORE_EXPORT int CfgSetScanStartegy(HCONFIG pConfig, uint16_t strategy);
extern "C" CORE_EXPORT uint16_t CfgGetScanStrategy(HCONFIG pConfig);
extern "C" CORE_EXPORT int CfgSetScanPointsX(HCONFIG pConfig, uint16_t x);
extern "C" CORE_EXPORT uint16_t CfgGetScanPointsX(HCONFIG pConfig);
extern "C" CORE_EXPORT int CfgSetScanPointsY(HCONFIG pConfig, uint16_t y);
extern "C" CORE_EXPORT uint16_t CfgGetScanPointsY(HCONFIG pConfig);
extern "C" CORE_EXPORT int CfgSetScanFlag(HCONFIG pConfig, uint16_t flag);
extern "C" CORE_EXPORT uint16_t CfgGetScanFlag(HCONFIG pConfig);
extern "C" CORE_EXPORT int CfgSetScanPixelTime(HCONFIG pConfig, float pixelTime);
extern "C" CORE_EXPORT float CfgGetScanPixelTime(HCONFIG pConfig);
extern "C" CORE_EXPORT int CfgSetHoleSize(HCONFIG pConfig, uint16_t size);
extern "C" CORE_EXPORT uint16_t CfgGetHoleSize(HCONFIG pConfig);

#pragma endregion

#endif // CORE_CFG_H
