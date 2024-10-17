#r "BaseTool\BaseTool.dll"
using System.Threading;
using System.IO;
using BaseTool;

/// <summary>
/// 打印可迭代对象，如数组，列表，使用", "分隔每个元素
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="_array"></param>
public static void Dump<T>(this IEnumerable<T> _array)
{
    Console.WriteLine(string.Join(", ", _array));
}

/// <summary>
/// 打印对象
/// </summary>
/// <param name="_item"></param>
public static void Dump(this object _item)
{
    Console.WriteLine(_item);
}

/// <summary>
/// 将字符串打印到控制台。
/// </summary>
/// <param name="_item">要打印的字符串。</param>
public static void Dump(this string _item)
{
    Console.WriteLine(_item);
}
