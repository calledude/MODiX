using System;
using System.Threading;

namespace Modix.Common.Test;

public class AsyncMethodTestContext
    : IDisposable
{
    public readonly CancellationTokenSource _cancellationTokenSource = new();

    public CancellationToken CancellationToken
        => _cancellationTokenSource.Token;

    ~AsyncMethodTestContext()
        => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(
        bool disposeManaged)
    {
        if (disposeManaged)
            _cancellationTokenSource.Dispose();
    }
}
