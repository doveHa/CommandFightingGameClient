using UnityEngine;

public class ConstController : MonoBehaviour
{
    public static ConstController Manager { get; private set; }
        
    [Tooltip("체공 지속 시간")] public float DurationTime { get; } = 4;

    [Tooltip("올라가는 속도")] public float JumpForce { get; } = 20f;

    [Tooltip("날아가는 속도")] public float FlyForce { get; } = 10f;

    [Tooltip("올라가는 시간")] public float WaitTime { get; } = 0.5f;

    [Tooltip("이동 속도")] public float MoveSpeed { get; } = 5f;

    public float GravityScale { get; set; } = 1f;

    void Awake()
    {
        Manager = this;
        DontDestroyOnLoad(gameObject);
    }
}