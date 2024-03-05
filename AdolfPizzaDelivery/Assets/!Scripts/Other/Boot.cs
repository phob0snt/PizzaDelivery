using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(LoadingScreen());
    }

    private IEnumerator LoadingScreen()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
