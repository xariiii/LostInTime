using UnityEngine;
using TMPro;

public class CodeSubmit : MonoBehaviour
{
    public TMP_InputField inputField;
    public SafeCodeUnlocker safeUnlocker;

    [Header("Objects to disable after success")]
    public GameObject canvasToDisable;   // cały panel z kodem
    public GameObject triggerToDisable;  // trigger przy sejfie

    public void OnSubmit()
    {
        string code = inputField.text;

        if (safeUnlocker.TryUnlock(code))
        {
            if (canvasToDisable != null)
                canvasToDisable.SetActive(false);

            if (triggerToDisable != null)
                triggerToDisable.SetActive(false);
        }
        else
        {
            Debug.Log("Wrong code!");
            inputField.text = "";
        }
    }
}
