
using IuvoUnity.BaseClasses;
using UnityEngine;

namespace IuvoUnity
{
    namespace Interfaces
    {
        public interface IAnimationHandler : IuvoInterfaceBase
        {
            void Play(string animationName);
            void Play(AnimationClip clip);
            void Stop();
            void SetBlend(float blendTime);
            bool IsPlaying(string animationName);
            AnimationClip GetCurrentClip();
        }
    }
}