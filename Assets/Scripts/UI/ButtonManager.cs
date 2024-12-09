using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
    public enum SceneName
    {
        LobbyScene,
        GameScene,
        GameEndScene
    }
    
    public static ButtonManager Instance {get; private set;}

    private void Awake() {
        if (Instance!=null) {
            Destroy(gameObject);
            return;
        }
        Instance=this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        AddButtonOnClickEvent();
    }

    private void AddButtonOnClickEvent() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName.Equals(SceneName.LobbyScene.ToString())) {
            Button startBtn = GameObject.Find("ButtonStart").GetComponent<Button>();
            if (startBtn !=null) {
                startBtn.onClick.AddListener(() => GameManager.Instance.LoadSceneWithName(SceneName.GameScene.ToString()));
            }
        }

        if (currentSceneName.Equals(SceneName.GameEndScene.ToString())) {
            Button retryBtn = GameObject.Find("ButtonRetry").GetComponent<Button>();
            if (retryBtn !=null) {
                retryBtn.onClick.AddListener(() => GameManager.Instance.LoadSceneWithName(SceneName.GameScene.ToString()));
            }
            Button goToLobbyBtn = GameObject.Find("ButtonGoToLobby").GetComponent<Button>();
            if (goToLobbyBtn !=null) {
                goToLobbyBtn.onClick.AddListener(() => GameManager.Instance.LoadSceneWithName(SceneName.LobbyScene.ToString()));
            }
        }
    }
}
