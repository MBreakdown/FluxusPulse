using UnityEngine;

static class QuitUtility
{
    public static void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    //~ fn
}
//~ class
