using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [Header("Paneles")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameplayPanel;

    [Header("Botones")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button reanudateButton;
    [SerializeField] private Button goToStarPanelButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playButton.onClick.AddListener(StartGame);
        continueButton.onClick.AddListener(() => ChangePanel(gameplayPanel, startPanel));
        exitButton.onClick.AddListener(ExitGame);
        reanudateButton.onClick.AddListener(() => ChangePanel(gameplayPanel, pausePanel));
        goToStarPanelButton.onClick.AddListener(() => ChangePanel(pausePanel, gameplayPanel));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangePanel(GameObject activatePanel, GameObject desactivatePanel)
    {
        activatePanel.SetActive(true);
        desactivatePanel.SetActive(false);
    }

    private void StartGame()
    {
        ChangePanel(gameplayPanel, startPanel);
        player.transform.position = new Vector3(0, 1.5f, 0);
    }

    private void ExitGame()
    {
        Debug.Log("SuccesfullExit");
        Application.Quit();
    }
}
