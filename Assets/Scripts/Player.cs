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
    public bool isHit;
    public bool IsAirborne;
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

    public void Airborne(int atk)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (isGuard)
        {
            Animator.StartGuardAnimation();
        }
        else if (IsAirborne)
        {
            rb.AddForce(Vector2.up * ConstController.Manager.ReAirbonneForceY, ForceMode2D.Impulse);
            Animator.ReAirborneHitAnimation();
        }
        else
        {
            IsAirborne = true;
            isHit = true;
            Animator.StartAirborneAnimation();

            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * ConstController.Manager.AirborneForceY, ForceMode2D.Impulse);
            health -= atk;
        }
    }

    public void Hit(int atk)
    {
        if (isGuard)
        {
            Debug.Log("guard");
            Animator.StartGuardAnimation();
        }
        else if (IsAirborne)
        {
            InputActionManager.Manager.LockInput();
            Debug.Log("Re");
            GetComponent<Rigidbody2D>()
                .AddForce(Vector2.up * ConstController.Manager.ReAirbonneForceY, ForceMode2D.Impulse);
            Animator.ReAirborneHitAnimation();
        }
        else
        {
            InputActionManager.Manager.LockInput();
            isHit = true;
            Animator.ChangeHitLayer();
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