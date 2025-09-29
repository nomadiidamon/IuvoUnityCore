using IuvoUnity.Interfaces;
using IuvoUnity.StateMachines.CSM;
using IuvoUnity.Debug;
using UnityEngine;


namespace IuvoUnity
{

    namespace Constants
    {
        public interface IGameDebug
        {
            void LogGameInfo();
            void LogGameState();
            void LogGameError(string message);
            void LogGameWarning(string message);
        }

        [System.Serializable]
        public class IuvoGame : MonoBehaviour, IGameDebug
        {
            ConstTag IuvoTag = new ConstTag(TagType.APPLICATION, ApplicationTag.BASE_APP, GameTag.NONE, UITag.NONE);

            public int screenWidth = 1920;
            public int screenHeight = 1080;

            public string gameVersion = "0.0.1";
            public string gameName = "IuvoGame";
            public string developerName = "Iuvo";
            public string publisherName = "Iuvo";

            [SerializeField] public ConditionalStateMachine systemsStateMachine;
            //[SerializeField] public GenericStateMachine gameStateMachine;
            [SerializeField] public ConditionalState gameStateMachine;
            //[SerializeField] public GenericStateMachine menuStateMachine;
            [SerializeField] public ConditionalState menuStateMachine;


            public void Awake()
            {
                // default constructor
                DontDestroyOnLoad(this.gameObject);
                if (systemsStateMachine != null)
                {
                    if (systemsStateMachine.defaultState != null)
                    {
                        systemsStateMachine.currentState = systemsStateMachine.defaultState;
                        systemsStateMachine.previousState = null;
                    }
                }
            }

            #region Startup and Shutdown


            public void InitializeGame()
            {
                // set up game systems
            }

            public void LoadGameAsync()
            {
                // load game resources
            }

            public void LoadGameSafe()
            {
                // load game resources safely
            }
            public void PlaySplashScreen()
            {

            }

            public void ShowSplashScreen()
            {
            }

            public void ShowMainMenu()
            {
            }

            public void StartGame()
            {
            }

            public void RunGame()
            {
            }
            public void SaveGame()
            {
            }

            public void QuitGame()
            {
            }

            public void ShutdownGame()
            {
            }

            #endregion

            #region Input

            public void EnableInput()
            {
            }
            public void DisableInput()
            {
            }

            #endregion

            #region Menus

            public void EnterMenu()
            {
            }

            public void LoadMainMenu()
            {
            }

            public void UpdateMenu()
            {
            }

            public void ExitMenu()
            {
            }

            public void PauseGame()
            {
            }

            public void UnpauseGame()
            {
            }


            public void ShowDeathMenu()
            {
            }
            public void ShowCreditsScreen()
            {
            }

            public void WinGame()
            {
            }

            #endregion


            #region Level Management

            public void LoadLevelSafe()
            {
            }

            public void LoadLevelAsync()
            {
            }

            public void ReloadLevel()
            {
            }
            public void RestartLevelAtCheckpoint()
            {
            }

            public void RespawnPlayerAtCheckpoint()
            {
            }

            public void RespawnPlayer()
            {
            }

            #endregion



            #region Settings

            public void SetScreenResolution(int width, int height)
            {
                screenWidth = width;
                screenHeight = height;
                Screen.SetResolution(screenWidth, screenHeight, Screen.fullScreenMode);
            }

            public void SetScreenMode(FullScreenMode mode)
            {
                Screen.fullScreenMode = mode;
            }

            #endregion

            #region IGameDebug Implementation

            public void LogGameInfo()
            {    
                IuvoDebug.DebugLog($"Game: {gameName}");
                IuvoDebug.DebugLog($"Game Version: {gameVersion}");
                IuvoDebug.DebugLog($"Developer: {developerName}");
                IuvoDebug.DebugLog($"Publisher: {publisherName}");
                IuvoDebug.DebugLog($"Screen Resolution: {screenWidth}x{screenHeight}");
            }

            public void LogGameState()
            {
                if(systemsStateMachine != null && systemsStateMachine.currentState != null)
                {
                    IuvoDebug.DebugLog($"Systems State: {systemsStateMachine.currentState.stateName}");
                }
                else
                {
                    IuvoDebug.DebugLog("Systems State: None");
                }
            }

            public void LogGameError(string message)
            {
                throw new System.NotImplementedException();
            }

            public void LogGameWarning(string message)
            {
                throw new System.NotImplementedException();
            }

            #endregion
        }
    }
}