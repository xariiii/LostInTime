using UnityEngine;

public class SafeCodeUnlocker : MonoBehaviour
{
    [SerializeField] private string correctCode = "1234";

    [Header("Safe Objects")]
    [SerializeField] private GameObject safeClosed;
    [SerializeField] private GameObject safeOpen;
    [SerializeField] private GameObject keyInsideSafe;

    public bool TryUnlock(string enteredCode)
    {
        if (enteredCode == correctCode)
        {
            Debug.Log("Safe opened!");

            safeClosed.SetActive(false);
            safeOpen.SetActive(true);
            keyInsideSafe.SetActive(true);

            return true;
        }

        return false;
    }
}
