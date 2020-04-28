#ifndef CONVERT_H
#define CONVERT_H

#include <QObject>
#include <QThread>
#include <QMutexLocker>

typedef struct _Convert_Info {
    int imagesComplated;       // num of images converted
    int startTickCount;        // start tick count
    double timespan;           // time span
    double imagesPerSecond;    // images per second
} Convert_Info;

class Convert : public QThread
{
    Q_OBJECT
public:
    Convert();
    ~Convert();
    bool isRunning();
    void run();
    void stop();
    Convert_Info getConvertInfo();

signals:

private:
    void reset();
    void init();
    void convert();
    void calculate();

private:
    QMutex m_mutex;
    bool m_bRunning;                // running flag
    Convert_Info m_convertInfo;     // convert info
};

#endif // CONVERT_H
