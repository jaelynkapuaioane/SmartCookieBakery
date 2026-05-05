using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject storyPanel;
    public GameObject instructionsPanel;

    [Header("Instructions Content")]
  //  public Image instructionImage;
  //  public Sprite[] pageSprites;
    public GameObject[] pages;
  //  public TextMeshProUGUI instructionText;
    public TextMeshProUGUI pageIndicator;
    public Button previousButton;
    public Button nextButton;

    [Header("Right Panel Buttons")]
    public Button playButton;
    public Button showInstructionsButton;
    public Button hideInstructionsButton;

    [Header("Instruction Pages")]
    public string[] instructionTexts = {
        "The cookies appear here!",
        "The equation panel is here!",
        "Count the cookies and input it here!",
        "Complete the equation!",
        "Watch the timer!"
    };

    private int currentPage = 0;

    void OnEnable()
    {
        foreach (EventSystem es in FindObjectsByType<EventSystem>(FindObjectsSortMode.None))
        {
            DestroyImmediate(es.gameObject);
        }
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<InputSystemUIInputModule>();
        DontDestroyOnLoad(eventSystem);

        AudioListener[] listeners = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);
        for (int i = 1; i < listeners.Length; i++)
        {
            DestroyImmediate(listeners[i].gameObject);
        }
    }

    // void Start()
    // {
    //     playButton.onClick.AddListener(OnPlayClicked);
    //    showInstructionsButton.onClick.AddListener(ShowInstructions);
    //     hideInstructionsButton.onClick.AddListener(HideInstructions);
    //     previousButton.onClick.AddListener(PreviousPage);
    //     nextButton.onClick.AddListener(NextPage);
    //
    //   HideInstructions();
    // }

    void Start()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        showInstructionsButton.onClick.AddListener(ShowInstructions);
        hideInstructionsButton.onClick.AddListener(HideInstructions);
        previousButton.onClick.AddListener(PreviousPage);
        nextButton.onClick.AddListener(NextPage);

        currentPage = 0;

        HideInstructions();
    }

    //   void ShowInstructions()
    //  {
    //     storyPanel.SetActive(false);
    //     instructionsPanel.SetActive(true);
    //      showInstructionsButton.gameObject.SetActive(false);
    //     hideInstructionsButton.gameObject.SetActive(true);
    //     currentPage = 0;
    //      UpdatePage();
    //  }

    void ShowInstructions()
    {
        storyPanel.SetActive(false);
        instructionsPanel.SetActive(true);

        currentPage = 0;

        UpdatePage();

        showInstructionsButton.gameObject.SetActive(false);
        hideInstructionsButton.gameObject.SetActive(true);
    }

    void HideInstructions()
    {
        storyPanel.SetActive(true);
        instructionsPanel.SetActive(false);

        showInstructionsButton.gameObject.SetActive(true);
        hideInstructionsButton.gameObject.SetActive(false);

        currentPage = 0;
    }



    //   void UpdatePage()
    //   {
    //       instructionImage.sprite = pageSprites[currentPage];
    //       instructionText.text = instructionTexts[currentPage];
    //      pageIndicator.text = (currentPage + 1) + " / " + instructionTexts.Length;
    //
    //       previousButton.gameObject.SetActive(currentPage > 0);
    //       nextButton.gameObject.SetActive(currentPage < instructionTexts.Length - 1);
    //  }

    void UpdatePage()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPage);
        }

        pageIndicator.text = (currentPage + 1) + " / " + pages.Length;

        previousButton.gameObject.SetActive(currentPage > 0);
        nextButton.gameObject.SetActive(currentPage < pages.Length - 1);
    }

    void PreviousPage()
    {
        currentPage--;
        UpdatePage();
    }

    void NextPage()
    {
        currentPage++;
        UpdatePage();
    }

    void OnPlayClicked()
    {
        SceneManager.LoadScene("GamePlay");
    }
}