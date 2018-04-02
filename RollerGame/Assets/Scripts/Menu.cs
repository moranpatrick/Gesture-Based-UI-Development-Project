using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pose = Thalmic.Myo.Pose;

public class Menu : MonoBehaviour {

	public GameObject myo = null;
	public GameObject calibration;
	private Pose _lastPose = Pose.Unknown;

	private float startTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Scene scene = SceneManager.GetActiveScene();
		// if(Input.GetKeyDown (KeyCode.Space)){
		// 	SceneManager.LoadScene(1);
		// }
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

		if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;

            if (thalmicMyo.pose == Pose.DoubleTap)
            {
			   // Quit Application
			   Application.Quit();
            }
            else if (thalmicMyo.pose == Pose.FingersSpread)
            {
           	
            }
            else if (thalmicMyo.pose == Pose.Fist)
            {
				var JointObject = GameObject.Find("Myo");
    
			   //show calibration screen for 5 seconds
				calibration.SetActive(true);
				
				while(startTime < 5)
				{
					startTime += Time.deltaTime;
					if(startTime > 4)
					{	  
						float x = JointObject.transform.eulerAngles.x;
        				float y = JointObject.transform.eulerAngles.y; 
						PlayerPrefs.SetFloat("diffX", x);
						PlayerPrefs.SetFloat("diffY", y);
						SceneManager.LoadScene(1);
					}
				}
            }
        }
        else
        {
           
        }
		
	}
}
