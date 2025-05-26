using DataTable.DataSet;
using Handler;
using Manager;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 Position;
    private SpriteRenderer spriteRenderer;
    private CharacterAnimatorHandler animator;
    public DataSet DataSet;

    private bool isLeft;
    private bool isGuard;
    public bool IsJumping { get; set; }

    private int health = 100;

    void Awake()
    {
        isLeft = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<CharacterAnimatorHandler>();
    }


    public void Flip()
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