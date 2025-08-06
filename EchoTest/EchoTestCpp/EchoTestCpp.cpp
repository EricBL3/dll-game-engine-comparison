//
//  EchoTestCpp.cpp
//  EchoTestCpp
//
//  Created by Eric Buitron on 2025-08-06.
//

#include "EchoTestCpp.h"
#include <chrono>
#include <cmath>
#include <cstring>
#include <string>
#include <memory>
#include <vector>
#include <iostream>

// Global callback storage
static StringCallbackFunc g_stringCallback = nullptr;
static StateOperationHandler g_stateHandler = nullptr;
static LoggingHandler g_logger = nullptr;

// Static string storage for return values
static std::string g_lastStringResult;

extern "C" {

    // Basic parameter passing tests
    ECHOTEST_API int AddIntegers(int a, int b)
    {
        return a + b;
    }
    
    ECHOTEST_API const char* ModifyString(const char* input)
    {
        g_lastStringResult = "Echo: ";
        if (input) {
            g_lastStringResult += input;
        }
        g_lastStringResult += " (processed by C++ DLL)";
        return g_lastStringResult.c_str();
    }
    
    // Array handling (matching your .NET ProcessIntArray)
    ECHOTEST_API void ProcessIntArray(int* input, int length, int* output)
    {
        if (!input || !output || length <= 0) return;
        
        for (int i = 0; i < length; i++) {
            output[i] = input[i] * 2; // Same logic as .NET version
        }
    }
    
    ECHOTEST_API int ValidateArrayData(int* data, int expectedLength)
    {
        if (!data) return 0;
        
        // Simple validation - in a real scenario you'd check content
        return 1; // Always valid for testing
    }
    
    // Callback pattern testing
    ECHOTEST_API void RegisterCallback(StringCallbackFunc callback)
    {
        g_stringCallback = callback;
    }
    
    ECHOTEST_API void TriggerCallback(const char* message)
    {
        if (g_stringCallback && message) {
            std::string callbackMessage = "Callback triggered: ";
            callbackMessage += message;
            g_stringCallback(callbackMessage.c_str());
        }
    }
    
    // Performance testing functions
    ECHOTEST_API void PerformHeavyCalculation(int iterations)
    {
        volatile double result = 0.0;
        for (int i = 0; i < iterations; i++) {
            result += std::sqrt(i) * std::sin(i);
        }
        // Prevent optimization
        (void)result;
    }
    
    ECHOTEST_API int64_t MeasureExecutionTimeMicroseconds(int iterations)
    {
        auto start = std::chrono::high_resolution_clock::now();
        PerformHeavyCalculation(iterations);
        auto end = std::chrono::high_resolution_clock::now();
        
        auto duration = std::chrono::duration_cast<std::chrono::microseconds>(end - start);
        return duration.count();
    }
    
    // High-precision timing (matching your .NET microsecond tests)
    ECHOTEST_API int64_t MeasureBatchProcessingMicroseconds(int characterCount, int operations)
    {
        auto start = std::chrono::high_resolution_clock::now();
        
        // Simulate the same processing as your .NET version
        for (int i = 0; i < characterCount; i++) {
            for (int j = 0; j < operations; j++) {
                // Mock memory lookup (matching your .NET mockMemory pattern)
                volatile int mockMemory[10] = {1, 5, 3, 8, 2, 9, 4, 7, 6, 0};
                volatile int memoryLookup = mockMemory[i % 10];
                
                // Mock state operations
                volatile int stateResult = (i * j) % 100;
                
                // Mock decision making
                volatile int decision = stateResult > 50 ? 1 : 0;
                
                // Prevent optimization
                (void)memoryLookup;
                (void)decision;
            }
        }
        
        auto end = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::microseconds>(end - start);
        return duration.count();
    }
    
    ECHOTEST_API int64_t MeasureSingleCallOverhead()
    {
        auto start = std::chrono::high_resolution_clock::now();
        // Minimal function call (equivalent to AddIntegers)
        volatile int result = AddIntegers(1, 1);
        auto end = std::chrono::high_resolution_clock::now();
        
        (void)result; // Prevent optimization
        auto duration = std::chrono::duration_cast<std::chrono::microseconds>(end - start);
        return duration.count();
    }
    
    ECHOTEST_API int64_t MeasureCallbackStressMicroseconds(int callbackCount)
    {
        if (!g_stringCallback) return 0;
        
        auto start = std::chrono::high_resolution_clock::now();
        
        for (int i = 0; i < callbackCount; i++) {
            std::string message = "Stress test callback " + std::to_string(i);
            g_stringCallback(message.c_str());
        }
        
        auto end = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::microseconds>(end - start);
        return duration.count();
    }
    
    // Framework simulation functions
    ECHOTEST_API void RegisterStateHandler(StateOperationHandler handler)
    {
        g_stateHandler = handler;
    }
    
    ECHOTEST_API void RegisterLogger(LoggingHandler logger)
    {
        g_logger = logger;
    }
    
    ECHOTEST_API void ProcessCharacterBatch(int* characterIds, int count, float deltaTime)
    {
        if (g_logger) {
            std::string logMessage = "Processing " + std::to_string(count) +
                                   " characters with deltaTime " + std::to_string(deltaTime);
            g_logger(logMessage.c_str(), 1); // Info level
        }
        
        // Simulate framework processing
        if (g_stateHandler && characterIds) {
            for (int i = 0; i < count; i++) {
                // Mock state operations (matching your .NET simulation)
                g_stateHandler(characterIds[i], 0, 4, 100); // SET operation
                g_stateHandler(characterIds[i], 1, 5, 1);   // INCREMENT
                g_stateHandler(characterIds[i], 2, 2, 50);  // CHECK operation
            }
        }
    }
    
    ECHOTEST_API void TriggerInterruption(int interruptionType, int* affectedCharacters, int count)
    {
        if (g_logger) {
            std::string logMessage = "Interruption " + std::to_string(interruptionType) +
                                   " affecting " + std::to_string(count) + " characters";
            g_logger(logMessage.c_str(), 2); // Warning level
        }
    }
    
    // Memory cleanup (important for cross-platform compatibility)
    ECHOTEST_API void FreeString(const char* str)
    {
        // In this implementation, we use static strings, so no cleanup needed
        // But this function provides the interface for future dynamic allocation
    }
}
