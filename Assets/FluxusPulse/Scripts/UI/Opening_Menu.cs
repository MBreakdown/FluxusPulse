using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Opening_Menu : MonoBehaviour
{
	[SerializeField]
	private string nextScene;

	Scene currentScene;

	private string currentName;

	[SerializeField]
	private GameObject popUp;

	void Start(){
		Scene currentScene = SceneManager.GetActiveScene();

		currentName = currentScene.name;

		popUp.SetActive(false);
	}

    public void LoadNextScene()
    {
		if (currentName == "OpeningScreen") {
			popUp.SetActive(true);
		} else {
			SceneManager.LoadScene (nextScene);
		}
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
