#ifndef DQUEUE_H
#define DQUEUE_H

#include <QQueue>
#include <QMutexLocker>
#define QUEUE_SIZE_DEFAULT 8

/* queue for original buffers */
class DQueue
{
public:
    DQueue();
    ~DQueue();
    void InitQueue(uint32_t eSize, uint32_t qSize = QUEUE_SIZE_DEFAULT);
    void ReleaseQueue(void);
    bool Initialized(void);
    uint32_t UsedSize(void);
    uint32_t UnusedSize(void);
    uint32_t ElementSize(void);
    uint16_t* DequeueUnused(void);
    uint16_t* DequeueUsed(void);
    void EnqueueUsed(uint16_t* buffer);
    void EnqueueUnused(uint16_t* buffer);

private:
    QMutex m_mutex;
    bool m_initialized;
    uint32_t m_elemSize;            // bytes per element[bytes per frame], one buffer one frame
    uint32_t m_queueSize;           // maximum num of elements[frames]
    QQueue<uint16_t*> m_unusedQueue;
    QQueue<uint16_t*> m_usedQueue;
};

#endif // QUEUE_H
