using UnityEngine;

public class LaserDetector : MonoBehaviour
{
    public bool activated;

    [Header("Ustawienia materiałów")]
    public Renderer targetRenderer;      // obiekt, którego materiał ma się zmieniać
    public Material activatedMaterial;   // materiał po aktywacji
    public Material defaultMaterial;     // materiał domyślny

    public void Hit()
    {
        activated = true;
        if (targetRenderer != null && activatedMaterial != null)
        {
            targetRenderer.material = activatedMaterial;
        }
    }

    public void ResetState()
    {
        activated = false;
        if (targetRenderer != null && defaultMaterial != null)
        {
            targetRenderer.material = defaultMaterial;
        }
    }
}
