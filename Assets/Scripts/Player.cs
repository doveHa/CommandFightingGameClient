using DataTable.DataSet;
using Handler;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public CharacterAnimatorHandler Animator { get; set; }
    private DataSet leftSide, rightSide;
    public DataSet DataSet { get; set; }

    public bool IsLeft { get; private set; }
    public bool IsGuard { get; set; }
    public bool IsJumping { get; set; }

    private int health = 100;

    public void Initialize()
    {
        IsLeft = true;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Animator = GetComponentInChildren<CharacterAnimatorHandler>();
    }

    public void Flip()
    {
        spriteRenderer.flipX = IsLeft;
        IsLeft = !IsLeft;

        if (IsLeft)
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

    public void Hit(int atk, int commandLength)
    {
        if (IsGuard)
        {
            Debug.Log("guard");
            Animator.StartGuardAnimation();
        }
        else
        {
            bool isPlayer;
            if (CompareTag("Player"))
            {
                isPlayer = true;
            }
            else
            {
                isPlayer = false;
            }
            HealthSystem.Manager.TakeDamage(isPlayer, atk * commandLength);
            Animator.StartHitAnimation();
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
            case "Kagetsu":
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
    }
}