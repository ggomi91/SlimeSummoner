using System;
using UnityEngine;

public class AccountManager : Manager<AccountManager>
{
    private string m_id = string.Empty;
    public string ID
    {
        get
        {
            return m_id;
        }
    }

    private string m_pw = string.Empty;
    public string PW
    {
        get
        {
            return m_pw;
        }
    }

    public void Login(string _id, string _pw, Action _onComplete)
    {
        m_id = _id;
        m_pw = _pw;

        if (_onComplete != null)
            _onComplete();
    }
}
