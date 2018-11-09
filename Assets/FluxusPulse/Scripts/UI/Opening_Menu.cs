using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Opening_Menu : MonoBehaviour
{
	[SerializeField]
	private string nextScene;
    
	[SerializeField]
	private GameObject popUp;

	void Start()
    {
        if (popUp)
            popUp.SetActive(false);
	}
    //~ fn

    public void LoadNextScene()
    {
		if (SceneManager.GetActiveScene().name == "OpeningScreen")
        {
			if (popUp)
                popUp.SetActive(true);

		}
        else
        {
			SceneManager.LoadScene(nextScene);
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
