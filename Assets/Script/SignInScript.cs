using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Signup: MonoBehaviour
{
    [SerializeField] TMP_InputField signupUserInput;
    [SerializeField] TMP_InputField signupPasswordInput;
    [SerializeField] TextMeshProUGUI signupErrorMsg;


    public void SignupButton()
    {
        StartCoroutine(PostWebSignUp());
    }

    public IEnumerator PostWebSignUp()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", signupUserInput.text);
        form.AddField("password", signupPasswordInput.text);
        string url = "http://localhost:3000/signup";
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            signupErrorMsg.text = request.downloadHandler.text;
            Debug.Log("Sucessful account creation");
            SceneManager.LoadScene("Login");
        }
        else
        {
            Debug.Log("Failed Signup");
            signupErrorMsg.text = request.downloadHandler.text;
        }
    }

    public void ReturnToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
