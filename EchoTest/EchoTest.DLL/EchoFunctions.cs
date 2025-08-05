using System;
using System.Diagnostics;

namespace EchoTest.DLL
{
    public static class EchoFunctions
    {
        private static Action<string> _registeredCallback;
        private static double _lastCalculationResult;

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
    }
}