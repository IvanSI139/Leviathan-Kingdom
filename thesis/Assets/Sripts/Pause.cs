using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject menu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        //Debug.Log("Game Paused");
        menu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        //Debug.Log("Game Resumed");
        menu.SetActive(false);
    }

    public void Quit()
    {
        //Debug.Log("Quit Game");
        Application.Quit();
    }


}
