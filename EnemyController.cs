using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    //public GameObject Enemy;
    GameController gameController;
    GameObject TargetObject;
    Vector3 previousPosition;
    float timePos;

    float timerDelay = 5f;
    float timerCD;

    NavMeshAgent agent;
    Transform target;
    //float nearestGap = 10f;

    public bool onGround = false;
    //float defaultSpeed;

    public GunType gunType;

    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        TargetObject = GameObject.Find("Player");
        target = TargetObject.transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        previousPosition = transform.position;
        timePos = 3f;
        timerCD = timerDelay;
    }

    void Update () {
        if (GameData.gameEnd) { return; }
        if (GameData.pause) { return; }
        if (GetComponent<EnemyStatus>().health <= 0)
        {
            gameController.spawnBuff(transform.position);
            gameController.removeEnemy(gameObject);
            return;
        }
        //3 Second not move then set onGround to true
        if (previousPosition == transform.position)
        {
            timePos -= Time.deltaTime;
            if (timePos <= 0f)
            {
                onGround = true;
            }
        }
        else
        {
            previousPosition = transform.position;
            timePos = 0f;
        }
        //Check On Ground
        if (onGround)
        {
            agent.SetDestination(target.transform.position);
        }
        timerCD -= Time.deltaTime;
        if (timerCD <= 0f)
        {
            StartCoroutine(DelayAgent(1f));
        }
    }

    private void LateUpdate()
    {
        if (GameData.gameEnd) { return; }
        if (GameData.pause) { return; }
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*4f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //StopCoroutine("DelayAgent");
        if (collision.gameObject.tag == "shell" || collision.collider.gameObject.tag == "shotgun_shell")
        {
            delayStart();
        }
        switch (collision.gameObject.tag)
        {
            case "shell":
                GetComponent<EnemyStatus>().deductHealth(3);
                break;
            case "shotgun_shell":
                GetComponent<EnemyStatus>().deductHealth(10);
                break;
            //case "rocket":
            //    GetComponent<EnemyStatus>().deductHealth(25);
            //    break;
        }
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }

    IEnumerator DelayAgent(float time) {
        yield return new WaitForSeconds(time);
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void delayStart()
    {
        StartCoroutine(DelayAgent(2f));
    }
}
