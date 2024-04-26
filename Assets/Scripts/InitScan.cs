using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScan : MonoBehaviour
{
    public static InitScan instance;
    public ParticleSystem scanner;
    public float duration = 3;
    public float size = 30;

    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scanner.Play();
        }
    }

}
