/*********************************************************************
*
* ANSI C Example program:
*    ContGen-IntClk.c
*
* Example Category:
*    AO
*
* Description:
*    This example demonstrates how to output a continuous periodic
*    waveform using an internal sample clock.
*
* Instructions for Running:
*    1. Select the Physical Channel to correspond to where your
*       signal is output on the DAQ device.
*    2. Enter the Minimum and Maximum Voltage Ranges.
*    3. Enter the desired rate for the generation. The onboard sample
*       clock will operate at this rate.
*    4. Select the desired waveform type.
*    5. The rest of the parameters in the Waveform Information
*       section will affect the way the waveform is created, before
*       it's sent to the analog output of the board. Select the
*       number of samples per cycle and the total number of cycles to
*       be used as waveform data.
*
* Steps:
*    1. Create a task.
*    2. Create an Analog Output Voltage channel.
*    3. Define the update Rate for the Voltage generation.
*       Additionally, define the sample mode to be continuous.
*    4. Write the waveform to the output buffer.
*    5. Call the Start function.
*    6. Wait until the user presses the Stop button.
*    7. Call the Clear Task function to clear the Task.
*    8. Display an error if any.
*
* I/O Connections Overview:
*    Make sure your signal output terminal matches the Physical
*    Channel I/O Control. For further connection information, refer
*    to your hardware reference manual.
*
*********************************************************************/

#include <NIDAQmx.h>
#include <stdio.h>
#include <math.h>
#include <string.h>
#include <windows.h>

#define DAQmxErrChk(functionCall) if( DAQmxFailed(error=(functionCall)) ) goto Error; else

#define PI	3.1415926535

int32 CVICALLBACK DoneCallback(TaskHandle taskHandle, int32 status, void *callbackData);
int Test(void);
int Test2(void);
int Test3(void);
int Test4(void);
int Test5(void);

int main(void)
{
	Test();
}

