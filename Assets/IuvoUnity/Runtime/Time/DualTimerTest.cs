using UnityEngine;
using IuvoUnity.DataStructs;
using IuvoUnity.IuvoPhysics;
using IuvoUnity.Controllers;
using IuvoUnity.Debug;
using IuvoUnity.Extensions;


public class DualTimerTest : MonoBehaviour
{
    public GravityBody gravityBody;
    public GroundCheck groundCheck;

    //public BasicTimer FallDownTimer;
    //public BasicTimer FlyUpTimer;

    private void Awake()
    {
        gravityBody = gameObject.GetOrAdd<GravityBody>();
        groundCheck = gameObject.GetOrAdd<GroundCheck>();
        //FallDownTimer.OnFinished.AddListener(() =>
        //{
        //    groundCheck.SetCheckOrigin(gravityBody.transform.position);
        //    gravityBody.customDirection = Vector3.down;
        //    //groundCheck.SetDirectionToCheck(gravityBody.customDirection);
        //    groundCheck.ForceGroundCheck();
        //    FlyUpTimer.Reset();
        //    //FlyUpTimer.Tick(Time.deltaTime); // Start the FlyUpTimer immediately
        //});


        //FlyUpTimer.OnFinished.AddListener(() =>
        //{
        //    groundCheck.SetCheckOrigin(gravityBody.transform.position);
        //    gravityBody.customDirection = Vector3.up;
        //    //groundCheck.SetDirectionToCheck(gravityBody.customDirection);
        //    groundCheck.ForceGroundCheck();
        //    FallDownTimer.Reset();
        //    //FallDownTimer.Tick(Time.deltaTime); // Start the FallDownTimer immediately
        //});
    }

    void Start()
    {
        //FallDownTimer.Tick(Time.deltaTime); // Start the FallDownTimer immediately
    }

    void Update()
    {
        //if (!FallDownTimer.IsRunning)
        //{
        //    FlyUpTimer.Tick(Time.deltaTime);
        //}



        //if (!FlyUpTimer.IsRunning)
        //{
        //    FallDownTimer.Tick(Time.deltaTime);
        //}



    }
}
