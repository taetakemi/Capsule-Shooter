using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Define
    [Header("Player")]
    public GameObject Player;
    string _playerName;
    [Header("Enemy")]
    public GameObject EnemyHandgunPrefab;
    public GameObject EnemyUziPrefab;
    public GameObject EnemyAkPrefab;
    public GameObject EnemyM4Prefab;
    public GameObject EnemyShotgunPrefab;
    public GameObject EnemyGrenadePrefab;
    public GameObject EnemyGatlingPrefab;
    public GameObject EnemySMAWPrefab;
    public List<GameObject> Enemies;
    [Header("Gun Sprites")]
    public Sprite HandgunSprite;
    public Sprite UziSprite;
    public Sprite Ak47Sprite;
    //public Sprite BarretSprite;
    public Sprite M4Sprite;
    public Sprite ShotgunSprite;
    public Sprite GrenadeSprite;
    public Sprite GatlingSprite;
    public Sprite SmawSprite;
    [Header("Gun")]
    public GameObject handgunPrefab;
    public GameObject uziPrefab;
    public GameObject ak47Prefab;
    public GameObject m4Prefab;
    public GameObject shotgunPrefab;
    public GameObject grenadePrefab;
    public GameObject gatlingPrefab;
    public GameObject smawPrefab;
    [Header("GunSound")]
    public AudioClip handgunSound;
    public AudioClip uziSound;
    public AudioClip ak47Sound;
    public AudioClip m4Sound;
    public AudioClip shotgunSound;
    public AudioClip gatlingSound;
    public AudioClip smawSound;
    [Header("Bullet")]
    public GameObject shell;
    public GameObject shotgunshell;
    public GameObject grenade;
    public GameObject rocket;
    [Header("UI")]
    public GameObject MobileUI;
    public GameObject Crosshair;
    public GameObject WaveStartCanvas;
    public GameObject WaveClearCanvas;
    public GameObject InputName;
    public GameObject FinishCanvas;
    public GameObject PauseScreen;
    public GameObject GunIcon;
    public GameObject BoxIcon;
    public Text waveText;
    public Text scoreText;
    public Text finalNameText;
    public Text finalScoreText;
    public GameObject speedIcon;
    public GameObject shieldIcon;
    [Header("Buff")]
    public GameObject heartBuffPrefab;
    public GameObject speedBuffPrefab;
    public GameObject shieldBuffPrefab;
    [Header("Effect")]
    public ParticleSystem DeadEffect;
    public GameObject ShieldEffect;
    //public GameObject ExplosionEffect;
    [Header("Wave")]
    public bool[] spawnFinish;
    public bool spawnLoaded = false;
    int wave = 0;
    //int count;
    public float waveTimeDelayDefault;
    public float waveTimeDelay;
    public float spawnRange;
    float tempX;
    float tempZ;
    Vector3 spawnLocation;
    [Header("Sound")]
    public AudioSource BGMusic;
    [Header("Dummy Prop")]
    public GameObject Crate;
    public GameObject Barrel;
    [Range(0, 10)]
    public int minRandomProp;
    [Range(10, 20)]
    public int maxRandomProp;
    #endregion

    private void Start()
    {
        FinishCanvas.SetActive(false);
        WaveClearCanvas.SetActive(false);
        Enemies = new List<GameObject>();

        //Crop Spawning
        SpawnProp();
        //Enemy Spawning
        //spawnFinish = false;
        waveTimeDelay = waveTimeDelayDefault;
        //count = 0;
        spawnFinish = new bool[8];
        //setSpawnFinish(true);
        wave++;
        StartCoroutine(StartWave(wave));
#if UNITY_STANDALONE_WIN
        MobileUI.SetActive(false);
#endif
#if UNITY_ANDROID
        MobileUI.SetActive(true);
#endif
    }

