using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumePause : MonoBehaviour
{
    

    public void PauseTime()
    {
        Time.timeScale = 0f;
        Debug.Log("Time Paused");
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
        Debug.Log("Time Resumed");
    }
}
