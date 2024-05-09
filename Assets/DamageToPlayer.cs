using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    public PlayerHealth playerHP;
    private float i = 0f;
    private float resetTime = 4f;

    private void Update()
    {
        i += Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if(i > resetTime && other.CompareTag("Player"))
        {
            playerHP.health -= 1;
            i = 0f;
        }
    }
}
