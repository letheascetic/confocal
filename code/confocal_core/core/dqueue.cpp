#include "dqueue.h"

DQueue::DQueue()
{
    m_elemSize = 0;
    m_queueSize = 0;
    m_initialized = false;
}

DQueue::~DQueue()
{
    ReleaseQueue();
}

void DQueue::InitQueue(uint32_t eSize, uint32_t qSize)
{
    ReleaseQueue();
    m_elemSize = eSize;
    uint16_t* buffer = NULL;
    while(m_unusedQueue.size() < (int)qSize)
    {
        buffer = (uint16_t*)malloc(m_elemSize);     // alloc one frame space
        if(buffer != NULL)
        {
            m_unusedQueue.enqueue(buffer);
        }
    }
    m_initialized = true;
}

void DQueue::ReleaseQueue(void)
{
    uint16_t* buffer = NULL;
    while(!m_unusedQueue.empty())
    {
        buffer = m_unusedQueue.dequeue();
        if(buffer != NULL)
        {
            free(buffer);
            buffer = NULL;
        }
    }

    while(!m_usedQueue.empty())
    {
        buffer = m_usedQueue.dequeue();
        if(buffer != NULL)
        {
            free(buffer);
            buffer = NULL;
        }
    }

    m_unusedQueue.clear();
    m_usedQueue.clear();
    m_initialized = false;
    m_elemSize = 0;
    m_queueSize = 0;
}

bool DQueue::Initialized(void)
{
    return m_initialized;
}

uint32_t DQueue::UsedSize(void)
{
    QMutexLocker locker(&m_mutex);
    return m_usedQueue.size();
}

uint32_t DQueue::UnusedSize(void)
{
    QMutexLocker locker(&m_mutex);
    return m_unusedQueue.size();
}

uint32_t DQueue::ElementSize()
{
    return m_elemSize;
}

uint16_t* DQueue::DequeueUnused()
{
   if(!m_initialized)
   {
       return NULL;
   }

   QMutexLocker locker(&m_mutex);
   if(m_unusedQueue.isEmpty())
   {
       return m_usedQueue.dequeue();
   }

    return m_unusedQueue.dequeue();
}

uint16_t* DQueue::DequeueUsed()
{
   if(!m_initialized)
   {
       return NULL;
   }

   QMutexLocker locker(&m_mutex);
   if(m_usedQueue.isEmpty())
   {
       return NULL;
   }

    return m_usedQueue.dequeue();
}

void DQueue::EnqueueUsed(uint16_t *buffer)
{
    if(!m_initialized || buffer == NULL)
    {
        return;
    }

    QMutexLocker locker(&m_mutex);
    m_usedQueue.enqueue(buffer);
}

void DQueue::EnqueueUnused(uint16_t* buffer)
{
    if(!m_initialized || buffer == NULL)
    {
        return;
    }

    QMutexLocker locker(&m_mutex);
    m_unusedQueue.enqueue(buffer);
}
