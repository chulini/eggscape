using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using TMPro;

public static class ExtensionMethods
{

	public static void CopyToClipboard(this string txt)
	{
		TextEditor te = new TextEditor();
		te.text = txt;
		te.SelectAll();
		te.Copy();
	}
	public static IEnumerator AnimateText(this TMP_Text uiText, string newAnimatedText)
	{
		string lastText = uiText.text;
		for (int i = 0; i <= newAnimatedText.Length; i++)
		{
			uiText.text = newAnimatedText.Substring(0, i);
			yield return new WaitForSecondsRealtime(.03f);
		}
		yield return new WaitForSecondsRealtime(2.0f);
		uiText.text = lastText;
	}
	public static Color WithAlpha(this Color color, float alpha)
	{
		return new Color(color.r, color.g, color.b, alpha);
	}

	public static void ResetTransformation(this Transform trans)
	{
		trans.position = Vector3.zero;
		trans.localRotation = Quaternion.identity;
		trans.localScale = new Vector3(1, 1, 1);
	}

	public static Vector3 WithX(this Vector3 v, float x)
	{
		return new Vector3(x, v.y, v.z);
	}

	public static Vector3 WithY(this Vector3 v, float y)
	{
		return new Vector3(v.x, y, v.z);
	}

	public static Vector3 WithZ(this Vector3 v, float z)
	{
		return new Vector3(v.x, v.y, z);
	}

	public static Vector2 WithX(this Vector2 v, float x)
	{
		return new Vector2(x, v.y);
	}

	public static Vector2 WithY(this Vector2 v, float y)
	{
		return new Vector2(v.x, y);
	}

	public static Vector3 Round(this Vector3 v)
	{
		return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
	}
	// public static Vector3 Round(Vector3 v)
	// {
	// 	return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
	// }

	public static void LocalResetTransformation(this Transform trans)
	{
		trans.localPosition = Vector3.zero;
		trans.localRotation = Quaternion.identity;
		trans.localScale = new Vector3(1, 1, 1);
	}

	public static Quaternion WithEulerX(this Quaternion q, float v)
	{
		return Quaternion.Euler(v, q.eulerAngles.y, q.eulerAngles.z);
	}

	public static Quaternion WithEulerY(this Quaternion q, float v)
	{
		return Quaternion.Euler(q.eulerAngles.x, v, q.eulerAngles.z);
	}

	public static Quaternion WithEulerZ(this Quaternion q, float v)
	{
		return Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, v);
	}

	public static IEnumerator WaitForRealSeconds(float time)
	{
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time)
		{
			yield return null;
		}
	}
	public static void SaveTextureAsPNG(Texture2D _texture, string filename)
	{
		byte[] _bytes =_texture.EncodeToPNG();
		string fullpath = Application.persistentDataPath + "/" + filename;
		System.IO.File.WriteAllBytes(fullpath, _bytes);
		Debug.Log(_bytes.Length/1024  + "Kb was saved as: " + fullpath);
	}

	public static string FirstCharToUpper(string input)
	{
		if (String.IsNullOrEmpty(input))
		{
			Debug.Log("<color=#ff0000>No input!</color>");
			return "";
		}

		return input[0].ToString().ToUpper() + input.Substring(1);
	}

	public static string FirstCharToUpperEveryWord(string input)
	{
		if (String.IsNullOrEmpty(input))
		{
			return "";
		}

		string r = "";
		string[] words = input.Split(' ');
		for (int i = 0; i < words.Length; i++)
		{
			r += FirstCharToUpper(words[i]);
			if (i < words.Length - 1)
				r += " ";
		}

		return r;
	}

	public static float LerpWithoutClamp(float a, float b, float t)
	{
		return a + (b - a) * t;
	}
	//Returns deterministic gaussian random between 0 and 1
	public static float DeterministicRandomNumber(float seed)
	{
		float p = Mathf.PerlinNoise(9.9871f + seed * 1.34432f, 76.31f + seed * 1.1233f);

		return Mathf.Abs((Mathf.RoundToInt(p * 100000000f) % 1000000) / 1000000f);
	}

	public static string Capitalize(this string s)
	{
		if (string.IsNullOrEmpty(s))
		{
			return string.Empty;
		}

		s = s.ToLower();
		char[] a = s.ToCharArray();
		a[0] = char.ToUpper(a[0]);
		return new string(a);
	}

	public static int RoundNumberToNearestMultiple(float number, int multiple)
	{
		return Mathf.CeilToInt(number / (float) multiple) * multiple;
	}

	public static string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 =
			new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}

	private static readonly DateTime UnixEpoch =
		new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	public static double GetCurrentUnixTimestampSeconds()
	{
		return DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000.0;
	}
	public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
	{
		if (val.CompareTo(min) < 0) return min;
		else if(val.CompareTo(max) > 0) return max;
		else return val;
	}

	public static DateTime GetNetworkTime()
	{
		//default Windows time server
		const string ntpServer = "time.windows.com";

		// NTP message size - 16 bytes of the digest (RFC 2030)
		var ntpData = new byte[48];

		//Setting the Leap Indicator, Version Number and Mode values
		ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

		var addresses = Dns.GetHostEntry(ntpServer).AddressList;

		//The UDP port number assigned to NTP is 123
		var ipEndPoint = new IPEndPoint(addresses[0], 123);
		//NTP uses UDP

		using(var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
		{
			socket.Connect(ipEndPoint);

			//Stops code hang if NTP is blocked
			socket.ReceiveTimeout = 3000;     

			socket.Send(ntpData);
			socket.Receive(ntpData);
			socket.Close();
		}

		//Offset to get to the "Transmit Timestamp" field (time at which the reply 
		//departed the server for the client, in 64-bit timestamp format."
		const byte serverReplyTime = 40;

		//Get the seconds part
		ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

		//Get the seconds fraction
		ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

		//Convert From big-endian to little-endian
		intPart = SwapEndianness(intPart);
		fractPart = SwapEndianness(fractPart);

		var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

		//**UTC** time
		var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

		return networkDateTime.ToLocalTime();
	}

	static uint SwapEndianness(ulong x)
	{
		return (uint) (((x & 0x000000ff) << 24) +
		               ((x & 0x0000ff00) << 8) +
		               ((x & 0x00ff0000) >> 8) +
		               ((x & 0xff000000) >> 24));
	}

	
}