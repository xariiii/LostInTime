using UnityEngine;

public class LaserMirror : MonoBehaviour
{
    public LineRenderer line;
    public float maxDistance = 100f;
    public int maxBounces = 10;

    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        line.positionCount = 1;
        line.SetPosition(0, origin);

        for (int i = 0; i < maxBounces; i++)
        {
            Ray ray = new Ray(origin, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, hit.point);
                if (hit.collider.CompareTag("Mirror"))
                {
                    direction = Vector3.Reflect(direction, hit.normal);
                    origin = hit.point + direction * 0.01f;
                }
                else
                {
                    break;
                }
            }
            else
            {                line.positionCount++;
                line.SetPosition(line.positionCount - 1, origin + direction * maxDistance);
                break;
            }
        }
    }
}
