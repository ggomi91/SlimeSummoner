using UnityEngine;

public class StartScene : Scene
{
    protected override void Start()
    {
        base.Start();
        
        SceneManager.Instance.Load<LoginScene>();
    }
}
