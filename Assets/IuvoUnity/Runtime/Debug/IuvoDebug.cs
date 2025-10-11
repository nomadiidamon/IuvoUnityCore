using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using IuvoUnity.IuvoPhysics;
using IuvoUnity.Core;
using IuvoUnity.Constants;

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
            public static void EnableValidationLevel(ValidationLevel level, bool enable)
            {
                if (enable) EnabledLevels.Add(level);
                else EnabledLevels.Remove(level);
            }
            public static void Initialize()
            {
                if (!coroutineStarted)
                {
                    // Create a persistent runner for coroutines
                    var runnerObj = new GameObject("IuvoDebugRunner");
                    UnityEngine.Object.DontDestroyOnLoad(runnerObj);
                    var runner = runnerObj.AddComponent<IuvoDebugRunner>();
                    runner.StartCoroutine(FlushPersistentLogQueueRoutine());
                    runner.StartCoroutine(FlushEditorLogQueueRoutine());
                    runner.StartCoroutine(FlushCustomLogQueueRoutine());

                    // Ensure final flush on quit
                    Application.quitting += FinalFlushOnQuitPersistentLog;
                    Application.quitting += FinalFlushOnQuitEditorLog;
                    Application.quitting += FinalFlushOnQuitCustomLog;

                    coroutineStarted = true;
                }
            }

            #region File Logging
            public enum LogDestination
            {
                PersistentFile,
                EditorFile,
                CustomFile
            }
            public static HashSet<LogDestination> EnabledDestinations = new HashSet<LogDestination>
            {
                LogDestination.PersistentFile,
                LogDestination.EditorFile,
                LogDestination.CustomFile
            };
            private static string persistentLogFilePath = Path.Combine(Application.persistentDataPath, "IuvoDebugLog_Persistent.txt");
            private static string editorLogFilePath = Path.Combine(Application.dataPath, "IuvoDebugLog_Editor.txt");
            private static string customLogFilePath = "";

            // Thread-safe log queue
            private static readonly Queue<string> persistentLogQueue = new Queue<string>();
            private static readonly Queue<string> editorLogQueue = new Queue<string>();
            private static readonly Queue<string> customLogQueue = new Queue<string>();
            private static readonly object fileLock = new object();
            private static bool coroutineStarted = false;

            public static void SetLogFilePath(string path)
            {
                lock (fileLock)
                {
                    customLogFilePath = string.IsNullOrEmpty(path) ? persistentLogFilePath : path;
                }
            }

            private static void EnqueueLog(string message)
            {
                lock (fileLock)
                {
                    if (EnabledDestinations.Contains(LogDestination.CustomFile) && !string.IsNullOrEmpty(customLogFilePath))
                    {
                        customLogQueue.Enqueue(message);
                    }

                    if (EnabledDestinations.Contains(LogDestination.PersistentFile))
                    {
                        persistentLogQueue.Enqueue(message);
                    }

                    if (EnabledDestinations.Contains(LogDestination.EditorFile))
                    {
                        editorLogQueue.Enqueue(message);
                    }
                }
            }

            private static System.Collections.IEnumerator FlushPersistentLogQueueRoutine()
            {
                while (true)
                {
                    lock (fileLock)
                    {
                        if (persistentLogQueue.Count > 0)
                        {
                            try
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(persistentLogFilePath));
                                File.AppendAllLines(persistentLogFilePath, persistentLogQueue);
                                persistentLogQueue.Clear(); // only clear on success
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

            private static System.Collections.IEnumerator FlushEditorLogQueueRoutine()
            {
                while (true)
                {
                    lock (fileLock)
                    {
                        if (editorLogQueue.Count > 0)
                        {
                            try
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(editorLogFilePath));
                                File.AppendAllLines(editorLogFilePath, editorLogQueue);
                                editorLogQueue.Clear(); // only clear on success
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

            private static System.Collections.IEnumerator FlushCustomLogQueueRoutine()
            {
                while (true)
                {
                    lock (fileLock)
                    {
                        if (editorLogQueue.Count > 0)
                        {
                            try
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(customLogFilePath));
                                File.AppendAllLines(customLogFilePath, customLogQueue);
                                customLogQueue.Clear(); // only clear on success
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


            private static void FinalFlushOnQuitPersistentLog()
            {
                lock (fileLock)
                {
                    if (persistentLogQueue.Count > 0)
                    {
                        try
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(persistentLogFilePath));
                            File.AppendAllLines(persistentLogFilePath, persistentLogQueue);
                            persistentLogQueue.Clear();
                        }
                        catch (Exception ex)
                        {
                            UnityEngine.Debug.LogError($"[IuvoDebug] Final flush failed: {ex}");
                        }
                    }
                }
            }

            private static void FinalFlushOnQuitEditorLog()
            {
                lock (fileLock)
                {
                    if (editorLogQueue.Count > 0)
                    {
                        try
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(editorLogFilePath));
                            File.AppendAllLines(editorLogFilePath, editorLogQueue);
                            editorLogQueue.Clear();
                        }
                        catch (Exception ex)
                        {
                            UnityEngine.Debug.LogError($"[IuvoDebug] Final flush failed: {ex}");
                        }
                    }
                }
            }

            private static void FinalFlushOnQuitCustomLog()
            {
                lock (fileLock)
                {
                    if (editorLogQueue.Count > 0)
                    {
                        try
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(customLogFilePath));
                            File.AppendAllLines(customLogFilePath, customLogQueue);
                            customLogQueue.Clear();
                        }
                        catch (Exception ex)
                        {
                            UnityEngine.Debug.LogError($"[IuvoDebug] Final flush failed: {ex}");
                        }
                    }
                }
            }

#endregion

            private static string FormatMessage(string level, string message, string memberName, string filePath, int lineNumber, out string richTextMessage, bool richTxtMsg)
            {
                string fileName = Path.GetFileName(filePath);

                string color = level == "[ERROR]" ? "orange" :
                               level == "[WARNING]" ? "yellow" :
                               "green";

                string richLevel = $"<color={color}>{level}</color>";



                string richMessage = $"<color={color}>{message}</color>";
                if (level == "[WARNING]") richMessage = $"<color={color}><i>{message}</i></color>";
                else if (level == "[ERROR]") richMessage = $"<color={color}><b>{message}</b></color>";

                string location = $"[{memberName} in {fileName}:{lineNumber}]";
                string richLocation = $"[{memberName} in {fileName}:{lineNumber}]";
                if (level == "[ERROR]") richLocation = $"<b>[{memberName} in {fileName}:{lineNumber}]</b>";

                string val = $"{level} {message} {location}";
                richTextMessage = val;
                if (!richTxtMsg)
                {
                    richMessage = message;
                    richLocation = $"[{memberName} in {fileName}:{lineNumber}]";
                    richTextMessage = $"{level} {richMessage} {richLocation}";
                }

                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                val = $"{dateTime} {val}";
                return val;
            }

            #region Specific Debuggers
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
                DebugLog($"Position: {position:F3}", false, memberName, filePath, lineNumber);
            }

            public static void DebugRotation(Quaternion rotation, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;
                DebugLog($"Quaternion: {rotation.x:F3}, {rotation.y:F3}, {rotation.z:F3}, {rotation.w:F3}", false, memberName, filePath, lineNumber);
            }

            public static void DebugEulerAngles(Vector3 eulerAngles, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;
                DebugLog($"Angles: {eulerAngles:F3}", false, memberName, filePath, lineNumber);
            }

            public static void DebugScale(Vector3 scale, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;
                DebugLog($"Scale: {scale:F3}", false, memberName, filePath, lineNumber);
            }

            public static void DebugLossyScale(Vector3 lossyScale, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;
                DebugLog($"Lossy Scale: {lossyScale:F3}", false, memberName, filePath, lineNumber);
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
                DebugLog($"IuvoUnity Version: {version}", false, memberName, filePath, lineNumber);
            }

            public static void DebugIuvoGame(IuvoGame game, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;
                if (game == null)
                {
                    DebugLogWarning("IuvoGame instance is null", false, memberName, filePath, lineNumber);
                    return;
                }
                DebugLog($"Game: {game.gameName}", false, memberName, filePath, lineNumber);
                DebugLog($"Game Version: {game.gameVersion}", false, memberName, filePath, lineNumber);
                DebugLog($"Developer: {game.developerName}", false, memberName, filePath, lineNumber);
                DebugLog($"Publisher: {game.publisherName}", false, memberName, filePath, lineNumber);
                DebugLog($"Screen Resolution: {game.screenWidth}x{game.screenHeight}", false, memberName, filePath, lineNumber);

                {/// will be removed once class fully implements
                    if (game.systemsStateMachine != null)
                    {
                        if (game.systemsStateMachine.currentState != null)
                        {
                            DebugLog($"Systems State: {game.systemsStateMachine.currentState.stateName}", false, memberName, filePath, lineNumber);
                        }
                        else
                        {
                            DebugLogWarning("Systems StateMachine current state is null", false, memberName, filePath, lineNumber);
                        }
                    }
                    else
                    {
                        DebugLogWarning("Systems StateMachine is null", false, memberName, filePath, lineNumber);
                    }
                }

                // check for the game debug interface
                IGameDebug gameDebug = game as IGameDebug;
                if (gameDebug != null)
                {
                    gameDebug.LogGameInfo();
                    gameDebug.LogGameState();
                }
                else
                {
                    DebugLogWarning("IuvoGame does not implement IGameDebug interface", false, memberName, filePath, lineNumber);
                }
            }

            #endregion

            public static void DebugLog(string message, bool richTxtMsg = true, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Debug)) return;
                string richFormatted;
                string formatted = FormatMessage("[DEBUG]", message, memberName, filePath, lineNumber, out richFormatted, richTxtMsg);
                UnityEngine.Debug.Log(richFormatted);
                EnqueueLog(formatted);
            }

            public static void DebugLogWarning(string message, bool richTxtMsg = true, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Warning)) return;
                string richFormatted;
                string formatted = FormatMessage("[WARNING]", message, memberName, filePath, lineNumber, out richFormatted, richTxtMsg);
                UnityEngine.Debug.LogWarning(richFormatted);
                EnqueueLog(formatted);
            }

            public static void DebugLogError(string message, bool richTxtMsg = false, [CallerMemberName] string memberName = "",
                [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
            {
                if (!EnabledLevels.Contains(ValidationLevel.Error)) return;
                string richFormatted;
                string formatted = FormatMessage("[ERROR]", message, memberName, filePath, lineNumber, out richFormatted, richTxtMsg);
                UnityEngine.Debug.LogError(richFormatted);
                EnqueueLog(formatted);
            }

            // Internal runner to survive scene changes
            private class IuvoDebugRunner : MonoBehaviour { }
        }
    }
}
