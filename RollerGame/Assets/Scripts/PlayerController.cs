﻿using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pose = Thalmic.Myo.Pose;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public GameObject myo = null;
    private Pose _lastPose = Pose.Unknown;
    public float speed;
    public Text countText;
    private Rigidbody rb;
    private int count;
    public Text winText;
    public GameObject pauseImage;
    private bool paused;

    private float elapsed;
    private float tempTime;

    private float diffX;
    private float diffY;

    //timer 
    public Text timerText;

   

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        timerText.text = "";
        paused = false;
        diffX =PlayerPrefs.GetFloat("diffX");
        diffY =PlayerPrefs.GetFloat("diffY");
        Debug.Log(diffX);
    }

    private void FixedUpdate()
    {

        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
        elapsed += Time.deltaTime;
        if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;
            if (thalmicMyo.pose == Pose.DoubleTap)
            {   
                if(paused){
                    Debug.Log("in exit");
                    //quit application
                    if (!Application.isEditor) System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else{
                    // Pause Menu Logic
                    rb.isKinematic = true;
                    paused = true;
                    pauseImage.SetActive(true);
                    tempTime = elapsed;
                }            
            }
            else if (thalmicMyo.pose == Pose.FingersSpread)
            {
                if(paused){
                    paused = false;
                    
                    pauseImage.gameObject.SetActive(false);
                    rb.isKinematic = false;
                    elapsed = tempTime;
                }
            }
            else if (thalmicMyo.pose == Pose.Fist)
            {
                if(paused){
                    paused = false;
                    SceneManager.LoadScene(1);
                }
            }
        }
        else
        {
            
        }
               
        var JointObject = GameObject.Find("Myo");
        
        float x = JointObject.transform.rotation.eulerAngles.x;
        float y = JointObject.transform.rotation.eulerAngles.y;

        if(x < diffX)
        {
            x = 360 -(diffX - x);
        }
        else
        {
            x = x - diffX;
        }
        if(y < diffY)
        {
            y = 360 -(diffY - y);
        }
        else
        {
            y = y - diffY;
        }
    
        float moveHorizontal = 0;
        float moveVertical = 0;
        //vertical movement
        if(x > 5 && x < 180)        
        {
            //move forward
            moveVertical = moveVertical + 1;
        }
        else if(x < 5 && x > 0)
        {
            // do nothing
        }
        else if(x < 360 || x > 180)
        {
            //move backward
            moveVertical = moveVertical - 1;
        }

        //horizontal movement
        if(y > 0 && y < 180)        
        {
            //move right
            moveHorizontal = moveHorizontal + 1;
        }
        else if(y < 360 && y > 180)
        {
            //move left
            moveHorizontal = moveHorizontal - 1;
        }


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
        if(paused == false && count != 12)
        {
            SetTimerText(elapsed);
        }
        
    }//FixedUpdate()

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            float winTime = elapsed;
            Debug.Log("win time" + winTime);
            winText.text = string.Format("You Win! Your Time: {0:0.00}", winTime);

        }
    }

    void SetTimerText(float t)
    {
        timerText.text = string.Format("Timer: {0:0.00}", t);
    }

}
