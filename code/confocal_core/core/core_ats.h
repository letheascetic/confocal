#ifndef CORE_ATS_H
#define CORE_ATS_H

#include <stdint.h>
#include <QObject>

#include "core_global.h"
#include "core_cfg.h"

struct ATS_INFO;

typedef void* HATS;

//typedef ATS_INFO* PATS_INFO;

#pragma region APIs

extern "C" CORE_EXPORT int AtsTest(int val);
extern "C" CORE_EXPORT int AtsFind(void);
extern "C" CORE_EXPORT HATS AtsOpen(void);
extern "C" CORE_EXPORT void AtsClose(void);
//extern "C" CORE_EXPORT int AtsGetInfo(HATS pAts, PATS_INFO pInfo);
//extern "C" CORE_EXPORT int AtsSetConfiguration(HATS pAts, HCONFIG pConfig);
//extern "C" CORE_EXPORT int AtsStart(HATS pAts, HCONFIG pConfig);
//extern "C" CORE_EXPORT int AtsStop(HATS pAts);

#pragma endregion

#endif // CORE_ATS_H
