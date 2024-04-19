#r "C:\Users\zeros\Documents\VisualStudioCode\ToolsRepository_WPF\BaseTool\bin\Debug\net8.0-windows7.0\BaseTool.dll"
#r "C:\Users\zeros\Documents\VisualStudioCode\ToolsRepository_WPF\SoftwareTools\BaseHSMSTool\bin\Debug\net8.0-windows\BaseHSMSTool.dll"

public static void Dump<T>(this IEnumerable<T> _array)
{
    Console.WriteLine(string.Join(", ", _array));
}

public static void Dump(this object _item)
{
    Console.WriteLine(_item);
}

public static void Dump(this string _item)
{
    Console.WriteLine(_item);
}