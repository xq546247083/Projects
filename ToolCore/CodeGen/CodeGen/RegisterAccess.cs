//===================================================================
//	created:	2004/03/31   13:40
//	file base:	RegisterAccess
//	author:		DoItNow
//	
//	purpose:	
//===================================================================

using System;
using Microsoft.Win32;


/// <summary>
/// RegisterAccess ��ժҪ˵����
/// </summary>
public class RegisterAccess
{
	static RegisterAccess()
	{
	}

	/// <summary>
	/// ��ȡע�������ָ������ֵ
	/// </summary>
	/// <param name="keyName"></param>
	/// <returns></returns>
	public static string ReadKey(string keyName)
	{
		return RegistFields.instance.GetValue(keyName);
	}

	/// <summary>
	/// ��ָ���ļ�д��ĳ��ֵ
	/// </summary>
	/// <param name="keyName"></param>
	/// <returns></returns>
	public static bool WriteKey(string keyName,string keyValue)
	{
		RegistFields.instance.SetValue(keyName, keyValue);
		return true;
	}
}

