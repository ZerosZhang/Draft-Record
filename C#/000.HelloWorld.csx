#! dotnet-script
#load "LoadBaseTool.csx"
using BaseTool;

// 测试日志
BaseLog.SendLog("测试日志", LogLevel.Debug);
BaseLog.SendLog("测试日志", LogLevel.Info);
BaseLog.SendLog("测试日志", LogLevel.Warning);
BaseLog.SendLog("测试日志", LogLevel.Error);

// 测试倒计时5秒的功能
BaseLog.SendLog($"当前时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss}", LogLevel.Info);
BaseTimer _time = new BaseTimer(5000);
_time.TimerStart();
while (!_time.StateOver) ;
BaseLog.SendLog($"当前时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss}", LogLevel.Info);