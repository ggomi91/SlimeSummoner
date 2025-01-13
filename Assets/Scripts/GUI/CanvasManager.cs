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

    public T Open<T>() where T : Panel
    {
        GameObject instance = null;

        {
            GameObject resource = Resources.Load<GameObject>("GUI/Prefabs/" + typeof(T).Name);
            if (resource == null)
                return null;

            bool activeSelf = resource.activeSelf;
            resource.SetActive(true);

            instance = Instantiate(resource, MainCanvas.transform) as GameObject;

            resource.SetActive(activeSelf);
        }

        if (instance == null)
            return null;

        T component = instance.GetComponent<T>();
        if (component == null)
            return null;

        return component;
    }
}
