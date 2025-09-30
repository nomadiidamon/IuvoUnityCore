using System.Collections.Concurrent;
using System.Collections.Generic;
using IuvoUnity.Interfaces;

namespace IuvoUnity
{
    namespace DataStructs
    {
        public enum ContextKey_STATE
        { IsUnderAttack, IsVisible, IsMoving, IsGrounded, IsAiming, IsDead, IsPaused, IsSprinting }

        public class ConditionContext : IContext
        {
            private ConcurrentDictionary<ContextKey_STATE, bool> Conditions = new ConcurrentDictionary<ContextKey_STATE, bool>();

            public ConditionContext()
            {
                foreach (ContextKey_STATE key in System.Enum.GetValues(typeof(ContextKey_STATE)))
                {
                    Conditions[key] = false;
                }
            }

            public void Dispose()
            {
                Conditions.Clear();
            }

            private bool GetCondition(ContextKey_STATE key)
            {
                return Conditions.TryGetValue(key, out bool value) && value;
            }

            public bool GetIsUnderAttack() => GetCondition(ContextKey_STATE.IsUnderAttack);
            public bool GetIsVisible() => GetCondition(ContextKey_STATE.IsVisible);
            public bool GetIsMoving() => GetCondition(ContextKey_STATE.IsMoving);
            public bool GetIsGrounded() => GetCondition(ContextKey_STATE.IsGrounded);
            public bool GetIsAiming() => GetCondition(ContextKey_STATE.IsAiming);
            public bool GetIsDead() => GetCondition(ContextKey_STATE.IsDead);
            public bool GetIsPaused() => GetCondition(ContextKey_STATE.IsPaused);
            public bool GetIsSprinting() => GetCondition(ContextKey_STATE.IsSprinting);
            public Dictionary<ContextKey_STATE, bool> GetAllConditions()
            {
                return new Dictionary<ContextKey_STATE, bool>(Conditions);
            }


            private void SetCondition(ContextKey_STATE key, bool value)
            {
                Conditions[key] = value;
            }
            public void SetIsUnderAttack(bool value) => SetCondition(ContextKey_STATE.IsUnderAttack, value);
            public void SetIsVisible(bool value) => SetCondition(ContextKey_STATE.IsVisible, value);
            public void SetIsMoving(bool value) => SetCondition(ContextKey_STATE.IsMoving, value);
            public void SetIsGrounded(bool value) => SetCondition(ContextKey_STATE.IsGrounded, value);
            public void SetIsAiming(bool value) => SetCondition(ContextKey_STATE.IsAiming, value);
            public void SetIsDead(bool value) => SetCondition(ContextKey_STATE.IsDead, value);
            public void SetIsPaused(bool value) => SetCondition(ContextKey_STATE.IsPaused, value);
            public void SetIsSprinting(bool value) => SetCondition(ContextKey_STATE.IsSprinting, value);


        }

    }

}