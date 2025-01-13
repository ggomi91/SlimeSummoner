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
        if (m_InputField_ID.text == string.Empty)
            return;

        if (m_InputField_PW.text == string.Empty)
            return;

        AccountManager.Instance.Login(m_InputField_ID.text, m_InputField_PW.text, () =>
        {
            SceneManager.Instance.Load<LobbyScene>();
        });
    }
}
