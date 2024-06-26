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

    public InputField EmailInput, PasswordInput, UsernameInput;
    private string _userEmail, _userPassword, _userUsername;
   [SerializeField] private LoginResult _loginResult;
    public void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void OnRegisterClick()
    {
        GetUserInput();
        var request = new RegisterPlayFabUserRequest()
        {
            Email = _userEmail,
            Password = _userPassword,
            RequireBothUsernameAndEmail = true,
            TitleId = "5B5F0",
            Username = _userUsername
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegistrationSuccess, OnPlayFabFailure);
    }

    private void GetUserInput()
    {
        _userEmail = EmailInput.text;
        _userPassword = PasswordInput.text;
        _userUsername = UsernameInput.text;
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

    public void IncreasePlayerFounds()
    {
        AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest
        { AuthenticationContext = _loginResult.AuthenticationContext, Amount = 100, VirtualCurrency = "GO" };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddUserVirtualCurrencySuccess, OnPlayFabFailure);

    }

    private void OnAddUserVirtualCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("You earned 100 gold! Your current balance: " + result.Balance);
        DisplayInAlertWindow("You earned 100 gold! Your current balance: " + result.Balance);
    }

    
}



