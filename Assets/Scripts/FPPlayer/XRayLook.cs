using UnityEngine;

public class XRayLook : MonoBehaviour
{
    public float maxDistance = 20f;
    public LayerMask interactLayer;
    public int xrayLayer = 6;
    public int normalLayer = 0;

    private GameObject current;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        GameObject hitObj = null;

        if (Physics.Raycast(ray, out hit, maxDistance, interactLayer))
        {
            if (hit.collider.CompareTag("XRayObject"))
                hitObj = hit.collider.gameObject;
        }

        if (hitObj != current)
        {
            if (current != null)
                current.layer = normalLayer;

            current = hitObj;

            if (current != null)
                current.layer = xrayLayer;
        }
    }
}
