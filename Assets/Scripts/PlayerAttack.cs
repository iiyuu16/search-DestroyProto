using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public ParticleSystem slashFX;
    public Collider attkCol;
    public float cooldownTime = 0.1f;
    public float colliderDisableTime = 0.1f;
    private float cooldownTimer;
    public int attkDMG = 1;
    public KeyCode Attack;

    public PlayerMovement playerMovement;
    public soundSource sfx;

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

        if (!playerMovement.isStunned && cooldownTimer <= 0f && Input.GetKey(Attack))
        {
            slashFX.Play();
            sfx.slashSFX();
            attkCol.enabled = true;

            Invoke("DisableCollider", colliderDisableTime);

            cooldownTimer = cooldownTime;
        }
    }

    void DisableCollider()
    {
        attkCol.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ScoreManager.instance.AddScore(100);
        }
        else if (other.CompareTag("Shield"))
        {
            ScoreManager.instance.AddScore(500);
        }
        else if (other.CompareTag("Target"))
        {
            ScoreManager.instance.AddScore(1500);
        }
        else if (other.CompareTag("Turret"))
        {
            ScoreManager.instance.AddScore(1000);
        }
    }
}
