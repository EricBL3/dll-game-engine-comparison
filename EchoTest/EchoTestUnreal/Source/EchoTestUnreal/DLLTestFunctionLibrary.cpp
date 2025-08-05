// Fill out your copyright notice in the Description page of Project Settings.


#include "DLLTestFunctionLibrary.h"
#include "Engine/Engine.h"

#if PLATFORM_WINDOWS
	#include "Windows/WindowsHWrapper.h"
	#include "Windows/AllowWindowsPlatformTypes.h"
	#include <windows.h>
	#include "Windows/HideWindowsPlatformTypes.h"
#elif PLATFORM_MAC
	#include <dlfcn.h>
#endif

void* UDLLTestFunctionLibrary::DLLHandle = nullptr;

bool UDLLTestFunctionLibrary::LoadDLL()
{
	if (DLLHandle != nullptr)
		return true;

#if PLATFORM_WINDOWS
		FString DLLPath = FPaths::ProjectDir() / TEXT("Binaries/Win64/EchoTest.DLL.dll");
		DLLHandle = FPlatformProcess::GetDllHandle(*DLLPath);
#elif PLATFORM_MAC
		FString DLLPath = FPaths::ProjectDir() / TEXT("Binaries/Mac/EchoTest.DLL.dll");
		DLLHandle = FPlatformProcess::GetDllHandle(*DLLPath);
#endif

	if (DLLHandle == nullptr)
	{
		UE_LOG(LogTemp, Error, TEXT("Failed to load DLL"));
		return false;
	}

	UE_LOG(LogTemp, Warning, TEXT("DLL loaded successfully"));
	return true;
}

int32 UDLLTestFunctionLibrary::TestAddIntegers(int32 A, int32 B)
{
	if (!LoadDLL())
		return -1;

	// This is where the complexity lies - calling .NET functions from C++
	// We'll need to use COM interop or similar
	UE_LOG(LogTemp, Warning, TEXT("TestAddIntegers called with %d, %d"), A, B);
    
	// For now, return a test value to verify the wrapper works
	return A + B + 100; // +100 to show it's coming from Unreal wrapper
}

FString UDLLTestFunctionLibrary::TestModifyString(const FString& Input)
{
	if (!LoadDLL())
		return TEXT("DLL Load Failed");

	UE_LOG(LogTemp, Warning, TEXT("TestModifyString called with: %s"), *Input);
    
	// Test implementation
	return FString::Printf(TEXT("Unreal processed: %s"), *Input);
}

int64 UDLLTestFunctionLibrary::TestBatchProcessing(int32 CharacterCount, int32 Operations)
{
	if (!LoadDLL())
		return -1;

	// Simulate timing test
	double StartTime = FPlatformTime::Seconds();
    
	// Mock processing
	for (int32 i = 0; i < CharacterCount * Operations; i++)
	{
		volatile int32 dummy = i * 2; // Prevent optimization
	}
    
	double EndTime = FPlatformTime::Seconds();
	int64 Microseconds = (EndTime - StartTime) * 1000000;
    
	return Microseconds;
}

void UDLLTestFunctionLibrary::TestCallbackRegistration()
{
	UE_LOG(LogTemp, Warning, TEXT("Callback registration test"));
}