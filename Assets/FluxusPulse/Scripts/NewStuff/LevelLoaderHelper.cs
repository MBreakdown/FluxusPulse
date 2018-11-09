using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderHelper : MonoBehaviour
{
	// Public Methods

	public void LoadScene(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }
    //~ fn

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    //~ fn

    public void LoadScene(int sceneBuildIndex, float delay)
    {
        StopSceneCoroutine();
        m_LoadSceneCoroutine = StartCoroutine(LoadSceneCoroutine(sceneBuildIndex, delay));
    }
    //~ fn

    public void LoadScene(string sceneName, float delay)
    {
        StopSceneCoroutine();
        m_LoadSceneCoroutine = StartCoroutine(LoadSceneCoroutine(sceneName, delay));
    }
    //~ fn

    public void LoadSceneRelative(int relativeSceneIndex)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + relativeSceneIndex);
    }
    //~ fn

    public void LoadSceneRelative()
    {
        LoadSceneRelative(1);
    }
    //~ fn



    // Inspector Fields
    
	public float delay = 0f;



	// Unity Event Methods

	private void OnDestroy()
	{
		StopSceneCoroutine();
	}
    //~ fn



    // Private Methods

    private IEnumerator LoadSceneCoroutine(int sceneBuildIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneBuildIndex);
    }
    //~ fn

    private IEnumerator LoadSceneCoroutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    //~ fn

	private void StopSceneCoroutine()
	{
		if (m_LoadSceneCoroutine != null)
		{
			StopCoroutine(m_LoadSceneCoroutine);
			m_LoadSceneCoroutine = null;
		}
	}
	//~ fn



	// Private Fields

	private Coroutine m_LoadSceneCoroutine = null;
}
//~ class
