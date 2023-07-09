using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{

    private IDamagable enemyHitData;

    public AudioSource audioPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetEnemy(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveEnmyRefrance(collision);
    }

    /// <summary>
    /// stores the refrance of the enemy, when they enter your hit box 
    /// </summary>
    /// <param name="collision"></param>
    public void GetEnemy(Collider2D collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

        if (damagable != null)
        {
            enemyHitData = damagable;
        }
        else
        {
            Debug.Log(" target does not posses Idamagble");
        }
    }

    /// <summary>
    /// removes the enemy refrance if they leave the hit detection
    /// </summary>
    public void RemoveEnmyRefrance(Collider2D collision)
    {
        enemyHitData = null;

    }

    public void AattackTheEnemy()
    {
        if(enemyHitData != null)
        {
            enemyHitData.TakeDamage(5);
            enemyHitData = null;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            audioPlayer.Play();
        }
    }
}
