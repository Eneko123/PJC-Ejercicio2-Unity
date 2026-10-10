using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour
{
    private enum State { Start, Playing, Paused, Disconnected }

    [SerializeField] private PlayerMovement player;

    [Header("Paneles")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject disconnectedPanel;

    [Header("Botones")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button backToMenuButton;

    [Header("Version (Fase 4)")]
    [SerializeField] private TMP_Text versionText;

    private State state;

    private void Start()
    {
        playButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
        resumeButton.onClick.AddListener(() => SetState(State.Playing));
        backToMenuButton.onClick.AddListener(() => SetState(State.Start));

        SetState(State.Start);
    }

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        player.PausePressed += TogglePause;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
        player.PausePressed -= TogglePause;
    }

    private void SetState(State newState)
    {
        state = newState;

        startPanel.SetActive(state == State.Start);
        gameplayPanel.SetActive(state == State.Playing);
        pausePanel.SetActive(state == State.Paused);
        disconnectedPanel.SetActive(state == State.Disconnected);

        Time.timeScale = (state == State.Playing) ? 1f : 0f;
        player.canMove = (state == State.Playing);

        // Elemento seleccionado inicialmente en cada menu
        if (state == State.Start) Select(playButton);
        if (state == State.Paused) Select(resumeButton);
    }

    private void Select(Button button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }

    private void StartGame()
    {
        player.ResetPosition();
        SetState(State.Playing);
    }

    private void TogglePause()
    {
        if (state == State.Playing) SetState(State.Paused);
        else if (state == State.Paused) SetState(State.Playing);
    }

    // Perdida de foco
    private void OnApplicationFocus(bool hasFocus)
    {
        Debug.Log("Foco de la aplicacion: " + hasFocus);
        if (!hasFocus && state == State.Playing) SetState(State.Paused);
    }

    // Desconexion / reconexion del mando
    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is not Gamepad) return;
        Debug.Log("Mando: " + device.displayName + " -> " + change);

        if (change == InputDeviceChange.Disconnected || change == InputDeviceChange.Removed)
        {
            if (state == State.Playing || state == State.Paused)
                SetState(State.Disconnected);
        }
        else if (change == InputDeviceChange.Reconnected || change == InputDeviceChange.Added)
        {
            if (state == State.Disconnected)
                SetState(State.Paused); 
        }
    }

    private void ExitGame()
    {
        Debug.Log("Saliendo de la aplicacion");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}