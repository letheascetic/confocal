#include "extract.h"
#include <windows.h>

Extract::Extract(ATS* pAts)
{
    m_ats = pAts;
    reset();
}

Extract::~Extract()
{
    stop();
    quit();
    wait();
}

bool Extract::isRunning()
{
    QMutexLocker locker(&m_mutex);
    return m_bRunning;
}

void Extract::run()
{
    init();
    extract();
}

void Extract::stop()
{
    QMutexLocker locker(&m_mutex);
    m_bRunning = false;
}

void Extract::extract()
{
    while(m_bRunning)
    {
        //1. get buffer

        //2. fill buffer
        msleep(1000);

        //3. give back buffer

        //4. calculate
        calculate();
    }
}

void Extract::reset()
{
    m_bRunning = false;
    m_extractInfo.buffersComplated = 0;
    m_extractInfo.startTickCount = 0;
    m_extractInfo.buffersPerSecond = 0.0;
    m_extractInfo.timespan = 0.0;
}

void Extract::init()
{
    reset();
    m_bRunning = true;
    m_extractInfo.startTickCount = GetTickCount();
}

/* calculate extract info */
void Extract::calculate()
{
    m_extractInfo.buffersComplated++;
    m_extractInfo.timespan = (GetTickCount() - m_extractInfo.startTickCount) / 1000.0;
    m_extractInfo.buffersPerSecond = m_extractInfo.buffersComplated / m_extractInfo.timespan;
}

Extrct_Info Extract::getExtractInfo()
{
    return m_extractInfo;
}

