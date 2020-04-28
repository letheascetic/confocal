
#include "core_def.h"
#include "core_ats.h"

#include "AlazarApi.h"
#include "AlazarCmd.h"
#include "AlazarError.h"

#define ALAZAR_BOARD_SYSTEM_ID 0x01
#define ALAZAR_BOARD_ID        0x01

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

int AtsGetBoardInfo(HATS pAts, PBoard_Info pInfo)
{
    if(pAts == NULL)
    {
        return API_FAILED_ATS_HANDLE_INVALID;
    }

    if(pInfo == NULL)
    {
        return API_FAILED_ATS_INFO_INSTANCE_INVALID;
    }



    pInfo->BOARD_MODEL = 0x01;
    pInfo->BOARD_MAJOR_NUMBER = 0x02;
    pInfo->BOARD_MINOR_NUMBER = 0x03;
    pInfo->CPLD_MAJOR_NUMBER = 0x05;
    pInfo->SDK_REVISION = 0x04;

    return API_SUCCESS;
}

