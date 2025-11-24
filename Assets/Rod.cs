using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Rod : MonoBehaviour
{
    public FishingTrigger fishingTrigger;
    public FishAI fishAI;
    public PlayerMovement playermovement;
    public GameObject bobber;
    public Collider bobberCollider;
    public bool isCasted = false;
    public bool caught;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (fishingTrigger.canFish == true && !isCasted)
            {
                Cast();
            }
            else if (isCasted == true)
            {
                if (fishAI.isDetectingRod == true)
                {
                    if (fishAI.isBiting == true)
                    {
                        Catch();
                        Uncast();
                    }
                    else
                    {
                        Fail();
                        Uncast();
                    }
                }
                else
                {
                    Uncast();
                }
            }
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
    void Catch()
    {
        fishAI.caught = true;
    }
    void Fail()
    {
        fishAI.failed = true;
    }
}

