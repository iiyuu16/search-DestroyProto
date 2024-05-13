using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using System.Collections;

public class StunEffects : MonoBehaviour
{
    public static StunEffects instance;

    public GameObject stunUI; // Reference to the UI object for stun effect
    public TextMeshProUGUI countdownText; // Reference to the TextMeshPro object for countdown

    public RenderPipelineAsset blitRenderer; // Reference to the custom Blit renderer feature

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
        // Try to find the Blit renderer feature in the renderer features list of the URP asset
        UniversalRenderPipelineAsset urpAsset = GraphicsSettings.renderPipelineAsset as UniversalRenderPipelineAsset;
        if (urpAsset != null)
        {
            foreach (var rendererFeature in urpAsset.rendererFeatures)
            {
                if (rendererFeature is Blit && rendererFeature.name == "SDurp Renderer")
                {
                    blitRenderer = rendererFeature as Blit;
                    break;
                }
            }
        }

        if (blitRenderer == null)
        {
            Debug.LogError("Blit renderer feature 'SDurp Renderer' is not found in the URP asset!");
        }
    }

    public void EnableStunEffects()
    {
        // Enable the Blit renderer feature
        if (blitRenderer != null)
        {
            blitRenderer.settings.Event = RenderPassEvent.AfterRendering; // Set the render pass event
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
        // Disable the Blit renderer feature
        if (blitRenderer != null)
        {
            blitRenderer.settings.Event = RenderPassEvent.BeforeRendering; // Set the render pass event
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
