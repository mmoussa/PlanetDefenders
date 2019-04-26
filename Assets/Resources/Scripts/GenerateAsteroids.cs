using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAsteroids : MonoBehaviour {

    public float minPos;
    public float maxPos;
    public float minPosSafe;
    public float maxPosSafe;

    private int[] randArray = new int[] { -30, -29, -28, -27, -26, -25, -24, -23, -22, -21, -20, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };

    public GameObject asteroid;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", 1, 4);

	}
	
	// Update is called once per frame
	void Update () {
	}

    void Spawn()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Enemy"); 
        if(asteroids.Length < 10)
        {
            float x = randArray[Random.Range(0, randArray.Length)];
            float y = randArray[Random.Range(0, randArray.Length)];

            Vector3 pos = new Vector3(x, y, 0);
            Instantiate(asteroid, pos, transform.rotation).name = "Asteroid";
        }
        
    }
}
