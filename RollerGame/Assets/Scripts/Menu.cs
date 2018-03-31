using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pose = Thalmic.Myo.Pose;

public class Menu : MonoBehaviour {

	public GameObject myo = null;
	private Pose _lastPose = Pose.Unknown;

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
			   //Load Scene 1 which is game		   
			   SceneManager.LoadScene(1);
            }
        }
        else
        {
           
        }
		
	}
}
