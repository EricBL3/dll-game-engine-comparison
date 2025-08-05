using System.Collections.Generic;
using System.Linq;
using EchoTest.DLL;
using UnityEngine;

public class AdvancedDLLTester : MonoBehaviour
{
    [Header("Performance Test Settings")]
    public int characterCount = 50;
    public int operationsPerCharacter = 10;
    public int testIterations = 5;
    
    [Header("Results")]
    public List<long> performanceResults = new List<long>();
    
    void Start()
    {
        Debug.Log("=== Advanced DLL Testing ===");
        
        TestPerformanceMetrics();
        TestMicrosecondPerformance();
        TestFrameworkSimulation();
        TestScalability();
        
        Debug.Log("=== Advanced Tests Complete ===");
    }
    
    void TestPerformanceMetrics()
    {
        Debug.Log("--- Performance Testing ---");
        
        performanceResults.Clear();
        
        for (int i = 0; i < testIterations; i++)
        {
            long duration = EchoFunctions.MeasureBatchProcessing(characterCount, operationsPerCharacter);
            performanceResults.Add(duration);
            Debug.Log($"Iteration {i + 1}: {duration}ms for {characterCount} characters");
        }
        
        float average = CalculateAverage(performanceResults);
        Debug.Log($"Average processing time: {average:F2}ms");
        Debug.Log($"Characters per second: {(characterCount / (average / 1000.0f)):F0}");
    }
    
    void TestFrameworkSimulation()
    {
        Debug.Log("--- Framework Simulation Testing ---");
        
        // Register handlers to simulate engine callbacks
        FrameworkSimulator.RegisterStateHandler(OnStateOperation);
        FrameworkSimulator.RegisterLogger(OnFrameworkLog);
        
        // Create test character array
        int[] testCharacters = new int[10];
        for (int i = 0; i < testCharacters.Length; i++)
        {
            testCharacters[i] = i + 1;
        }
        
        // Simulate framework execution cycle
        FrameworkSimulator.ExecuteFrameworkCycle(testCharacters, 0.016f); // 60 FPS
        
        // Test interruption handling
        bool interruptionHandled = FrameworkSimulator.SimulateInterruption(1, 5);
        Debug.Log($"Interruption handling: {(interruptionHandled ? "PASSED" : "FAILED")}");
    }
    
    void TestScalability()
    {
        Debug.Log("--- Scalability Testing ---");
        
        int[] testSizes = {10, 50, 100, 200, 500};
        
        foreach (int size in testSizes)
        {
            long duration = EchoFunctions.MeasureBatchProcessing(size, 5);
            float charactersPerMs = size / (float)duration;
            
            Debug.Log($"{size} characters: {duration}ms ({charactersPerMs:F2} chars/ms)");
            
            // Test callback stress
            var callbackStopwatch = System.Diagnostics.Stopwatch.StartNew();
            EchoFunctions.TestCallbackStress(size);
            callbackStopwatch.Stop();
            
            Debug.Log($"{size} callbacks: {callbackStopwatch.ElapsedMilliseconds}ms");
        }
    }
    
    // Simulate engine's state operation handler
    bool OnStateOperation(int targetId, int stateKey, int operation, int value)
    {
        // Simulate state processing - always return true for testing
        // In real implementation, this would interact with Unity's game objects
        return true;
    }
    
    // Simulate engine's logging system
    void OnFrameworkLog(string message, int severity)
    {
        string prefix = severity switch
        {
            0 => "[DEBUG]",
            1 => "[INFO]",
            2 => "[WARNING]",
            _ => "[ERROR]"
        };
        
        Debug.Log($"{prefix} Framework: {message}");
    }
    
    float CalculateAverage(List<long> values)
    {
        if (values.Count == 0) return 0;
        
        long sum = 0;
        foreach (var value in values)
        {
            sum += value;
        }
        
        return sum / (float)values.Count;
    }
    
    void TestMicrosecondPerformance()
    {
        Debug.Log("--- Microsecond Performance Testing ---");
    
        // Test single call overhead
        List<long> singleCallTimes = new List<long>();
        for (int i = 0; i < 100; i++)
        {
            long overhead = EchoFunctions.MeasureSingleCallOverhead();
            singleCallTimes.Add(overhead);
        }
    
        long avgSingleCall = (long)singleCallTimes.Average();
        Debug.Log($"Average single DLL call: {avgSingleCall} microseconds");
    
        // Test batch processing with microsecond precision
        int[] testSizes = {10, 50, 100, 200, 500};
    
        foreach (int size in testSizes)
        {
            List<long> batchTimes = new List<long>();
        
            for (int i = 0; i < 10; i++) // Multiple runs for accuracy
            {
                long duration = EchoFunctions.MeasureBatchProcessingMicroseconds(size, 5);
                batchTimes.Add(duration);
            }
        
            long avgBatch = (long)batchTimes.Average();
            float microsecondsPerCharacter = avgBatch / (float)size;
        
            Debug.Log($"{size} characters: {avgBatch}μs avg ({microsecondsPerCharacter:F2}μs per character)");
        
            // Test callback precision
            long callbackTime = EchoFunctions.MeasureCallbackStressMicroseconds(size);
            float microsecondsPerCallback = callbackTime / (float)size;
            Debug.Log($"{size} callbacks: {callbackTime}μs ({microsecondsPerCallback:F2}μs per callback)");
        }
    }
}