using System;
using System.Collections.Generic;


/// <summary>
/// This is main file for the whole app
/// </summary>
namespace Assignment
{
    class MainUI
    {
        public static List<User> allUsers = new List<User>();                                       // to save all user accounts
        public static List<Property> allProperties { get; set; } = new List<Property>();            // to save all registered properties
        public static List<Bid> allBids = new List<Bid>();                                          // to save all bids

        bool finishMainMenu;

        void quitMainMenu()
        {
            finishMainMenu = true;
        }

        // main menu
        public void RunMainMenu()
        {
            while (!finishMainMenu)
            {
                Menu mainMenu = new Menu();
                mainMenu.Add("Register as new Customer", RegisterAcct);
                mainMenu.Add("Login as existing Customer", LoginAcct);
                mainMenu.Display();

            }

        }

        private void LoginAcct()
        {
            User.Login(allUsers);
        }

        private void RegisterAcct()
        {
            User.Register(allUsers);
        }


        // main run
        static void Main(string[] args)
        {
            MainUI runProgram = new MainUI();
            runProgram.RunMainMenu();

        }
    }


}
