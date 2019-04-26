using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script detects whether an asteroid is shot outside of the game area, and deletes it.
public class AsteroidGarbageCollector : MonoBehaviour {

    private float timestamp;
    private float delay = 5f;
	// Use this for initialization
	void Start () {
        timestamp = Time.time + delay;
	}
	
	// Update is called once per frame
	void Update () {
		if(timestamp < Time.time)
        {

            if(gameObject.transform.position.x > 30 || gameObject.transform.position.x < -30 || gameObject.transform.position.y > 30 || gameObject.transform.position.y < 30)
            {
                Destroy(gameObject);
            }

            timestamp += delay;
        }
        else
        {
            timestamp = Time.time + delay;
        }
	}
}
