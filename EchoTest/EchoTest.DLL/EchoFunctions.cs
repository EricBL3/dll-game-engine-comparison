using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EchoTest.DLL
{
    // Add COM wrapper class
    [ComVisible(true)]
    [Guid("c3a21856-68d7-4e08-a3f9-28a99bb7fc89")] // Generate a real GUID
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class EchoFunctionsCOM
    {
        public int AddIntegers(int a, int b)
        {
            return EchoFunctions.AddIntegers(a, b);
        }
    
        public string ModifyString(string input)
        {
            return EchoFunctions.ModifyString(input);
        }
    
        public long MeasureBatchProcessingMicroseconds(int characterCount, int operations)
        {
            return EchoFunctions.MeasureBatchProcessingMicroseconds(characterCount, operations);
        }
    }
    
    public static class EchoFunctions
    {
        private static Action<string> _registeredCallback;
        private static double _lastCalculationResult;
        
        private static int[] _mockMemory = {1, 5, 3, 8, 2, 9, 4, 7, 6, 0};

        // Test parameter passing
        public static int AddIntegers(int a, int b)
        {
            return a + b;
        }

        public static string ModifyString(string input)
        {
            return $"Echo: {input} (processed by DLL)";
        }

        // Test array handling
        public static int[] ProcessIntArray(int[] input)
        {
            if(input == null)
                return new int[0];
            
            var result = new int[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                result[i] = input[i] * 2;
            }
            
            return result;
        }

        public static bool ValidateArrayData(int[] data, int expectedLength)
        {
            return data != null && data.Length == expectedLength;
        }

        // Test callback pattern
        public static void RegisterCallback(Action<string> callback)
        {
            _registeredCallback = callback;
        }

        public static void TriggerCallback(string message)
        {
            _registeredCallback?.Invoke($"Callback triggered: {message}");
        }

        // Test performance
        public static void PerformHeavyCalculation(int iterations)
        {
            double result = 0;
            for (int i = 0; i < iterations; i++)
            {
                result += Math.Sqrt(i) * Math.Sin(i);
            }

            _lastCalculationResult = result;
        }

        public static long MeasureExecutionTime(Action operation)
        {
            var stopwatch = Stopwatch.StartNew();
            operation?.Invoke();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        
        public static void ProcessCharacterBatch(int characterCount, int operationsPerCharacter)
        {
            for (int i = 0; i < characterCount; i++)
            {
                for (int j = 0; j < operationsPerCharacter; j++)
                {
                    // Simulate memory lookups (like your transition memory)
                    var memoryLookup = FindInArray(_mockMemory, i % 10);
                
                    // Simulate state operations
                    var stateResult = (i * j) % 100;
                
                    // Simulate decision making
                    var decision = stateResult > 50 ? 1 : 0;
                }
            }
        }
        
        public static long MeasureBatchProcessing(int characterCount, int operations)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            ProcessCharacterBatch(characterCount, operations);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        
        private static int FindInArray(int[] array, int target)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target) return i;
            }
            return -1;
        }
    
        // Test concurrent callback handling
        public static void TestCallbackStress(int callbackCount)
        {
            for (int i = 0; i < callbackCount; i++)
            {
                _registeredCallback?.Invoke($"Stress test callback {i}");
            }
        }
        
        // High-precision timing using Stopwatch ticks
        public static long MeasureBatchProcessingMicroseconds(int characterCount, int operations)
        {
            var stopwatch = Stopwatch.StartNew();
            ProcessCharacterBatch(characterCount, operations);
            stopwatch.Stop();
        
            // Convert ticks to microseconds
            return (stopwatch.ElapsedTicks * 1000000) / Stopwatch.Frequency;
        }
    
        public static long MeasureCallbackStressMicroseconds(int callbackCount)
        {
            var stopwatch = Stopwatch.StartNew();
            TestCallbackStress(callbackCount);
            stopwatch.Stop();
        
            return (stopwatch.ElapsedTicks * 1000000) / Stopwatch.Frequency;
        }
    
        // Also useful: measure single DLL call overhead
        public static long MeasureSingleCallOverhead()
        {
            var stopwatch = Stopwatch.StartNew();
            // Minimal DLL function call
            AddIntegers(1, 1);
            stopwatch.Stop();
        
            return (stopwatch.ElapsedTicks * 1000000) / Stopwatch.Frequency;
        }
    }
}