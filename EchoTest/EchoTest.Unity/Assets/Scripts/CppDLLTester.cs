using System;
using System.Collections.Generic;
using UnityEngine;

public class CppDLLTester : MonoBehaviour
{
    [Header("Performance Test Settings")]
    public int characterCount = 50;
    public int operationsPerCharacter = 10;
    public int testIterations = 5;
    
    [Header("Results")]
    public List<long> performanceResults = new List<long>();
    
    void Start()
    {
        Debug.Log("=== C++ DLL Testing ===");
        
        TestBasicFunctions();
        TestArrayHandling();
        TestCallbacks();
        TestMicrosecondPerformance();
        TestFrameworkSimulation();
        
        Debug.Log("=== C++ DLL Tests Complete ===");
    }
    
    void TestBasicFunctions()
    {
        Debug.Log("--- Testing Basic Functions (C++) ---");
        
        try
        {
            // Test integer addition
            int result = CppDLLWrapper.AddIntegers(5, 3);
            Debug.Log($"C++ AddIntegers(5, 3) = {result}");
            
            // Test string modification
            IntPtr stringPtr = CppDLLWrapper.ModifyString("Hello from Unity to C++");
            string stringResult = CppDLLWrapper.GetStringFromPtr(stringPtr);
            Debug.Log($"C++ ModifyString result: {stringResult}");
            
            Debug.Log("Basic functions: " + (result == 8 ? "PASSED" : "FAILED"));
        }
        catch (Exception ex)
        {
            Debug.LogError($"Basic functions failed: {ex.Message}");
        }
    }
    
    void TestArrayHandling()
    {
        Debug.Log("--- Testing Array Handling (C++) ---");
        
        try
        {
            int[] testArray = {1, 2, 3, 4, 5};
            int[] processed = new int[testArray.Length];
            
            CppDLLWrapper.ProcessIntArray(testArray, testArray.Length, processed);
            
            Debug.Log($"Original: [{string.Join(", ", testArray)}]");
            Debug.Log($"Processed: [{string.Join(", ", processed)}]");
            
            bool isValid = CppDLLWrapper.ValidateArrayData(processed, 5) == 1;
            Debug.Log("Array handling: " + (isValid ? "PASSED" : "FAILED"));
        }
        catch (Exception ex)
        {
            Debug.LogError($"Array handling failed: {ex.Message}");
        }
    }
    
    void TestCallbacks()
    {
        Debug.Log("--- Testing Callbacks (C++) ---");
        
        try
        {
            // Register Unity function as callback
            CppDLLWrapper.RegisterCallback(OnCppCallback);
            
            // Trigger the callback from C++ DLL
            CppDLLWrapper.TriggerCallback("Test message from Unity");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Callback test failed: {ex.Message}");
        }
    }
    
    void OnCppCallback(string message)
    {
        Debug.Log($"C++ Callback received: {message}");
        Debug.Log("Callbacks: PASSED");
    }
    
    void TestMicrosecondPerformance()
    {
        Debug.Log("--- C++ Microsecond Performance Testing ---");
        
        try
        {
            // Test single call overhead
            List<long> singleCallTimes = new List<long>();
            for (int i = 0; i < 100; i++)
            {
                long overhead = CppDLLWrapper.MeasureSingleCallOverhead();
                if (overhead > 0) // Only record non-zero measurements
                    singleCallTimes.Add(overhead);
            }
            
            if (singleCallTimes.Count > 0)
            {
                long avgSingleCall = (long)CalculateAverage(singleCallTimes);
                Debug.Log($"C++ Average single DLL call: {avgSingleCall} microseconds");
            }
            else
            {
                Debug.Log("C++ Single DLL calls: 0 microseconds (below measurement precision)");
            }
            
            // Test batch processing with microsecond precision
            int[] testSizes = {10, 50, 100, 200, 500};
            
            foreach (int size in testSizes)
            {
                List<long> batchTimes = new List<long>();
                
                for (int i = 0; i < 10; i++) // Multiple runs for accuracy
                {
                    long duration = CppDLLWrapper.MeasureBatchProcessingMicroseconds(size, 5);
                    batchTimes.Add(duration);
                }
                
                long avgBatch = (long)CalculateAverage(batchTimes);
                float microsecondsPerCharacter = avgBatch / (float)size;
                
                Debug.Log($"C++ {size} characters: {avgBatch}μs avg ({microsecondsPerCharacter:F2}μs per character)");
                
                // Test callback precision
                long callbackTime = CppDLLWrapper.MeasureCallbackStressMicroseconds(size);
                float microsecondsPerCallback = size > 0 ? callbackTime / (float)size : 0;
                Debug.Log($"C++ {size} callbacks: {callbackTime}μs ({microsecondsPerCallback:F2}μs per callback)");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Performance testing failed: {ex.Message}");
        }
    }
    
    void TestFrameworkSimulation()
    {
        Debug.Log("--- C++ Framework Simulation Testing ---");
        
        try
        {
            // Register handlers to simulate engine callbacks
            CppDLLWrapper.RegisterStateHandler(OnStateOperation);
            CppDLLWrapper.RegisterLogger(OnFrameworkLog);
            
            // Create test character array
            int[] testCharacters = new int[10];
            for (int i = 0; i < testCharacters.Length; i++)
            {
                testCharacters[i] = i + 1;
            }
            
            // Simulate framework execution cycle
            CppDLLWrapper.ProcessCharacterBatch(testCharacters, testCharacters.Length, 0.016f); // 60 FPS
            
            // Test interruption handling
            CppDLLWrapper.TriggerInterruption(5, new int[] { 1 }, 1);
            
            Debug.Log("Framework simulation: PASSED");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Framework simulation failed: {ex.Message}");
        }
    }
    
    // Simulate engine's state operation handler
    int OnStateOperation(int targetId, int stateKey, int operation, int value)
    {
        // In real implementation, this would interact with Unity's game objects
        return 1; // Always return success for testing
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
        
        Debug.Log($"{prefix} C++ Framework: {message}");
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
}