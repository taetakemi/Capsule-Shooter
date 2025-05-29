using UnityEngine;

public class RotateAroundPlayer : MonoBehaviour {

    public GameObject Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = 1f;
        transform.LookAt(Player.transform);
        transform.RotateAround(Player.transform.position, Vector3.up, 20 * Time.deltaTime);
	}
}
