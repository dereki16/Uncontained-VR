using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public KeycardScanner ks;
    public DeathBoard board;
    public GameObject boardGO;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fingertip"))
        {
            Debug.Log("End game");
            ks.close = true;
            FindObjectOfType<AudioManager>().Play("ElevatorDing");
            FindObjectOfType<AudioManager>().Stop("ElevatorMusic");
            board.gameWon = true;
            boardGO.SetActive(true);
        }
    }
}
