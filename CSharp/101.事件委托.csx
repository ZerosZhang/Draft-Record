#! dotnet-script
#load "LoadBaseTool.csx"
using BaseTool;

Action mAction = null;

public void Function1()
{
    BaseLog.SendLog("执行函数1", LogLevel.Info);
}

public void Function2()
{
    BaseLog.SendLog("执行函数2", LogLevel.Info);
}

mAction += Function1;
mAction += Function2;

mAction?.Invoke();