using UnityEngine;

public class StartScene : Scene
{
    private void Start()
    {
        SceneManager.Instance.Load<LoginScene>();
    }
}
