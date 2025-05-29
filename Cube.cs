using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour {

    public bool cube = true;
    public bool atBase = false;
    //public float timeLimit;

    public void cubeCreated()
    {
        cube = true;
        atBase = false;
    }
    private void Update()
    {
        //timeLimit -= Time.deltaTime;
        //if (timeLimit <= 0f && !atBase)
        //{
        //    transform.SetParent(null);
        //    Destroy(gameObject);
        //}
        if (atBase)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void LateUpdate () {
        if (!atBase)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Lift(GameObject lifter) {
        transform.SetParent(lifter.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BaseBorder")
        {
            atBase = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BaseBorder")
        {
            atBase = true;
        }
    }
}
