
// 用于基元的操作
try
{
    BaseLogManager.SendLog(LogLevel.Debug, $"【{ToolName}】", "", StateShowLog);
    return BaseAction.ResultOK;
}
catch (Exception ex)
{
    BaseLogManager.SendLog(LogLevel.Error, $"【{ToolName}】", ex.ToString(), StateShowLog);
    return BaseAction.ResultError;
}