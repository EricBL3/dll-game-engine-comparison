namespace EchoTest.DLL
{
    public delegate bool StateOperationHandler(int targetId, int stateKey, int operation, int[] values);
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
                    _stateHandler(characterId, 0, 4, new []{100});
                }
            }
        }

        public static void TriggerInterruption(int interruptionType, int[] affectedCharacters)
        {
            _logger?.Invoke($"Interruption {interruptionType} affecting {affectedCharacters?.Length ?? 0} characters", 2);
        }
    }
}