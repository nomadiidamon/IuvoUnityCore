using System.Collections.Concurrent;
using System.Collections.Generic;
using IuvoUnity.Interfaces;

namespace IuvoUnity
{
    namespace DataStructs
    {
        public enum ContextKey_CONDITION
        { IsUnderAttack, IsVisible, IsMoving, IsGrounded, IsAiming, IsDead, IsPaused, IsSprinting }

        public class ConditionContext : IContext
        {
            private ConcurrentDictionary<ContextKey_CONDITION, bool> Conditions = new ConcurrentDictionary<ContextKey_CONDITION, bool>();

            public ConditionContext()
            {
                foreach (ContextKey_CONDITION key in System.Enum.GetValues(typeof(ContextKey_CONDITION)))
                {
                    Conditions[key] = false;
                }
            }

            public void Dispose()
            {
                Conditions.Clear();
            }

            private bool GetCondition(ContextKey_CONDITION key)
            {
                return Conditions.TryGetValue(key, out bool value) && value;
            }

            public bool GetIsUnderAttack() => GetCondition(ContextKey_CONDITION.IsUnderAttack);
            public bool GetIsVisible() => GetCondition(ContextKey_CONDITION.IsVisible);
            public bool GetIsMoving() => GetCondition(ContextKey_CONDITION.IsMoving);
            public bool GetIsGrounded() => GetCondition(ContextKey_CONDITION.IsGrounded);
            public bool GetIsAiming() => GetCondition(ContextKey_CONDITION.IsAiming);
            public bool GetIsDead() => GetCondition(ContextKey_CONDITION.IsDead);
            public bool GetIsPaused() => GetCondition(ContextKey_CONDITION.IsPaused);
            public bool GetIsSprinting() => GetCondition(ContextKey_CONDITION.IsSprinting);
            public Dictionary<ContextKey_CONDITION, bool> GetAllConditions()
            {
                return new Dictionary<ContextKey_CONDITION, bool>(Conditions);
            }


            private void SetCondition(ContextKey_CONDITION key, bool value)
            {
                Conditions[key] = value;
            }
            public void SetIsUnderAttack(bool value) => SetCondition(ContextKey_CONDITION.IsUnderAttack, value);
            public void SetIsVisible(bool value) => SetCondition(ContextKey_CONDITION.IsVisible, value);
            public void SetIsMoving(bool value) => SetCondition(ContextKey_CONDITION.IsMoving, value);
            public void SetIsGrounded(bool value) => SetCondition(ContextKey_CONDITION.IsGrounded, value);
            public void SetIsAiming(bool value) => SetCondition(ContextKey_CONDITION.IsAiming, value);
            public void SetIsDead(bool value) => SetCondition(ContextKey_CONDITION.IsDead, value);
            public void SetIsPaused(bool value) => SetCondition(ContextKey_CONDITION.IsPaused, value);
            public void SetIsSprinting(bool value) => SetCondition(ContextKey_CONDITION.IsSprinting, value);


        }

    }

}