using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLinkOnClick : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private string url;
#pragma warning restore 0649
    public void OnClick()
    {
        Application.OpenURL(url);
    }
}
