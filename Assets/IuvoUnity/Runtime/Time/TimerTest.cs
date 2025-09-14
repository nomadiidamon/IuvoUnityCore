using UnityEngine;
using IuvoUnity.Controllers;
using IuvoUnity.IuvoPhysics;
using IuvoUnity.DataStructs;
using IuvoUnity.Extensions;
using IuvoUnity.Debug;

public class TimerTest : MonoBehaviour
{
    public GravityBody gravityBody;
    public GroundCheck groundCheck;

    //public BasicTimer ResetPositionTimer;

    public void Awake()
    {
        gravityBody = gameObject.GetOrAdd<GravityBody>();
        groundCheck = gameObject.GetOrAdd<GroundCheck>();
        //ResetPositionTimer.duration = 5.0f;

        //ResetPositionTimer.OnFinished.AddListener(() =>
        //{
        //    //gravityBody.transform.position = new Vector3(0, 55, 0);
        //    groundCheck.SetCheckOrigin(gravityBody.transform.position);
        //    gravityBody.customDirection = Vector3.up;
        //    groundCheck.SetDirectionToCheck(gravityBody.customDirection);
        //    groundCheck.ForceGroundCheck();
        //    ResetPositionTimer.Reset();
        //    IuvoDebug.DebugLog("Position Reset!");
        //});
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (gravityBody.Grounded)
        {
           // ResetPositionTimer.Tick(Time.deltaTime);
        }
    }
}
