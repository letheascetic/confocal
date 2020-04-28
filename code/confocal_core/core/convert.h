#ifndef CONVERT_H
#define CONVERT_H

#include <QObject>
#include <QThread>
#include <QMutexLocker>

class Convert : public QThread
{
    Q_OBJECT
public:
    Convert();
    ~Convert();
    bool isRunning();
    void run();
    void stop();
signals:

private:
    void convert();

private:
    QMutex m_mutex;
    bool m_bRunning;
};

#endif // CONVERT_H
