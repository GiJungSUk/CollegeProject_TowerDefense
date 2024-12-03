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
                print("�����!");
            }
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator RestartIntroSceneAfterDelay()
    {
        yield return null; // �� ������ ���

        // ���� ������ �ε�� �� �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
