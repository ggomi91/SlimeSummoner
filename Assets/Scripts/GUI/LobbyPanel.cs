using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : Panel
{
    [SerializeField]
    private TMP_Text m_Text_ID = null;

    [SerializeField]
    private TMP_Text m_Text_PW = null;

    [SerializeField]
    private Button m_Button_Battle = null;

    protected override void Awake()
    {
        base.Awake();

        m_Text_ID.text = AccountManager.Instance.ID;
        m_Text_PW.text = AccountManager.Instance.PW;

        m_Button_Battle.onClick.AddListener(_OnClick_Button_Battle);
    }

    private void _OnClick_Button_Battle()
    {
        SceneManager.Instance.Load<GameScene>();
    }
}
