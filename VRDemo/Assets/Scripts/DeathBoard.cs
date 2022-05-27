using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class DeathBoard : MonoBehaviour
{
    public GameObject DeathCanvas;
    public int zombiesKilled;
    public int roundSurvived;

    public TextMeshProUGUI zombiesKilledText;
    public TextMeshProUGUI pointsAcquiredText;
    public TextMeshProUGUI roundSurvivedText;

    public GameObject leftRay;
    public GameObject rightRay;

    public ContinuousMovement movement;

    public bool gameWon;
    public string scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene().name;

        if (gameWon)
            // play other song
            FindObjectOfType<AudioManager>().Play("WinGame");
        else if (!gameWon)
            FindObjectOfType<AudioManager>().Play("AimByTaheda");

        zombiesKilled = ZombieAction.zombiesKilled;
        roundSurvived = ZombieSpawner.wave;
        zombiesKilledText.text = "Zombies Killed          " + zombiesKilled;
        pointsAcquiredText.text = "Points Acquired           " + PointsFromBullets.totalPoints;
        roundSurvivedText.text = "After Surviving " + roundSurvived + " Rounds";
        leftRay.SetActive(true);
        rightRay.SetActive(true);
        movement.speed = 0;
    }

    public void Restart()
    {
        if (!gameWon)
            FindObjectOfType<AudioManager>().Stop("AimByTaheda");
        else if (gameWon)
            FindObjectOfType<AudioManager>().Stop("WinGame");
        ZombieAction.zombiesKilled = 0;
        ZombieSpawner.wave = 1;
        PointsFromBullets.points = 0;
        PointsFromBullets.totalPoints = 0;
        if (scene == "Tutorial")
            SceneManager.LoadScene("Tutorial");
        else
            SceneManager.LoadScene("ZombieScene");
    }

    public void Home()
    {
        PointsFromBullets.points = 0;
        PointsFromBullets.totalPoints = 0;
        if (!gameWon)
            FindObjectOfType<AudioManager>().Stop("AimByTaheda");
        else if (gameWon)
            FindObjectOfType<AudioManager>().Stop("WinGame");
        SceneManager.LoadScene("RoomLayout");
    }
}
