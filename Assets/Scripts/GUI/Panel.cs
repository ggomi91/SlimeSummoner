using System.Reflection;
using UnityEngine;

public class Panel : MonoBehaviour
{
    protected virtual void Awake()
    {
        FieldInfo[] fields = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            if (field.GetCustomAttribute<SerializeField>() == null)
                continue;

            Transform find = this.transform.Find(field.Name);
            if (find == null)
                continue;

            Component component = find.GetComponent(field.FieldType);
            if (component == null)
                continue;

            field.SetValue(this, component);
        }
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
