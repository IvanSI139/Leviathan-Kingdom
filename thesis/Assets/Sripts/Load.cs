using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{

    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = (currentIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }
}