int Test(void)
{
	int i = 0;
	double xn;

	double galv_response_time = 200;				// ����Ӧʱ�䣬��λus
	double field_size = 200;						// �ӳ���С����λum
	int x_pixels = 512;							// ɨ������X��������
	int y_pixels = 512;							// ɨ������Y��������
	double pixel_time = 2e-3;						// ����ʱ�䣬��λms
	double voltage_ratio = 4.09855e-5;				// �궨��ѹ��ֵ��ÿ5nm��Ӧ�ĵ�ѹ

	double curve_coff = 0.1;						// ����ϵ��
	double maximum_volatge_step = 0.2;				// ����ѹ��
	double daq_ao_rate = 1000 / pixel_time;			// DAQ�豸ģ�����Ƶ�ʣ�AO_SAMPLE_CLOCK��

	double pixel_size = field_size * 1000 / x_pixels;				// ���ش�С����λ���ض�Ӧ��ʵ�ʴ�С����λnm
	double voltage_per_pixel = voltage_ratio * pixel_size / 5.0;	// ��λ���ض�Ӧ���񾵵�ѹ�仯

	double w = pixel_time * x_pixels;			// ɨ��һ����Ч������Ҫ��ʱ�䣬��λms
	double h = x_pixels * voltage_per_pixel;	// ɨ��һ����Ч�����񾵵ĵ�ѹ�仯��ֵ����λV
	double cosx = w / sqrt(w*w + h * h);		// ��ݲ����ҽǶ�ֵ
	double sinx = sqrt(1 - cosx * cosx);		// ��ݲ����ҽǶ�ֵ
	double r = curve_coff * h / (1 - cosx);		// ǰ��ɨ�����ߵİ뾶������ϵ��Ĭ��0.1

	double prev_xc = -(w / 2 + r * sinx);		// ǰ��ɨ������Բ�ĵ�x����ֵ
	double prev_yc = -(h / 2 - r * cosx);		// ǰ��ɨ������Բ�ĵ�y����ֵ
	double prev_x0 = -(w / 2 + 2 * r * sinx);
	double prev_y0 = -h / 2;
	int prev_total_samples = (int)(2 * r * sinx / w * x_pixels);				// ��prev_x0��prev_xn�ܹ���samples����
	int prev_samples = (int)ceil( galv_response_time / pixel_time / 1000) * 2;	// ǰ��ɨ�����ߵ���������
	//if (prev_samples < prev_total_samples && prev_samples > 1)
	//{
	//	prev_samples = prev_total_samples;
	//}
	
	double prev_data[512];
	for (i = 0; i < prev_samples / 2; i++)
	{
		xn = prev_x0 + 2 * r * sinx / prev_total_samples * i;
		prev_data[i] = -sqrt(r * r - (xn - prev_xc)* (xn - prev_xc)) + prev_yc;
		prev_data[prev_samples - 1 - i] = prev_data[i];
	}

	double valid_data[4096];
	int valid_samples = x_pixels;
	for (i = 0; i < 4096; i++)
	{
		if (i < valid_samples)
		{
			valid_data[i] = (i - (valid_samples / 2)) * voltage_per_pixel;
		}
		else
		{
			valid_data[i] = 0;
		}
	}

	int post_first_total_samples = (int)(2 * r * sinx / w * x_pixels);
	int post_first_samples = (int)ceil(galv_response_time / pixel_time / 1000) * 2;

	int post_second_samples = (int)ceil(galv_response_time / pixel_time / 1000);
	int post_second_minimum_samples = (int)(h / maximum_volatge_step);
	if (post_second_samples < post_second_minimum_samples)
	{
		post_second_samples = post_second_minimum_samples;
	}
	
	int post_samples = post_first_samples + post_second_samples;
	int post_data_index = 0;

	double post_xc = w / 2 + r * sinx;
	double post_yc = h / 2 - r * cosx;

	double post_data[1024];
	for (i = 0; i < post_first_samples/2; i++)
	{
		xn = w / 2 + 2 * r * sinx / post_first_total_samples * i;
		post_data[i] = sqrt(r * r - (xn - post_xc)* (xn - post_xc)) + post_yc;
		post_data[post_first_samples - 1 - i] = post_data[i];
	}
	post_data_index = post_first_samples;

	for (i = 0; i < post_second_samples; i++)
	{
		post_data[i + post_data_index] = h / 2 - h / post_second_samples * i;
	}
	post_data_index = post_first_samples + post_second_samples;

	int total_samples = valid_samples + post_samples + prev_samples;
	double total_data[5632];

	//double* waves = (double *)malloc(total_samples * sizeof(double));

	memcpy(total_data, prev_data, sizeof(double) * prev_samples);
	memcpy(total_data + prev_samples, valid_data, sizeof(double) * valid_samples);
	memcpy(total_data + prev_samples + valid_samples, post_data, sizeof(double) * post_samples);

	int32       error = 0;
	TaskHandle  taskHandle = 0;
	char        errBuff[2048] = { '\0' };

	/*********************************************/
	// DAQmx Configure Code
	/*********************************************/
	DAQmxErrChk(DAQmxCreateTask("", &taskHandle));
	// ����һ������ͨ�����󶨵�����ͨ��Ϊ"Dev1/ao0"
	// ����ͨ��û����ʾ��������Ĭ��ʹ������ͨ��������
	// ��������СֵΪ-10.0��10.0���������Ϊ��ѹ��ֵ
	DAQmxErrChk(DAQmxCreateAOVoltageChan(taskHandle, "Dev1/ao0", "", -10.0, 10.0, DAQmx_Val_Volts, NULL));
	// ����ʱ��Դ��ʱ��Ƶ�ʡ����ɻ��ȡ��������
	// ʹ���ڲ�ʱ��
	// rate=1000��1000fpsÿ��ͨ��������ʱ�ӵ����������ɻ��ȡ����
	// DAQmx_Val_ContSamps����ʾ�������ɻ��ȡ������ֱ���û�ֹͣ����
	DAQmxErrChk(DAQmxCfgSampClkTiming(taskHandle, "", daq_ao_rate, DAQmx_Val_Rising, DAQmx_Val_ContSamps, total_samples));

	DAQmxErrChk(DAQmxRegisterDoneEvent(taskHandle, 0, DoneCallback, NULL));

	/*********************************************/
	// DAQmx Write Code
	/*********************************************/
	// numSampsPerChan = 1000,ÿ��ͨ����������
	// autoStart = 0,���Զ���������
	// timeout = 10.0��д���ݵ�NI�豸�ĵȴ�ʱ��
	DAQmxErrChk(DAQmxWriteAnalogF64(taskHandle, total_samples, 0, 10.0, DAQmx_Val_GroupByChannel, total_data, NULL, NULL));

	/*********************************************/
	// DAQmx Start Code
	/*********************************************/
	DAQmxErrChk(DAQmxStartTask(taskHandle));

	printf("Generating voltage continuously. Press Enter to interrupt\n");
	getchar();

Error:
	if (DAQmxFailed(error))
		DAQmxGetExtendedErrorInfo(errBuff, 2048);
	if (taskHandle != 0) {
		/*********************************************/
		// DAQmx Stop Code
		/*********************************************/
		DAQmxStopTask(taskHandle);
		DAQmxClearTask(taskHandle);
	}
	if (DAQmxFailed(error))
		printf("DAQmx Error: %s\n", errBuff);
	printf("End of program, press Enter key to quit\n");
	getchar();
	return 0;
}

