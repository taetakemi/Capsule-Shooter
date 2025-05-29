using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Status : MonoBehaviour {

    public int health;
    public int maxHealth;

    public GameController gameController;
    public Image bloodImage;
    public Text hpUI;
    bool shield = false;

    void Start() {
        hpUI.text = health.ToString();
    }

    public void deductHealth(int damage) {
        if (shield){return; }
        health -= damage;
        hpUI.text = health.ToString();
        if (tag == "Player")
        {
            if (health <= 0)
            {
                gameController.playerDeath();
                return;
            }
            StartCoroutine(PlayerDamaged(2f));
        }
    }

    IEnumerator PlayerDamaged(float seconds)
    {
        bloodImage.color = new Color(1f,1f,1f,0.8f);
        float elapsed = 0f;
        float alpha = 1f;
        while (elapsed >= 0f)
        {
            if (elapsed >= seconds)
            {
                break;
            }
            bloodImage.color = new Color(1f,1f,1f,alpha);
            elapsed += Time.deltaTime;
            alpha -= Time.deltaTime/seconds;
            yield return null;
        }
        bloodImage.color = Color.clear;
    }

    //Buff
    public void getHeart()
    {
        health += 20;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        hpUI.text = health.ToString();
    }

    public IEnumerator getShield()
    {
        gameController.shieldIcon.SetActive(true);
        gameController.ShieldEffect.SetActive(true);
        shield = true;
        yield return new WaitForSeconds(15f);
        shield = false;
        gameController.shieldIcon.SetActive(false);
        gameController.ShieldEffect.SetActive(false);
    }
}