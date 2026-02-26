using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public static bool HasKey = false;

    public void PickUpKey()
    {
        HasKey = true;
        gameObject.SetActive(false);
    }
}
