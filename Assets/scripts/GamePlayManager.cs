using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class GamePlayManager : MonoBehaviour
{
    void OnEnable()
    {
        // Destroy any existing EventSystems first
        foreach (EventSystem es in FindObjectsByType<EventSystem>(FindObjectsSortMode.None))
        {
            DestroyImmediate(es.gameObject);
        }

        // Create a fresh one
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<InputSystemUIInputModule>();
        DontDestroyOnLoad(eventSystem);
    }

    void Start() { }

    void Update() { }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}