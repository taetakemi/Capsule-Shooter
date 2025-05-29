using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    public Animation anim;

	void Start () {

        anim.Play();
    }

    void Update () {
        if (!anim.isPlaying)
        {
            SceneManager.LoadScene("MainMenu");
        }
	}
}
