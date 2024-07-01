using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdNDBullet : MonoBehaviour
{
    public float bulletLife = 5f;
    public Renderer bulletSkin;

    public ParticleSystem hitFX;
    public ParticleSystem sparksFX;
    public ParticleSystem flashFX;
    public ParticleSystem fireFX;
    public ParticleSystem smokeFX;

    public Collider col;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player hit");
            sparksFX.Play();
            smokeFX.Play();
            fireFX.Play();
            sdPlayerMovement.instance.PlayerHit();
            col.enabled = false;
            bulletSkin.enabled = false;
            StartCoroutine(DelayDestroy());
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

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}