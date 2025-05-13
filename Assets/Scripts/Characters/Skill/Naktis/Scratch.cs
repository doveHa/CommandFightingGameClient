using System.Collections;
using System.Collections.Generic;
using Characters.AnimationHandler;
using UnityEngine;

public class Scratch : MonoBehaviour
{
    private NaktisAnimationHandler naktisAnimationHandler;

    public void SetCoff()
    {
    }

    public void Run()
    {
        naktisAnimationHandler = transform.parent.GetComponent<NaktisAnimationHandler>();
        naktisAnimationHandler.StartScratchAnimation();
        //StartCoroutine(WaitScratchTiming());
    }

    private IEnumerator WaitScratchTiming()
    {
        yield return new WaitUntil(() => naktisAnimationHandler.FirstScratch);
        FirstScratch();
        naktisAnimationHandler.FirstScratch = false;
        
        yield return new WaitUntil(() => naktisAnimationHandler.SecondScratch);
        SecondScratch();
        naktisAnimationHandler.SecondScratch = false;
    }

    private void FirstScratch()
    {
        Debug.Log(1);
    }

    private void SecondScratch()
    {
        Debug.Log(2);
    }
}