using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform player;
    public GameObject dBulletPrefab; // Destroyable bullet prefab
    public GameObject ndBulletPrefab; // Non-destroyable bullet prefab
    public Transform bulletSpawn;
    public float bulletSpeed;
    public int maxHP = 5;
    private int currentHP;
    public PlayerAttack plyr;
    public Renderer turretSkin;

    public ParticleSystem hitFX;
    public ParticleSystem sparksFX;
    public ParticleSystem flashFX;
    public ParticleSystem fireFX;
    public ParticleSystem smokeFX;

    public float fireRate = 0.5f;
    private float nextFireTime;
    private bool fireDestroyableBullet = true; // Track which bullet type to fire next

    void Start()
    {
        currentHP = maxHP;
        nextFireTime = 0f;
        turretSkin.enabled = true;
    }

    void Update()
    {
        if (Time.time >= nextFireTime && !PlayerMovement.instance.isStunned)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + 1 / fireRate;
        }
    }

    void ShootAtPlayer()
    {
        Vector3 directionToPlayer = (player.position - bulletSpawn.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

        // Alternate between destroyable and non-destroyable bullets
        GameObject bulletPrefab = fireDestroyableBullet ? dBulletPrefab : ndBulletPrefab;
        GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.velocity = directionToPlayer * bulletSpeed;

        fireDestroyableBullet = !fireDestroyableBullet; // Toggle bullet type for next shot

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
            turretSkin.enabled = false;
            StartCoroutine(DelayDestroy());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAttk")
        {
            TakeDamage(plyr.attkDMG);
        }
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
