using IuvoUnity.Constants;
using IuvoUnity.Debug;
using UnityEngine;

public class TestDebugButton : MonoBehaviour
{
    public DebugButton myButton = new DebugButton { Label = "Do Something" };

    private void Awake()
    {
        IuvoDebug.Initialize();
        myButton.OnClick.AddListener(TestFunction);
    }

    private void TestFunction()
    {
        IuvoDebug.DebugLog("");
        IuvoDebug.DebugLog("This is a log message.");
        IuvoDebug.DebugLog("This is a log message with rich text.", true);

        IuvoDebug.DebugLogWarning("");
        IuvoDebug.DebugLogWarning("This is a warning message.");
        IuvoDebug.DebugLogWarning("This is a warning message with rich text.", true);

        IuvoDebug.DebugLogError("");
        IuvoDebug.DebugLogError("This is an error message.");
        IuvoDebug.DebugLogError("This is an error message with rich text.", true);

        IuvoDebug.DebugLogIuvoVersion();

        IuvoGame gameInstance = FindAnyObjectByType<IuvoGame>();
        if (gameInstance != null)
        {
            IuvoDebug.DebugIuvoGame(gameInstance);
        }
        else
        {
            IuvoDebug.DebugLogWarning("No IuvoGame instance found in the scene.");
        }

    }
}
