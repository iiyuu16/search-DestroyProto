using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdSoundSource : MonoBehaviour
{
    public AudioSource src;
    public AudioClip hit, hurt, scan, slash, stunned, explosion, recover, boost, reveal;

    public void hitSFX()
    {
        src.PlayOneShot(hit);
    }

    public void hurtSFX()
    {
        src.PlayOneShot(hurt);
    }

    public void scanSFX()
    {
        src.PlayOneShot(scan);
    }

    public void slashSFX()
    {
        src.PlayOneShot(slash);
    }

    public void stunSFX()
    {
        src.clip = stunned;
        src.loop = true;
        src.Play();
    }

    public void stopStunSFX()
    {
        if (src.clip == stunned && src.isPlaying)
        {
            src.Stop();
            src.loop = false;
        }
    }

    public void explosionSFX()
    {
        src.PlayOneShot(explosion);
    }

    public void recoverSFX()
    {
        src.PlayOneShot(recover);
    }

    public void boostSFX()
    {
        src.PlayOneShot(boost);
    }

    public void revealSFX()
    {
        src.PlayOneShot(reveal);
    }
}
