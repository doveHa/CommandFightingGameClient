using Characters.AnimationHandler;
using UnityEngine;

public class UpperWing : MonoBehaviour
{
    private NaktisAnimationHandler naktisAnimationHandler;

    public void SetCoff()
    {
    }

    public void Run()
    {
        naktisAnimationHandler = transform.parent.GetComponent<NaktisAnimationHandler>();
        naktisAnimationHandler.StartUpperWingAnimation();
    }
}