using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System;

class SimpleProgram
{
    static void Main()
    {
        CancellationTokenSource mTokenSource = new CancellationTokenSource();
        mTokenSource.Token.Register(() => Console.WriteLine("我被取消了."));

        mTokenSource.CancelAfter(5000);
        Console.WriteLine("不会阻塞,我会执行.");
    }
}

