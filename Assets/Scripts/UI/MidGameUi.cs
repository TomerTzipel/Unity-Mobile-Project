using UnityEngine.SceneManagement;
using UnityEngine;


public class MidGameUi : MonoBehaviour
{
    public void OnBackToMenuPressed()
    {
        SceneManager.LoadScene("Menu");
    }
}
