using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    public void OpenItchPage(string username)
    {
        Application.OpenURL("https://"+username+".itch.io");
    }
}
