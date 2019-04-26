using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attractor : MonoBehaviour
{

    //public float G = 667.4f;

    public static List<Attractor> Attractors;
    public Rigidbody rb;
    

    void FixedUpdate()
    {
        foreach (Attractor attractor in Attractors)
        {


            if (attractor != this)
                
                //do not attract ships/interceptors
                if (gameObject.name.Contains("Ship") && attractor.gameObject.name == "Earth")
                {

                }
                else if (this.gameObject.name == "Earth" && attractor.gameObject.name.Contains("Ship"))
                {

                }
                else
                {
                    Attract(attractor);
                }
                   
        }

    }

    void OnEnable()
    {
        if (Attractors == null)
        {
            Attractors = new List<Attractor>();
        }
        Attractors.Add(this);
    }

    void OnDisable()
    {
        Attractors.Remove(this);
    }
    void Attract(Attractor objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;

        float distance = direction.sqrMagnitude;

        if (distance == 0f)
            return;

        float forceMagnitude = Constants.G * (rb.mass * rbToAttract.mass) / distance;
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);

    }
}
