using DataTable.DataSet;
using Handler;
using Manager;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 Position;
    private SpriteRenderer spriteRenderer;
    public CharacterAnimatorHandler Animator { get; set; }
    private DataSet leftSide, rightSide;
    public DataSet DataSet { get; set; }

    private bool isLeft;
    public bool isGuard;
    private bool isHit;
    public bool IsJumping { get; set; }

    private int health = 100;

    void Awake()
    {
        isLeft = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<CharacterAnimatorHandler>();
    }


    public void Flip()
    {
        spriteRenderer.flipX = isLeft;
        isLeft = !isLeft;

        if (isLeft)
        {
            Debug.Log("left side");
            DataSet.SetLeftSide();
        }
        else
        {
            Debug.Log("right side");
            DataSet.SetRightSide();
        }
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
            Animator.StartGuardAnimation();
        }
        else
        {
            isHit = true;
            Animator.StartHitAnimation();
            health -= atk;
            Debug.Log(health);
        }
    }

    public void HitEnd()
    {
        isHit = false;
    }

    public void SetDataSet(string charaterName)
    {
        switch (charaterName)
        {
            case "Naktis":
                DataSet = new NaktisFrameDataSet();
                DataSet.SetLeftSide();
                break;
            case "Kaegetsu":
                DataSet = new KagetsuFrameDataSet();
                DataSet.SetLeftSide();
                break;
            case "Vargon":
                DataSet = new VargonFrameDataSet();
                DataSet.SetLeftSide();
                break;
        }
    }

    public void UseSkill(string skillName)
    {
    }
}