using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : Panel
{
    [SerializeField]
    private TMP_InputField m_InputField_ID = null;

    [SerializeField]
    private TMP_InputField m_InputField_PW = null;

    [SerializeField]
    private Button m_Button_Login = null;

    protected override void Awake()
    {
        base.Awake();

        m_Button_Login.onClick.AddListener(_OnClick_Button_Login);
    }

    private void _OnClick_Button_Login()
    {
        Debug.LogErrorFormat("ID : {0}", m_InputField_ID.text);
        Debug.LogErrorFormat("PW : {0}", m_InputField_PW.text);
    }
}
