using UnityEngine;

public class Bullet : MonoBehaviour {
    private void OnCollisionEnter(Collision collision)
    {
        if (tag == "rocket") { return; }
        //if (collision.gameObject.tag == "BaseBorder") { return; }
        Destroy(gameObject);
    }
}
