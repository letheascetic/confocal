


ao0		
	控制X振镜

ao1/ao2
	控制Y1/Y2振镜

ai0:3
	四路激光经PMT后信号输入，采集光强信息
	行采集触发信号为数字触发，触发源为PFI4

PFI0		
	debugging模式下，路由Ao Sample Clcok到PFI0

PFI1	
	debugging模式下，路由Do Sample Clcok到PFI1

PFI2/PFI3
	debugging模式下，路由AI Sample Clcok到PFI2， AI Convert Clock到PFI3

PFI4
	设置Ai Start Trigger源为PFI4，PFI4与P0.0物理直连，接收Do的输出信号，作为触发

port0/line0
	数字触发信号输出
	路由Do Sample Clcok到PFI1
	设置Do Start Trigger源为Ao Start Trigger，实现启动同步
