CancellationTokenSource mTokenSource1 = new CancellationTokenSource();

CancellationTokenSource mTokenSource2 = new CancellationTokenSource();
mTokenSource2.Token.Register(() => System.Console.WriteLine("mTokenSource2被取消了"));

CancellationTokenSource mTokenSourceNew = CancellationTokenSource.CreateLinkedTokenSource(mTokenSource1.Token, mTokenSource2.Token);
mTokenSourceNew.Token.Register(() => System.Console.WriteLine("mTokenSourceNew被取消了"));

mTokenSource1.Cancel();