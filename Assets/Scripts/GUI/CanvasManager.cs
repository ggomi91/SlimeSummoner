using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager g_instance = null;
    public static CanvasManager Instance
    {
        get
        {
            if (g_instance == null)
                return null;

            return g_instance;
        }
    }

    private void Awake()
    {
        if (g_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        g_instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    [SerializeField]
    private Canvas MainCanvas = null;
}
