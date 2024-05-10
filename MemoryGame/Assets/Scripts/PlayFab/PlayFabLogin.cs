using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
public class PlayFabLogin: MonoBehaviour
{
    [DllImport ("__Internal")]

    private static extern void DisplayInAlertWindow(string message);

    public InputField EmailInput, PasswordInput;
    private string _userEmail, _userPassword;
   [SerializeField] private LoginResult _loginResult;
    public void OnRegisterClick()
    {
        GetUserInput();
        var request = new RegisterPlayFabUserRequest()
        {
            Email = _userEmail,
            Password = _userPassword,
            RequireBothUsernameAndEmail = true,
            TitleId = "5B5F0",
            Username = "Oxizine"
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegistrationSuccess, OnPlayFabFailure);
    }

    private void GetUserInput()
    {
        _userEmail = EmailInput.text;
        _userPassword = PasswordInput.text;
    }

    private void OnRegistrationSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log(result.PlayFabId);
        DisplayInAlertWindow("Registration successful!");
    }
    private void OnPlayFabFailure(PlayFabError error)
    {
        Debug.LogError(error.ErrorMessage);
         DisplayInAlertWindow(error.ErrorMessage);
    }

    public void OnLoginClick()
    {
        GetUserInput();
        var request = new LoginWithEmailAddressRequest()
        {
            Email = _userEmail,
            Password = _userPassword,
            TitleId = "5B5F0",
        };
        PlayFabClientAPI.LoginWithEmailAddress(request,OnLoginSuccess,OnPlayFabFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login Successful");
         DisplayInAlertWindow("Login Successful!");
        _loginResult = result;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    
}


