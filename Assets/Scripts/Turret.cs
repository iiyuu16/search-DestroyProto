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

    [SerializeField]
    private float timer = 5f;
    private float bulletTime;

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        ShootAtPlayer();
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;
        if (bulletTime > 0)
        {
            return;
        }

        bulletTime = timer;

        Vector3 directionToPlayer = (player.position - bulletSpawn.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

        GameObject bulletObj = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.velocity = directionToPlayer * bulletSpeed;
        Destroy(bulletObj, 5f);
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
        if(other.tag == "PlayerAttk")
        {
            TakeDamage(plyr.attkDMG);
        }
    }
}
