#include "convert.h"

Convert::Convert()
{
    m_bRunning = false;
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
