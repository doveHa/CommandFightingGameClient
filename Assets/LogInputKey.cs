using Characters.AnimationHandler;
using TMPro;
using UnityEngine;

public class LogInputKey : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    public GameObject Opponent;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        Destroy(Opponent.GetComponentInChildren<NaktisAnimationHandler>());
        Destroy(Opponent.GetComponentInChildren<SendKey>());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            textMesh.text += "↓ ";
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            textMesh.text += "↑ ";
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            textMesh.text += "→ ";
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            textMesh.text += "← ";
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            textMesh.text += "Space ";
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            textMesh.text += "Z ";
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            textMesh.text += "X ";
        }

        if (Input.GetKeyDown(KeyCode.Slash))
        {
            textMesh.text = string.Empty;
        }

        new WaitForSeconds(1f);
    }
}