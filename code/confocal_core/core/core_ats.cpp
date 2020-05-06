
#include "core_def.h"
#include "core_ats.h"

#include "AlazarApi.h"
#include "AlazarCmd.h"
#include "AlazarError.h"

#define ALAZAR_BOARD_SYSTEM_ID 0x01
#define ALAZAR_BOARD_ID        0x01

ATS m_ats;
HATS h_ats = &m_ats;

ATS::ATS(QObject *parent) : QObject(parent)
{
    handle = NULL;
    m_bufferQ = new DQueue();
    m_sampleQ = new SampleQ();
    m_convert = new Convert(this);
    m_extract = new Extract(this);
}

ATS::~ATS()
{
    handle = NULL;
    if(m_extract != NULL)
    {
        delete m_extract;
    }
    m_extract = NULL;

    if(m_convert != NULL)
    {
        delete m_convert;
    }

    if(m_bufferQ != NULL)
    {
        delete m_bufferQ;
    }

    if(m_sampleQ != NULL)
    {
        delete m_sampleQ;
    }
}

void* ATS::GetHandle(void)
{
    return handle;
}

void ATS::SetHandle(void * handle)
{
    this->handle = handle;
}

DQueue* ATS::GetBufferQ(void)
{
    return m_bufferQ;
}

SampleQ* ATS::GetSampleQ(void)
{
    return m_sampleQ;
}

Extract* ATS::GetExtract(void)
{
    return m_extract;
}

Convert* ATS::GetConvert(void)
{
    return m_convert;
}

#pragma region APIs

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
    if(m_ats.GetHandle() != NULL)
    {
        return h_ats;
    }

    HANDLE handle = AlazarGetBoardBySystemID(ALAZAR_BOARD_SYSTEM_ID, ALAZAR_BOARD_ID);
    if(handle != NULL)
    {
        m_ats.SetHandle(handle);
        return h_ats;
    }

    return NULL;
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

    HANDLE handle = m_ats.GetHandle();
    RETURN_CODE code = ApiSuccess;

    // get board model
    pInfo->BOARD_MODEL = AlazarGetBoardKind(handle);

    // get board version
    code = AlazarGetBoardRevision(handle, &pInfo->BOARD_MAJOR_NUMBER, &pInfo->BOARD_MINOR_NUMBER);
    if(code != ApiSuccess)
    {
        return API_FAILED_ATS_GET_BOARD_VERSION_FAILED;
    }

    // get sdk version
    code = AlazarGetSDKVersion(&pInfo->SDK_MAJOR_NUMBER, &pInfo->SDK_MINOR_NUMBER, &pInfo->SDK_REVISION);
    if(code != ApiSuccess)
    {
        return API_FAILED_ATS_GET_SDK_VERSION_FAILED;
    }

    // get driver version
    code = AlazarGetDriverVersion(&pInfo->DRIVER_MAJOR_NUMBER, &pInfo->DRIVER_MINOR_NUMBER, &pInfo->DRIVER_REVISION);
    if(code != ApiSuccess)
    {
        return API_FIALED_ATS_GET_DRIVER_VERSION_FAILED;
    }

    // get cpld version
    code = AlazarGetCPLDVersion(handle, &pInfo->CPLD_MAJOR_NUMBER, &pInfo->CPLD_MINOR_NUMBER);
    if(code != ApiSuccess)
    {
        return API_FIALED_ATS_GET_CPLD_VERSION_FAILED;
    }

    return API_SUCCESS;
}

int AtsSetConfiguration(HATS pAts, HCONFIG pConfig)
{
    if(pAts == NULL)
    {
        return API_FAILED_ATS_HANDLE_INVALID;
    }

    if(pConfig == NULL)
    {
        return API_FAILED_CONFIG_HANDLE_INVALID;
    }

    HANDLE handle = m_ats.GetHandle();
    RETURN_CODE code = ApiSuccess;

    // set clock: internal_clock, sample_rate, clock_edge
    code = AlazarSetCaptureClock(handle, INTERNAL_CLOCK, SAMPLE_RATE_125MSPS, CLOCK_EDGE_RISING, 0);
    if(code != ApiSuccess)
    {
        return API_FAILED_ATS_SET_CLOCK_FAILED;
    }



}

#pragma endregion
