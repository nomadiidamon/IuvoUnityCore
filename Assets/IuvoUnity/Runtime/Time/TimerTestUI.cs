using UnityEngine;
using IuvoUnity.DataStructs;
using TMPro;
using IuvoUnity.Extensions;
using IuvoUnity.IuvoTime;
using IuvoUnity.Configurations;
using IuvoUnity.Debug;
using IuvoUnity.Editor;

public class TimerTestUI : MonoBehaviour
{
    [Expandable]
    public TimerConfiguration timerConfig;
    public TextMeshProUGUI timerText;
    public Timer testTimer;


    public void Awake()
    {
        if (timerConfig == null)
        {
            IuvoDebug.DebugLogError("TimerTestUI: No TimerConfiguration assigned in inspector.");
            return;
        }
        if (timerText == null)
        {
            IuvoDebug.DebugLogError("TimerTestUI: No TextMeshPro assigned in inspector.");
            return;
        }

        testTimer = gameObject.GetOrAdd<Timer>();
        timerConfig.Configure(testTimer);
    }

    public void Start()
    {
        timerText.SetText(TimeKeeper.FormatElapsedTime(testTimer));
        IuvoDebug.DebugLog("TimerTestUI: Timer started with mode " + testTimer.activityMode.ToString());
        testTimer.StartTimer();
    }

    public void Update()
    {
        if (testTimer == null || timerText == null) return;

        if (TimeKeeper.IsDecrement(testTimer.activityMode))
        {
            timerText.SetText(TimeKeeper.FormatRemainingTime(testTimer));
        }
        else if (TimeKeeper.IsIncrement(testTimer.activityMode) || TimeKeeper.IsStopwatch(testTimer.activityMode))
        {
            timerText.SetText(TimeKeeper.FormatElapsedTime(testTimer));
        }


    }


}
