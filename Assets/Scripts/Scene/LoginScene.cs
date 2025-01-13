using UnityEngine;

public class LoginScene : Scene
{
    protected override void Start()
    {
        base.Start();

        CanvasManager.Instance.Open<LoginPanel>();
    }
}