int Test2(void)
{
	int32       error = 0;
	TaskHandle  taskHandle = 0;
	float64     data[1000];
	char        errBuff[2048] = { '\0' };
	int         i = 0;

	for (; i < 1000; i++)
		data[i] = 5.0*sin((double)i*2.0*PI / 1000.0);

	/*********************************************/
	// DAQmx Configure Code
	/*********************************************/
	DAQmxErrChk(DAQmxCreateTask("", &taskHandle));
	// ����һ������ͨ�����󶨵�����ͨ��Ϊ"Dev1/ao0"
	// ����ͨ��û����ʾ��������Ĭ��ʹ������ͨ��������
	// ��������СֵΪ-10.0��10.0���������Ϊ��ѹ��ֵ
	DAQmxErrChk(DAQmxCreateAOVoltageChan(taskHandle, "Dev1/ao0:1", "", -10.0, 10.0, DAQmx_Val_Volts, NULL));

	// ����ʱ��Դ��ʱ��Ƶ�ʡ����ɻ��ȡ��������
	// ʹ���ڲ�ʱ��
	// rate=1000��1000fpsÿ��ͨ��������ʱ�ӵ����������ɻ��ȡ����
	// DAQmx_Val_ContSamps����ʾ�������ɻ��ȡ������ֱ���û�ֹͣ����
	DAQmxErrChk(DAQmxCfgSampClkTiming(taskHandle, "", 1000, DAQmx_Val_Rising, DAQmx_Val_ContSamps, 5000));

	DAQmxErrChk(DAQmxRegisterDoneEvent(taskHandle, 0, DoneCallback, NULL));

	/*********************************************/
	// DAQmx Write Code
	/*********************************************/
	// numSampsPerChan = 1000,ÿ��ͨ����������
	// autoStart = 0,���Զ���������
	// timeout = 10.0��д���ݵ�NI�豸�ĵȴ�ʱ��
	// д�������numSampsPerChan������buffer size�Ĵ�С�������ڵ����������ǰ��ʾ����DAQmxCfgOutputBuffer��
	DAQmxErrChk(DAQmxWriteAnalogF64(taskHandle, 500, 0, 10.0, DAQmx_Val_GroupByChannel, data, NULL, NULL));

	/*********************************************/
	// DAQmx Start Code
	/*********************************************/
	DAQmxErrChk(DAQmxStartTask(taskHandle));

	printf("Generating voltage continuously. Press Enter to interrupt\n");
	getchar();

Error:
	if (DAQmxFailed(error))
		DAQmxGetExtendedErrorInfo(errBuff, 2048);
	if (taskHandle != 0) {
		/*********************************************/
		// DAQmx Stop Code
		/*********************************************/
		DAQmxStopTask(taskHandle);
		DAQmxClearTask(taskHandle);
	}
	if (DAQmxFailed(error))
		printf("DAQmx Error: %s\n", errBuff);
	printf("End of program, press Enter key to quit\n");
	getchar();
	return 0;
}

