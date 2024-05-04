using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public ParticleSystem slashFX;
    public Collider attkCol;
    public float cooldownTime = 1f;
    public float colliderDisableTime = 0.5f;
    private float cooldownTimer;

    void Start()
    {
        attkCol = GetComponent<Collider>();
        attkCol.enabled = false;
        cooldownTimer = 0f;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f && Input.GetKeyDown(KeyCode.K))
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
