using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float acceleration = 5f;
    public float deceleration = 10f;
    public float boostSpeed = 10f;
    public float boostCD = 3f;
    public float recoveryTime = 5f;
    public float boostRegenDelay = 10f;
    private float currentSpeed = 0f;
    public int maxHP;
    private int currHP;

    public Bullet bullet;

    private bool isBraking = false;
    private bool isBoosting = false;
    private bool isStunned = false;
    private Coroutine recoveryCoroutine;

    public int maxBoosts;
    private int currBoosts;
    public Image[] boostIndicator;
    public Sprite boostIcon;
    public Sprite nullIcon;

    public KeyCode BoostKey;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        if (!isStunned)
        {
            HandleMovement();
            HandleRotation();
            ApplyBrake();
            HandleBoost();
        }
        HandleLife();
        RecoveryState();
    }

    private void HandleLife()
    {
        if (currHP <= 0 && !isStunned)
        {
            isStunned = true;
            Debug.Log("Stunned state activated");

            currentSpeed = 0f;

            recoveryCoroutine = StartCoroutine(RecoveryState());
        }
    }

    private IEnumerator RecoveryState()
    {
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(recoveryTime);

        GetComponent<Collider>().enabled = true;

        isStunned = false;
        Debug.Log("Player recovered");
        currHP = maxHP;
        UpdateBoostIndicator();

        if (recoveryCoroutine != null)
        {
            StopCoroutine(recoveryCoroutine);
        }
    }


    public void PLayerHit()
    {
        currHP -= 1;
        Debug.Log("hp:" + currHP);
        CamShake.instance.ShakeCamera();

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
