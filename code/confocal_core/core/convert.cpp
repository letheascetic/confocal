#include "convert.h"
#include <windows.h>

Convert::Convert()
{
    reset();
}

Convert::~Convert()
{
    stop();
    quit();
    wait();
}

bool Convert::isRunning()
{
    QMutexLocker locker(&m_mutex);
    return m_bRunning;
}

void Convert::run()
{
    m_bRunning = true;
    convert();
}

void Convert::stop()
{
    QMutexLocker locker(&m_mutex);
    m_bRunning = false;
}

void Convert::convert()
{
    while(m_bRunning)
    {
        //1. check if new origin buffer received

        //2. get new origin buffer

        //3. get unused image buffer
        msleep(1000);

        //4. give back origin buffer

        //5. give back image buffer
    }
}

void Convert::reset()
{
    m_bRunning = false;
    m_convertInfo.imagesComplated = 0;
    m_convertInfo.startTickCount = 0;
    m_convertInfo.imagesPerSecond = 0.0;
    m_convertInfo.timespan = 0.0;
}

void Convert::init()
{
    reset();
    m_bRunning = true;
    m_convertInfo.startTickCount = GetTickCount();
}

/* calculate convert info */
void Convert::calculate()
{
    m_convertInfo.imagesComplated++;
    m_convertInfo.timespan = (GetTickCount() - m_convertInfo.startTickCount) / 1000.0;
    m_convertInfo.imagesPerSecond = m_convertInfo.imagesComplated / m_convertInfo.timespan;
}

Convert_Info Convert::getConvertInfo()
{
    return m_convertInfo;
}

