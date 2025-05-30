using Characters.AnimationHandler;
using Manager;
using TMPro;
using UnityEngine;

public class LogInputKey : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textMesh.text = RollbackManager.Manager.CurrentFrame.ToString();
    }
}