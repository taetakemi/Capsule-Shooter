using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyStatus : MonoBehaviour
{
    public int health = 10;

    public void deductHealth(int damage)
    {
        health -= damage;
    }
}