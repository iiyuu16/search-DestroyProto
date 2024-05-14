using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public StunEffects stunEffects;
    public ParticleSystem playerTrail;

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
    public bool isStunned = false;
    private Coroutine recoveryCoroutine;

    public int maxBoosts;
    private int currBoosts;
    public Image[] boostIndicator;
    public Sprite boostIcon;
    public Sprite nullIcon;
    public Slider sliderHP;

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
        stunEffects.DisableStunEffects();
        Debug.Log("starting hp:" + currHP);

        if (sliderHP != null)
        {
            sliderHP.minValue = 0;
            sliderHP.maxValue = maxHP;
            sliderHP.value = maxHP;
            sliderHP.onValueChanged.AddListener(OnHealthChanged);
        }
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
        stunEffects.EnableStunEffects();
        stunEffects.ShowRecoveryTime(recoveryTime);

        float timer = recoveryTime;
        while (timer > 0)
        {
            if (sliderHP != null)
            {
                sliderHP.value = Mathf.RoundToInt(timer);
            }
            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }

        GetComponent<Collider>().enabled = true;
        stunEffects.DisableStunEffects();

        isStunned = false;
        Debug.Log("Player recovered");
        currHP = maxHP;
        UpdateBoostIndicator();
    }

    public void OnHealthChanged(float value)
    {
        currHP = Mathf.RoundToInt(value);
    }

    public void PlayerHit()
    {
        currHP -= 1;
        Debug.Log("hp:" + currHP);
        CamShake.instance.ShakeCamera();
        if (sliderHP != null)
        {
            sliderHP.value = currHP;
        }
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
        playerTrail.Play();

        yield return new WaitForSeconds(boostCD);

        isBoosting = false;
        playerTrail.Stop();
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
