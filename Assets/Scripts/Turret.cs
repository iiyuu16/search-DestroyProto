using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform player;
    public GameObject bullet;
    public Transform bulletSpawn;
    public float bulletSpeed;
    public int maxHP = 5;
    private int currentHP;
    public PlayerAttack plyr;

    public ParticleSystem hitFX;
    public ParticleSystem sparksFX;
    public ParticleSystem flashFX;
    public ParticleSystem fireFX;
    public ParticleSystem smokeFX;

    public float fireRate = 0.5f; // Adjust the fire rate as needed
    private float nextFireTime;

    void Start()
    {
        currentHP = maxHP;
        nextFireTime = 0f;
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + 1 / fireRate; // Calculate the next fire time based on the fire rate
        }
    }

    void ShootAtPlayer()
    {
        Vector3 directionToPlayer = (player.position - bulletSpawn.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

        // Fire multiple bullets
        for (int i = 0; i < 3; i++)
        {
            GameObject bulletObj = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
            Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
            bulletRig.velocity = directionToPlayer * bulletSpeed;
            Destroy(bulletObj, 5f);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        hitFX.Play();

        Debug.Log("Turret HP: " + currentHP);

        if (currentHP <= 0)
        {
            Debug.Log("Turret destroyed!");
            sparksFX.Play();
            smokeFX.Play();
            flashFX.Play();
            fireFX.Play();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAttk")
        {
            TakeDamage(plyr.attkDMG);
        }
    }
}
