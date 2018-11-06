using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public bool increment = true;
	public int value = 1;
	public float delay = 0f;

	public void LoadScene()
	{
		StartCoroutine(LoadSceneCoroutine(
			delay,
			increment ? SceneManager.GetActiveScene().buildIndex + value : value
		));
	}

	IEnumerator LoadSceneCoroutine(float delay, int buildIndex)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(buildIndex);
	}
}
//~ class
