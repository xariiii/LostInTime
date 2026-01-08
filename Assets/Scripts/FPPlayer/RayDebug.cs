using UnityEngine;

public class RayMaskDebug : MonoBehaviour
{
    public LayerMask interactLayer;
    public float maxDistance = 20f;

    void Update()
    {
        Debug.Log("Mask value: " + interactLayer.value);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, interactLayer))
        {
            Debug.Log("Ray hit: " + hit.collider.name + " on layer " + hit.collider.gameObject.layer);
            
        }
        else
        {
            Debug.Log("Ray hit nothing");
            
        }
    }
}
