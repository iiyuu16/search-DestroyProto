using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class sdVolumeManager : MonoBehaviour
{
    public static sdVolumeManager instance;

    public VolumeProfile volumeProfile;
    private Vignette vignette;

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
