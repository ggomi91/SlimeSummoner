using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static SceneManager g_instance = null;
    public static SceneManager Instance
    {
        get
        {
            if (g_instance == null)
                return null;

            return g_instance;
        }
    }

    private void Awake()
    {
        if (g_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        g_instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void Load<T>()
    {
        StartCoroutine(_Load<T>());
    }

    private IEnumerator _Load<T>()
    {
        yield return null;

        AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(typeof(T).Name);
        op.allowSceneActivation = false;

        while (op.isDone == false)
        {
            if (op.progress < 0.9f)
                yield return null;

            if (op.allowSceneActivation == true)
                yield return null;

            op.allowSceneActivation = true;
            yield return null;
        }
    }
}
