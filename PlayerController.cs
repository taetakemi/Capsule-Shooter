using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;

    public GameController gameController;
    public float velocity = 30f;
    float maxSpeed = 5f;
    public bool onGround;
    Status status;

	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        status = GetComponent<Status>();
    }

    private void FixedUpdate()
    {
        if (GameData.gameEnd) { return; }
        if (GameData.pause) { return; }

        //Movement
#if UNITY_STANDALONE_WIN
        if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") > 0)
        {
            //rb.velocity = transform.forward * velocity/3;
            rb.AddForce(transform.forward * velocity, ForceMode.Force);
        }
        if (Input.GetButton("Vertical") && Input.GetAxis("Vertical") < 0)
        {
            //rb.velocity = -transform.forward * velocity/3;
            rb.AddForce(-transform.forward * velocity, ForceMode.Force);
        }
        if (Input.GetButton("Horizontal") && Input.GetAxis("Horizontal") > 0)
        {
            //rb.velocity = transform.right * velocity/3;
            rb.AddForce(transform.right * velocity, ForceMode.Force);
        }
        if (Input.GetButton("Horizontal") && Input.GetAxis("Horizontal") < 0)
        {
            //rb.velocity = -transform.right * velocity/3;
            rb.AddForce(-transform.right * velocity, ForceMode.Force);
        }
        normalizedSpeed();
#endif
#if UNITY_ANDROID

#endif
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            Jump();
        }
    }

    public void Jump()
    {
        rb.AddForce(new Vector3(0f, 180f, 0f), ForceMode.Force);
    }

    void normalizedSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
        //Bullet Damage
        switch (collision.gameObject.tag)
        {
            case "shell":
                GetComponent<Status>().deductHealth(3);
                break;
            case "shotgun_shell":
                GetComponent<Status>().deductHealth(10);
                break;
            //case "rocket":
            //    GetComponent<Status>().deductHealth(25);
            //    break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }

    #region Buff
    public void OnTriggerEnter(Collider other)
    {
        //Buff
        if (other.gameObject.tag == "Heart")
        {
            status.getHeart();
        }
        if (other.gameObject.tag == "Speed")
        {
            StartCoroutine(speedIncrease());
        }
        if (other.gameObject.tag == "Shield")
        {
            StartCoroutine(status.getShield());
        }
        Destroy(other.gameObject);
    }

    IEnumerator speedIncrease()
    {
        gameController.speedIcon.SetActive(true);
        maxSpeed = 10f;
        yield return new WaitForSeconds(15f);
        maxSpeed = 5f;
        gameController.speedIcon.SetActive(false);
    }
    #endregion

#if UNITY_ANDROID
    public void androidMove(Vector3 toward)
    {
        Vector3 movement = new Vector3(toward.x, 0f, toward.z);
        rb.AddForce(toward * velocity, ForceMode.Force);
        normalizedSpeed();
    }
#endif
}
