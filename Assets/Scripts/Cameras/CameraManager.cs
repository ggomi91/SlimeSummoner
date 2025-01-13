using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager g_instance = null;
    public static CameraManager Instance
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
}
