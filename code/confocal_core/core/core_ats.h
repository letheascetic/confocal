#ifndef CORE_ATS_H
#define CORE_ATS_H

#include <stdint.h>
#include <QObject>

#include "core_global.h"
#include "core_cfg.h"
#include "dqueue.h"

typedef void* HATS;
typedef void* HANDLE;

/* ats9440 board info */
typedef struct _BOARD_INFO {
    uint32_t BOARD_MODEL;             // board model
    uint8_t BOARD_MAJOR_NUMBER;       // board version
    uint8_t BOARD_MINOR_NUMBER;
    uint8_t SDK_MAJOR_NUMBER;         // sdk version
    uint8_t SDK_MINOR_NUMBER;
    uint8_t SDK_REVISION;
    uint8_t DRIVER_MAJOR_NUMBER;      // driver version
    uint8_t DRIVER_MINOR_NUMBER;
    uint8_t DRIVER_REVISION;
    uint8_t CPLD_MAJOR_NUMBER;        // CPLD version
    uint8_t CPLD_MINOR_NUMBER;
}Board_Info, *PBoard_Info;

//class ATS: public QObject
//{
//    Q_OBJECT
//public:
//    explicit ATS(QObject *parent = nullptr);
//    ~ATS();

//private:
//    HANDLE handle;         // handle for ats device
//    DQueue m_bufferQ;      // buffers queue
//};


#pragma region APIs

extern "C" CORE_EXPORT int AtsTest(int val);
extern "C" CORE_EXPORT int AtsFind(void);
extern "C" CORE_EXPORT HATS AtsOpen(void);
extern "C" CORE_EXPORT void AtsClose(void);
extern "C" CORE_EXPORT int AtsGetBoardInfo(HATS pAts, PBoard_Info pInfo);

//extern "C" CORE_EXPORT int AtsSetConfiguration(HATS pAts, HCONFIG pConfig);
//extern "C" CORE_EXPORT int AtsStart(HATS pAts, HCONFIG pConfig);
//extern "C" CORE_EXPORT int AtsStop(HATS pAts);

#pragma endregion

#endif // CORE_ATS_H
