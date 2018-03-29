using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pose = Thalmic.Myo.Pose;

public class PlayerController : MonoBehaviour {

    public GameObject myo = null;
    private Pose _lastPose = Pose.Unknown;
    public float speed;
    public Text countText;
    private Rigidbody rb;
    private int count;
    public Text winText;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    private void FixedUpdate()
    {
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
     
        var JointObject = GameObject.Find("Myo");
        
        float x = JointObject.transform.right.x;
        float y = JointObject.transform.right.y;
        
        float moveHorizontal = 0;
        float moveVertical = 0;
        //Debug.Log("move Vertical in les than " + JointObject.transform.right.x);
        //Debug.Log("move horizontal in les than " + JointObject.transform.right.y);
        //vertical movement
        if(x > 0.1)        
        {
            //move forward
            moveVertical = moveVertical + 0.5f;
        }
        else if(x < 0.1 && x > 0.0)
        {
            // do nothing
        }
        else if(x < 0.0)
        {
            //move backward
            moveVertical = moveVertical - 0.5f;
        }

        //horizontal movement
        if(y < -0.96 )        
        {
            //move right
            moveHorizontal = moveHorizontal + 1;
        }
        else if(y > -0.96 && y < -0.95)
        {
            // do nothing
        }
        else if(y > -0.95)
        {
            //move left
            moveHorizontal = moveHorizontal - 1;
        }



        if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;
            if (thalmicMyo.pose == Pose.WaveOut)
            {
                Debug.Log("turn right");
                //moveHorizontal = moveHorizontal + 10;
            }
            else if(thalmicMyo.pose == Pose.WaveIn)
            {
                //moveHorizontal = moveHorizontal - 10;
            }
            else if (thalmicMyo.pose == Pose.DoubleTap)
            {
                Debug.Log("turn speed up");
            }
            else if (thalmicMyo.pose == Pose.FingersSpread)
            {
                Debug.Log("turn speed down");
            }
            else if (thalmicMyo.pose == Pose.Fist)
            {
                Debug.Log("Restart");
            }
        }
        else
        {
            //Debug.Log("do nothing");
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
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
            winText.text = "You Win!";
        }
    }

}
