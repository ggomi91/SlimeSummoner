using UnityEngine;

public class PlayerManager : Manager<PlayerManager>
{
    GameObject m_background = null;
    GameObject m_slime = null;

    protected override void Start()
    {
        base.Start();

        SceneManager.Instance.OnSceneLoaded += _OnSceneLoaded;
        SceneManager.Instance.OnSceneUnloaded += _OnSceneUnloaded;
    }

    private void _OnSceneLoaded()
    {
        GameScene gameScene = SceneManager.Instance.GetScene<GameScene>();
        if (gameScene == null)
            return;

        {
            GameObject instance = null;

            {
                GameObject resource = Resources.Load<GameObject>("Spine/Slime/Slime_Water");
                if (resource == null)
                    return;

                bool activeSelf = resource.activeSelf;
                resource.SetActive(true);

                instance = Instantiate(resource, gameScene.transform) as GameObject;

                resource.SetActive(activeSelf);
            }

            if (instance == null)
                return;

            m_slime = instance;
        }

        {
            GameObject instance = null;

            {
                GameObject resource = Resources.Load<GameObject>("BG/BG_Forest_Ground");
                if (resource == null)
                    return;

                bool activeSelf = resource.activeSelf;
                resource.SetActive(true);

                instance = Instantiate(resource, gameScene.transform) as GameObject;

                resource.SetActive(activeSelf);
            }

            if (instance == null)
                return;

            m_background = instance;
        }
    }

    private void _OnSceneUnloaded()
    {
        GameScene gameScene = SceneManager.Instance.GetScene<GameScene>();
        if (gameScene == null)
            return;

        m_background = null;
        m_slime = null;
    }
}
