using IuvoUnity.BaseClasses;
using IuvoUnity.Interfaces;


namespace IuvoUnity
{
    namespace DataStructs
    {
        public class BasicCondition<T> : IDataStructBase, IStateACondition where T : IDataStructBase, IStateACondition
        {
            private T Value;

            public BasicCondition(T value)
            {
                Value = value;
            }

            public virtual bool Evaluate()
            {
                return !string.IsNullOrEmpty(Value.ToString());
            }

            public virtual bool IsConditionMet()
            {
                return Evaluate();
            }
        }
    }
}