using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

   [Header("Enemy Stats")]

    [SerializeField] float health = 100f;
    [SerializeField] int scoreValue = 150;

    [Header("Shooting")]

    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Sound Effects")]

    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip explosionAudioClip;
    [SerializeField] float explosionVolume = 0.7f;
    [SerializeField] AudioClip laserFireAudioClip;
    [SerializeField] float laserFireVolume = 0.4f;



    // Use this for initialization
    void Start ()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

	}
	
	// Update is called once per frame
	void Update () 
    {
        CountDownandShoot();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) { return; }

        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        damageDealer.Hit();

        if (health <= 0)

        {
            Die();

        }
    }

    private void Die()
    {
        FindObjectOfType<GameController>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(explosionAudioClip, Camera.main.transform.position, explosionVolume);
    }

    private void CountDownandShoot()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0f)

        {
            Fire();

            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()

    {
        GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;

        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);

        AudioSource.PlayClipAtPoint(laserFireAudioClip, Camera.main.transform.position, laserFireVolume);




    }

   
   
    
}
