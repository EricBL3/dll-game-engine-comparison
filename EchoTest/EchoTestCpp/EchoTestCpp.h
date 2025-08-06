//
//  EchoTestCpp.h
//  EchoTestCpp
//
//  Created by Eric Buitron on 2025-08-06.
//

#pragma once

// Cross-platform export macros
#ifdef _WIN32
    #ifdef ECHOTESTCPP_EXPORTS
        #define ECHOTEST_API __declspec(dllexport)
    #else
        #define ECHOTEST_API __declspec(dllimport)
    #endif
#else
    #define ECHOTEST_API __attribute__((visibility("default")))
#endif

#include <cstdint>

// Extern C for clean symbol export
extern "C" {
    // Basic parameter passing tests (matching your .NET version)
    ECHOTEST_API int AddIntegers(int a, int b);
    ECHOTEST_API const char* ModifyString(const char* input);
    
    // Array handling
    ECHOTEST_API void ProcessIntArray(int* input, int length, int* output);
    ECHOTEST_API int ValidateArrayData(int* data, int expectedLength);
    
    // Callback pattern testing
    typedef void (*StringCallbackFunc)(const char* message);
    ECHOTEST_API void RegisterCallback(StringCallbackFunc callback);
    ECHOTEST_API void TriggerCallback(const char* message);
    
    // Performance testing functions
    ECHOTEST_API void PerformHeavyCalculation(int iterations);
    ECHOTEST_API int64_t MeasureExecutionTimeMicroseconds(int iterations);
    
    // High-precision timing (matching your .NET microsecond tests)
    ECHOTEST_API int64_t MeasureBatchProcessingMicroseconds(int characterCount, int operations);
    ECHOTEST_API int64_t MeasureSingleCallOverhead();
    ECHOTEST_API int64_t MeasureCallbackStressMicroseconds(int callbackCount);
    
    // Framework simulation (matching FrameworkInterfaces.cs)
    typedef int (*StateOperationHandler)(int targetId, int stateKey, int operation, int value);
    typedef void (*LoggingHandler)(const char* message, int severity);
    
    ECHOTEST_API void RegisterStateHandler(StateOperationHandler handler);
    ECHOTEST_API void RegisterLogger(LoggingHandler logger);
    ECHOTEST_API void ProcessCharacterBatch(int* characterIds, int count, float deltaTime);
    ECHOTEST_API void TriggerInterruption(int interruptionType, int* affectedCharacters, int count);
    
    // Memory cleanup
    ECHOTEST_API void FreeString(const char* str);
}
