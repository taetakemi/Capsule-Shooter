using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPull : MonoBehaviour {

    public List<GameObject> objects;
    //public GameObject planet;

    public float gravitationalPull;

    void FixedUpdate()
    {
        //apply spherical gravity to selected objects (set the objects in editor)
        foreach (GameObject o in objects)
        {
            if (o.GetComponent<Rigidbody>() as Rigidbody != false)
            {
                o.GetComponent<Rigidbody>().AddForce((transform.position - o.transform.position).normalized * gravitationalPull);
            }
        }
        //or apply gravity to all game objects with rigidbody
        foreach (GameObject o in UnityEngine.Object.FindObjectsOfType<GameObject>())
        {
            if (o.GetComponent<Rigidbody>() as Rigidbody != false && o != gameObject)
            {
                o.GetComponent<Rigidbody>().AddForce((transform.position - o.transform.position).normalized * gravitationalPull);
            }
        }
    }
}