int Test3(void)
{
	int i = 0;
	double xn;

	double galv_response_time = 200;				// ����Ӧʱ�䣬��λus
	double field_size = 200;						// �ӳ���С����λum
	int x_pixels = 4096;							// ɨ������X��������
	int y_pixels = 4096;							// ɨ������Y��������
	double pixel_time = 200e-3;						// ����ʱ�䣬��λms
	double voltage_ratio = 4.09855e-5;				// �궨��ѹ��ֵ��ÿ5nm��Ӧ�ĵ�ѹ

	double curve_coff = 0.1;						// ����ϵ��
	double maximum_volatge_step = 0.2;				// ����ѹ��
	double daq_ao_rate = 1000 / pixel_time;			// DAQ�豸ģ�����Ƶ�ʣ�AO_SAMPLE_CLOCK��

	double pixel_size = field_size * 1000 / x_pixels;				// ���ش�С����λ���ض�Ӧ��ʵ�ʴ�С����λnm
	double voltage_per_pixel = voltage_ratio * pixel_size / 5.0;	// ��λ���ض�Ӧ���񾵵�ѹ�仯

	double w = pixel_time * x_pixels;			// ɨ��һ����Ч������Ҫ��ʱ�䣬��λms
	double h = x_pixels * voltage_per_pixel;	// ɨ��һ����Ч�����񾵵ĵ�ѹ�仯��ֵ����λV
	double h2 = y_pixels * voltage_per_pixel;
	double cosx = w / sqrt(w*w + h * h);		// ��ݲ����ҽǶ�ֵ
	double sinx = sqrt(1 - cosx * cosx);		// ��ݲ����ҽǶ�ֵ
	double r = curve_coff * h / (1 - cosx);		// ǰ��ɨ�����ߵİ뾶������ϵ��Ĭ��0.1

	double prev_xc = -(w / 2 + r * sinx);		// ǰ��ɨ������Բ�ĵ�x����ֵ
	double prev_yc = -(h / 2 - r * cosx);		// ǰ��ɨ������Բ�ĵ�y����ֵ
	double prev_x0 = -(w / 2 + 2 * r * sinx);
	double prev_y0 = -h / 2;
	int prev_total_samples = (int)(2 * r * sinx / w * x_pixels);				// ��prev_x0��prev_xn�ܹ���samples����
	int prev_samples = (int)ceil(galv_response_time / pixel_time / 1000) * 2;	// ǰ��ɨ�����ߵ���������
	//if (prev_samples < prev_total_samples && prev_samples > 1)
	//{
	//	prev_samples = prev_total_samples;
	//}

	double prev_data[512];
	for (i = 0; i < prev_samples / 2; i++)
	{
		xn = prev_x0 + 2 * r * sinx / prev_total_samples * i;
		prev_data[i] = -sqrt(r * r - (xn - prev_xc)* (xn - prev_xc)) + prev_yc;
		prev_data[prev_samples - 1 - i] = prev_data[i];
	}

	double valid_data[4096];
	int valid_samples = x_pixels;
	for (i = 0; i < 4096; i++)
	{
		if (i < valid_samples)
		{
			valid_data[i] = (i - (valid_samples / 2)) * voltage_per_pixel;
		}
		else
		{
			valid_data[i] = 0;
		}
	}

	int post_first_total_samples = (int)(2 * r * sinx / w * x_pixels);
	int post_first_samples = (int)ceil(galv_response_time / pixel_time / 1000) * 2;

	int post_second_samples = (int)ceil(galv_response_time / pixel_time / 1000);
	int post_second_minimum_samples = (int)(h / maximum_volatge_step);
	if (post_second_samples < post_second_minimum_samples)
	{
		post_second_samples = post_second_minimum_samples;
	}

	int post_samples = post_first_samples + post_second_samples;
	int post_data_index = 0;

	double post_xc = w / 2 + r * sinx;
	double post_yc = h / 2 - r * cosx;

	double post_data[1024];
	for (i = 0; i < post_first_samples / 2; i++)
	{
		xn = w / 2 + 2 * r * sinx / post_first_total_samples * i;
		post_data[i] = sqrt(r * r - (xn - post_xc)* (xn - post_xc)) + post_yc;
		post_data[post_first_samples - 1 - i] = post_data[i];
	}
	post_data_index = post_first_samples;

	for (i = 0; i < post_second_samples; i++)
	{
		post_data[i + post_data_index] = h / 2 - h / post_second_samples * i;
	}
	post_data_index = post_first_samples + post_second_samples;

	int samples_per_line = valid_samples + post_samples + prev_samples;
	double total_data[5632];

	memcpy(total_data, prev_data, sizeof(double) * prev_samples);
	memcpy(total_data + prev_samples, valid_data, sizeof(double) * valid_samples);
	memcpy(total_data + prev_samples + valid_samples, post_data, sizeof(double) * post_samples);

	int samples_per_channel = samples_per_line * y_pixels;
	double* waves = (double *)malloc(samples_per_channel * sizeof(double) * 3);

	int j;
	int offset, offset2;
	double y_sample;
	for (i = 0; i < y_pixels; i++)
	{
		memcpy(waves + i * samples_per_line, total_data, sizeof(double) * samples_per_line);
		y_sample = voltage_per_pixel * i - h2 / 2;
		//y_sample = 19.0 / y_pixels * i - 9.5;

		offset = samples_per_channel + i * samples_per_line;
		offset2 = samples_per_channel * 2 + i * samples_per_line;
		for (j = 0; j < samples_per_line; j++)
		{
			waves[offset + j] = y_sample;
			waves[offset2 + j] = y_sample * 2;
		}
	}

	int32       error = 0;
	TaskHandle  taskHandle = 0;
	char        errBuff[2048] = { '\0' };

	/*********************************************/
	// DAQmx Configure Code
	/*********************************************/
	DAQmxErrChk(DAQmxCreateTask("", &taskHandle));
	// ����һ������ͨ�����󶨵�����ͨ��Ϊ"Dev1/ao0"
	// ����ͨ��û����ʾ��������Ĭ��ʹ������ͨ��������
	// ��������СֵΪ-10.0��10.0���������Ϊ��ѹ��ֵ
	DAQmxErrChk(DAQmxCreateAOVoltageChan(taskHandle, "Dev1/ao0:2", "", -10.0, 10.0, DAQmx_Val_Volts, NULL));
	// ����ʱ��Դ��ʱ��Ƶ�ʡ����ɻ��ȡ��������
	// ʹ���ڲ�ʱ��
	// rate=1000��1000fpsÿ��ͨ��������ʱ�ӵ����������ɻ��ȡ����
	// DAQmx_Val_ContSamps����ʾ�������ɻ��ȡ������ֱ���û�ֹͣ����
	DAQmxErrChk(DAQmxCfgSampClkTiming(taskHandle, "", daq_ao_rate, DAQmx_Val_Rising, DAQmx_Val_ContSamps, samples_per_channel));

	DAQmxErrChk(DAQmxRegisterDoneEvent(taskHandle, 0, DoneCallback, NULL));

	/*********************************************/
	// DAQmx Write Code
	/*********************************************/
	// numSampsPerChan = 1000,ÿ��ͨ����������
	// autoStart = 0,���Զ���������
	// timeout = 10.0��д���ݵ�NI�豸�ĵȴ�ʱ��
	DAQmxErrChk(DAQmxWriteAnalogF64(taskHandle, samples_per_channel, 0, 10.0, DAQmx_Val_GroupByChannel, waves, NULL, NULL));

	/*********************************************/
	// DAQmx Start Code
	/*********************************************/
	DAQmxErrChk(DAQmxStartTask(taskHandle));

	printf("Generating voltage continuously. Press Enter to interrupt\n");
	getchar();
	
Error:
	if (DAQmxFailed(error))
		DAQmxGetExtendedErrorInfo(errBuff, 2048);
	if (taskHandle != 0) {
		/*********************************************/
		// DAQmx Stop Code
		/*********************************************/
		DAQmxStopTask(taskHandle);
		DAQmxClearTask(taskHandle);
	}
	if (DAQmxFailed(error))
		printf("DAQmx Error: %s\n", errBuff);

	free(waves);

	printf("End of program, press Enter key to quit\n");
	getchar();

	return 0;
}

