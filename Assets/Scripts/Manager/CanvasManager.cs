using UnityEngine;

public class CanvasManager : Manager<CanvasManager>
{
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
