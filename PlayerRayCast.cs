using UnityEngine;

public class PlayerRayCast : MonoBehaviour {

    public GameController gameController;
    public Base myBase;
    public GameObject Gun;
    public GameObject player;

    public float distance;

    GameObject liftedGO;
    bool lifting;
    bool rayHit;
    GameObject go;
    RaycastHit[] laserArr;
    LayerMask cubeLayer;

    void Start () {
        cubeLayer = 1 << LayerMask.NameToLayer("Cube");
        //borderLayer = 1 << LayerMask.NameToLayer("BaseBorder");
	}
	
	void Update () {
        if (GameData.gameEnd) { return; }
        if (GameData.pause) { return; }

        //Only Laser Cube
        laserArr = Physics.RaycastAll(transform.position, transform.forward, distance, cubeLayer);
        if(laserArr.Length > 0){ rayHit = true; }
        else { rayHit = false; }

#if UNITY_STANDALONE_WIN
        if (Input.GetButtonDown("Lift"))
        {
            liftObject();
        }

        if (Input.GetButtonDown("Throw") && lifting)
        {
            throwObject();
            return;
        }

        if (Input.GetButtonDown("Action"))
        {
            makeGun();
        }
#endif
    }
    #region Action
    void makeGun()
    {
        if (!rayHit) { return; }
        go = laserArr[0].collider.gameObject;
        if (!go.GetComponent<Cube>()) { return; }
        if (!go.GetComponent<Cube>().atBase) { return; }
        if (myBase.checkCube() > 0)
        {
            //Debug.Log(myBase.checkCube());
            //Check what gun should be instantiate
            string name = gameController.gunChoice(myBase.checkCube()).name;
            Gun.GetComponent<Fire>().resetGun(name);
            Debug.Log(name);
            //Destroy Cube
            myBase.destroyCube();
        }
    }
    public void liftObject()
    {
        if (lifting) { dropObject(); return; }

        if (!rayHit) { return; }
        liftedGO = laserArr[0].collider.gameObject;
        if (!liftedGO) { lifting = false; return; }
        if (!liftedGO.GetComponent<Cube>()) { lifting = false; return; }
        liftedGO.transform.SetParent(player.transform);
        lifting = true;
        liftedGO.transform.localPosition = new Vector3(0f, 1.5f, 1.5f);
        liftedGO.GetComponent<Rigidbody>().isKinematic = true;
        liftedGO.GetComponent<Cube>().atBase = false;
        gameController.HoldBox(true);
    }
    public void throwObject()
    {
        dropObject();
        liftedGO.GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
    }
    void dropObject()
    {
        if (!liftedGO) { lifting = false; return; }
        if (!liftedGO.GetComponent<Cube>()) { lifting = false; return; }
        liftedGO.transform.SetParent(null);
        lifting = false;
        liftedGO.GetComponent<Rigidbody>().isKinematic = false;
        gameController.HoldBox(false);
    }
    #endregion
}