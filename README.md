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
    STaskCompletionSource tcs1 = new STaskCompletionSource();
    // 替代TaskCompletionSource<int>
    STaskCompletionSource<int> tcs2 = new STaskCompletionSource<int>();
    return tcs2.Task;
}

// 运行ShowVoid
ShowVoid().Coroutine();
```

  
