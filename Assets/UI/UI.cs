using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;

public class UI : MonoBehaviour
{

    [Header("Endscreen")]
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject endText;

    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject restartButtom;
    

    private void Start()
    {
        SwitchTo(inGameUI);
    }

    public void SwitchTo(GameObject _menu)
    {

        for (int i = 0; i < transform.childCount; i++) {

            bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null;

            if (fadeScreen == false)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (_menu != null) { 
            
            _menu.SetActive(true);
        }
    }


    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
                return;
        }

        SwitchTo(inGameUI);
    }
    
    public void SwitchOnEndScreen()
    {

        
        fadeScreen.FadeOut();
        StartCoroutine(EndScreenCoroutine());
    }

    IEnumerator EndScreenCoroutine()
    {
        yield return new WaitForSeconds(1);

        endText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        restartButtom.SetActive(true);
    }

    public void RestartGameButton() => GameManager.instance.RestartScene();
}
