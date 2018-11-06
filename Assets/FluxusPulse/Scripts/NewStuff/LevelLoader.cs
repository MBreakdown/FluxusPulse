using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Public Methods

	public void LoadScene()
	{
        if (m_LoadSceneCoroutine != null)
        {
            StopCoroutine(m_LoadSceneCoroutine);
            m_LoadSceneCoroutine = null;
        }

        m_LoadSceneCoroutine = StartCoroutine(LoadSceneCoroutine(
			delay,
			increment ? SceneManager.GetActiveScene().buildIndex + buildIndex : buildIndex
        ));
	}
    //~ fn



    // Inspector Fields

    [Tooltip("If increment is false, this is the build index of the scene to load." +
        "If increment is true, this is added to the build index of the current scene.")]
    public int buildIndex = 1;
    public bool increment = false;
    public float delay = 0f;



    // Unity Event Methods

    private void OnDestroy()
    {
        StopCoroutine(m_LoadSceneCoroutine);
        m_LoadSceneCoroutine = null;
    }
    //~ fn



    // Private Methods

    IEnumerator LoadSceneCoroutine(float delay, int buildIndex)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(buildIndex);
    }
    //~ fn



    // Private Fields

    private Coroutine m_LoadSceneCoroutine = null;
}
//~ class
