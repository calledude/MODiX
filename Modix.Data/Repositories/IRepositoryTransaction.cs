using System;

namespace Modix.Data.Repositories;

/// <summary>
/// <para>Describes a object used to synchronize multi-step create or update operations to be performed upon a repository.</para>
/// <para>
/// A repository transaction is a mechanism for reliably rolling back multi-step operations, if a single step fails,
/// and for ensuring concurrency between operations being run in parallel.
/// </para>
/// </summary>
public interface IRepositoryTransaction : IDisposable
{
    /// <summary>
    /// <para>Commits any changes performed during the transaction to be written to the underlying data storage provider.</para>
    /// <para>This should usually be called right before <see cref="IDisposable.Dispose"/>, and may only be called once per transaction.</para>
    /// <para>If this method is not called before <see cref="IDisposable.Dispose"/>, all changes are automatically rolled back.</para>
    /// </summary>
    void Commit();
}
