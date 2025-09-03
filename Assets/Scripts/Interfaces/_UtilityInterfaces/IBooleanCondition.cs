using System;

namespace IuvoUnity
{
    namespace Interfaces
    {
            public interface IBooleanCondition : IConditional
            {
                bool Evaluate();
            }
        
    }
}