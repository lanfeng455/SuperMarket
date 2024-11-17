using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class UserData : MonoBehaviour
{
    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int GetUserName(StringBuilder lpBuffer, ref int pcbBuffer);

    void Start()
    {
        string displayName = GetCurrentUserDisplayName();
        Debug.Log("当前登录的账户名: " + displayName);
    }

    private string GetCurrentUserDisplayName()
    {
        StringBuilder sb = new StringBuilder(256);
        int length = sb.Capacity;
        if (GetUserName(sb, ref length) > 0) // 判断函数调用是否成功
        {
            return sb.ToString(); // 返回用户显示名称
        }
        return "未知用户"; // 读取失败时的默认值
    }
}
