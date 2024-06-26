using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager instance;

    public VolumeProfile volumeProfile; // Reference to the global volume profile

    private Vignette vignette; // Reference to the vignette effect

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Initialize();
    }

    public void Initialize()
    {
        if (volumeProfile == null)
        {
            Debug.LogError("Volume Profile is not assigned to VolumeManager!");
            return;
        }

        // Try to get the vignette effect from the volume profile
        if (!volumeProfile.TryGet(out vignette))
        {
            Debug.LogError("Vignette effect is not found in the assigned Volume Profile!");
        }
    }

    public void EnableVignette()
    {
        if (vignette != null)
        {
            vignette.active = true;
        }
    }

    public void DisableVignette()
    {
        if (vignette != null)
        {
            vignette.active = false;
        }
    }
}
