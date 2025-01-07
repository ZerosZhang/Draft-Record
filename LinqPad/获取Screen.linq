<Query Kind="Statements">
  <Connection>
    <ID>54bf9502-9daf-4093-88e8-7177c12aaaaa</ID>
    <NamingService>2</NamingService>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AttachFileName>&lt;ApplicationData&gt;\LINQPad\ChinookDemoDb.sqlite</AttachFileName>
    <DisplayName>Demo database (SQLite)</DisplayName>
    <DriverData>
      <PreserveNumeric1>true</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.Sqlite</EFProvider>
      <MapSQLiteDateTimes>true</MapSQLiteDateTimes>
      <MapSQLiteBooleans>true</MapSQLiteBooleans>
    </DriverData>
  </Connection>
  <NuGetReference>Microsoft.Extensions.Caching.Memory</NuGetReference>
  <Namespace>System.Windows.Forms</Namespace>
  <RuntimeVersion>8.0</RuntimeVersion>
</Query>

Screen[] _screens_list = Screen.AllScreens;
Console.WriteLine("当前电脑连接了 {0} 个显示器。", _screens_list.Length);
for (int i = 0; i < _screens_list.Length; i++)
{
	int _width = _screens_list[i].Bounds.Width;
	int _height = _screens_list[i].Bounds.Height;
	Console.WriteLine($"显示器【{i + 1}】, 分辨率：{_width}x{_height}");
}