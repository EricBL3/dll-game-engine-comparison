
using EchoTest.DLL;
using UnityEngine;

public class DLLTester : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("=== Starting DLL Tests ===");
        
        TestBasicFunctions();
        TestArrayHandling();
        TestCallbacks();
        
        Debug.Log("=== DLL Tests Complete ===");
    }

    void TestBasicFunctions()
    {
        Debug.Log("--- Testing Basic Functions ---");
        
        // Test integer addition
        var result = EchoFunctions.AddIntegers(5, 3);
        Debug.Log($"AddIntegers(5, 3) = {result}");
        
        // Test string modification
        var stringResult = EchoFunctions.ModifyString("Hello World");
        Debug.Log($"ModifyString result: {stringResult}");
        
        Debug.Log("Basic functions: " + (result == 8 ? "PASSED" : "FAILED"));
    }
    
    void TestArrayHandling()
    {
        Debug.Log("--- Testing Array Handling ---");
        
        int[] testArray = {1, 2, 3, 4, 5};
        var processed = EchoFunctions.ProcessIntArray(testArray);
        
        Debug.Log($"Original: [{string.Join(", ", testArray)}]");
        Debug.Log($"Processed: [{string.Join(", ", processed)}]");
        
        bool isValid = EchoFunctions.ValidateArrayData(processed, 5);
        Debug.Log("Array handling: " + (isValid ? "PASSED" : "FAILED"));
    }
    
    void TestCallbacks()
    {
        Debug.Log("--- Testing Callbacks ---");
        
        // Register Unity function as callback
        EchoFunctions.RegisterCallback(OnDLLCallback);
        
        // Trigger the callback from DLL
        EchoFunctions.TriggerCallback("Test message");
    }
    
    void OnDLLCallback(string message)
    {
        Debug.Log($"Callback received: {message}");
        Debug.Log("Callbacks: PASSED");
    }
}
