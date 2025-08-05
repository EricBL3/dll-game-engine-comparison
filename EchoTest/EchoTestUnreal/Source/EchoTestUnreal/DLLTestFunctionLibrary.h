// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Kismet/BlueprintFunctionLibrary.h"
#include "DLLTestFunctionLibrary.generated.h"

/**
 * 
 */
UCLASS()
class ECHOTESTUNREAL_API UDLLTestFunctionLibrary : public UBlueprintFunctionLibrary
{
	GENERATED_BODY()
public:
	// Test basic DLL integration
	UFUNCTION(BlueprintCallable, Category = "DLL Test")
	static int32 TestAddIntegers(int32 A, int32 B);
    
	UFUNCTION(BlueprintCallable, Category = "DLL Test")
	static FString TestModifyString(const FString& Input);
    
	// Test performance
	UFUNCTION(BlueprintCallable, Category = "DLL Test")
	static int64 TestBatchProcessing(int32 CharacterCount, int32 Operations);
    
	// Test callbacks (more complex)
	UFUNCTION(BlueprintCallable, Category = "DLL Test")
	static void TestCallbackRegistration();

private:
	// DLL loading and function pointers
	static void* DLLHandle;
	static bool LoadDLL();
	static void UnloadDLL();
    
	// Function pointer typedefs
	typedef int32(*AddIntegersFunc)(int32, int32);
	typedef const char*(*ModifyStringFunc)(const char*);
	typedef int64(*BatchProcessingFunc)(int32, int32);
	
};
