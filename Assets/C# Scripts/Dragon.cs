using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dragon : MonoBehaviour
{
    private int HP = 100;
    public Slider healthBar;
    public Animator animator;

    void Update()
    {
        healthBar.value = HP;
    }

    public void TakeDamage(int damgeAmount)
    {
        HP -= damgeAmount;
        if (HP <= 0)
        {
            AudioManager.instance.Play("DragonDeath");
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 1.0f);
        }
        else
        {
            AudioManager.instance.Play("DragonDamage");
            animator.SetTrigger("damage");
        }
    }


}
