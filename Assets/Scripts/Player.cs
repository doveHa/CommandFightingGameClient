using Handler;
using Manager;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 Position;
    private SpriteRenderer spriteRenderer;

    private bool isLeft, isPlayerLeft;
    private bool isGuard;
    private CharacterAnimatorHandler animator;
    public bool IsJumping { get; set; }

    private int health = 100;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<CharacterAnimatorHandler>();
        
        isLeft = true;
        isPlayerLeft = true;

        if (gameObject.CompareTag("Opponent"))
        {
            Flip();
        }
    }

    void Update()
    {
        if (isPlayerLeft != GameManager.Manager.IsPlayerLeft)
        {
            Flip();
            isPlayerLeft = !isPlayerLeft;
        }
    }

    private void Flip()
    {
        spriteRenderer.flipX = isLeft;
        isLeft = !isLeft;
    }

    public void SetGuard(bool isGuard)
    {
        this.isGuard = isGuard;
    }

    public void Hit(int atk)
    {
        if (isGuard)
        {
            Debug.Log("guard");
        }
        else
        {
            health -= atk;
            Debug.Log(health);
        }
    }


    public void UseSkill(string skillName)
    {
    }
}