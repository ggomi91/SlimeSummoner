using UnityEngine;

public class PlayerManager : Manager<PlayerManager>
{
    Field m_field = null;
    Slime m_slime = null;

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
            GameObject field = new GameObject("Field");
            if (field == null)
                return;

            field.transform.parent = gameScene.transform;

            {
                GameObject resource = Resources.Load<GameObject>("BG/BG_Forest_Ground");
                if (resource == null)
                    return;

                bool activeSelf = resource.activeSelf;
                resource.SetActive(true);

                Instantiate(resource, gameScene.transform);

                resource.SetActive(activeSelf);
            }

            m_field = field.AddComponent<Field>();
        }

        {
            GameObject player = new GameObject("Player");
            if (player == null)
                return;

            player.transform.parent = gameScene.transform;

            {
                GameObject resource = Resources.Load<GameObject>("Spine/Slime/Slime_Water");
                if (resource == null)
                    return;

                bool activeSelf = resource.activeSelf;
                resource.SetActive(true);

                Instantiate(resource, player.transform);

                resource.SetActive(activeSelf);
            }

            m_slime = player.AddComponent<Slime_Water>();
        }
    }

    private void _OnSceneUnloaded()
    {
        GameScene gameScene = SceneManager.Instance.GetScene<GameScene>();
        if (gameScene == null)
            return;

        m_field = null;
        m_slime = null;
    }
}
