using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using System.Collections;

public class StunEffects : MonoBehaviour
{
    public static StunEffects instance;

    public GameObject stunUI;
    public TextMeshProUGUI countdownText;

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

        sdVolumeManager.instance.Initialize();
    }

    public void EnableStunEffects()
    {
        if (stunUI != null)
        {
            stunUI.SetActive(true);
        }

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }

        sdVolumeManager.instance.EnableVignette();
    }

    public void DisableStunEffects()
    {
        if (stunUI != null)
        {
            stunUI.SetActive(false);
        }

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        sdVolumeManager.instance.DisableVignette();
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
