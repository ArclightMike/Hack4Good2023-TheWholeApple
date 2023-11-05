using CompanionApp.Utility;
using CompanionApp.Models;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CompanionApp.View;

public partial class LoginPage : ContentPage
{
    public IConfiguration _configuration;
    public LoginPage()
    {
        InitializeComponent();
    }

    public LoginPage(IConfiguration config)
    {
        InitializeComponent();
        _configuration = config;
    }

    private async void OnSubmitButtonClicked(object sender, EventArgs e)
    {
        //use the data recieved from the form to make an API call that checks the login data of the user
        //set up a user object
        LoginModel user = new LoginModel();
        user.Email = UsernameEntry.Text;

        //hash the user's password which has been collected as plain text
        string hashedPassword = Helper.EncryptCredentials(PasswordEntry.Text);

        //update the object so we're not sending plain text password data to our API
        user.Password = hashedPassword;

        try
        {
            LoginResponseModel ResponseModel = null;
            var stringObj = JsonConvert.SerializeObject(user);

            ServiceHelper objService = new ServiceHelper();
            string response = await objService.PostRequest(stringObj, ConstantValues.LoginUser, false, string.Empty).ConfigureAwait(true);
            ResponseModel = JsonConvert.DeserializeObject<LoginResponseModel>(response);
            if (ResponseModel == null)
            {
                //error with the API that caused a null return. what happened?  Needs handled

            }
            else if (ResponseModel.success != true)
            {
                //did not log in. Maybe a wrong username or password
            }
            else
            {
                //everything is OK
                //the user should now be considered authenticated
                //create claims details based on the user information
                

                //Take the user to the UserDashboard
               
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ProcessLogin API " + ex.Message);
        }
    

    }

}