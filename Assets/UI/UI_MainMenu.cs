using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_MainMenu : MonoBehaviour
{

    [SerializeField] private string scenceName = "MainScene";
    [SerializeField] UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject continueButton;

    private void Start()
    {
        if (SaveManager.instance.HasSavedData() == false)
        {
            continueButton.SetActive(false);
        }
    }
    public void ContinueGame()
    {
        StartCoroutine(LoadScrenWithFadeEffect(1.5f));
    }

    public void NewGame()
    {
        SaveManager.instance.NewGame();
        StartCoroutine(LoadScrenWithFadeEffect(1.5f));
    }


    public void ExitGame()
    {

    }

    IEnumerator LoadScrenWithFadeEffect(float _delay)
    {

        fadeScreen.FadeOut();
        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(scenceName);
    }
}



