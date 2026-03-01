using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EndMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _quitButtonEnd;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        var root = _document.rootVisualElement;
        _quitButtonEnd = root.Q<Button>("QuitButtonEnd");

        // WŁĄCZENIE KURSORA – jawne wskazanie UnityEngine.Cursor
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        if (_quitButtonEnd != null)
        {
            _quitButtonEnd.RegisterCallback<ClickEvent>(OnQuitGameClick);
        }
        else
        {
            Debug.LogError("Nie znaleziono QuitButtonEnd w EndMenuPanel!");
        }
    }

    private void OnQuitGameClick(ClickEvent evt)
    {
        var result = System.Windows.Forms.MessageBox.Show(
            "Czy na pewno chcesz wyjść?",
            "Potwierdzenie",
            System.Windows.Forms.MessageBoxButtons.YesNo,
            System.Windows.Forms.MessageBoxIcon.Question
        );

        if (result == System.Windows.Forms.DialogResult.Yes)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
