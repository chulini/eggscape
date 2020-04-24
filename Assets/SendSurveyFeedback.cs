using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class SendSurveyFeedback : MonoBehaviour
{
    [SerializeField] private string baseURL = "https://docs.google.com/forms/d/e/1FAIpQLSdtuMZD9ofQfDHvk4y0B6iJIrczJEMHSkr1g2trviXN0L7GYw/formResponse";
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button buttonDiscord;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private string newText;

    IEnumerator SendFeedbackURL(bool answer) {
        WWWForm form = new WWWForm();
        form.AddField("entry.136224330", answer ? "Yes" : "No");
        WWW www = new WWW(baseURL, form.data);
        yield return www;
        /*
        using (UnityWebRequest webRequest = UnityWebRequest.Post(baseURL, form)) {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError) {
                Debug.Log(webRequest.error);
            } else {
                Debug.Log("Form upload complete!");
            }
        }
        */
    }
    
    public void SendFeedback(bool answer) {
        print("Clicked button");
        SetActive(button1, false);
        SetActive(button2, false);
        SetActive(buttonDiscord, true);
        tmp.SetText(newText);
        StartCoroutine(SendFeedbackURL(answer));
    }

    void SetActive(Button button, bool active) {
        button.interactable = active;
        button.enabled = active;
        button.image.enabled = active;
        button.gameObject.SetActive(active);
    }
}
