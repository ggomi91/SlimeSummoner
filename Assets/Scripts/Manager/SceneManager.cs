using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : Manager<SceneManager>
{
    protected override void Awake()
    {
        UnityEngine.SceneManagement.Scene activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        if (activeScene == null)
            return;

        GameObject[] rootGameObjects = activeScene.GetRootGameObjects();
        foreach (GameObject rootGameObject in rootGameObjects)
        {
            Scene scene = rootGameObject.GetComponent<Scene>();
            if (scene == null)
                continue;

            m_scene = scene;
            break;
        }
    }

    private Scene m_scene = null;
    private Coroutine m_coroutine = null;

    public void Load<T>()
    {
        if (m_coroutine != null)
        {
            StopCoroutine(m_coroutine);
            m_coroutine = null;
        }

        m_coroutine = StartCoroutine(_Load<T>(() =>
        {
            m_coroutine = null;

            UnityEngine.SceneManagement.Scene activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (activeScene == null)
                return;

            GameObject[] rootGameObjects = activeScene.GetRootGameObjects();
            foreach (GameObject rootGameObject in rootGameObjects)
            {
                Scene scene = rootGameObject.GetComponent<Scene>();
                if (scene == null)
                    continue;

                m_scene = scene;
                break;
            }
        }));
    }

    private IEnumerator _Load<T>(Action _onComplete = null)
    {
        yield return null;

        UnityEngine.AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(typeof(T).Name);
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.isDone == false)
        {
            if (asyncOperation.progress < 0.9f)
                yield return null;

            if (asyncOperation.allowSceneActivation == true)
                yield return null;

            asyncOperation.allowSceneActivation = true;
            yield return null;
        }

        if (_onComplete != null)
            _onComplete();
    }
}
