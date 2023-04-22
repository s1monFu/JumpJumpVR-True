using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_game : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Demo");
    }
}
