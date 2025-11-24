using UnityEngine;

public class FishingTrigger : MonoBehaviour
{
    public LayerMask waterMask;
    public LayerMask fishMask;
    public bool canFish = false;
    public bool isToucingFish = false;
    void Update()
    {
        RaycastHit hit;
        // Does the ray intersect water
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 100, waterMask))

        {
            canFish = true;
        }
        else
        {
            canFish = false;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 100, fishMask))

        {
            isToucingFish = true;
        }
        else
        {
            isToucingFish = false;
        }
    }
}
