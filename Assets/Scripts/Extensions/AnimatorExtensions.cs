using UnityEngine;

namespace IuvoUnity
{
    namespace _Extensions
    {
        public static class AnimatorExtensions
        {
            /// <summary>
            /// Sets the speed of the animator.
            /// </summary>
            /// <param name="animator">The Animator component to modify.</param>
            /// <param name="speed">The speed value to set.</param>
            public static void SetSpeed(this Animator animator, float speed)
            {
                animator.speed = speed;
            }

            /// <summary>
            /// Gets the speed of the animator.
            /// </summary>
            /// <param name="animator">The Animator component to query.</param>
            /// <returns>The current speed of the animator.</returns>
            public static float GetSpeed(this Animator animator)
            {
                return animator.speed;
            }

            /// <summary>
            /// Checks if the specified animation is currently playing.
            /// </summary>
            /// <param name="animator">The Animator component to query.</param>
            /// <param name="animationName">The name of the animation to check.</param>
            /// <returns>True if the animation is currently playing, otherwise false.</returns>
            public static bool IsPlaying(this Animator animator, string animationName)
            {
                return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
            }

            /// <summary>
            /// Gets the current normalized time of the animation.
            /// </summary>
            /// <param name="animator">The Animator component to query.</param>
            /// <returns>The normalized time of the current animation.</returns>
            public static float GetCurrentNormalizedTime(this Animator animator)
            {
                return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            }

            /// <summary>
            /// Gets the length of the current animation.
            /// </summary>
            /// <param name="animator">The Animator component to query.</param>
            /// <returns>The length of the current animation.</returns>
            public static float GetCurrentAnimationLength(this Animator animator)
            {
                return animator.GetCurrentAnimatorStateInfo(0).length;
            }

            /// <summary>
            /// Resets all parameters in the animator to their default values.
            /// </summary>
            /// <param name="animator">The Animator component to modify.</param>
            public static void ResetAllParameters(this Animator animator)
            {
                foreach (AnimatorControllerParameter param in animator.parameters)
                {
                    switch (param.type)
                    {
                        case AnimatorControllerParameterType.Bool:
                            animator.SetBool(param.name, false);
                            break;
                        case AnimatorControllerParameterType.Float:
                            animator.SetFloat(param.name, 0f);
                            break;
                        case AnimatorControllerParameterType.Int:
                            animator.SetInteger(param.name, 0);
                            break;
                        case AnimatorControllerParameterType.Trigger:
                            animator.ResetTrigger(param.name);
                            break;
                    }
                }
            }


            /// <summary>
            /// Checks if the current animation is looping.
            /// </summary>
            /// <param name="animator">The Animator component to query.</param>
            /// <returns>True if the current animation is looping, otherwise false.</returns>
            public static bool IsAnimationLooping(this Animator animator)
            {
                return animator.GetCurrentAnimatorStateInfo(0).loop;
            }


            /// <summary>
            /// Sets the time scale for the animator.
            /// </summary>
            /// <param name="animator">The Animator component to modify.</param>
            /// <param name="timeScale">The time scale to set for the animator.</param>
            public static void SetAnimationTimeScale(this Animator animator, float timeScale)
            {
                animator.speed = timeScale;
            }

            /// <summary>
            /// Gets the total time of all animations in the animator.
            /// </summary>
            /// <param name="animator">The Animator component to query.</param>
            /// <returns>The total time of all animations in the animator.</returns>
            public static float GetTotalAnimationTime(this Animator animator)
            {
                float totalTime = 0f;
                foreach (var state in animator.GetCurrentAnimatorClipInfo(0))
                {
                    totalTime += state.clip.length;
                }
                return totalTime;
            }

            /// <summary>
            /// Pauses all animations in the animator.
            /// </summary>
            /// <param name="animator">The Animator component to modify.</param>
            public static void PauseAnimations(this Animator animator)
            {
                animator.speed = 0;
            }

            /// <summary>
            /// Resumes all animations in the animator.
            /// </summary>
            /// <param name="animator">The Animator component to modify.</param>
            public static void ResumeAnimations(this Animator animator)
            {
                animator.speed = 1;
            }

        }





    }
}