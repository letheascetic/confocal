#include "sampleq.h"

SampleQ::SampleQ()
{
    m_initialized = false;
    m_channelmask = 0x00;
    m_activatedChannelNum = 0;
    m_sampleSize = 0;
    m_queueSize = 0;

    for(int i=0; i<4; i++)
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
    for(int i=0; i<4; i++)
    {
        if(m_channelmask || (1U << i))
        {
            channel = m_samples.at(i);
            channel->InitQueue(m_sampleSize, m_queueSize);
        }
    }

    m_initialized = true;
}

void SampleQ::ReleaseQueue()
{
    for(int i=0; i<m_samples.size(); i++)
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

