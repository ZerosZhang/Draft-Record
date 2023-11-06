#! dotnet-script
#load "LoadBaseTool.csx"
using BaseTool;

BaseLog.SendLog(DateTime.Now.ToString(), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("d"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("D"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("f"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("F"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("g"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("G"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("t"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("T"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("u"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("U"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("m"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("M"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("r"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("R"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("y"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("Y"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("o"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("O"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("s"), LogLevel.Info);
BaseLog.SendLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff"), LogLevel.Info);

