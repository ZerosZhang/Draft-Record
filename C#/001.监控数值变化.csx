#! dotnet-script
#load "LoadBaseTool.csx"
using BaseTool;

class ValueChanged<ValueType>
{
    private ValueType mValue;
    public Action OnMyValueChanged;

    public ValueChanged(ValueType _init_value)
    {
        mValue = _init_value;   // 设置初始值
    }

    public void SetValue(ValueType _new_value)
    {
        if (!mValue.Equals(_new_value))
        {
            mValue = _new_value;
            OnMyValueChanged?.Invoke();
        }
    }

    public override string ToString()
    {
        return $"{mValue}";
    }
}


// 测试代码
public void ValueChangedFunction()
{
    BaseLog.SendLog("数值发生了改变...", LogLevel.Warning);
}

ValueChanged<int> _variable = new ValueChanged<int>(10);
_variable.OnMyValueChanged += ValueChangedFunction;
BaseLog.SendLog($"当前数值为：{_variable}", LogLevel.Info);
_variable.SetValue(11);
BaseLog.SendLog($"当前数值为：{_variable}", LogLevel.Info);