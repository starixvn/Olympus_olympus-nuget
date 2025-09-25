using System;
using System.Diagnostics;
using System.Runtime;

namespace Olympus.Diagnostic;

/// <summary>
/// Provides functionality for aggressive garbage collection and memory management.
/// </summary>
public class GarbageCollector
{
    private const long BYTES_TO_MEGABYTES_FACTOR = 1024 * 1024;

    /// <summary>
    /// Performs an aggressive garbage collection and returns detailed memory statistics.
    /// </summary>
    /// <returns>A string containing memory usage statistics before and after collection.</returns>
    public static string CollectExtremely()
    {
        using var currentProcess = Process.GetCurrentProcess();
        var stopwatch = Stopwatch.StartNew();

        // Capture initial memory state
        var (mem, heap) = GetMemoryStats(currentProcess);
        
        // Perform aggressive garbage collection
        PerformAggressiveCollection();

        var (finalMem, finalHeap) = GetMemoryStats(currentProcess);

         return $"Mem: {mem} Mb -> {finalMem} Mb. " +
                $"Heap: {finalMem} Mb -> {finalHeap} Mb. " +
                $"Duration: {stopwatch.ElapsedMilliseconds} ms";
    }

    private static void PerformAggressiveCollection()
    {
        // First collection pass
        ExecuteFullGarbageCollection();

        // Configure and perform LOH compaction
        GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
        ExecuteFullGarbageCollection();
    }

    private static void ExecuteFullGarbageCollection()
    {
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, true, true);
        GC.WaitForPendingFinalizers();
        _ = GC.WaitForFullGCComplete();
    }

    private static (long workingSetMb, long heapSizeMb) GetMemoryStats(Process process)
    {
        process.Refresh();
        return (
            workingSetMb: process.WorkingSet64 / BYTES_TO_MEGABYTES_FACTOR,
            heapSizeMb: GC.GetTotalMemory(false) / BYTES_TO_MEGABYTES_FACTOR
        );
    }
}