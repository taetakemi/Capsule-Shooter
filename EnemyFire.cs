using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    GameController gameController;
    //GameObject target;
    public EnemyController self;

    GameObject bulletPrefab;
    GameObject gun;
    string gunName;
    float velocity;
    float cooldown;
    AudioSource sound;

    public GameObject fireParticlePrefab;
    ParticleSystem FireEffect;

    RaycastHit[] laserArr;
    LayerMask playerLayer;
    float distance = 10f;

    void Start()
    {
        playerLayer = 1 << LayerMask.NameToLayer("Player");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        gunName = self.gunType.ToString();
        //Instantiate Gun
        gun = Instantiate(gameController.gunChoiceByName(gunName), transform);
        gun.name = gunName;
        //Get Bullet data
        bulletPrefab = gameController.bulletType(gunName);
        cooldown = gameController.bulletCooldown(gunName);
        velocity = gameController.bulletSpeed(gunName);

        if (gun.name == "Grenade") { return; }
        sound = GetComponent<AudioSource>();
        sound.clip = gameController.gunSound(gunName);
        //Fire Effect
        makeFireParticle(gameController.firePos(name));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.gameEnd) { return; }
        if (GameData.pause) { return; }

        //cameraDirection = new Vector2(Input.GetAxisRaw("Mouse X") + 
        //                    cameraDirection.x,
        //                    Mathf.Clamp(Input.GetAxisRaw("Mouse Y") + 
        //                    cameraDirection.y, -50f, 90f));

        //Vector3 lookPos = target.transform.position - transform.position;
        //lookPos.x = 0;
        //lookPos.z = 0;
        //Debug.Log(string.Format("lookpos {0}", lookPos));
        //Quaternion rotation = Quaternion.LookRotation(lookPos,transform.up);
        //Debug.Log(string.Format("rotation {0}", rotation));
        //transform.localRotation = Quaternion.Lerp(transform.localRotation, rotation, Time.deltaTime);
        //Debug.DrawRay(transform.position, transform.forward * distance, Color.cyan, 1f);
        //transform.forward = Vector3.Lerp(transform.forward, target.transform.position, Time.deltaTime);
        //return;
        cooldown -= Time.deltaTime;
        //Aim for Player only
        laserArr = Physics.RaycastAll(transform.position, transform.forward * distance, distance, playerLayer.value);
        Debug.DrawRay(transform.position, transform.forward * distance, Color.cyan, 1f);
        if(!(laserArr.Length > 0)) { return; }
        if (laserArr[0].collider.gameObject.name != "Player") { return; }
        if (cooldown > 0) { return; }
        //Fire Bullet
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.Euler(0f, 0f, 0f), transform);
        bullet.transform.localScale = bullet.transform.localScale;
        bullet.transform.localPosition = gameController.firePos(gunName);
        bullet.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        bullet.transform.localScale = bullet.transform.localScale;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * velocity, ForceMode.VelocityChange);
        if (gun.name == "Grenade")
        {
            cooldown = gameController.bulletCooldown(gunName);
            return;
        }
        //Fire Sound
        GetComponent<AudioSource>().Play();
        //Animation
        gun.GetComponent<Animation>().Play();
        FireEffect.Play();
        cooldown = gameController.bulletCooldown(gunName);
        //Debug.Log("Enemy Shot");
    }


    //public void resetGun(string name)
    //{
    //    gunName = name;

    //    GameObject gun = Instantiate(gameController.gunChoiceByName(gunName), transform);
    //    gun.name = gunName;
    //    bulletPrefab = gameController.bulletType(gunName);
    //    cooldown = gameController.bulletCooldown(gunName);
    //    velocity = gameController.bulletSpeed(gunName);
    //    Debug.Log(velocity);
    //    gameController.changeGunIcon(gunName);
    //    makeFireParticle(gameController.firePos(name));
    //}

    public void makeFireParticle(Vector3 pos)
    {
        GameObject fireParticle = Instantiate(fireParticlePrefab, Vector3.zero, Quaternion.Euler(0f, 0f, 0f), transform);
        fireParticle.transform.localPosition = pos;
        FireEffect = fireParticle.GetComponent<ParticleSystem>();
        FireEffect.name = "FireParticle";
    }
}
