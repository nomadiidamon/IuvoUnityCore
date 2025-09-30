using IuvoUnity.Interfaces;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace IuvoUnity
{
    namespace Animations
    {
        [System.Serializable]
        public class AnimationHandler : IAnimationHandler
        {
            Animator animator;

            public AnimationHandler(Animator animator)
            {
                this.animator = animator;
            }

            public void Play(string animationName)
            {
                animator.Play(animationName);
            }

            public void Play(AnimationClip clip)
            {
                if (clip == null) return;
                animator.Play(clip.name);
            }

            public void Stop()
            {
                animator.StopPlayback();
            }

            public void SetBlend(float blendTime)
            {
                // Blend handled by Animator Controller transitions or parameters.
                // Possibly implement crossfade
                animator.CrossFade(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, blendTime);
            }

            public bool IsPlaying(string animationName)
            {
                return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
            }

            public AnimationClip GetCurrentClip()
            {
                // Animator API doesn't expose current clip easily
                // User may track this externally or via AnimationClip array
                return null;
            }
        }


        public class LegacyAnimationHandler : IAnimationHandler
        {
            Animation animation;

            public LegacyAnimationHandler(Animation animation)
            {
                this.animation = animation;
            }

            public void Play(string animationName)
            {
                animation.Play(animationName);
            }

            public void Play(AnimationClip clip)
            {
                if (clip == null) return;
                animation.clip = clip;
                animation.Play();
            }

            public void Stop()
            {
                animation.Stop();
            }

            public void SetBlend(float blendTime)
            {
                // Legacy Animation supports crossfade
                animation.CrossFade(animation.clip.name, blendTime);
            }

            public bool IsPlaying(string animationName)
            {
                return animation.IsPlaying(animationName);
            }

            public AnimationClip GetCurrentClip()
            {
                return animation.clip;
            }
        }

        public class PlayablesHandler : IAnimationHandler
        {
            PlayableGraph graph;
            AnimationPlayableOutput output;
            AnimationClipPlayable clipPlayable;

            public PlayablesHandler(PlayableGraph graph, AnimationPlayableOutput output)
            {
                this.graph = graph;
                this.output = output;
            }

            public void Play(string animationName)
            {
                // Playables API usually works with AnimationClip, so you'd need a clip reference
                // This method might be less useful without clip
            }

            public void Play(AnimationClip clip)
            {
                if (clip == null) return;
                if (clipPlayable.IsValid())
                    clipPlayable.Destroy();

                clipPlayable = AnimationClipPlayable.Create(graph, clip);
                output.SetSourcePlayable(clipPlayable);
                graph.Play();
            }

            public void Stop()
            {
                graph.Stop();
            }

            public void SetBlend(float blendTime)
            {
                // Could implement blending by mixing multiple playables, but complex
            }

            public bool IsPlaying(string animationName)
            {
                // Not trivial, you'd need to track currently playing clip manually
                return false;
            }

            public AnimationClip GetCurrentClip()
            {
                if (!clipPlayable.IsValid()) return null;
                return clipPlayable.GetAnimationClip();
            }
        }


    }
}