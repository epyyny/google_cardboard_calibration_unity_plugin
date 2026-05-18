//-----------------------------------------------------------------------
// disables VR for Scene 0 and enables it for any scene after Scene 0.
// Attach this script to a GameObject in your initial scene (Scene 0)

// Needed so that startup scene with ruler does not have VR enabled.
//-----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class DisableVR : MonoBehaviour
{
    private bool vrEnabled = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // disable VR for the initial scene (Scene 0) on startup
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            DisableXR();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // if loaded scene is still scene 0, ensure VR is disabled
        if (scene.buildIndex == 0)
        {
            DisableXR();
            vrEnabled = false;
        }
        // if loaded scene is not scene 0, start VR
        else
        {
            // any scene after scene 0 should have VR
            if (!vrEnabled)
            {
                StartCoroutine(StartXR());
                vrEnabled = true;
            }
        }
    }

    private void DisableXR()
    {
        if (XRGeneralSettings.Instance != null && XRGeneralSettings.Instance.Manager != null && XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }
    }

    private System.Collections.IEnumerator StartXR()
    {
        // check if XRGeneralSettings and Manager are available, if not, exit coroutine
        if (XRGeneralSettings.Instance == null || XRGeneralSettings.Instance.Manager == null)
            yield break;

        // check if XR is already initialized and running, if so, skip initialization
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            yield break;

        // initialize and start XR
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            XRGeneralSettings.Instance.Manager.StartSubsystems();

            // wait one frame to ensure everything is ready
            yield return null;

            // recalculate rectangles for the new screen dimensions and DPI
            Google.XR.Cardboard.XRLoader.RecalculateRectangles(Screen.safeArea);
        }
    }
}