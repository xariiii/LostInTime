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
    private UnityEngine.UIElements.Button _goBackButton;
    private UnityEngine.UIElements.Button _saveSettingsButton;
    private UnityEngine.UIElements.DropdownField _qualityDropdown;
    private UnityEngine.UIElements.DropdownField _resolutionDropdown;
    private UnityEngine.UIElements.Slider _volumeSlider;

    public GameObject playerPrefab;
    public VisualElement MainMenuVisual;
    public VisualElement SettingsVisual;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        // Getting the panels
        MainMenuVisual = _document.rootVisualElement.Q<VisualElement>("MainMenuPanel");
        SettingsVisual = _document.rootVisualElement.Q<VisualElement>("SettingsPanel");

        // Getting the buttons
        _startButton = _document.rootVisualElement.Q<UnityEngine.UIElements.Button>("StartGameButton");
        _settingsButton = _document.rootVisualElement.Q<UnityEngine.UIElements.Button>("SettingsButton");
        _quitButton = _document.rootVisualElement.Q<UnityEngine.UIElements.Button>("QuitButton");
        _goBackButton = _document.rootVisualElement.Q<UnityEngine.UIElements.Button>("GoBackButton");
        _saveSettingsButton = _document.rootVisualElement.Q<UnityEngine.UIElements.Button>("SaveSettingsButton");

        // Getting the settings

        DropdownField _qualityDropdown = _document.rootVisualElement.Q<DropdownField>("QualityDropdown");
        DropdownField _resolutionDropdown = _document.rootVisualElement.Q<DropdownField>("ResolutionDropdown");
        Slider _volumeSlider = _document.rootVisualElement.Q<Slider>("VolumeSlider");

        List<string> QualityOptions = new List<string> { "Niska", "Średnia", "Wysoka" };
        _qualityDropdown.choices = QualityOptions;
        _qualityDropdown.value = QualityOptions[1]; // medium set as default

        List<string> ResolutionOptions = new List<string> { "2560x1440", "1920x1080", "1600x900", "1366x768", "1280x720" };


        _resolutionDropdown.choices = ResolutionOptions;
        _resolutionDropdown.value = ResolutionOptions[1]; // 1920 x 1080 set as default

        _qualityDropdown.RegisterValueChangedCallback(evt =>
        {
            Debug.Log("Wybrano jakość: " + evt.newValue);
            QualitySettings.SetQualityLevel(QualityOptions.IndexOf(evt.newValue));
        });

        _resolutionDropdown.RegisterValueChangedCallback(evt =>
        {
            Debug.Log("Wybrano rozdzielczosc: " + evt.newValue);

            string[] resParts = evt.newValue.Split('x');

            if (resParts.Length == 2 &&
                int.TryParse(resParts[0], out int width) &&
                int.TryParse(resParts[1], out int height))
            {
                UnityEngine.Screen.SetResolution(width, height, FullScreenMode.Windowed);
            }
            else
            {
                Debug.LogWarning("Nieprawidłowy format rozdzielczości: " + evt.newValue);
            }
        });
        
        _volumeSlider.RegisterValueChangedCallback(evt =>
        {
            Debug.Log("Ustawiono głośność: " + evt.newValue);
            AudioListener.volume = evt.newValue / 100f;
        });



        // Registering button clicks
        _startButton?.RegisterCallback<ClickEvent>(OnPlayGameClick);
        _settingsButton?.RegisterCallback<ClickEvent>(OnSettingsGameClick);
        _quitButton?.RegisterCallback<ClickEvent>(OnQuitGameClick);
        _goBackButton?.RegisterCallback<ClickEvent>(OnGoBackClick);
        _saveSettingsButton?.RegisterCallback<ClickEvent>(OnSaveSettingsClick);

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        _startButton?.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _settingsButton?.UnregisterCallback<ClickEvent>(OnSettingsGameClick);
        _quitButton?.UnregisterCallback<ClickEvent>(OnQuitGameClick);
        _goBackButton?.UnregisterCallback<ClickEvent>(OnGoBackClick);
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Start Button");
        SceneManager.LoadScene("playerScene");
    }

    private void OnSettingsGameClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Settings Button");

        HidePanel(MainMenuVisual);
        ShowPanel(SettingsVisual);
    }

    private void OnGoBackClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Go Back Button");

        HidePanel(SettingsVisual);
        ShowPanel(MainMenuVisual);
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

    private void HidePanel(VisualElement panel)
    {
        if (panel != null)
            panel.style.display = DisplayStyle.None;
    }

    private void ShowPanel(VisualElement panel)
    {
        if (panel != null)
            panel.style.display = DisplayStyle.Flex;
    }

    private void OnSaveSettingsClick(ClickEvent evt)
    {
        Debug.Log("Saved the settings");
    }
}
