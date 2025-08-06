using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class CppDLLWrapper
{
    // Platform-specific DLL names
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
    private const string DLL_NAME = "EchoTestCpp";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    private const string DLL_NAME = "EchoTestCpp";
#else
    private const string DLL_NAME = "EchoTestCpp";
#endif

    // Basic parameter passing tests
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddIntegers(int a, int b);
    
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr ModifyString([MarshalAs(UnmanagedType.LPStr)] string input);
    
    // Array handling
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ProcessIntArray(int[] input, int length, int[] output);
    
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ValidateArrayData(int[] data, int expectedLength);
    
    // Callback pattern testing
    public delegate void StringCallbackDelegate([MarshalAs(UnmanagedType.LPStr)] string message);
    
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern void RegisterCallback(StringCallbackDelegate callback);
    
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TriggerCallback([MarshalAs(UnmanagedType.LPStr)] string message);
    
    // Performance testing
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern void PerformHeavyCalculation(int iterations);
    
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern long MeasureExecutionTimeMicroseconds(int iterations);
    
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern long MeasureBatchProcessingMicroseconds(int characterCount, int operations);
    
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern long MeasureSingleCallOverhead();
    
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern long MeasureCallbackStressMicroseconds(int callbackCount);
    
    // Framework simulation
    public delegate int StateOperationDelegate(int targetId, int stateKey, int operation, int value);
    public delegate void LoggingDelegate([MarshalAs(UnmanagedType.LPStr)] string message, int severity);
    
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern void RegisterStateHandler(StateOperationDelegate handler);
    
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern void RegisterLogger(LoggingDelegate logger);
    
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ProcessCharacterBatch(int[] characterIds, int count, float deltaTime);
    
    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern void TriggerInterruption(int interruptionType, int[] affectedCharacters, int count);
    
    // Helper method for string marshaling
    public static string GetStringFromPtr(IntPtr ptr)
    {
        if (ptr == IntPtr.Zero)
            return string.Empty;
            
        return Marshal.PtrToStringAnsi(ptr);
    }
}