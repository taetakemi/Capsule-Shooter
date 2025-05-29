using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour {

    public GameController gameController;

    private void Start()
    {
        gameObject.GetComponent<InputField>().Select();
    }
    void Update () {
        if (Input.GetButtonDown("Submit"))
        {
            OnNext();
        }
    }

    public void OnNext()
    {
        GetComponent<AudioSource>().Play();
        gameController.playerName = gameObject.GetComponent<InputField>().text;
        gameController.finishGame();
    }
}
