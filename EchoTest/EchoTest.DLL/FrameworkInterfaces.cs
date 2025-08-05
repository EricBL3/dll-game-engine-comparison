namespace EchoTest.DLL
{
    public delegate bool StateOperationHandler(int targetId, int stateKey, int operation, int value);
    public delegate void LoggingHandler(string message, int severity);
    
    public static class FrameworkSimulator
    {
        private static StateOperationHandler _stateHandler;
        private static LoggingHandler _logger;

        public static void RegisterStateHandler(StateOperationHandler handler)
        {
            _stateHandler = handler;
        }

        public static void RegisterLogger(LoggingHandler logger)
        {
            _logger = logger;
        }

        public static void ProcessCharacterBatch(int[] characterIds, float deltaTime)
        {
            _logger?.Invoke($"Processing {characterIds?.Length ?? 0} characters with deltaTime {deltaTime}", 1);

            if (_stateHandler != null && characterIds != null)
            {
                foreach (var characterId in characterIds)
                {
                    _stateHandler(characterId, 0, 4, 100);
                }
            }
        }

        public static void TriggerInterruption(int interruptionType, int[] affectedCharacters)
        {
            _logger?.Invoke($"Interruption {interruptionType} affecting {affectedCharacters?.Length ?? 0} characters", 2);
        }
        
        // Simulate your framework's core execution cycle
        public static void ExecuteFrameworkCycle(int[] characterIds, float deltaTime)
        {
            _logger?.Invoke($"Starting framework cycle for {characterIds?.Length} characters", 1);
        
            if (characterIds == null) return;
        
            foreach (var charId in characterIds)
            {
                // Simulate sequence execution decision making
                SimulateSequenceExecution(charId);
            
                // Simulate memory-based selection
                SimulateMemorySelection(charId);
            
                // Simulate state operations
                SimulateStateOperations(charId);
            }
        
            _logger?.Invoke("Framework cycle complete", 1);
        }
        
        private static void SimulateSequenceExecution(int characterId)
        {
            // Mock your sequence execution process
            var currentNode = characterId % 5; // Mock current node
            var availableTransitions = 3; // Mock available transitions
        
            // Simulate transition selection with memory
            for (int i = 0; i < availableTransitions; i++)
            {
                var transitionValid = _stateHandler?.Invoke(characterId, i, 0, currentNode) ?? false;
                if (transitionValid)
                {
                    _logger?.Invoke($"Character {characterId} selected transition {i}", 0);
                    break;
                }
            }
        }
        
        private static void SimulateMemorySelection(int characterId)
        {
            // Simulate your memory-driven selection logic
            var entityOptions = new int[] { 1, 2, 3, 4, 5 };
            var selectedEntity = entityOptions[characterId % entityOptions.Length];
        
            _stateHandler?.Invoke(characterId, 10, 4, selectedEntity); // Mock memory update
        }
    
        private static void SimulateStateOperations(int characterId)
        {
            // Simulate multiple state operations per character
            _stateHandler?.Invoke(characterId, 0, 4, 100); // SET energy
            _stateHandler?.Invoke(characterId, 1, 5, 1);   // INCREMENT action_count
            _stateHandler?.Invoke(characterId, 2, 2, 50);  // CHECK mood > 50
        }
        
        // Test interruption handling
        public static bool SimulateInterruption(int characterId, int interruptionType)
        {
            _logger?.Invoke($"Processing interruption {interruptionType} for character {characterId}", 2);
        
            // Simulate interruption context saving
            var contextSaved = _stateHandler?.Invoke(characterId, 100, 4, interruptionType) ?? false;
        
            if (contextSaved)
            {
                _logger?.Invoke($"Interruption context saved for character {characterId}", 1);
                return true;
            }
        
            return false;
        }
    }
}