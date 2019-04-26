using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

    public GameObject explosionEffect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   void OnCollisionEnter(Collision col)
    {
        print("Collided");
        if(col.gameObject.name == "Asteroid")
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(col.gameObject);
            //check if earth, then end game / show score / menu
            if(gameObject.name == "Earth")
            {
                //TODO: show score / menu / end game
                Destroy(gameObject);
                Destroy(explosion, 3f);
            }
            else
            {
                Destroy(gameObject);
                Destroy(explosion, 3f);
            }
            
        }

        

        
    }
}
