using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor.Animations;
using IuvoUnity._Physics;
using IuvoUnity.Core;

namespace IuvoUnity
{
    namespace Debug
    {
        /// <summary> If you ever cant see the debug logs, make sure to call IuvoDebug.Initialize() in your main scene 
        /// and that the correct Validation Levels are set.</summary>
        public static class IuvoDebug
        {
            public enum ValidationLevel
            {
                Debug,
                Warning,
                Error
            }

            // Levels enabled by default
            public static HashSet<ValidationLevel> EnabledLevels = new HashSet<ValidationLevel>
            {
                ValidationLevel.Debug,
                ValidationLevel.Warning,
                ValidationLevel.Error
            };

            public static void EnableLevel(ValidationLevel level, bool enable)
            {
                if (enable) EnabledLevels.Add(level);
                else EnabledLevels.Remove(level);
            }

            private static string defaultLogFilePath = Path.Combine(Application.persistentDataPath, "IuvoDebugLog.txt");
            private static string logFilePath = defaultLogFilePath;

            // Thread-safe log queue
            private static readonly Queue<string> logQueue = new Queue<string>();
            private static readonly object fileLock = new object();
            private static bool coroutineStarted = false;

            public static void Initialize()
            {
                if (!coroutineStarted)
                {
                    // Create a persistent runner for coroutines
                    var runnerObj = new GameObject("IuvoDebugRunner");
                    UnityEngine.Object.DontDestroyOnLoad(runnerObj);
                    var runner = runnerObj.AddComponent<IuvoDebugRunner>();
                    runner.StartCoroutine(FlushLogQueueRoutine());

                    // Ensure final flush on quit
                    Application.quitting += FinalFlushOnQuit;

                    coroutineStarted = true;
                }
            }

            public static void SetLogFilePath(string path)
            {
                lock (fileLock)
                {
                    logFilePath = string.IsNullOrEmpty(path) ? defaultLogFilePath : path;
                }
            }

            private static void EnqueueLog(string message)
            {
                lock (fileLock)
                {
                    logQueue.Enqueue(message);
                }
            }

            private static System.Collections.IEnumerator FlushLogQueueRoutine()
            {
                while (true)
                {
                    lock (fileLock)
                    {
                        if (logQueue.Count > 0)
                        {
                            try
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                                File.AppendAllLines(logFilePath, logQueue);
                                logQueue.Clear(); // only clear on success
                            }
                            catch (Exception ex)
                            {
                                UnityEngine.Debug.LogError($"[IuvoDebug] Failed to write log to file: {ex}");
                            }
                        }
                    }
                    yield return new WaitForSecondsRealtime(1f); // batch write every 1 sec
                }
            }

            private static void FinalFlushOnQuit()
            {
                lock (fileLock)
                {
                    if (logQueue.Count > 0)
                    {
                        try
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                            File.AppendAllLines(logFilePath, logQueue);
                            logQueue.Clear();
                        }
                        catch (Exception ex)
                        {
                            UnityEngine.Debug.LogError($"[IuvoDebug] Final flush failed: {ex}");
                        }
                    }
                }
            }

            private static string FormatMessage(string level, string message, string memberName, string filePath, int lineNumber)
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string fileName = Path.GetFileName(filePath);
                return $"[{time}] [{level}] [{memberName} in {fileName}:{lineNumber}] {message}";
            }

            public static void DebugTransform(Transform toDebug, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;

                DebugPosition(toDebug.position, memberName, filePath, lineNumber);
                DebugRotation(toDebug.rotation, memberName, filePath, lineNumber);
                DebugEulerAngles(toDebug.eulerAngles, memberName, filePath, lineNumber);
                DebugScale(toDebug.localScale, memberName, filePath, lineNumber);
                DebugLossyScale(toDebug.lossyScale, memberName, filePath, lineNumber);
            }

            public static void DebugPosition(Vector3 position, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;
                DebugLog($"Position: {position:F3}", memberName, filePath, lineNumber);
            }

            public static void DebugRotation(Quaternion rotation, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;
                DebugLog($"Quaternion: {rotation.x:F3}, {rotation.y:F3}, {rotation.z:F3}, {rotation.w:F3}", memberName, filePath, lineNumber);
            }

            public static void DebugEulerAngles(Vector3 eulerAngles, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;
                DebugLog($"Angles: {eulerAngles:F3}", memberName, filePath, lineNumber);
            }

            public static void DebugScale(Vector3 scale, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;
                DebugLog($"Scale: {scale:F3}", memberName, filePath, lineNumber);
            }

            public static void DebugLossyScale(Vector3 lossyScale, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;
                DebugLog($"Lossy Scale: {lossyScale:F3}", memberName, filePath, lineNumber);
            }


            public static void DebugAnimator(Animator animator, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;
                if (animator == null)
                {
                    DebugLogWarning("Animator is null", memberName, filePath, lineNumber);
                    return;
                }
                DebugLog($"Animator: {animator.name}", memberName, filePath, lineNumber);


                string stateName = animator.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString("X8");
                if (string.IsNullOrEmpty(stateName))
                {
                    DebugLogWarning("Animator state name is empty", memberName, filePath, lineNumber);
                    return;
                }
                DebugLog($"Current Animator State: {stateName}", memberName, filePath, lineNumber);
                string clipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                if (string.IsNullOrEmpty(clipName))
                {
                    DebugLogWarning("Animator clip name is empty", memberName, filePath, lineNumber);
                    return;
                }
                DebugLog($"Current Animator Clip: {clipName}", memberName, filePath, lineNumber);


                if (animator.runtimeAnimatorController == null)
                {
                    DebugLogWarning("Animator does not have a runtime controller", memberName, filePath, lineNumber);
                    return;
                }
                AnimatorController controller = animator.runtimeAnimatorController as AnimatorController;
                if (controller == null)
                {
                    DebugLogWarning("Animator does not have a valid controller", memberName, filePath, lineNumber);
                    return;
                }
                DebugLog($"Animator Controller: {controller.name}", memberName, filePath, lineNumber);
            }

            public static bool RaycastDebug(Vector3 origin, Vector3 direction, out RaycastHit hit, float distance = Mathf.Infinity, 
                int layerMask = Physics.DefaultRaycastLayers, Color? debugColor = null, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug))
                {
                    hit = new RaycastHit();
                    return false;
                }

                return PhysicsHelpers.RaycastDebug(origin, direction, out hit, distance, layerMask, debugColor);
            }


            public static void DebugLogIuvoVersion([CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                string version = IuvoCore.GetVersion();
                DebugLog($"IuvoUnity Version: {version}", memberName, filePath, lineNumber);
            }

            public static void DebugLog(string message, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;
                string formatted = FormatMessage("DEBUG", message, memberName, filePath, lineNumber);
                UnityEngine.Debug.Log(formatted);
                EnqueueLog(formatted);
            }

            public static void DebugLogWarning(string message, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Warning)) return;
                string formatted = FormatMessage("WARNING", message, memberName, filePath, lineNumber);
                UnityEngine.Debug.LogWarning(formatted);
                EnqueueLog(formatted);
            }

            public static void DebugLogError(string message, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Error)) return;
                string formatted = FormatMessage("ERROR", message, memberName, filePath, lineNumber);
                UnityEngine.Debug.LogError(formatted);
                EnqueueLog(formatted);
            }

            // Internal runner to survive scene changes
            private class IuvoDebugRunner : MonoBehaviour { }
        }
    }
}
