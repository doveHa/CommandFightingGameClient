using System.Collections;
using Characters;
using Unity.VisualScripting;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Coroutine flyCoroutine;

    public void SetCoff()
    {
        
    }
    
    public void Run()
    {
        if (!flyCoroutine.IsUnityNull())
        {
            Debug.Log("Stop fly");
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
        ConstController.Manager.GravityScale = GetComponentInChildren<Rigidbody2D>().gravityScale;
        GetComponentInChildren<Rigidbody2D>().gravityScale = 0;
        GetComponentInChildren<Rigidbody2D>().linearVelocityY = 0;
        transform.GetChild(0).position =
            new Vector3(transform.GetChild(0).position.x,
                ConstController.Manager.NaktisFlyYPosition, 0);

        float elapsedTime = 0;
        while (elapsedTime < ConstController.Manager.DurationTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GetComponentInChildren<Rigidbody2D>().gravityScale = ConstController.Manager.GravityScale;
        flyCoroutine = null;
    }
}