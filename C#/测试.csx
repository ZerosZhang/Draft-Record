/// <summary>
/// 定义binary数据类型，对应SESC里binary(二进制10)类型
/// </summary>
public class Binary
{
    byte[] mValue;

    /// <summary>
    /// byte类型，新建字节数组，数组长度为1
    /// </summary>
    /// <param name="v"></param>
    public Binary(byte _value)
    {
        mValue = new byte[] {_value};
    }

    /// <summary>
    /// 返回第一个字节用于比较
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator byte(Binary _value)
    {
        return _value.GetLength() == 1 ? _value.mValue[0] : (byte)255;
    }

    public static implicit operator byte[](Binary _value)
    {
        return _value.mValue;
    }

    /// <summary>
    /// 新建一个字节的Binary值
    /// </summary>
    /// <param name="_byte_value"></param>
    public static implicit operator Binary(byte _byte_value)
    {
        return new Binary(_byte_value);
    }

    /// <summary>
    /// 新建一个以字节数组为值的Binary值
    /// </summary>
    /// <param name="_byte_array"></param>
    public static implicit operator Binary(byte[] _byte_array)
    {
        return new Binary(_byte_array, 0, _byte_array.Length);
    }


    /// <summary>
    /// byte数组转为二进制
    /// </summary>
    /// <param name="_byte_array">byte数组</param>
    /// <param name="_start">起始位置</param>
    /// <param name="_length">转换长度，0为全部转换</param>
    public Binary(byte[] _byte_array, int _start = 0, int _length = 0)
    {
        if(_start <0 || _start >= _byte_array.Length) {_start = 0;}
        if(_length < 0 || _start + _length >= _byte_array.Length) {_length = 0;}

        mValue = new byte[_length];
        for (int i = 0; i < _length; i++)
        {
            mValue[i] = _byte_array[i + _start];
        }
    }


    /// <summary>
    /// 二进制数据长度
    /// </summary>
    /// <returns></returns>
    public int GetLength()
    {
        return mValue.Length;
    }

    /// <summary>
    /// 返回byte类型数据
    /// </summary>
    /// <returns></returns>
    public byte[] GetBytes()
    {
        return mValue;
    }

    /// <summary>
    /// 将值显示十进制的字符串
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        if (mValue.Length == 0)
        {
            return "";
        }
        else if (mValue.Length == 1)
        {
            return mValue[0].ToString();
        }
        else
        {
            StringBuilder _strbd = new StringBuilder();
            _strbd.Append(mValue[0]);
            for (int i = 1; i < v.Length; i++)
            {
                _strbd.Append(" ");
                _strbd.Append(mValue[i]);
            }
            return _strbd.ToString();
        }
    }


    /// <summary>
    /// 将值显示为16进制的字符串
    /// </summary>
    /// <returns></returns>
    public string ToHexString()
    {
        if (mValue.Length == 0)
        {
            return "";
        }
        else if (mValue.Length == 1)
        {
            return mValue[0].ToString("X2");
        }
        else
        {
            StringBuilder _strbd = new StringBuilder();
            _strbd.Append(mValue[0].ToString("X2"));
            for (int i = 1; i < mValue.Length; i++)
            {
                _strbd.Append(" ");
                _strbd.Append(mValue[i].ToString("X2"));
            }
            return _strbd.ToString();

        }
    }
}