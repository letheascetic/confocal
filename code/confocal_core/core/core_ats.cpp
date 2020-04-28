
#include "core_def.h"
#include "core_ats.h"

#include "AlazarApi.h"
#include "AlazarCmd.h"
#include "AlazarError.h"

#define ALAZAR_BOARD_SYSTEM_ID 0x01
#define ALAZAR_BOARD_ID        0x01

/* ats9440 board info */
struct ATS_INFO {
    uint32_t BOARD_MODEL;             // board model
    uint8_t BOARD_MAJOR_NUMBER;       // board version
    uint8_t BOARD_MINOR_NUMBER;
    uint8_t SDK_MAJOR_NUMBER;         // sdk version
    uint8_t SDK_MINOR_NUMBER;
    uint8_t SDK_REVISION;
    uint8_t DRIVER_MAJOR_NUMBER;      // driver version
    uint8_t DRIVER_MINOR_NUMBER;
    uint8_t DRIVER_REVISION;
    uint8_t CPLD_MAJOR_NUMBER;        // CPLD version
    uint8_t CPLD_MINOR_NUMBER;
};

//class ATS: public QObject
//{
//    Q_OBJECT
//public:
//    explicit ATS(QObject *parent = nullptr);
//    ~ATS();

//public:
//    HANDLE handle;       // handle for ats device
//    ATS_QUEUE m_buffers; // buffers queue
//    ATS_QUEUE m_images;  // images queue
//    EXTRACT *m_extract;  // pointer to extract thread
//    CONVERT *m_convert;  // pointer to convert thread

//signals:
//    void BufferToConvert();

//public slots:
//    void BufferExtracted();

//};

//ATS m_ats;
//HATS h_ats = &m_ats;

//ATS::ATS(QObject *parent) : QObject(parent)
//{
//    this->handle = NULL;
//}

//ATS::~ATS()
//{
//    this->handle = NULL;
//}

//void ATS::BufferExtracted()
//{
//    //qDebug() << "[MIATS]: buffer extarcted.";
//}

int AtsTest(int val)
{
    if (val == 0)
    {
        return API_SUCCESS;
    }
    else
    {
        return API_FAILED_ATS_CONFIG_ASYNC_READ_FAILED;
    }
}

HATS AtsOpen(void)
{
    return AlazarGetBoardBySystemID(ALAZAR_BOARD_SYSTEM_ID, ALAZAR_BOARD_ID);
}

void AtsClose(void)
{

}

int AtsFind(void)
{
    return AlazarNumOfSystems();
}



