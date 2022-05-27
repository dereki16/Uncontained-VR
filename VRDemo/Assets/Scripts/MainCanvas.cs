using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvas : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainMenu");
    }
    public void Play()
    {
        FindObjectOfType<AudioManager>().Stop("MainMenu");
        SceneManager.LoadScene("ZombieScene");
    }

    public void Tutorial()
    {
        FindObjectOfType<AudioManager>().Stop("MainMenu");
        SceneManager.LoadScene("Tutorial");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
