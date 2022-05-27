using UnityEngine;

public class CombinationRandomizer : MonoBehaviour
{
    public int combo;
    void Awake()
    {
        combo = Random.Range(100, 999);
    }
}
