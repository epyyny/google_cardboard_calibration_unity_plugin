using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{

    public void SwapScenes()
    {
        SceneManager.LoadScene(1);
    }
}