#region Weapon
    public float bulletSpeed(string name)
    {
        switch (name)
        {
            case "Handgun":
                return 30f;
            case "Uzi":
                return 30f;
            case "AK":
                return 40f;
            case "ColtM4":
                return 45f;
            case "Shotgun":
                return 30f;
            case "Grenade":
                return 15f;
            case "Gatling":
                return 40f;
            case "SMAW":
                return 35f;
            default:
                return 30f;
        }
    }

    public float bulletCooldown(string name) {
        switch (name) {
            case "Handgun":
                return 1f;
            case "Uzi":
                return 0.8f;
            case "AK":
                return 0.6f;
            case "ColtM4":
                return 0.5f;
            case "Shotgun":
                return 0.9f;
            case "Grenade":
                return 1.5f;
            case "Gatling":
                return 0.2f;
            case "SMAW":
                return 3f;
            default:
                return 1f;
        }
    }

    public GameObject bulletType(string name)
    {
        switch (name)
        {
            case "Handgun":
                return shell;
            case "Uzi":
                return shell;
            case "AK":
                return shell;
            case "ColtM4":
                return shell;
            case "Shotgun":
                return shotgunshell;
            case "Grenade":
                return grenade;
            case "Gatling":
                return shell;
            case "SMAW":
                return rocket;
            default:
                return shell;
        }
    }

    public GameObject gunChoiceByName(string name)
    {
        switch (name)
        {
            case "Handgun":
                return handgunPrefab;
            case "Uzi":
                return uziPrefab;
            case "AK":
                return ak47Prefab;
            case "ColtM4":
                return m4Prefab;
            case "Shotgun":
                return shotgunPrefab;
            case "Grenade":
                return grenadePrefab;
            case "Gatling":
                return gatlingPrefab;
            case "SMAW":
                return smawPrefab;
            default:
                return handgunPrefab;
        }
    }

    public AudioClip gunSound(string name)
    {
        switch (name)
        {
            case "Handgun":
                return handgunSound;
            case "Uzi":
                return uziSound;
            case "AK":
                return ak47Sound;
            case "ColtM4":
                return m4Sound;
            case "Shotgun":
                return shotgunSound;
            case "Gatling":
                return gatlingSound;
            case "SMAW":
                return smawSound;
            default:
                return handgunSound;
        }
    }

    public GameObject gunChoice(int number)
    {
        switch (number) {
            case 1:
                return handgunPrefab;
            case 2:
                return uziPrefab;
            case 3:
                return ak47Prefab;
            case 4:
                return m4Prefab;
            case 5:
                return shotgunPrefab;
            case 6:
                return grenadePrefab;
            case 7:
                return gatlingPrefab;
            case 8:
                return smawPrefab;
            default:
                return handgunPrefab;
        }
    }

    public Vector3 firePos(string name)
    {
        switch (name)
        {
            case "Handgun":
                return new Vector3(-0.0098f, 0.0034f, 0.0769f);
            case "Uzi":
                return new Vector3(0f, 0.11f, 0.21f);
            case "AK":
                return new Vector3(0.003f, 0.003f, 0.526f);
            case "ColtM4":
                return new Vector3(0.021f, -0.012f, 0.581f);
            case "Shotgun":
                return new Vector3(-0.012f, 0.0187f, 0.5498f);
            case "Grenade":
                return Vector3.zero;
            case "Gatling":
                return new Vector3(-0.118f, 0.146f, 1.645f);
            case "SMAW":
                return new Vector3(-0.013f, 0.078f, 1.374f);
            default:
                return Vector3.zero;
        }
    }

    public void changeGunIcon(string name)
    {
        switch (name)
        {
            case "Handgun":
                GunIcon.GetComponent<Image>().sprite = HandgunSprite;
                break;
            case "Uzi":
                GunIcon.GetComponent<Image>().sprite = UziSprite;
                break;
            case "AK":
                GunIcon.GetComponent<Image>().sprite = Ak47Sprite;
                break;
            case "ColtM4":
                GunIcon.GetComponent<Image>().sprite = M4Sprite;
                break;
            case "Shotgun":
                GunIcon.GetComponent<Image>().sprite = ShotgunSprite;
                break;
            case "Grenade":
                GunIcon.GetComponent<Image>().sprite = GrenadeSprite;
                break;
            case "Gatling":
                GunIcon.GetComponent<Image>().sprite = GatlingSprite;
                break;
            case "SMAW":
                GunIcon.GetComponent<Image>().sprite = SmawSprite;
                break;
            default:
                GunIcon.GetComponent<Image>().sprite = HandgunSprite;
                break;
        }
    }

    public int getScore(string name)
    {
        switch (name)
        {
            case "Handgun":
                return 10;
            case "Uzi":
                return 13;
            case "AK":
                return 16;
            case "ColtM4":
                return 17;
            case "Shotgun":
                return 19;
            case "Gatling":
                return 30;
            case "SMAW":
                return 35;
            default:
                return 0;
        }
    }
#endregion

