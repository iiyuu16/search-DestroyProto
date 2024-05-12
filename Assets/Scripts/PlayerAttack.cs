using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public ParticleSystem slashFX;
    public Collider attkCol;
    public float cooldownTime = .1f;
    public float colliderDisableTime = .1f;
    private float cooldownTimer;
    public int attkDMG = 1;
    public KeyCode Attack;

    public PlayerMovement playerMovement; // Reference to PlayerMovement script

    void Start()
    {
        attkCol = GetComponent<Collider>();
        attkCol.enabled = false;
        cooldownTimer = 0f;

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement reference not set!");
        }
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        // Check if the player is not stunned and the attack input is pressed
        if (!playerMovement.isStunned && cooldownTimer <= 0f && Input.GetKey(Attack))
        {
            slashFX.Play();
            attkCol.enabled = true;

            Invoke("DisableCollider", colliderDisableTime);

            cooldownTimer = cooldownTime;
        }
    }

    void DisableCollider()
    {
        attkCol.enabled = false;
    }
}
