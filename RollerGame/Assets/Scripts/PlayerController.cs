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
        float x = JointObject.transform.rotation.eulerAngles.x;
        float y = JointObject.transform.rotation.eulerAngles.y;
        //Debug.Log("X IS: " + x);
        //Debug.Log("Y IS: " + y);
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = 0;
        float moveVertical = 0;

        //vertical movement
        if(x < 13)        
        {
            //move forward
            moveVertical = moveVertical + 1;
            Debug.Log("moveVertical in les than 5" +x);
        }
        else if(x > 13 && x < 23 || x < 0)
        {
            // do nothing
        }
        else if(x > 23)
        {
            //move backward
            moveVertical = moveVertical - 1;
             Debug.Log("moveVertical in greater than 5" +x);
        }

        //horizontal movement
        if(y < 150)        
        {
            //move left
            moveHorizontal = moveHorizontal - 1;
            Debug.Log("moveVertical in les than 5" +x);
        }
        else if(y > 150 && y < 165)
        {
            // do nothing
        }
        else if(y > 165 || y < 0)
        {
            //move right
            moveHorizontal = moveHorizontal + 1;
             Debug.Log("moveVertical in greater than 5" +x);
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
