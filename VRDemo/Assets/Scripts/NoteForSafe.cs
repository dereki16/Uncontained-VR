using UnityEngine;
using TMPro;

public class NoteForSafe : MonoBehaviour
{
    public TextMeshProUGUI note;
    public CombinationRandomizer cr;
    void Start()
    {
        note.text = "\n   Boss' code: \n   " + cr.combo.ToString() + " \n\t";
    }
}