#region Wave
    IEnumerator StartWave(int currentWave)
    {
        spawnLoaded = false;
        setSpawnFinish(false);
        yield return new WaitForSeconds(2f);
        StartCoroutine(WaveStartText(1f));
        waveText.text = currentWave.ToString();
        yield return new WaitForSeconds(waveTimeDelayDefault);
        //Dynamic Spawn Enemy
        StartCoroutine(holdSpawn(currentWave));
    }

    IEnumerator holdSpawn(int currentWave)
    {
        //StartCoroutine(waitSpawn(1, Mathf.FloorToInt(currentWave * Random.Range(1f, 1.5f)), EnemyHandgunPrefab, Random.Range(1f, 2f)));
        StartCoroutine(waitSpawn(1, Mathf.FloorToInt(currentWave * Random.Range(1f, 1.5f)), EnemyHandgunPrefab, Random.Range(3f, 7f)));
        yield return new WaitForSeconds(1f);
        StartCoroutine(waitSpawn(2, Mathf.FloorToInt(currentWave * Random.Range(0.5f, 1f)), EnemyUziPrefab, Random.Range(4f, 8f)));
        yield return new WaitForSeconds(2f);
        StartCoroutine(waitSpawn(3, Mathf.FloorToInt(currentWave * Random.Range(0.08f, 0.5f)), EnemyAkPrefab, Random.Range(5f, 9f)));
        yield return new WaitForSeconds(8f);
        StartCoroutine(waitSpawn(4, Mathf.FloorToInt(currentWave * Random.Range(0.07f, 0.5f)), EnemyM4Prefab, Random.Range(6f, 9f)));
        yield return new WaitForSeconds(9f);
        StartCoroutine(waitSpawn(5, Mathf.FloorToInt(currentWave * Random.Range(0.02f, 0.5f)), EnemyShotgunPrefab, Random.Range(8f, 15f)));
        StartCoroutine(waitSpawn(6, Mathf.FloorToInt(currentWave * Random.Range(0.01f, 0.5f)), EnemyGrenadePrefab, Random.Range(12f, 15f)));
        StartCoroutine(waitSpawn(7, Mathf.FloorToInt(currentWave * Random.Range(0.01f, 0.3f)), EnemyGatlingPrefab, Random.Range(12, 30f)));
        StartCoroutine(waitSpawn(8, Mathf.FloorToInt(currentWave * Random.Range(0.01f, 0.2f)), EnemySMAWPrefab, Random.Range(15f, 30f)));
        spawnLoaded = true;
    }

    IEnumerator waitSpawn(int count, int spawnNumber, GameObject enemy, float delay)
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            yield return new WaitForSeconds(delay);
            bool inRange = true;
            while (inRange)
            {
                tempX = Random.Range(-spawnRange, spawnRange);
                tempZ = Random.Range(-spawnRange, spawnRange);
                spawnLocation = new Vector3(tempX, 10f, tempZ);
                if (!Physics.CheckSphere(spawnLocation, 2f))
                {
                    inRange = false;
                }
            }
            Enemies.Add(Instantiate(enemy, spawnLocation,
            Quaternion.identity));
        }
        spawnFinish[count-1] = true;
    }

    public void spawnBuff(Vector3 position)
    {
        int rand = Random.Range(1, 10);
        if (rand == 1)
        {
            Instantiate(heartBuffPrefab, position, Quaternion.Euler(0f, 0f, 0f));
        }
        if (rand == 2)
        {
            Instantiate(speedBuffPrefab, position, Quaternion.Euler(0f, 0f, 0f));
        }
        if (rand == 3)
        {
            Instantiate(shieldBuffPrefab, position, Quaternion.Euler(0f, 0f, 0f));
        }
    }

    IEnumerator WaveStartText(float wait)
    {
        WaveStartCanvas.SetActive(true);
        yield return new WaitForSeconds(wait);
        WaveStartCanvas.SetActive(false);
    }

    IEnumerator WaveFinishText(float wait)
    {
        WaveClearCanvas.SetActive(true);
        yield return new WaitForSeconds(wait);
        WaveClearCanvas.SetActive(false);
    }

    #endregion

    void setSpawnFinish(bool check)
    {
        for (int i = 0; i < 8; i++)
        {
            spawnFinish[i] = check;
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(spawnLocation, spawnRange);
    //}

    void SpawnProp()
    {
        for (int i = 0; i < Random.Range(minRandomProp, maxRandomProp); i++)
        {
            Instantiate(Crate, new Vector3(Random.Range(-30f, 30f), 10f, Random.Range(-30f, 30f)), Quaternion.identity);
        }
        for (int i = 0; i < Random.Range(minRandomProp, maxRandomProp); i++)
        {
            Instantiate(Barrel, new Vector3(Random.Range(-30f, 30f), 10f, Random.Range(-30f, 30f)), Quaternion.identity);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !GameData.gameEnd){ GameData.pause = !GameData.pause; }
        //Pause OR End
        if (GameData.gameEnd || GameData.pause)
        {
            Time.timeScale = 0;
            Crosshair.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            if (GameData.pause) { PauseScreen.SetActive(true); }
            return;
        }
        else
        {
            Time.timeScale = 1;
            Crosshair.SetActive(true);
#if UNITY_STANDALONE_WIN
            Cursor.lockState = CursorLockMode.Locked;
#endif
            PauseScreen.SetActive(false);
        }

        //Spawn Cube
        //timeDown -= Time.deltaTime;
        //if (timeDown < 0f)
        //{
        //    Cubes.Add(Instantiate(cubePrefab,
        //        new Vector3(Random.Range(-30f, 30f), 10f, Random.Range(-30f, 30f)),
        //        Quaternion.identity));
        //    timeDown = cubeSpawnTime;
        //}
        //Check Waves
        //bool spawnValid = true;
        for (int i = 0; i < spawnFinish.Length; i++)
        {
            if (!spawnFinish[i]){ return; }
        }
        if (spawnLoaded)
        {
            if (Enemies.Count > 0) { return; }
            StartCoroutine(WaveFinishText(1f));
            wave++;
            StartCoroutine(StartWave(wave));
        }
    }

    public void playerDeath()
    {
        //BGMusic.Stop();
        InputName.SetActive(true);
        GameData.gameEnd = true;
    }

    public void finishGame()
    {
        InputName.SetActive(false);
        FinishCanvas.SetActive(true);
        finalNameText.text = playerName;
        finalScoreText.text = GameData.score.ToString();
        //string[] arrayName = GameData.hsName;
        //int[] arrayScore = GameData.hsScore;
        //if (PlayerPrefs.HasKey("score") && PlayerPrefs.HasKey("name"))
        //{
        //    GameData.hsName = PlayerPrefsX.GetStringArray("name");
        //    GameData.hsScore = PlayerPrefsX.GetIntArray("score");
        //    for (int i = 0; i < GameData.hsScore.Length; i++)
        //    {
        //        if (GameData.score >= GameData.hsScore[i])
        //        {
        //            //Cycle Score
        //            int temp = GameData.hsScore[i];
        //            GameData.hsScore[i] = GameData.score;
        //            GameData.hsScore[i + 1] = temp;
        //            //Cycle Name
        //            string tempName = GameData.hsName[i];
        //            GameData.hsName[i] = playerName;
        //            GameData.hsName[i + 1] = tempName;
        //        }
        //        Debug.Log(GameData.hsName[i]);
        //        Debug.Log(GameData.hsScore[i]);
        //    }
        //}
        //PlayerPrefsX.SetStringArray("name", GameData.hsName);
        //PlayerPrefsX.SetIntArray("score", GameData.hsScore);
    }

    public void removeEnemy(GameObject go)
    {
        ParticleSystem effect = Instantiate(DeadEffect, go.transform.position, Quaternion.Euler(0f, 0f, 0f));
        effect.Play();
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].Equals(go))
            {
                GameData.score += getScore(Enemies[i].GetComponent<EnemyController>().gunType.ToString());
                scoreText.text = GameData.score.ToString();
                Enemies.RemoveAt(i);
            }
        }
        StartCoroutine(destroyDeathEffect(effect.gameObject));
        Destroy(go);
    }

    IEnumerator destroyDeathEffect(GameObject go)
    {
        while (go.GetComponent<ParticleSystem>().isEmitting)
        {
            yield return null;
        }
        Destroy(go);
    }

    public void HoldBox(bool isHoldCube)
    {
        if (isHoldCube)
        {
            BoxIcon.GetComponent<Image>().color = Color.white;
        }
        else
        {
            BoxIcon.GetComponent<Image>().color = new Color32(92, 92, 92, 87);
        }
    }

    public string playerName{ get { return _playerName; } set { _playerName = value; } }
}

public enum GunType
{
    Handgun = 1,
    Uzi = 2,
    AK = 3,
    ColtM4 = 4,
    Shotgun = 5,
    Grenade = 6,
    Gatling = 7,
    SMAW = 8,
}