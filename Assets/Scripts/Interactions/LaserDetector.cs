using UnityEngine;

public class LaserDetector : MonoBehaviour
{
    public bool activated;

    public void Hit()
    {
        activated = true;
    }

    public void ResetState()
    {
        activated = false;
    }
}
