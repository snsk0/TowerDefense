using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ControllerBase : IDisposable
{
    readonly CompositeDisposable disposable = new CompositeDisposable();

    void IDisposable.Dispose() => disposable.Dispose();

    public void AddDisposable(IDisposable item)
    {
        disposable.Add(item);
    }
}

public static class DisposableExtensions
{
    public static T AddTo<T>(this T disposable, ControllerBase controller)
        where T : IDisposable
    {
        controller.AddDisposable(disposable);
        return disposable;
    }
}
