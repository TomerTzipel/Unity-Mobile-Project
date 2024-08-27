using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ProfileUIManager : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] TMP_InputField ageInputField;
    [SerializeField] RawImage profileImage;

    private ProfileManager profileManager;
    private string photoPath = "";

    private void Start()
    {
        profileManager = FindObjectOfType<ProfileManager>();
    }

    public void OnSaveButtonClicked()
    {
        string name = nameInputField.text;
        int age = int.Parse(ageInputField.text);

        int bestScore = PlayerPrefs.GetInt("PlayerBestScoreKey", 0);

        PlayerProfile profile = new PlayerProfile(name, age, bestScore, photoPath);

        profileManager.SaveProfile(profile);

        Debug.Log("Profile Saved!");
    }

    public void OnLoadButtonClicked()
    {
        PlayerProfile profile = profileManager.LoadProfile();

        if (profile != null)
        {
            nameInputField.text = profile.name;
            ageInputField.text = profile.age.ToString();

            PlayerPrefs.SetInt("PlayerBestScoreKey", profile.BestScore);
            PlayerPrefs.Save();

           
            if (File.Exists(profile.photoPath))
            {
                byte[] imgData = File.ReadAllBytes(profile.photoPath);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imgData);
                profileImage.texture = texture;
            }

            Debug.Log("Profile Loaded!");
        }
        else
        {
            Debug.Log("No profile found.");
        }
    }

    public void OnUploadButtonClicked()
    {
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            if (path != null)
            {
                
                photoPath = path;

                
                byte[] imgData = File.ReadAllBytes(photoPath);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imgData);
                profileImage.texture = texture;

                Debug.Log("Image selected: " + photoPath);
            }
        });

        if (permission == NativeCamera.Permission.Denied)
        {
            Debug.LogWarning("Camera permission denied.");
        }
        else if (permission == NativeCamera.Permission.ShouldAsk)
        {
            Debug.Log("Asking for camera permission.");
        }
    }
}
