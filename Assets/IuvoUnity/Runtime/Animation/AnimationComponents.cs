using System.Collections.Generic;
using UnityEngine;
using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace _DataStructs
    {
        public class AnimationStateData : IDataStructBase
        {
            public int HashID { get; private set; }
            public string Name;

            public AnimationStateData()
            {
                Name = "animName";
                HashID = Animator.StringToHash(Name);
            }

            public AnimationStateData(string name)
            {
                Name = name;
                HashID = Animator.StringToHash(name);
            }
        }

        public class AnimationBlendData : IDataStructBase
        {
            public float _blendSpeed;
            public float _blendTime;

            public AnimationBlendData()
            {
                _blendSpeed = 1.0f;
                _blendTime = 0.0f;
            }

            public AnimationBlendData(float blendSpeed, float blendTime)
            {
                _blendSpeed = blendSpeed;
                _blendTime = blendTime;
            }
        }

        public class AnimationTransitionData : IDataStructBase
        {
            public float _transitionSpeed;
            public float _transitionTime;

            public AnimationTransitionData()
            {
                _transitionSpeed = 1.0f;
                _transitionTime = 0.0f;
            }

            public AnimationTransitionData(float transitionSpeed, float transitionTime)
            {
                _transitionSpeed = transitionSpeed;
                _transitionTime = transitionTime;
            }
        }

        public class AnimationClipData : IDataStructBase
        {
            public AnimationClip _animationClip { get; set; }

            public AnimationClipData()
            {
                _animationClip = null;
            }

            public AnimationClipData(AnimationClip clip)
            {
                _animationClip = clip;
            }

        }

        public class AnimationClipsData : IDataStructBase
        {
            public List<AnimationClipData> _clips;

            public AnimationClipsData()
            {
                _clips = new List<AnimationClipData>();
            }

            public AnimationClipsData(List<AnimationClipData> clips)
            {
                _clips = clips ?? new List<AnimationClipData>();
            }
        }


        /// TODO: need to find a way to have a flexible event for this. Loss of QOL without
        public class AnimationEventData : IDataStructBase
        {
            public AnimationEvent _unityAnimEvent { get; set; }
            public System.Action _nativeAnimEvent {  get; set; }

            public AnimationEventData()
            {
                _unityAnimEvent = new AnimationEvent();
                _nativeAnimEvent = null;
            }

            public AnimationEventData(AnimationEvent unityEvent, System.Action nativeEvent)
            {
                _unityAnimEvent = unityEvent;
                _nativeAnimEvent = nativeEvent;
            }
        }

    }
}