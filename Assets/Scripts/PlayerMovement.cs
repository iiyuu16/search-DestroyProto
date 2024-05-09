using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float acceleration = 5f;
    public float deceleration = 10f;
    public float boostSpeed = 10f;
    public float boostCD = 3f;
    public float boostRegenDelay = 10f; // Time delay for boost regeneration
    private float currentSpeed = 0f;

    private bool isBraking = false;
    private bool isBoosting = false;

    public int maxBoosts;
    private int currBoosts;
    public Image[] boostIndicator;
    public Sprite boostIcon;
    public Sprite nullIcon;

    public KeyCode BoostKey;

    private void Start()
    {
        currBoosts = maxBoosts;
        UpdateBoostIndicator();
        StartCoroutine(BoostRegeneration());
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        ApplyBrake();
        HandleBoost();
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
