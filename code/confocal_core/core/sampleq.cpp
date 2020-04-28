#include "sampleq.h"

SampleQ::SampleQ()
{
    m_initialized = false;
    m_channelmask = 0x00;
    m_activatedChannelNum = 0;
    m_sampleSize = 0;
    m_queueSize = 0;

    for(int i=0; i<ChannelCount; i++)
    {
        DQueue* channel = new DQueue();
        m_samples.append(channel);
    }
}

SampleQ::~SampleQ()
{
    ReleaseQueue();
    DQueue* channel = NULL;
    while(!m_samples.empty())
    {
        channel = m_samples.takeFirst();
        delete channel;
    }
}

void SampleQ::InitQueue(uint32_t channelMask, uint32_t activatedChannelNum, uint32_t sampleSize, uint32_t queueSize)
{
    ReleaseQueue();
    m_channelmask = channelMask;
    m_activatedChannelNum = activatedChannelNum;
    m_sampleSize = sampleSize;
    m_queueSize = queueSize;

    DQueue* channel = NULL;
    for(int i=0; i<ChannelCount; i++)
    {
        if(m_channelmask & (1U << i))
        {
            channel = m_samples.at(i);
            channel->InitQueue(m_sampleSize, m_queueSize);
        }
    }

    m_initialized = true;
}

void SampleQ::ReleaseQueue()
{
    for(int i=0; i<ChannelCount; i++)
    {
        DQueue* channel = m_samples.at(i);
        channel->ReleaseQueue();
    }

    m_initialized = false;
    m_channelmask = 0x00;
    m_activatedChannelNum = 0;
    m_sampleSize = 0;
    m_queueSize = 0;
}

void SampleQ::DequeueUnused(uint16_t *bufA, uint16_t *bufB, uint16_t *bufC, uint16_t *bufD)
{
    if(!m_initialized)
    {
        return;
    }

    QMutexLocker locker(&m_mutex);
    bufA = m_samples.at(0)->DequeueUnused();
    bufB = m_samples.at(1)->DequeueUnused();
    bufC = m_samples.at(2)->DequeueUnused();
    bufD = m_samples.at(3)->DequeueUnused();
}

void SampleQ::EnqueueUsed(uint16_t *bufA, uint16_t *bufB, uint16_t *bufC, uint16_t *bufD)
{
    if(!m_initialized)
    {
        return;
    }

    QMutexLocker locker(&m_mutex);
    m_samples.at(0)->EnqueueUsed(bufA);
    m_samples.at(1)->EnqueueUsed(bufB);
    m_samples.at(2)->EnqueueUsed(bufC);
    m_samples.at(3)->EnqueueUsed(bufD);
}
