using System.Collections.Generic;
using UnityEngine;

public class PrintHitBoxData : MonoBehaviour
{
    private static string allInfo = "";
    /*
    void Start()
    {
        string print = "";
        print += transform.parent.parent.parent.name + "\n";
        Transform[] children = GetComponentsInChildren<Transform>();
        print += gameObject.transform.localPosition + " " + transform.name + "\n";
        foreach (Transform child in children)
        {
            print += child.name + "\n";
            if (child.TryGetComponent<BoxCollider>(out BoxCollider boxCollider))
            {
                print += boxCollider.center + " " + boxCollider.size + "\n";
            }

            if (child.TryGetComponent<BoxCollider2D>(out BoxCollider2D boxCollider2D))
            {
                print += boxCollider2D.offset + " " + boxCollider2D.size + "\n";
            }
        }

        allInfo += print + "\n";
        Debug.Log(allInfo);
    }*/
}