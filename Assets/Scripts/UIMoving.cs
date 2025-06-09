using UnityEngine;

public class UIMoving : MonoBehaviour
{
    public string path;
    
    private float speed = 30f;
    private float distanceFromCamera = 5f;

    private bool moveToCenter = true;
    private Vector3 targetPosition;

    void Start()
    {
        Vector3 screenCenter = new Vector3(0.5f, 0.7f, distanceFromCamera);
        targetPosition = Camera.main.ViewportToWorldPoint(screenCenter);
        GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>(path));
    }

    void Update()
    {
        if (moveToCenter)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                moveToCenter = false;
            }
        }
    }
}