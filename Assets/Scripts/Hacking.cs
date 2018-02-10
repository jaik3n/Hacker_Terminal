using UnityEngine;

public class Hacking : MonoBehaviour {

    //Game configuration
    string[] LibraryPasswords = new string[] { "books", "aisle", "shelf", "password", "font", "borrow" };
    string[] PolicePasswords = new string[] { "prisoner", "handcuffs", "holster", "uniform", "arrest" };
    string[] NasaPasswords = new string[] { "spaceship", "discovery", "spaceshuttle", "vacuum", "supernovae", "piloting" };

    //Game State
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;

    string UserName = "";
    int level;
    string CurrentPassword;

    //string[] week = new string[] { "Sunday", "Monday", "Tuesday" };

	void Start ()
    {
        AskForUsername();
	}

    void AskForUsername()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("What is your name?");
    }

    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Hello " + UserName);
        Terminal.WriteLine("Please choose your target.");
        Terminal.WriteLine("Press 1 for Library");
        Terminal.WriteLine("Press 2 for Police Station");
        Terminal.WriteLine("Press 3 for NASA");
        Terminal.WriteLine("Enter your selection:");
    }
       
    void OnUserInput(string input)
    {
        if (UserName == "")
        {
            UserName = input;
            ShowMainMenu();
        }
        else if (input == "menu") // we can always go directly to the main menu
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input =="3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else 
        {
            Terminal.WriteLine("Invalid selection.");
        }
    }

    string SetPassword(string CurrentPassword)
    {
        if (level == 1)
        {
            CurrentPassword = LibraryPasswords[Random.Range(0, LibraryPasswords.Length)];
        }
        else if (level == 2)
        {
            CurrentPassword = PolicePasswords[Random.Range(0, PolicePasswords.Length)];
        }
        else if (level == 3)
        {
            CurrentPassword = NasaPasswords[Random.Range(0, NasaPasswords.Length)];
        }
        else { }

        return CurrentPassword;
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        CurrentPassword = SetPassword(CurrentPassword);
        /*switch(level)
        {
            case 1:
                int index = Random.Range(0, LibraryPasswords.Length);
                CurrentPassword = LibraryPasswords[index];
                break;
            case 2:
                int index = Random.Range(0, PolicePasswords.Length);
                CurrentPassword = PolicePasswords[index];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
        */
        Terminal.ClearScreen();
        Terminal.WriteLine("Please Enter your password.");
        Terminal.WriteLine("Here's a hint: " + CurrentPassword.Anagram());
    }

    void CheckPassword(string input)
    {
        if (input == CurrentPassword)
        {
            DisplayWinScreen();
        }
        else
        {
            Terminal.WriteLine("Incorrect Password.");
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Access Granted");
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    _______
   /     //
  /     //
 /_____//               
(_____(/
                ");
                break;
            case 2:
                Terminal.WriteLine("Access Granted");
                Terminal.WriteLine("Welcome to the Force.");
                Terminal.WriteLine(@"
   ,   /\   ,
  / '-'  '-' \
  |  POLICE  |
  |   .--.   |
  |  ( 19 )  |
  \   '--'   /
   '--.  .--'
       \/
                ");
                break;
            case 3:
                Terminal.WriteLine("Access Granted");
                Terminal.WriteLine("Space awaits you Commander.");
                Terminal.WriteLine(@"
 _ __   __ _ ___  __ _ 
| '_ \ / _` / __|/ _` |
| | | | (_| \__ \ (_| |
|_| |_|\__,_|___/\__,_|

                ");
                break;
        }
    }
}
