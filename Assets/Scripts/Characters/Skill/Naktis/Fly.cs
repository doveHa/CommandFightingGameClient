using System.Collections;
using Characters;
using Unity.VisualScripting;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Coroutine flyCoroutine;

    public void SetCoff()
    {
        
    }
    
    public void Run()
    {
        if (!flyCoroutine.IsUnityNull())
        {
            Debug.Log("Stop fly");
            ChangeAnimationIdleLayer();
            GetComponentInChildren<Rigidbody2D>().gravityScale = ConstController.Manager.GravityScale;
            StopCoroutine(flyCoroutine);

            flyCoroutine = null;
        }
        else
        {
            flyCoroutine = StartCoroutine(NaktisFly());
            GetComponentInParent<Player>().IsJumping = true;
        }

        Debug.Log("NaktisS1");
    }

    
    private IEnumerator NaktisFly()
    {
        //ConstController.Manager.GravityScale = GetComponentInChildren<Rigidbody2D>().gravityScale;
        ChangeAnimationFlyLayer();
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
        body.linearVelocityY = 0;
        body.AddForce(Vector2.up * ConstController.Manager.JumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(ConstController.Manager.WaitTime);
        body.AddForce(Vector2.down * ConstController.Manager.JumpForce, ForceMode2D.Impulse);

        float elapsedTime = 0;
        while (elapsedTime < ConstController.Manager.DurationTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GetComponentInChildren<Rigidbody2D>().gravityScale = ConstController.Manager.GravityScale;
        ChangeAnimationIdleLayer();
        flyCoroutine = null;
    }

    private void ChangeAnimationFlyLayer()
    {
        animator.SetLayerWeight(0, 0);
        animator.SetBool("IsFlying",true);
        animator.SetLayerWeight(1, 1);
    }

    private void ChangeAnimationIdleLayer()
    {
        animator.SetBool("IsFlying",false);
        //animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(0, 1);
    }
    
}