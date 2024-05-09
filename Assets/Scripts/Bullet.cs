using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 5f;
    public int bulletDMG = 1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("player hit");
            Destroy(gameObject);
        }
        else if (other.tag == "PlayerAttk")
        {
            Debug.Log("bullet destroyed");
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(BulletLifetime());

        }
    }

    private IEnumerator BulletLifetime()
    {
        yield return new WaitForSeconds(bulletLife);
        Destroy(gameObject);
    }
}
