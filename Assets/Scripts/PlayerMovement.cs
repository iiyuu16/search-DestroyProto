using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement playerMovement;

    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float acceleration = 5f;
    public float deceleration = 10f;
    public float boostSpeed = 10f;
    public float boostCD = 3f;
    public float boostRegenDelay = 10f;
    private float currentSpeed = 0f;
    public int maxHP;
    private int currHP;

    public Bullet bullet;

    private bool isBraking = false;
    private bool isBoosting = false;

    public int maxBoosts;
    private int currBoosts;
    public Image[] boostIndicator;
    public Sprite boostIcon;
    public Sprite nullIcon;

    public KeyCode BoostKey;




    private void Awake()
    {
        if(playerMovement == null)
        {
            playerMovement = this;
        }
        else
            Destroy(gameObject);
    }




    private void Start()
    {
        currBoosts = maxBoosts;
        currHP = maxHP;
        UpdateBoostIndicator();
        StartCoroutine(BoostRegeneration());
        Debug.Log("starting hp:" + currHP);
    }


    void Update()
    {
        HandleMovement();
        HandleRotation();
        ApplyBrake();
        HandleBoost();
        HandleLife();
        recoveryTime();
    }

    private void HandleLife()
    {
        if(currHP <= 0)
        {
            //apply stunned state - player is invincible for 3s then hp is back to 3 again
        }
    }

    public void PLayerHit()
    {
        currHP -= 1;
        Debug.Log("starting hp:" + currHP);

        //add screen shake here

    }


    private IEnumerator recoveryTime()
    {
        yield return new WaitForSeconds(5);
        //make player return to normal state
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!isBraking)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
            else
            {
                isBraking = false;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            isBraking = true;
        }

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }

    private void ApplyBrake()
    {
        if (isBraking)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, moveSpeed);
        }
    }

    private void HandleBoost()
    {
        if (Input.GetKeyDown(BoostKey) && !isBoosting && currBoosts > 0)
        {
            StartCoroutine(ActivateBoost());
        }
    }

    private IEnumerator ActivateBoost()
    {
        isBoosting = true;
        currentSpeed += boostSpeed;
        currBoosts--;
        UpdateBoostIndicator();

        yield return new WaitForSeconds(boostCD);

        isBoosting = false;
        currentSpeed -= boostSpeed;
    }

    private IEnumerator BoostRegeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(boostRegenDelay);

            if (currBoosts < maxBoosts)
            {
                currBoosts++;
                UpdateBoostIndicator();
            }
        }
    }

    private void UpdateBoostIndicator()
    {
        for (int i = 0; i < boostIndicator.Length; i++)
        {
            if (i < currBoosts)
            {
                boostIndicator[i].sprite = boostIcon;
            }
            else
            {
                boostIndicator[i].sprite = nullIcon;
            }
        }
    }
}
