using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyscript : MonoBehaviour,IDamagable
{
    public int health;
 

    public void TakeDamage()
    {
       Health();
    }

   
    public void PlayDeath()
    {
        Debug.Log("im dead");
    }
    public void Health()
    {
        health--;

        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
