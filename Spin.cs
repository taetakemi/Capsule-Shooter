using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    float x;
    float y;
    float z;
    float minSpeed = 30f;
    float maxSpeed = 90f;

    void Update () {
        x += Random.Range(minSpeed,maxSpeed) * Time.deltaTime;
        y += Random.Range(minSpeed,maxSpeed) * Time.deltaTime;
        z += Random.Range(minSpeed,maxSpeed) * Time.deltaTime;
        transform.rotation = Quaternion.Euler(x, y, z);
	}
}
