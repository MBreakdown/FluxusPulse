using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening_Menu : MonoBehaviour
{
	[SerializeField]
	private string nextScene;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
    //~ fn

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    //~ fn

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    //~ fn

    public void Quit()
    {
        QuitUtility.Quit();
    }
    //~ fn
}
//~ class
