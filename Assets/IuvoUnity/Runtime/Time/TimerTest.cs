using UnityEngine;
using IuvoUnity.DataStructs;
using IuvoUnity.IuvoPhysics;
using IuvoUnity.Extensions;
using IuvoUnity.Debug;
using IuvoUnity.Editor;

public class TimerTest : MonoBehaviour
{
    public GravityBody gravityBody;
    public GroundCheck groundCheck;

    [Expandable]
    public Timer ResetPositionTimer;

    public void Awake()
    {
        gravityBody = gameObject.GetOrAdd<GravityBody>();
        groundCheck = gameObject.GetOrAdd<GroundCheck>();
        ResetPositionTimer.SetDuration(5.0f);
        ResetPositionTimer.activityMode = IuvoUnity.IuvoTime.Timer_Activity_Mode.DECREMENT;

        ResetPositionTimer.timer.countUpTimer.OnFinished.AddListener(() =>
        {
            gravityBody.transform.position = new Vector3(0, 55, 0);
            groundCheck.SetCheckOrigin(gravityBody.transform.position);
            gravityBody.customDirection = gravityBody.customDirection.Inverse();
            groundCheck.SetDirectionToCheck(gravityBody.customDirection);
            groundCheck.ForceGroundCheck();
            ResetPositionTimer.ResetTimer();
            IuvoDebug.DebugLog("Position Reset!");
        });
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (gravityBody.Grounded)
        {
           ResetPositionTimer.timer.Tick(ResetPositionTimer.activityMode, Time.deltaTime);
        }
    }
}
