namespace Modix.Common.Test;

public class AsyncMethodWithLoggerTestContext
    : AsyncMethodTestContext
{
    public readonly TestLoggerFactory _loggerFactory = new();

    protected override void Dispose(
        bool disposeManaged)
    {
        if (disposeManaged)
            _loggerFactory.Dispose();
        base.Dispose(disposeManaged);
    }
}
