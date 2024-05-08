using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform player;
    public GameObject bullet;
    public Transform bulletSpawn;
    public float bulletSpeed;

    [SerializeField]
    private float timer = 5f;
    private float bulletTime;

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

        // Calculate the direction from the turret to the player
        Vector3 directionToPlayer = (player.position - bulletSpawn.position).normalized;

        // Rotate the turret to face the player
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

        // Instantiate and shoot the bullet in the direction of the player
        GameObject bulletObj = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.velocity = directionToPlayer * bulletSpeed;
        Destroy(bulletObj, 5f);
    }

}
