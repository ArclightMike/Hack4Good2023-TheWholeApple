using CompanionApp.Utility;
using CompanionApp.Models;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CompanionApp.View;

public partial class RegisterPage : ContentPage
{
    public IConfiguration _configuration;

    public RegisterPage()
	{
		InitializeComponent();
	}

    public RegisterPage(IConfiguration config)
    {
        
        InitializeComponent();
        _configuration = config;
    }

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {

        //use the data recieved from the form to make an API call that checks the email address of the user
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
            string response = await objService.PostRequest(stringObj, ConstantValues.RegisterUser, false, string.Empty).ConfigureAwait(true);
            ResponseModel = JsonConvert.DeserializeObject<LoginResponseModel>(response);
            if (ResponseModel == null)
            {
                //error with the API that caused a null return. what happened?  Needs handled
                Error.Text = ResponseModel.message;

            }
            else if (ResponseModel.success != true)
            {
                //did not succeed. Maybe a wrong username or they are not signed up with good dads
                Error.Text = ResponseModel.message;
            }
            else
            {
                //everything is OK
                Error.Text = ResponseModel.message;
                //the user should now have a login
                //create claims details based on the user information


                //Take the user to the Login Page


            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ProcessLogin API " + ex.Message);
        }

    }
}