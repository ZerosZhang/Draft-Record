<Query Kind="Statements">
  <NuGetReference>Microsoft.Extensions.DependencyInjection</NuGetReference>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <RuntimeVersion>8.0</RuntimeVersion>
</Query>

// 创建一个服务容器
var serviceCollection = new ServiceCollection();
// 注册服务
serviceCollection.AddSingleton<IMyService, MyService>();
// 构建服务提供者
var serviceProvider = serviceCollection.BuildServiceProvider();

// 获取服务实例
IMyService myService = serviceProvider.GetService<IMyService>();
if (myService != null)
{
	myService.DoSomething();
}

interface IMyService
{
	void DoSomething();
}

class MyService : IMyService
{
	public void DoSomething()
	{
		Console.WriteLine("Doing something...");
	}
}