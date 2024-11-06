/// <summary>
/// 接收消息 S1F11，用于 Host 获取 Status Variable 的定义。接收该消息后，根据 S1F11 的数据项，回复 S1F12
/// </summary>
/// <param name="_s1f11_message"></param>
/// <exception cref="Exception"></exception>
[ReceivedHandle(1, 11)]
internal void Received_S1F11_Status_Variable_Namelist_Request(HSMSMessage _s1f11_message)
{
    // S1F11 结构：长度为n的List
    //      1. < SVID >
    //      2. ……
    //      n. < SVID >
    //或者：< SVID > 数组

    // 判断 S1F11 的数据项格式是否满足要求
    if (_s1f11_message.DataItem is not SECSItem _s1f11_item)
    {
        Send_S9F7_Illegal_Data(_s1f11_message, out _);
        throw new Exception($"S1F11 数据项解析失败");
    }

    if (!TryConvertToUInt32(_s1f11_item, out uint[]? _svid_list))
    {
        Send_S9F7_Illegal_Data(_s1f11_message, out _);
        throw new Exception($"S1F11 数据项格式并非预定义");
    }

    //S1F12 结构：长度为n的列表
    //1. 长度为3的列表
    //    1. <SVID>
    //    2. <SVNAME>
    //    3. <UNITS>
    //2. ..........
    //n. 长度为3的列表
    //    1. <SVID>
    //    2. <SVNAME>
    //    3. <UNITS>

    // 用于存储 S1F12 数据项中的 SV 的定义
    List<SECSItem> _sv_name_list = [];
    if (_svid_list.Length == 0)
    {
        // 如果 S1F11 的数据项是空列表，表示接收所有的 SV 定义
        foreach (Variable _sv in StatusVariableList)
        {
            _sv_name_list.Add(new SECSItem(_sv.ID, _sv.Name, _sv.Unit));
        }
    }
    else
    {
        // 根据 S1F11 数据项发送的 SVID 获取 StatusVariable
        foreach (uint _svid in _svid_list)
        {
            // SVNAME 和 UNITS 都是空字符串表明对应的 SVID 不存在
            if (TryGetStatusVariable(_svid) is Variable _sv)
            {
                _sv_name_list.Add(new SECSItem(_sv.ID, _sv.Name, _sv.Unit));
            }
            else
            {
                _sv_name_list.Add(new SECSItem(_svid, "", ""));
            }
        }
    }

    // 发送 S1F12
    SECSItem _data_item = new([.. _sv_name_list]);
    uint _ret = Send_SecondaryMessage(_s1f11_message, _data_item, out _);
    if (_ret != BaseAction.ResultOK) { throw new Exception("发送消息 S1F12 异常"); }
}