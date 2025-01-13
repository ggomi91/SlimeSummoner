using System;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : Manager<CanvasManager>
{
    [SerializeField]
    private Canvas MainCanvas = null;

    private List<Panel> m_panels = new List<Panel>();

    protected override void Start()
    {
        base.Start();

        SceneManager.Instance.OnSceneUnloaded += () =>
        {
            List<Panel> panels = new List<Panel>(m_panels);
            m_panels.Clear();

            foreach(Panel panel in panels)
                Destroy(panel.gameObject);
        };
    }

    public T Open<T>() where T : Panel
    {
        {
            Int32 findIndex = m_panels.FindIndex((Panel _panel) =>
            {
                return _panel.GetType() == typeof(T);
            });

            if (findIndex >= 0)
            {
                Panel panel = m_panels[findIndex];
                m_panels.RemoveAt(findIndex);
                m_panels.Add(panel);

                panel.transform.SetSiblingIndex(MainCanvas.transform.childCount - 1);
                return panel as T;
            }
        }

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

        m_panels.Add(component);
        return component;
    }
}
