// 该脚本引用.net framework 4.8实现的BaseTool

#r "C:\Users\zeros\Documents\VisualStudioCode\ToolsRepository\ToolsRepository\BaseTool\BaseTool.dll"

public static void Dump<T>(this IEnumerable<T> _array)
{
    Console.WriteLine(string.Join(", ", _array));
}

public static void Dump(this object _item)
{
    Console.WriteLine(_item);
}