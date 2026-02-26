using UnityEngine;

public class LaserPuzzleManager : MonoBehaviour
{
    public LaserDetector[] detectors;
    public GameObject closedChest;
    public GameObject openChest;
    public GameObject itemInside;

    void Update()
    {
        if (AllDetectorsActive())
        {
            closedChest.SetActive(false);
            openChest.SetActive(true);
            itemInside.SetActive(true);
        }
    }

    bool AllDetectorsActive()
    {
        foreach (var d in detectors)
        {
            if (!d.activated) return false;
        }
        return true;
    }
}
