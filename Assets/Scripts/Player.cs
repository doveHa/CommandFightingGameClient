using DataTable.DataSet;
using Handler;
using Manager;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 Position;
    private SpriteRenderer spriteRenderer;
    public CharacterAnimatorHandler Animator { get; set; }
    public DataSet DataSet { get; set; }

    private bool isLeft;
    private bool isGuard;
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

    public void SetDataSet(string charaterName)
    {
        switch (charaterName)
        {
            case "Naktis":
                DataSet = new NaktisFrameDataSet();
                break;
            case "Kaegetsu":
                DataSet = new KagetsuFrameDataSet();
                break;
            case "Vargon":
                DataSet = new VargonFrameDataSet();
                break;
        }
    }

    public void UseSkill(string skillName)
    {
    }
}