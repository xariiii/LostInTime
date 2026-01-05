using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TriggerUI : MonoBehaviour
{
    public GameObject uiPanel;
    private Volume volume;
    private Vignette vignette;


    void OnTriggerEnter(Collider other)
    {
        volume = GetComponent<Volume>();
        if (other.CompareTag("Player"))
        {
            uiPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