int Test4(void)
{
	int32       error = 0;
	TaskHandle  taskHandle = 0;
	float64     data[1000];
	char        errBuff[2048] = { '\0' };
	int         i = 0;

	uInt32 buf_size = 0;
	int32 status_code = 0;

	for (; i < 1000; i++)
		data[i] = 5.0*sin((double)i*2.0*PI / 1000.0);

	/*********************************************/
	// DAQmx Configure Code
	/*********************************************/
	DAQmxErrChk(DAQmxCreateTask("", &taskHandle));
	// ����һ������ͨ�����󶨵�����ͨ��Ϊ"Dev1/ao0"
	// ����ͨ��û����ʾ��������Ĭ��ʹ������ͨ��������
	// ��������СֵΪ-10.0��10.0���������Ϊ��ѹ��ֵ
	DAQmxErrChk(DAQmxCreateAOVoltageChan(taskHandle, "Dev1/ao0", "", -10.0, 10.0, DAQmx_Val_Volts, NULL));

	DAQmxGetWriteRegenMode(taskHandle, &status_code);


	//DAQmxGetBufOutputBufSize(taskHandle, &buf_size);
	//DAQmxSetBufOutputBufSize(taskHandle, 10000);
	// Overrides the automatic output buffer allocation that NI-DAQmx performs.
	// numSampsPerChan: The number of samples the buffer can hold for each channel in the task. 
	// Zero indicates no buffer should be allocated. Use a buffer size of 0 to perform a hardware-timed operation without using a buffer.
	//DAQmxCfgOutputBuffer(taskHandle, 10000);

	//DAQmxGetBufOutputBufSize(taskHandle, &buf_size);

	// ����ʱ��Դ��ʱ��Ƶ�ʡ����ɻ��ȡ��������
	// ʹ���ڲ�ʱ��
	// rate=1000��1000fpsÿ��ͨ��������ʱ�ӵ����������ɻ��ȡ����
	// DAQmx_Val_ContSamps����ʾ�������ɻ��ȡ������ֱ���û�ֹͣ����
	DAQmxErrChk(DAQmxCfgSampClkTiming(taskHandle, "", 2000, DAQmx_Val_Rising, DAQmx_Val_ContSamps, 1500));

	DAQmxErrChk(DAQmxRegisterDoneEvent(taskHandle, 0, DoneCallback, NULL));

	/*********************************************/
	// DAQmx Write Code
	/*********************************************/
	// numSampsPerChan = 1000,ÿ��ͨ����������
	// autoStart = 0,���Զ���������
	// timeout = 10.0��д���ݵ�NI�豸�ĵȴ�ʱ��
	// д�������numSampsPerChan������buffer size�Ĵ�С�������ڵ����������ǰ��ʾ����DAQmxCfgOutputBuffer��
	DAQmxErrChk(DAQmxWriteAnalogF64(taskHandle, 1000, 0, 10.0, DAQmx_Val_GroupByChannel, data, NULL, NULL));

	/*********************************************/
	// DAQmx Start Code
	/*********************************************/
	DAQmxErrChk(DAQmxStartTask(taskHandle));

	printf("Generating voltage continuously. Press Enter to interrupt\n");

	status_code = 1;
	while (status_code<100)
	{
		Sleep(1000);
		if (status_code & 0x01)
		{
			for (i=0; i < 1000; i++)
				data[i] = 3.0*cos((double)i*2.0*PI / 1000.0);
			status_code++;
		}
		else
		{
			for (i=0; i < 1000; i++)
				data[i] = 5.0*sin((double)i*2.0*PI / 1000.0);
			status_code++;
		}
		DAQmxErrChk(DAQmxWriteAnalogF64(taskHandle, 1000, 0, 10.0, DAQmx_Val_GroupByChannel, data, NULL, NULL));
	}

	//getchar();

Error:
	if (DAQmxFailed(error))
		DAQmxGetExtendedErrorInfo(errBuff, 2048);
	if (taskHandle != 0) {
		/*********************************************/
		// DAQmx Stop Code
		/*********************************************/
		DAQmxStopTask(taskHandle);
		DAQmxClearTask(taskHandle);
	}
	if (DAQmxFailed(error))
		printf("DAQmx Error: %s\n", errBuff);
	printf("End of program, press Enter key to quit\n");
	getchar();
	return 0;
}

