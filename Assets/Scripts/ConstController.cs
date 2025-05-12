using UnityEngine;

public class ConstController : MonoBehaviour
{
    public static ConstController Manager { get; private set; }
    public float NaktisFlyYPosition = 0.25f;
    public float DurationTime = 4;
    
    public float JumpForce = 5f;
    public float MoveSpeed = 1f;

    public float GravityScale = 1f;

    public int WaitTime = 1;
    void Awake()
    {
        Manager = this;
        DontDestroyOnLoad(gameObject);
    }
}