using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Windows.Forms;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private UnityEngine.UIElements.Button _startButton;
    private UnityEngine.UIElements.Button _settingsButton;
    private UnityEngine.UIElements.Button _quitButton;

    public GameObject playerPrefab;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        // start button
        _startButton = _document.rootVisualElement.Q<UnityEngine.UIElements.Button>("StartGameButton");
        _startButton.RegisterCallback<ClickEvent>(OnPlayGameClick);

        // settings button
        _settingsButton = _document.rootVisualElement.Q<UnityEngine.UIElements.Button>("SettingsButton");
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsGameClick);

        // quit button
        _quitButton = _document.rootVisualElement.Q<UnityEngine.UIElements.Button>("QuitButton");
        _quitButton.RegisterCallback<ClickEvent>(OnQuitGameClick);

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        _startButton.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _settingsButton.UnregisterCallback<ClickEvent>(OnSettingsGameClick);
        _quitButton.UnregisterCallback<ClickEvent>(OnQuitGameClick);
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Start Button");
        SceneManager.LoadScene("playerScene"); 
    }

    private void OnSettingsGameClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Settings Button");
        
    }

    private void OnQuitGameClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Quit Button");

        DialogResult result = System.Windows.Forms.MessageBox.Show(
            "Czy na pewno chcesz wyjść?",
            "Potwierdzenie",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (result == DialogResult.Yes)
        {
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "playerScene")
        {
            if (GameObject.FindWithTag("Player") == null)
            {
                Instantiate(playerPrefab);
                Debug.Log("Player instantiated after scene load.");
            }
            Destroy(gameObject);
        }
    }
}
