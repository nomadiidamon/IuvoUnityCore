using IuvoUnity.Interfaces;
using IuvoUnity.DataStructs;
using UnityEngine;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        [System.Serializable]
        public class IuvoAnimation : IDataStructBase
        {
            public AnimationStateData stateData;
            public AnimationBlendData blendData;
            public AnimationTransitionData transitionData;
            public AnimationClipsData animClips;
            public AnimationEventData eventData;

            IAnimationHandler _handler;

            public IuvoAnimation()
            {
                stateData = new AnimationStateData();
                blendData = new AnimationBlendData();
                transitionData = new AnimationTransitionData();
                animClips = new AnimationClipsData();
                eventData = new AnimationEventData();
            }

            public IuvoAnimation(AnimationStateData _stateData, AnimationBlendData _blendData,
                AnimationTransitionData _transitionData, AnimationClipsData _animClips,
                AnimationEventData _eventData)
            {
                stateData = _stateData;
                blendData = _blendData;
                transitionData = _transitionData;
                animClips = _animClips;
                eventData = _eventData;
            }

            public void SetAnimationHandler(IAnimationHandler handler)
            {
                _handler = handler;
            }

            public void Play(string animationName)
            {
                _handler?.Play(animationName);
            }

            public void Play(AnimationClip clip)
            {
                _handler?.Play(clip);
            }

            public void Stop()
            {
                _handler?.Stop();
            }

            public void SetBlend(float blendTime)
            {
                _handler?.SetBlend(blendTime);
            }

            public bool IsPlaying(string animationName)
            {
                return _handler != null && _handler.IsPlaying(animationName);
            }

            public AnimationClip GetCurrentClip()
            {
                return _handler?.GetCurrentClip();
            }



        }
    }
}
