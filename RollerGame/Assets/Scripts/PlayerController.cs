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
        Debug.Log("X IS: " + x);
        Debug.Log("Y IS: " + y);
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;
            if (thalmicMyo.pose == Pose.WaveOut)
            {
                
            }
            else if(thalmicMyo.pose == Pose.WaveIn)
            {
                
            }
            else if (thalmicMyo.pose == Pose.DoubleTap)
            {
                
            }
            else if (thalmicMyo.pose == Pose.FingersSpread)
            {
                
            }
            else if (thalmicMyo.pose == Pose.Fist)
            {
            
            }
        }
        else
        {
            
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
