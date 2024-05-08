using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("player hit");
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
