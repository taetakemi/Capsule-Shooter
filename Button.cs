using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

    public GameObject loadingScreen;

    //void Start()
    //{
    //    loadingScreen = GameObject.Find("loadingScreen");
    //}

    public void OnPlay()
    {
        GetComponent<AudioSource>().Play();
        loadingScreen.GetComponent<Loading>().loading("MainPlay");
        resetGameData();
        //SceneManager.LoadScene("MainPlay");
    }
    public void OnMenu()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("MainMenu");
    }
    public void OnQuit()
    {
        GetComponent<AudioSource>().Play();
        Application.Quit();
    }
    public void OnCredit()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Credits");
    }
    public void OnResume()
    {
        GetComponent<AudioSource>().Play();
        GameData.pause = false;
    }

    void resetGameData()
    {
        GameData.gameEnd = false;
        GameData.pause = false;
        GameData.score = 0;
    }
}
