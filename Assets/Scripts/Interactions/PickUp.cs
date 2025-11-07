using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PickUp : MonoBehaviour
{
    bool isHolding = false;

    [SerializeField] float throwForce = 600f;
    [SerializeField] float maxDistance = 3f;
    float distance;
    TempParent tempParent;
    Rigidbody rb;

    Vector3 objectPos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tempParent = TempParent.Instance;
    }

    void Update()
    {
        if (isHolding)
            Hold();
    }

    private void OnMouseDown()
    {
        // pickup
        if (tempParent != null)
        {
            isHolding = true;
            rb.useGravity = false;
            rb.detectCollisions = true;

            this.transform.SetParent(tempParent.transform);
        }
        else
        {
            Debug.Log("Temp parent not found");
        }
    }

    private void OnMouseUp()
    {
        // drop
    }

    private void OnMouseExit()
    {
        // drop
    }
    
    private void Hold()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if(Input.GetMouseButtonDown(1))
        {
            // throw
        }
    }
}
