using UnityEngine;

public class LoginScene : Scene
{
    private void Start()
    {
        CanvasManager.Instance.Open<LoginPanel>();
    }
}
