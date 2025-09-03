using IuvoUnity.Interfaces;
using IuvoUnity.StateMachine;
using System.Collections.Generic;
using UnityEngine;


namespace IuvoUnity
{

    namespace Constants
    {
        [System.Serializable]
        public class IuvoGame : MonoBehaviour
        {
            ConstTag IuvoTag = new ConstTag(TagType.APPLICATION, ApplicationTag.BASE_APP, GameTag.NONE, UITag.NONE);

            private static int screenWidth = 1920;
            private static int screenHeight = 1080;

            public static string gameVersion = "0.0.1";
            public static string gameName = "IuvoGame";
            public static string developerName = "IuvoUnity";
            public static string publisherName = "IuvoUnity";

            [SerializeField] public GenericStateMachine systemsStateMachine;
            //[SerializeField] public GenericStateMachine gameStateMachine;
            [SerializeField] public GenericState gameStateMachine;
            //[SerializeField] public GenericStateMachine menuStateMachine;
            [SerializeField] public GenericState menuStateMachine;


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

            public void EnableInput()
            {
            }

            public void EnterMenu()
            {
            }

            public void UpdateMenu()
            {
            }

            public void ExitMenu()
            {
            }


            public void StartGame()
            {
            }

            public void RunGame()
            {
            }

            public void PauseGame()
            {
            }

            public void UnpauseGame()
            {
            }

            public void DisableInput()
            {
            }

            public void ShowDeathMenu()
            {
            }

            public void RespawnPlayer()
            {
            }

            public void WinGame()
            {
            }

            public void ShowCreditsScreen()
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

            public void LoadLevelSafe()
            {
            }

            public void LoadLevelAsync()
            {
            }


            public void LoadMainMenu()
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
        }
    }
}