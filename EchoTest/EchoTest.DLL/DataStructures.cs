using System.Runtime.InteropServices;

namespace EchoTest.DLL
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TestStateOperation
    {
        public int TargetId;
        public int StateKey;
        public int Operation;
        public int[] Values;
    }

    public struct TestEntity
    {
        public int EntityId;
        public int LocationId;
        public int StateCount;
    }

    public static class DataProcessor
    {
        public static bool ProcessStateOperation(TestStateOperation operation)
        {
            // simple validation for test
            return operation.TargetId >= 0 && operation.StateKey >= 0;
        }

        public static TestEntity[] GetEntitiesInLocation(int locationId)
        {
            // mock test entities
            return new TestEntity[]
            {
                new TestEntity { EntityId = 1, LocationId = locationId, StateCount = 3 },
                new TestEntity { EntityId = 2, LocationId = locationId, StateCount = 5 },
            };
        }
    }
}