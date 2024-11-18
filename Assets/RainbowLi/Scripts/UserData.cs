using UnityEngine;
using Microsoft.Win32;
using System;
using UnityEngine.UI;
public class UserData : MonoBehaviour
{
    public Text text;
    void Start()
    {
        text.text = "Welcome: " + GetUserName();
    }

    private string GetUserName()
    {
        string accountName = string.Empty;

        // 打开 HKEY_LOCAL_MACHINE\SAM\SAM 路径，获取用户账户信息
        try
        {
            using (RegistryKey key = Registry.Users.OpenSubKey(@".DEFAULT\Software\Microsoft\IdentityCRL\StoredIdentities"))
            {
                if (key != null)
                {
                    foreach (string identity in key.GetSubKeyNames())
                    {
                        using (RegistryKey subkey = Registry.Users.OpenSubKey(@".DEFAULT\Software\Microsoft\IdentityCRL\StoredIdentities\" + identity))
                        {
                                using (RegistryKey subsubkey = Registry.Users.OpenSubKey(@".DEFAULT\Software\Microsoft\IdentityCRL\StoredIdentities\" + identity + @"\"+ subkey.GetSubKeyNames()[0]))
                                {
                                    foreach (string valueName in subsubkey.GetValueNames())
                                    {
                                       if(valueName == "DisplayName")
                                       {
                                        Debug.Log("当前用户名：" + subsubkey.GetValue(valueName));
                                        accountName = subsubkey.GetValue(valueName).ToString();
                                       }
                                    }
                                }
                        }
                     }           
                }
                else
                {
                    Debug.Log("未找到指定的注册表项。");
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            Debug.Log("没有权限访问该注册表项。请以管理员权限运行此程序。");
        }
        catch (Exception ex)
        {
            Debug.Log("发生异常: " + ex.Message);
        }

        return accountName;
    }
}