int Test5(void)
{
	double galv_response_time = 200;
	double curve_coff = 0.1;

	double field_size[2] = { 120, 200 };
	double pixel_time[] = { 1e-3, 2e-3, 10e-3, 50e-3, 100e-3, 1.0, 10.0, 100.0 };
	int x_pixels[] = { 256, 512, 1024, 2048, 4096 };
	double voltage_ratio = 4.09855e-5;

	int i, j, k;
	double cur_field_size, cur_pixel_time, cosx, sinx, r, x, w, h;
	int cur_x_pixels;
	int prev_total_samples;
	int prev_samples;

	for (i = 0; i < 2; i++)
	{
		cur_field_size = field_size[i];

		for (j = 0; j < 7; j++)
		{
			cur_pixel_time = pixel_time[j];

			for (k = 0; k < 5; k++)
			{
				cur_x_pixels = x_pixels[k];

				w = cur_pixel_time * cur_x_pixels;
				h = voltage_ratio * cur_field_size * 200;
				cosx = w / sqrt(w*w + h * h);
				sinx = sqrt(1 - cosx * cosx);		// ��ݲ����ҽǶ�ֵ
				r = curve_coff * h / (1 - cosx);	// ǰ��ɨ�����ߵİ뾶������ϵ��Ĭ��0.1

				prev_total_samples = (int)(r * sinx / w * cur_x_pixels);			// ��prev_x0��prev_xn�ܹ���samples����
				prev_samples = (int)ceil(galv_response_time / cur_pixel_time / 1000);	// ǰ��ɨ�����ߵ���������
				
				x = acos(cosx) * 180 / 3.1415;

				//printf("field_size:[%.1f], pixel_time:[%f], x_pixels:[%d], w:[%.2f], h:[%.2f], cosx:[%.2f], x:[%.1f]\n", cur_field_size, cur_pixel_time, cur_x_pixels, w, h, cosx, x);
				
				printf("field_size:[%.1f], pixel_time:[%f], x_pixels:[%d], prev_total_samples:[%d], prev_samples:[%d]\n", cur_field_size, cur_pixel_time, cur_x_pixels, prev_total_samples, prev_samples);
				//printf("field_size:[%.1f], pixel_time:[%f], x_pixels:[%d]\n", cur_field_size, cur_pixel_time, cur_x_pixels);
				//printf("w:[%.2f], h:[%.2f], cosx:[%.2f], x:[%.1f]\n", w, h, cosx, x);
			}
		}
	}

	getchar();
	return 0;
}

int32 CVICALLBACK DoneCallback(TaskHandle taskHandle, int32 status, void *callbackData)
{
	int32   error = 0;
	char    errBuff[2048] = { '\0' };

	// Check to see if an error stopped the task.
	DAQmxErrChk(status);

Error:
	if (DAQmxFailed(error)) {
		DAQmxGetExtendedErrorInfo(errBuff, 2048);
		DAQmxClearTask(taskHandle);
		printf("DAQmx Error: %s\n", errBuff);
	}
	return 0;
}
