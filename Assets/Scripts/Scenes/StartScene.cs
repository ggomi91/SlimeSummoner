using UnityEngine;

public class StartScene : Scene
{
    private void Start()
    {
        Debug.LogError("StartScene");

        SceneManager.Instance.Load<LoginScene>();
    }
}
