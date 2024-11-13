/// <summary>
/// 触发事件
/// </summary>
/// <param name="_event_id"></param>
/// <returns></returns>
public async Task<uint> TriggerEvent(Event _event)
{
    try
    {
        // 尝试从报告列表中获取该报告中定义的变量的值
        List<SECSItem> _report_variable_list = [];
        foreach (uint _report_id in _event.LinkedReportIDList)
        {
            // 尝试获取 Report，如果 Report 不存在，则在对应的 Event 中删除该 Report ID
            Report? _report = TryGetReport(_report_id);
            if (_report is null) { _event.LinkedReportIDList.Remove(_report_id); continue; }

            // 将 Report 中定义的变量加入列表
            List<SECSItem> _value_list = [];
            foreach (uint _variable_id in _report.LinkedVariableID)
            {
                Variable? _variable = TryGetVariable(_variable_id);
                // 此处如果未获取变量，赋值为空列表
                if (_variable is null || _variable.Value is null)
                {
                    _value_list.Add(SECSItem.EmptyList);
                }
                else
                {
                    _value_list.Add(_variable.Value);
                }
            }
            _report_variable_list.Add(new SECSItem(_report_id, new SECSItem([.. _value_list])));
        }

        // 发送 S6F11
        SECSItem _data_item = new(UniqueDataID.Data, _event.ID, new SECSItem([.. _report_variable_list]));
        uint _ret = Send_PrimaryMessage(true, 6, 11, _data_item, out HSMSMessage _s6f11_message);
        if (_ret != BaseAction.ResultOK) { throw new Exception("发送S6F11消息失败"); }

        // 等待S6F12消息回复
        HSMSMessage? _s6f12_message = await ReceivePool.TryGetMessage((_message) =>
        {
            return _message.SystemID == _s6f11_message.SystemID &&
                    _message.SType is SType.DataMessage &&
                    _message is { StreamID: 6, FunctionID: 12 };
        }, T7);

        // 等待超时，发送S9F9
        if (_s6f12_message is null)
        {
            Send_S9F9_Transaction_Timer_Timeout(_s6f11_message, out _);
            throw new Exception($"未正确收到 S6F12，ID = {_s6f11_message.SystemID}");
        }

        // 判断 S6F12 的数据项格式是否满足要求
        if (_s6f12_message.DataItem is not SECSItem _s6f12_item)
        {
            Send_S9F7_Illegal_Data(_s6f12_message, out _);
            throw new Exception($"S6F12 数据项解析失败");
        }
        if (_s6f12_item is not { Type: SECSItemType.Binary } _commack)
        {
            Send_S9F7_Illegal_Data(_s6f12_message, out _);
            throw new Exception($"S6F12 数据项格式并非预定义");
        }

        if (_commack != (Binary)0) 
        { 
            throw new Exception("S6F12的响应码非0"); 
        }

        return BaseAction.ResultOK;
    }
    catch (Exception ex)
    {
        BaseLogManager.SendLog(LogLevel.Error, $"触发事件【{_event.ID} = {_event.Name}】失败", $"{ex}", DefaultConfig.StateShowLog);
        return BaseAction.ResultError;
    }
}