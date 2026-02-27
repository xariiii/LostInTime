using UnityEngine;

public class LaserDetector : MonoBehaviour
{
    public bool activated;

    [Header("Ustawienia materiałów")]
    public Renderer targetRenderer;
    public Material activatedMaterial;
    public Material defaultMaterial;

    private bool laserOnThisFrame = false;

    void Update()
    {
        // Jeśli laser NIE dotknął w tej klatce → reset
        if (!laserOnThisFrame)
        {
            if (activated)
            {
                activated = false;
                if (targetRenderer != null && defaultMaterial != null)
                    targetRenderer.material = defaultMaterial;
            }
        }

        // Reset flagi na kolejną klatkę
        laserOnThisFrame = false;
    }

    public void Hit()
    {
        laserOnThisFrame = true;

        if (!activated)
        {
            activated = true;
            if (targetRenderer != null && activatedMaterial != null)
                targetRenderer.material = activatedMaterial;
        }
    }
}
