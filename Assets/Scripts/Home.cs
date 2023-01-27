using UnityEngine.SceneManagement;
using UnityEngine;

public class Home : MonoBehaviour
{
    public void EnterScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
