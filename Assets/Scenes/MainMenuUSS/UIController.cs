using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEditor;
public class UIController : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;
    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        playButton = root.Q<Button>("play-button");
        settingsButton = root.Q<Button>("settings-button");
        exitButton = root.Q<Button>("exit-button");

        playButton.clicked += StartButtonPressed;
        exitButton.clicked += ExitButtonPressed;
    }
    /// <summary>
    /// When you press play button, start stage assigned as level 1
    /// </summary>
    void StartButtonPressed() { 
        SceneManager.LoadScene("Stage 1");
        Debug.Log("Test");
    }
/// <summary>
/// when pressed on exit, stop unity application editor for now
/// TODO: When the game is executable, make it quit application.
/// </summary>
    void ExitButtonPressed() {
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Game Closed");
    }

}
