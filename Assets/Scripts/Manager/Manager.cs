using UnityEngine;

public class Manager<T> : MonoBehaviour where T : class
{
    private static T g_instance = null;
    public static T Instance
    {
        get
        {
            if (g_instance == null)
                return null;

            return g_instance;
        }
    }

    protected virtual void Awake()
    {
        if (g_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        g_instance = this.gameObject.GetComponent<T>();

        DontDestroyOnLoad(this.gameObject);
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void LateUpdate()
    {
    }

    protected virtual void OnDestroy()
    {
    }
}
