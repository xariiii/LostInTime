using UnityEngine;

public class TriggerUI : MonoBehaviour
{
    public GameObject uiPanel; // przypisz panel z Canvas w Inspectorze

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // sprawdzamy czy to gracz
        {
            uiPanel.SetActive(true); // pokaż UI
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiPanel.SetActive(false); // ukryj UI
        }
    }
}
