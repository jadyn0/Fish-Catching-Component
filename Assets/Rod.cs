using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Rod : MonoBehaviour
{
    public LayerMask waterMask;
    public LayerMask fishMask;
    public PlayerMovement playermovement;
    public GameObject bobber;
    public Collider bobberCollider;
    public bool isCasted = false;
    public bool caught;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanFish() && !isCasted)
            {
                Cast();
            }
            else if (isCasted == true)
            {
               Uncast();
            }
        }
    }

    bool CanFish()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 10, 0), transform.TransformDirection(Vector3.down) * 100, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 10, 0), transform.TransformDirection(Vector3.down), out hit, 100, waterMask))
        {
            return true;
            
        }
        else
        {
            return false;
        }
    }
    public bool isToucingFish()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 10, 0), transform.TransformDirection(Vector3.down), out hit, 100, fishMask))
        {
            return true;
            
        }
        else
        {
            return false;
        }
    }

    void Cast()
    {
        caught = false;
        isCasted = true;
        bobber.GetComponent<MeshRenderer>().enabled = true;
        bobberCollider.enabled = true;
        playermovement.isFishing = true;
    }
    void Uncast()
    {
        isCasted = false;
        bobber.GetComponent<MeshRenderer>().enabled = false;
        bobberCollider.enabled = false;
        playermovement.isFishing = false;
    }
}

