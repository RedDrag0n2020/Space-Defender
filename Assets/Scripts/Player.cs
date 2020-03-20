using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // configuration parameters

    [Header("Player")]

    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float xPadding = 0.9f;
    [SerializeField] float yPadding = 0.9f;
    [SerializeField] int health = 200;
    [SerializeField] GameObject deathVFXPlayer;
    [SerializeField] float durationOfExplosionPlayer = 1f;
    [SerializeField] AudioClip explosionAudioClipPlayer;
    [SerializeField] float explosionVolumePlayer = 0.7f;

    [Header("Projectile")]

    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] AudioClip laserFireAudioClip;
    [SerializeField] float laserFireVolume = 0.4f;

    Coroutine firingCoroutine;

    // state variables

    float xMin;
    float xMax;
    float yMin;
    float yMax;




    // Use this for initialization
    void Start()
    {

        SetUpMoveBoundaries();

    }



    // Update is called once per frame
    void Update()
    {

        Move();
        Fire();

    }



    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        else if (Input.GetButtonUp("Fire1"))
        {

            StopCoroutine(firingCoroutine);
        }


    }

    IEnumerator FireContinuously()

    {
        while (true)

        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            AudioSource.PlayClipAtPoint(laserFireAudioClip, Camera.main.transform.position, laserFireVolume);

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        //var deltaX = Input.GetAxis("LeftJoystickHorizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);


    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if(!damageDealer) { return; }
        
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
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFXPlayer, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosionPlayer);
        AudioSource.PlayClipAtPoint(explosionAudioClipPlayer, Camera.main.transform.position, explosionVolumePlayer);
        

    }
}
