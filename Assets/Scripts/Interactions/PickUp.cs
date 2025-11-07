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

    [SerializeField] Collider playerCollider;
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
            distance = Vector3.Distance(this.transform.position, tempParent.transform.position);
            if (distance <= maxDistance)
            {
                isHolding = true;
                rb.isKinematic = false;
                rb.detectCollisions = true;

                if (playerCollider != null)
                    Physics.IgnoreCollision(playerCollider, GetComponent<Collider>(), true);
            }
        }
        else
        {
            Debug.Log("Temp parent not found");
        }
    }

    private void OnMouseUp()
    {
        // drop
        Drop();
    }

    private void OnMouseExit()
    {
        // drop
        Drop();
    }

    private void Hold()
    {
        distance = Vector3.Distance(this.transform.position, tempParent.transform.position);

        if (distance >= maxDistance)
        {
            Drop();
            return;
        }

        Vector3 desiredPosition = tempParent.transform.position;
        rb.MovePosition(Vector3.Lerp(rb.position, desiredPosition, Time.deltaTime * 12f));

        Quaternion desiredRotation = tempParent.transform.rotation;
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, desiredRotation, Time.deltaTime * 12f));

        
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (Input.GetMouseButtonDown(1))
        {
            // throw
            Drop();
            rb.AddForce(tempParent.transform.forward * throwForce);
        }
    }
    
    private void Drop()
    {
        if(isHolding)
        {
            isHolding = false;
            objectPos = this.transform.position;
            this.transform.position = objectPos;

            rb.useGravity = true;

            rb.isKinematic = false;
            rb.freezeRotation = false;

            if (playerCollider != null)
                Physics.IgnoreCollision(playerCollider, GetComponent<Collider>(), false);
        }
    }
}
