using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : Manager<SceneManager>
{
    protected override void Awake()
    {
        base.Awake();

        UnityEngine.SceneManagement.Scene activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        if (activeScene != null)
        {
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
    }

    private Scene m_scene = null;
    private Coroutine m_coroutine = null;

    private List<Action> m_onSceneLoadeds = new List<Action>();
    public event Action OnSceneLoaded
    {
        add
        {
            m_onSceneLoadeds.Add(value);
        }

        remove
        {
            m_onSceneLoadeds.Remove(value);
        }
    }

    private List<Action> m_onSceneUnloadeds = new List<Action>();
    public event Action OnSceneUnloadeds
    {
        add
        {
            m_onSceneUnloadeds.Add(value);
        }

        remove
        {
            m_onSceneUnloadeds.Remove(value);
        }
    }

    public void Load<T>()
    {
        if (m_coroutine != null)
        {
            StopCoroutine(m_coroutine);
            m_coroutine = null;
        }

        List<Action> onSceneUnloadeds = new List<Action>(m_onSceneUnloadeds);
        foreach(Action onSceneUnloaded in onSceneUnloadeds)
            onSceneUnloaded();

        m_coroutine = StartCoroutine(_Load<T>(() =>
        {
            m_coroutine = null;

            UnityEngine.SceneManagement.Scene activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (activeScene != null)
            {
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

            List<Action> onSceneLoadeds = new List<Action>(m_onSceneLoadeds);
            foreach (Action onSceneLoaded in onSceneLoadeds)
                onSceneLoaded();
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
