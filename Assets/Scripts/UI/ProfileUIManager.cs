using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ProfileUIManager : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInputFieldPortrait;
    [SerializeField] TMP_InputField ageInputFieldPortrait;
    [SerializeField] RawImage profileImagePortrait;

    [SerializeField] TMP_InputField nameInputFieldLandscape;
    [SerializeField] TMP_InputField ageInputFieldLandscape;
    [SerializeField] RawImage profileImageLandscape;

    private ProfileManager profileManager;

    private string photoPath = "";

    private void Awake()
    {
        profileManager = new ProfileManager();
    }

    public void OnSaveButtonClickedPortrait()
    {
        OnSaveButtonClicked(nameInputFieldPortrait, ageInputFieldPortrait);
    }
    public void OnSaveButtonClickedLandscape()
    {
        OnSaveButtonClicked(nameInputFieldLandscape, ageInputFieldLandscape);
    }
    public void OnLoadButtonClicked()
    {
        OnLoadButtonClicked(nameInputFieldPortrait, ageInputFieldPortrait, profileImagePortrait);
        OnLoadButtonClicked(nameInputFieldLandscape, ageInputFieldLandscape, profileImageLandscape);
    }

    public void OnUploadButtonClicked()
    {
        OnUploadButtonClicked(profileImagePortrait);
        OnUploadButtonClicked(profileImageLandscape);
    }


    private void OnSaveButtonClicked(TMP_InputField nameInputField, TMP_InputField ageInputField)
    {
        string name = nameInputField.text;
        int age = int.Parse(ageInputField.text);

        int bestScore = PlayerPrefs.GetInt("PlayerBestScoreKey", 0);

        PlayerProfile profile = new PlayerProfile(name, age, bestScore, photoPath);

        profileManager.SaveProfile(profile);

        Debug.Log("Profile Saved!");
    }

    private void OnLoadButtonClicked(TMP_InputField nameInputField, TMP_InputField ageInputField, RawImage profileImage)
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

    private void OnUploadButtonClicked(RawImage profileImage)
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
