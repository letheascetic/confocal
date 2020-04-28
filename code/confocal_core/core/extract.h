#ifndef EXTRACT_H
#define EXTRACT_H

#include <QObject>
#include <QThread>
#include <QMutexLocker>

typedef struct _Extract_Info {
    int buffersComplated;       // num of buffers extracted
    int startTickCount;         // start tick count
    double timespan;            // time span
    double buffersPerSecond;    // buffers per second
} Extrct_Info;

class Extract : public QThread
{
    Q_OBJECT
public:
    Extract();
    ~Extract();
    bool isRunning();
    void run();
    void stop();
    Extrct_Info getExtractInfo();

signals:

private:
    void reset();
    void init();
    void extract();
    void calculate();

private:
    QMutex m_mutex;
    bool m_bRunning;            // running flag
    Extrct_Info m_extractInfo;  // extract info
};

#endif // EXTRACT_H
