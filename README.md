STask一个高效没有GC的单线程的async/await
==========

使用例子
==========
```csharp
/// <summary>
/// 替代async void
/// </summary>
/// <returns></returns>
public async SVoid ShowVoid()
{
    await ShowTask1();
}

/// <summary>
/// 替代async Task
/// </summary>
/// <returns></returns>
public async STask ShowTask1()
{
    await STask.CompletedTask;
}

/// <summary>
/// 替代async Task<int>
/// </summary>
/// <returns></returns>
public STask<int> ShowTask2()
{
    // 替代TaskCompletionSource
    var tcs1 = STask.Create();
    // 替代TaskCompletionSource<int>
    STask<int> tcs2 = STask<int>.Create();
    return tcs2;
}

// 运行ShowVoid
ShowVoid().Coroutine();
```

  
