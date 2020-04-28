#ifndef SAMPLEQ_H
#define SAMPLEQ_H

#include <QList>
#include <QQueue>
#include <QMutexLocker>

#include "dqueue.h"

class SampleQ
{
public:
    SampleQ();
    ~SampleQ();
    void InitQueue(uint32_t channelMask, uint32_t activatedChannelNum, uint32_t sampleSize, uint32_t queueSize);
    void ReleaseQueue(void);
    void DequeueUnused(uint16_t* bufA, uint16_t* bufB, uint16_t* bufC, uint16_t* bufD);
    void EnqueueUsed(uint16_t* bufA, uint16_t* bufB, uint16_t* bufC, uint16_t* bufD);

private:
    const int ChannelCount = 4;
    QMutex m_mutex;
    bool m_initialized;
    uint32_t m_channelmask;
    uint32_t m_activatedChannelNum;       // num of activated channels
    uint32_t m_sampleSize;                // bytes per sample[image]
    uint32_t m_queueSize;                 // queue size per image quque
    QList<DQueue*> m_samples;                  // samples for all channels
};

#endif // SAMPLEQ_H
