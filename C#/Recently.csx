#! dotnet-script
#r "C:\Users\ZerosZhang\Documents\VisualStudioCode\ToolsRepository\ToolsRepository\BaseTool\BaseTool.dll"
using BaseTool;

BaseLog.SendLog("测试日志", LogLevel.Debug);
BaseLog.SendLog("测试日志", LogLevel.Info);
BaseLog.SendLog("测试日志", LogLevel.Warning);
BaseLog.SendLog("测试日志", LogLevel.Error);

BaseLog.SendLog($"当前时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss}", LogLevel.Info);
BaseTimer _time = new BaseTimer(5000);
_time.TimerStart();
while (!_time.StateOver) ;
BaseLog.SendLog($"当前时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss}", LogLevel.Info);
