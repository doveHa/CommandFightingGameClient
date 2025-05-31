using DataTable.DataSet;
using Handler;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public Vector2 Position;
    private SpriteRenderer spriteRenderer;
    public CharacterAnimatorHandler Animator { get; set; }
    private DataSet leftSide, rightSide;
    public DataSet DataSet { get; set; }

    private bool isLeft;
    public bool isGuard;
    public bool IsJumping { get; set; }

    private int health = 100;

    public void Initialize()
    {
        isLeft = true;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Animator = GetComponentInChildren<CharacterAnimatorHandler>();
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
            health -= atk;
            Debug.Log(health);
        }
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

    void Update()
    {
        Debug.Log(spriteRenderer.sprite.name);
    }
}