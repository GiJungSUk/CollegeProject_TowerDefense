using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    string sceneName;
    private bool isIntroRestarted = false;

    public void ChangeScene_()
    {
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
            if(sceneName == "IntroScene" && !isIntroRestarted)
            {
                StartCoroutine(RestartIntroSceneAfterDelay());
                print("재시작!");
            }
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator RestartIntroSceneAfterDelay()
    {
        yield return null; // 한 프레임 대기

        // 씬이 완전히 로드된 후 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
