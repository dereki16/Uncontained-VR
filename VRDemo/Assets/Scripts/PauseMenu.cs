using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject leftRay;
    public GameObject rightRay;

    public string scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene().name;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        leftRay.SetActive(true);
        rightRay.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        pauseCanvas.SetActive(false);
        leftRay.SetActive(false);
        rightRay.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        PointsFromBullets.points = 0;
        PointsFromBullets.totalPoints = 0;
        if (scene == "Tutorial")
            SceneManager.LoadScene("Tutorial");
        else 
            SceneManager.LoadScene("ZombieScene");
    }

    public void Home()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        PointsFromBullets.points = 0;
        PointsFromBullets.totalPoints = 0;
        SceneManager.LoadScene("RoomLayout");
    }
}
