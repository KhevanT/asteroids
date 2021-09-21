using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region Fields

    // timer duration
    float totalSeconds = 0;

    // timer execution
    float elapsedSeconds = 0;
    bool running = false;

    //support for finished property
    bool started = false;

    // The bools here are refered to as flags since they keep track of the state of the timer

    #endregion

    #region Properties

    public float Duration
    {
        // We use this property to set the duration of our timer

        set
        {
            if (!running)
            {
                // We only allow the value of duration to be changed if timer isn't running
                // It would be illogical to do otherwise

                totalSeconds = value;
            }
        }
    }

    public bool Finished
    {
        // We use this property to get whether timer has finished running or not

        // Here we use the && Boolean expression
        // Hence the timer will only be considered finished
        // If timer has been "started" and is "notrunning"
        get { return started && !running; }
    }

    public bool Running
    {
        // To get whether timer is currently running or not
        get { return running; }
    }

    #endregion

    #region Methods

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // We have added these statements in the Update method since this method
        // Is updated every secomd/frame


        // Update timer and check if it's finished
        if (running)
        {
            // First updating the timer using Time.deltaTime
            elapsedSeconds += Time.deltaTime;

            // Using if statement to turn of Timer if elapsed duration has crossed total duration
            if (elapsedSeconds >= totalSeconds)
            {
                running = false;
            }
        }
    }

    // Creating a new method for the timer
    public void RunTimer()
    {
        //only run with a duration that is valid i.e not negative or 0
        if (totalSeconds > 0)
        {
            // Converting the flags to indicate timer has started and is running
            started = true;
            running = true;

            // Resetting the elapsed duration
            elapsedSeconds = 0;
        }

    }

    #endregion
}

