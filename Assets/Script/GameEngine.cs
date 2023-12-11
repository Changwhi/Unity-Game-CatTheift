using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{
    [SerializeField] TMP_InputField userInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] TextMeshProUGUI textMsg;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoginButton()
    {
        StartCoroutine(PostWebLogin());
    }

    public IEnumerator PostWebLogin()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", userInput.text);
        form.AddField("password", passwordInput.text);
        string url = "http://localhost:3000/login";
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            textMsg.text = request.downloadHandler.text;
            Debug.Log("Sucessful Login");
            SceneManager.LoadScene("SampleScene");
        } else
        {
            Debug.Log("Failed Login");
            textMsg.text = request.downloadHandler.text;
        }
    }

    public void ReturnToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
