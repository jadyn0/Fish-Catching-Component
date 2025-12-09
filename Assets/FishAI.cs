using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;
using System.Runtime.CompilerServices;

public class FishAI : MonoBehaviour
{
    public float minSwimWait;
    public float maxSwimWait;
    public bool isDetectingRod = false;
    public bool canSwim = true;
    public bool isBiting = false;
    public LayerMask layerMask;
    public float swimRayDistance;
    public Rigidbody rb;
    public FishingTrigger fishingTrigger;
    //public Rod rod;
    public FishDecider fishDecider;
    public float maxSwimSpeed;
    public float minSwimSpeed;
    public float maxTurnSpeed;
    public float minTurnSpeed;

    public int biteCount;
    public float turnMove;
    public float turnTurn;
    public float forward;
    public bool left = true;
    private bool first = true;

    public bool caught = false;
    public bool failed = false;
    public float biteSpeed = 0.5f;
    public string fishType;


    private Transform target;
    void Start()
    {
        fishType = fishDecider.RandomFish();
        StartCoroutine(Swim());
    }
    private IEnumerator RodDetect()
    {
        float maxBites = Random.Range(0.5f, 4f);
        while (true && isDetectingRod && !isBiting)
        {
            if (failed == true)
            {
                Destroy(gameObject);
            }
            var lookPos = target.position - transform.position;                     // code taken from https://discussions.unity.com/t/making-an-object-rotate-to-face-another-object/27560
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 15);
            if (fishingTrigger.isToucingFish == false)
            {
                rb.AddRelativeForce(Vector3.forward * biteSpeed);
                yield return null;
            }
            else
            {
                biteCount += 1;
                if (biteCount >= maxBites)
                {
                    StartCoroutine(Bite());
                }
                if (!isBiting)
                {
                    rb.AddRelativeForce(Vector3.back * 3f, ForceMode.VelocityChange);
                    float biteInterval = Random.Range(1f, 2f);
                    yield return new WaitForSeconds(biteInterval);
                }
            }
        }
    }
    private IEnumerator Bite()
    {
        isBiting = true;
        rb.linearVelocity = Vector3.zero;
        for (float countdown = 1f; countdown >= 0; countdown -= 0.1f)
        {
            if (caught == true)
            {
                Debug.Log(fishType);
                Destroy(gameObject);
            }
            if (failed == true)
            {
                Destroy(gameObject);
            }
            Flop();
            yield return new WaitForSeconds(0.2f);
        }
        failed = true;
        Destroy(gameObject);
    }
    private void Flop()
    {
        if (first)
        {
            rb.AddRelativeForce(Vector3.right * turnMove/2f, ForceMode.VelocityChange);
            rb.AddRelativeTorque(Vector3.up * -turnTurn/2f, ForceMode.VelocityChange);
            first = false;
        }
        else if (left == true)
        {
            rb.AddRelativeForce(Vector3.left * turnMove, ForceMode.VelocityChange);
            rb.AddRelativeTorque(Vector3.up * turnTurn, ForceMode.VelocityChange);
            rb.AddRelativeForce(Vector3.forward * forward, ForceMode.VelocityChange);
            left = false;
        }
        else
        {
            rb.AddRelativeForce(Vector3.right * turnMove, ForceMode.VelocityChange);
            rb.AddRelativeTorque(Vector3.up * -turnTurn, ForceMode.VelocityChange);
            rb.AddRelativeForce(Vector3.forward * forward, ForceMode.VelocityChange);
            left = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bobber"))
        {
            target = other.transform;
            isDetectingRod = true;
            StartCoroutine(RodDetect());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bobber"))
        {
            target = null;
            isDetectingRod = false;
        }
    }

    private IEnumerator Swim()
    {
        while (true && !isDetectingRod && !isBiting)
        {
            float interval = Random.Range(minSwimWait, maxSwimWait);        //code taken from https://www.youtube.com/watch?v=pQajI2xHe5U
            yield return new WaitForSeconds(interval);                      // random delay between fish swimming
            if (!isDetectingRod)
            {
                RaycastHit hit;
                                                                            // Does the ray intersect ground
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, swimRayDistance, layerMask))
                {
                    canSwim = false;
                }
                else
                {
                    canSwim = true;
                }
                float swimOrTurn = Random.Range(0, 5);

                if (canSwim == false)                                       // if the fish can't swim forward, it turns
                {
                    float turnSpeed = Random.Range(minTurnSpeed, maxTurnSpeed);
                    rb.AddTorque(new Vector3(0, turnSpeed, 0), ForceMode.Impulse);
                }
                else
                {
                    if (swimOrTurn >= 1.5f)                                    // random chance to swim forward or turn a random amount
                    {
                        float swimSpeed = Random.Range(minSwimSpeed, maxSwimSpeed);
                        rb.AddRelativeForce(new Vector3(0, 0, swimSpeed), ForceMode.Impulse);
                        float turnWhilstMoving = Random.Range(-0.1f, 0.1f);
                        rb.AddTorque(new Vector3(0, turnWhilstMoving, 0), ForceMode.Impulse);
                    }
                    else
                    {
                        float turnSpeed = Random.Range(minTurnSpeed, maxTurnSpeed);
                        rb.AddTorque(new Vector3(0, turnSpeed, 0), ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
