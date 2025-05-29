using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameController gameController;
    public GameObject fireParticlePrefab;
    ParticleSystem FireEffect;
    float velocity;
    float cooldown;

    string gunName;
    GameObject gun;
    AudioSource sound;
    GameObject bulletPrefab;
    public float weaponResetTime;
    float onWeaponresetTime;

    public GunType defaultGun;

    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        resetGun(defaultGun.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.gameEnd) { return; }
        if (GameData.pause) { return; }
        onWeaponresetTime += Time.deltaTime;
        cooldown -= Time.deltaTime;

        if (onWeaponresetTime >= weaponResetTime)
        {
            resetGun(defaultGun.ToString());
        }
#if UNITY_STANDALONE_WIN
        if (Input.GetButton("Fire1"))
        {
            shoot();
        }
#endif
    }

    public void resetGun(string name)
    {
        //Remove All Gun in Hands
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        //Reset Weapon Time
        onWeaponresetTime = 0f;
        //Instantiate Gun
        gunName = name;
        gun = Instantiate(gameController.gunChoiceByName(gunName), transform);
        gun.name = gunName;
        bulletPrefab = gameController.bulletType(gunName);
        cooldown = gameController.bulletCooldown(gunName);
        velocity = gameController.bulletSpeed(gunName);
        sound.clip = gameController.gunSound(gunName);
        gameController.changeGunIcon(gunName);
        makeFireParticle(gameController.firePos(name));
    }

    public void makeFireParticle(Vector3 pos)
    {
        GameObject fireParticle = Instantiate(fireParticlePrefab, Vector3.zero, Quaternion.Euler(0f, 0f, 0f), transform);
        fireParticle.transform.localPosition = pos;
        FireEffect = fireParticle.GetComponent<ParticleSystem>();
        FireEffect.name = "FireParticle";
    }

    public void shoot()
    {
        if (cooldown > 0) { return; }
        //Fire/Initiating Bullet
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.Euler(0f, 0f, 0f), transform);
        bullet.transform.localScale = bullet.transform.localScale;
        bullet.transform.localPosition = gameController.firePos(gunName);
        bullet.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        bullet.transform.localScale = bullet.transform.localScale;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * velocity, ForceMode.VelocityChange);
        bullet.transform.parent = null;
        if (gun.name == "Grenade")
        {
            cooldown = gameController.bulletCooldown(gunName);
            return;
        }
        //Fire Sound
        gameObject.GetComponent<AudioSource>().Play();
        //Animation
        gun.GetComponent<Animation>().Play();
        FireEffect.Play();
        cooldown = gameController.bulletCooldown(gunName);
    }
}
