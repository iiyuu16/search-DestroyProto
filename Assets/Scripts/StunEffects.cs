using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using System.Collections;

public class StunEffects : MonoBehaviour
{
    public static StunEffects instance;

    public VolumeProfile volumeProfile; // Reference to the global volume profile
    public GameObject stunUI; // Reference to the UI object for stun effect
    public TextMeshProUGUI countdownText; // Reference to the TextMeshPro object for countdown

    private Vignette vignette; // Reference to the vignette effect

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Initialize();
    }

    private void Initialize()
    {
        if (volumeProfile == null)
        {
            Debug.LogError("Volume Profile is not assigned to StunEffectsManager!");
            return;
        }

        volumeProfile.TryGet(out vignette); // Try to get the vignette effect from the volume profile
    }

    public void EnableStunEffects()
    {
        if (vignette != null)
        {
            vignette.active = true;
        }

        if (stunUI != null)
        {
            stunUI.SetActive(true);
        }

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }
    }

    public void DisableStunEffects()
    {
        if (vignette != null)
        {
            vignette.active = false;
        }

        if (stunUI != null)
        {
            stunUI.SetActive(false);
        }

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }
    }

    public void ShowRecoveryTime(float recoveryTime)
    {
        if (countdownText != null)
        {
            StartCoroutine(UpdateRecoveryTime(recoveryTime));
        }
    }

    private IEnumerator UpdateRecoveryTime(float recoveryTime)
    {
        float timer = recoveryTime;

        while (timer > 0)
        {
            countdownText.text = "==" + Mathf.CeilToInt(timer) + "==";
            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }

        countdownText.text = "";
    }
}
