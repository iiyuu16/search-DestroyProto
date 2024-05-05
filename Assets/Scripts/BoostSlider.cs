using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoostSlider : MonoBehaviour
{
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    public Slider slider; // Reference to the UI slider
    public float decreaseRate = 1f; // Rate at which boost decreases while active
    public float regenRate = 3f; // Rate at which boost regenerates after use
    public float regenDelay = 0.5f; // Delay before boost starts regenerating after use

    private Coroutine regenCoroutine; // Reference to the coroutine for boost regeneration

    void Update()
    {
        // Check if the playerMovement script is assigned and the slider exists
        if (playerMovement != null && slider != null)
        {
            if (playerMovement.isBoosting)
            {
                if (slider.value > 0)
                {
                    slider.value -= decreaseRate * Time.deltaTime; // Decrease slider value during boost
                }
                else
                {
                    playerMovement.isBoosting = false; // Disable boosting if slider value reaches 0
                }
            }
            else if (slider.value < 100)
            {
                StartRegeneration(); // Start boost regeneration if the boost is not active and the slider value is less than 100
            }
        }
    }

    // Method to start boost regeneration
    private void StartRegeneration()
    {
        if (regenCoroutine == null)
        {
            regenCoroutine = StartCoroutine(RegenerateBoost());
        }
    }

    // Coroutine for boost regeneration
    private IEnumerator RegenerateBoost()
    {
        yield return new WaitForSeconds(regenDelay); // Wait for the regen delay before starting regeneration

        while (slider.value < 100)
        {
            slider.value = Mathf.MoveTowards(slider.value, 100f, regenRate * Time.deltaTime); // Increase slider value gradually
            yield return null;
        }

        regenCoroutine = null; // Reset the coroutine reference
    }
}
