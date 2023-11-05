
using IntakeApp.Utility;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Net.Mail;

namespace IntakeApp;

public partial class MainPage : ContentPage
{

    private List<(decimal SupportAmount, DateTime Birthday)> childrenData = new List<(decimal, DateTime)>();
    private int currentRow = 2; // Start adding entries from the second row (index 1)
    private string stmpServer = "";

    public MainPage()
    {
        InitializeComponent();
    }


    private void AddEntry_Clicked(object sender, EventArgs e)
    {
        // Add a new row definition for the new label and entry/DatePicker
        DynamicGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        DynamicGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        // Create a new label and entry for the first column
        var newEntry1 = new Entry { Placeholder = $"Child {currentRow}" };

        // Create a new label and DatePicker for the second column
        var newDatePicker2 = new DatePicker();

        // Place the new  Entry to the Grid
        DynamicGrid.Children.Add(newEntry1);
        Grid.SetColumn(newEntry1, 0);
        Grid.SetRow(newEntry1, currentRow);


        // Place the new LDatePicker to the Grid

        DynamicGrid.Children.Add(newDatePicker2);
        Grid.SetColumn(newDatePicker2, 1);
        Grid.SetRow(newDatePicker2, currentRow);


        // Increment the currentRow index after adding the new elements
        currentRow += 1;

        // Move the button down by setting its row index
        Grid.SetRow((Button)sender, currentRow);
        Grid.SetRow((Button)submit, currentRow);
    }

    private void OnOvalTapped(object sender, EventArgs e)
    {
        // Handle the tap event here
        DisplayAlert("Processing!", "Processing the form - please wait", "OK");

        // Clear the list before adding new entries
        childrenData.Clear();

        // Assuming you're adding one Entry and one DatePicker per child in sequential order
        for (int i = 0; i < DynamicGrid.Children.Count; i += 2)
        {
            var entryView = DynamicGrid.Children[i];
            var datePickerView = DynamicGrid.Children[i + 1];

            if (entryView is Entry entry && datePickerView is DatePicker datePicker)
            {
                // Add the child name and birthday to the list
                childrenData.Add((Convert.ToDecimal(entry.Text), datePicker.Date));
            }
        }

        // Now convert the list of tuples into a 2D array
        string[,] childrenArray = new string[childrenData.Count, 2];

        for (int i = 0; i < childrenData.Count; i++)
        {
            childrenArray[i, 0] = childrenData[i].SupportAmount.ToString(); // Child support amount
            childrenArray[i, 1] = childrenData[i].Birthday.ToShortDateString(); // Child's birthday
        }

        // If you want to see the array in your debug output
        for (int i = 0; i < childrenData.Count; i++)
        {
            Console.WriteLine($"Child Support: {childrenArray[i, 0]}, Birthday: {childrenArray[i, 1]}");
        }

        Submit_Clicked();
    }

    private async void Submit_Clicked()
    {
        //get all of the form data
        SaveIntakeRequest userData = new SaveIntakeRequest();

        userData.FirstName = firstName.Text;
        userData.LastName = lastName.Text;
        userData.Address1 = address.Text;
        userData.Address2 = address2.Text;
        userData.City = city.Text;
        userData.State = state.Text;
        userData.PostalCode = zip.Text;
        userData.Email = email.Text;
        userData.PrimaryPhone = phone.Text;
        userData.WorkPhone = work.Text;
        userData.AlternateContact = alt.Text;

        //get the children from the array
        List<Dependent> dependentsList = new List<Dependent>();

        foreach (var childData in childrenData)
        {
            Dependent dependent = new Dependent
            {
                supportPerMonth = childData.SupportAmount,
                BirthDate = DateOnly.FromDateTime(childData.Birthday)
            };

            dependentsList.Add(dependent);

        }

        userData.Dependents = dependentsList;

        //call the API with the user data
        try
        {
            AddUserResponseModel responseModel = null;
            var serializedData = JsonConvert.SerializeObject(userData);
            ServiceHelper objService = new ServiceHelper();
            string response = await objService.PostRequest(serializedData, ConstantValues.AddUser, false, string.Empty).ConfigureAwait(true);
            responseModel = JsonConvert.DeserializeObject<AddUserResponseModel>(response);

            if (responseModel == null)
            {
                //oh no!
            }
            else if (responseModel.success == true)
            {
                CreateTestMessage(stmpServer);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Process Add - " + ex.Message);
        }

        await Navigation.PushAsync(new ThanksPage());
    }

    public static void CreateTestMessage(string server)
    {
        
        string to = ""; //change the to line to the actual email of the person
        string from = "";
        MailMessage message = new MailMessage(from, to);
        message.Subject = "Welcome to Good Dads";
        message.Body = @"Thanks for completing your paperwork and welcome to Good Dads. You can now download our Participant App that will connect you with your progress and resources. {Add link to Play Store and App Store here} ";
        SmtpClient client = new SmtpClient(server);
        //super fun login credentials here
        client.Credentials = new NetworkCredential("", "");


        try
        {
            client.Send(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                ex.ToString());
        }
    }

}

