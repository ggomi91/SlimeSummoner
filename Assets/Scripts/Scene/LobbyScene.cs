using UnityEngine;

public class LobbyScene : Scene
{
    protected override void Start()
    {
        base.Start();

        CanvasManager.Instance.Open<LobbyPanel>();
    }
}
