using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    [SerializeField]
    private Animator animator;
    private string sceneName;
    private void Awake()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        Instance = this;
    }
    public void LoadScene(string sceneName)
    {
        Player.Instance.isOnFade = true;
        animator.SetTrigger("Fade Out");
        PlayerInventoryManager.Instance.CheckPlayerInventoryUIIsActive();
        this.sceneName = sceneName;
    }
    public string ReturnCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }
    public void OnFadeComplete()
    {
        Player.Instance.isOnFade = false;
        SceneManager.LoadScene(sceneName);
    }
    private void ChangedActiveScene(Scene current, Scene next)
    {
        UIManager.Instance.GetCashTextsUI();
        Player.Instance.InvokeCashUpdate(Player.Instance.cash);
    }
}
