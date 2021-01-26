using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField]
    Sprite deadSprite;

    [SerializeField]
    ParticleSystem particleSystem;


    private bool hasDied;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();

        if (ShouldMonsterDie(collision)) 
        {
            Die();
        }

        
    }

    private bool ShouldMonsterDie(Collision2D collision)
    {

        if (hasDied)
            return false;

        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;


        if (collision.contacts[0].normal.y < -0.5)
            return true;

        return false;
    }

    private void Die()
    {
        hasDied = true;
        GetComponent<SpriteRenderer>().sprite = deadSprite;

        particleSystem.Play();
        
        // gameObject.SetActive(false);
    }
}
