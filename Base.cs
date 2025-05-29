using UnityEngine;

public class Base : MonoBehaviour {

    public GameController gameController;
    CubeSpawner cubeSpawner;
    public GameObject[] cubeTiles;
    float distance = 2f;
    RaycastHit laser;
    RaycastHit[] laserArr;
    bool rayHit;
    LayerMask cubeLayer;

    private void Start()
    {
        cubeLayer = 1 << LayerMask.NameToLayer("Cube");
        cubeSpawner = GameObject.Find("CubeSpawner").GetComponent<CubeSpawner>();
    }

    void Update()
    {
        //if (Physics.Raycast(transform.position, transform.forward, out laser, distance))
        //{
        //    if (Input.GetButtonDown("Lift"))
        //    {
        //        go = laser.collider.gameObject;
        //        go.transform.position = Vector3.Lerp(go.transform.position, go.transform.up+go.transform.position, Time.deltaTime/1f );
        //    }
        //}
        //return;

        if (GameData.pause) { return; }
        //Set atBase value for Cube
        for (int i = 0; i < cubeTiles.Length; i++)
        {
            laserArr = Physics.RaycastAll(cubeTiles[i].transform.position, Vector3.up, distance, cubeLayer.value);
            Debug.DrawRay(cubeTiles[i].transform.position, Vector3.up * distance, Color.cyan, 1f);
            for (int a = 0; a < laserArr.Length; a++)
            {
                if (laserArr[a].collider.gameObject.transform.parent)
                {
                    if (laserArr[a].collider.gameObject.transform.parent.name == "Player"){ break; }
                }
                laserArr[a].collider.gameObject.GetComponent<Cube>().atBase = true;
                //laserArr[a].collider.gameObject.transform.position =
                //    cubeTiles[i].transform.position + new Vector3(0f, 0.5f + a, 0f);
                laserArr[a].collider.gameObject.transform.position = Vector3.Slerp(laserArr[a].collider.gameObject.transform.position,
                    cubeTiles[i].transform.position + new Vector3(0f, 0.5f + a, 0f),0.1f);
                laserArr[a].collider.gameObject.transform.rotation = Quaternion.Slerp(cubeTiles[i].transform.rotation, 
                    Quaternion.Euler(0f, 0f, 0f),0.1f);
            }
        }

    }

    public int checkCube()
    {
        int baseCube = 0;
        //Check how many cube
        Debug.Log("checkCUbe");
        for (int i = 0; i < cubeTiles.Length; i++)
        {
            laserArr = Physics.RaycastAll(cubeTiles[i].transform.position, Vector3.up * distance, distance, cubeLayer.value);
            for (int a = 0; a < laserArr.Length; a++)
            {
                baseCube += 1;
                rayHit = true;
                //Destroy(laserArr[a].collider.gameObject);
            }
            //if (Physics.Raycast(cubeTiles[i].transform.position, Vector3.up*distance, out laser, distance))
            //{
            //    if (laser.collider.gameObject.GetComponent<Cube>())
            //    {
            //        baseCube += 1;
            //        rayHit = true;
            //        Destroy(laser.collider.gameObject);
            //    }
            //}
        }
        if (rayHit)
        {
            return baseCube;
        }
        return 0;
        //Destroy Cube after use
        //if (baseCube == 3)
        //{
        //    for (int i = 0; i < distance; i++)
        //    {
        //        if (Physics.Raycast(cubeTiles[i].transform.position, Vector3.up, out laser, distance))
        //        {
        //            Destroy(laser.collider.gameObject);
        //        }
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < baseCube; i++)
        //    {
        //        if (Physics.Raycast(cubeTiles[i].transform.position, Vector3.up, out laser, distance))
        //        {
        //            Debug.Log(baseCube);
        //            Destroy(laser.collider.gameObject);
        //        }
        //    }
        //}
    }

    public void destroyCube()
    {
        for (int i = 0; i < cubeTiles.Length; i++)
        {
            laserArr = Physics.RaycastAll(cubeTiles[i].transform.position, Vector3.up * 4, distance, cubeLayer.value);
            for (int a = 0; a < laserArr.Length; a++)
            {
                //if (laserArr[a].collider.gameObject.GetComponent<Cube>())
                //{
                for (int b = 0; b < cubeSpawner.cubePool.Count; b++)
                {
                    if (cubeSpawner.cubePool[b].Equals(laserArr[a].collider.gameObject))
                    {
                        //Debug.Log("Cube Destroyed");
                        //cubeSpawner.cubePool.RemoveAt(b);
                        //Destroy(laserArr[a].collider.gameObject);
                        cubeSpawner.DestroyCube(laserArr[a].collider.gameObject);
                        break;
                    }
                }
                //}
            }
        }
    }

    //public void checkCubeAtBase()
    //{
    //    for (int i = 0; i < cubeTiles.Length; i++)
    //    {
    //        laserArr = Physics.RaycastAll(cubeTiles[i].transform.position, Vector3.up, distance * 2f, cubeLayer.value);
    //        for (int a = 0; a < laserArr.Length; a++)
    //        {
    //            int number;
    //            for (int c = 0; c < gameController.Cubes.Count; c++)
    //            {
    //                if (gameController.Cubes[c].GetComponent<Cube>().atBase)
    //                {
    //                    if (gameController.Cubes[c].Equals(laserArr[a].collider.gameObject))
    //                    {
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}
