using System;
using System.Collections.Generic;
using System.Linq;
using static Assignment.MainUI;


namespace Assignment
{
    public class User
    {
        // User fields
        private string name;
        private string email;
        private string password;

        // lists for the user
        public List<Property> ownership;
        private List<Property> tempList;
        private List<double> tempPrice = new List<double>();
        private List<double> tempTax = new List<double>();
        private List<Property> tempProp = new List<Property>();
        private List<Bid> displayBid = new List<Bid>();

        // User constructor
        public User(string name, string email, string password)
        {
            this.name = name;
            this.email = email;
            this.password = password;
            ownership = new List<Property>();
            tempList = new List<Property>();

        }

        // set User object property
        public string getName
        {
            get { return name; }
        }
        public string getEmail
        {
            get { return email; }
        }
        public string getPassword
        {
            get { return password; }
        }

        public static User loginUser;


        // ====================================================================


        // US1. User register
        public static void Register(List<User> userLists)
        {
            Console.WriteLine("Please create a new account");
            string newName = UserInterface.GetInput("Name");
            string newEmail = UserInterface.GetInput("Email");
            string newPassword = UserInterface.GetPassword("Password");

            User newUser = new User(newName, newEmail, newPassword);

            userLists.Add(newUser);
            Console.WriteLine("{0} {1} registered successfully", newUser.getName, newUser.getEmail);
            Console.WriteLine();
        }

        // US2. User login
        public static void Login(List<User> userLists)
        {
            Console.WriteLine("Please login your account");
            string loginName = UserInterface.GetInput("Name");
            string loginEmail = UserInterface.GetInput("Email");
            string loginPassword = UserInterface.GetPassword("Password");

            // account authentication 
            if (userLists.Exists(n => n.getName == loginName) == true && userLists.Exists(e => e.getEmail == loginEmail) == true && userLists.Exists(p => p.getPassword == loginPassword) == true)
            {
                Console.WriteLine("Login success");
                Console.WriteLine();
                loginUser = new User(loginName, loginEmail, loginPassword);
                loginUser.accountMenu();
            }
            else
            {
                Console.WriteLine("Invalid account");
                Console.WriteLine();
            }
        }

        // US4. Register new land
        public void RegisterNewLand()
        {
            Land newLand = Land.Create(this);
            allProperties.Add(newLand);
        }

        // US5. Register new house
        public void RegisterNewHouse()
        {
            House newHouse = House.Create(this);
            allProperties.Add(newHouse);
        }

        // US6. Display all properties in the ownership list
        public void DisplayMyProerty()
        {
            ownership.Clear();
            string title = "Property owned by " + name;

            foreach (Property myProp in allProperties)
            {
                if (myProp.getOwner.getEmail == loginUser.getEmail)
                {
                    ownership.Add(myProp);
                   
                }
               
            }
        

            UserInterface.DisplayList(title, ownership);
            Console.WriteLine();
        }


        // US7. Search for sale properties via postcode
        /// <summary>
        /// 1. if no property registered
        /// 2. if a/some properties registered, save these into a temp list then display
        /// </summary>
        public void SearchProperty()
        {
            tempList.Clear();
            try
            {
                Console.WriteLine("Please enter a postcode: ");
                string postcodeInput = Console.ReadLine();
                int postcodeSearch = Convert.ToInt32(postcodeInput);
                string searchTitle = "Postcode: " + postcodeSearch + " properties for sale are following: ";

                if (allProperties.Count == 0)
                {
                    Console.WriteLine("No property registered.");
                    Console.WriteLine();
                }
                else
                {
                    if (allProperties.Exists((Property obj) => obj.getPostcode == postcodeSearch))
                    {
                        foreach (Property item in allProperties)
                        {
                            if (item.getPostcode == postcodeSearch)
                            {
                                tempList.Add(item);

                            }
                           
                        }
                        UserInterface.DisplayList(searchTitle, tempList);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("No matching.");
                        Console.WriteLine();
                    }
                }
            }
            // if user inputs for postcode is not integer
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            // go back to the account menu
            finally
            {
                accountMenu();
            }
            
        }

        // US8. Place bid
        private void PlaceBid()
        {
           
            Console.WriteLine("Choose a property and place your bid");
            var bidOption = UserInterface.ChooseFromList(allProperties);
            Console.WriteLine(bidOption);
            try
            {
                Bid newBid = Bid.placeNewBid(this, bidOption);
                allBids.Add(newBid);
                Console.WriteLine("Your bid for address: {0} and postcode: {1} is ${2} .", bidOption.getAddress, bidOption.getPostcode, newBid.getUserBid);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // US.9 View a list of current bids under this login user account
        private void ViewBids()
        {
            displayBid.Clear();
            if (allBids.Count == 0)
            {
                Console.WriteLine("No bid.");
            }
            else
            {
                if (allBids.Exists((Bid obj) => obj.getPropertyItem.getOwner.getEmail == loginUser.email))
                {
                    foreach(Bid bidItem in allBids)
                    {
                        if(bidItem.getPropertyItem.getOwner.getEmail == loginUser.email)
                        {
                            displayBid.Add(bidItem);
                
                        }
                        else
                        {
                            UserInterface.Message("There is no bid under your properties.");
                        }
                    }
                    string title = "You have received these bids";
                    UserInterface.DisplayList(title, displayBid);
                }
                else
                {
                    UserInterface.Message("There is no bid under your properties.");
                }   
            }
        }

        // US.10 + US.11 Sell property to the highest price and display the tax
        /// <summary>
        /// 1. check if the bid list contains bids
        /// 2. display the property list with bids
        /// 3. sell it to the highest price
        /// 4. remove the sold property
        /// </summary>
        private void SellProperty()
        {
            tempProp.Clear();
            tempPrice.Clear();
            tempTax.Clear();

            double priceAmt = 0;
            double taxAmt = 0;

            if (allBids.Count == 0)
            {
                Console.WriteLine("No one place a bid.");
            }
            else
            {
                if(allBids.Exists((Bid obj) => obj.getPropertyItem.getOwner.getName == loginUser.name))
                {
                    foreach(Bid bidProperty in allBids)
                    {
                        if (!tempProp.Contains(bidProperty.getPropertyItem))
                        {
                            tempProp.Add(bidProperty.getPropertyItem);
                        }
                        
                    }
                    var sellOption = UserInterface.ChooseFromList(tempProp);
                    foreach(Bid bidProperty in allBids)
                    {
                        if (bidProperty.getUserBid > priceAmt && bidProperty.getPropertyItem.getAddress == sellOption.getAddress)
                        {
                            priceAmt = bidProperty.getUserBid;
                            tempPrice.Add(priceAmt);
                            taxAmt = bidProperty.getPropertyItem.Tax(priceAmt);
                            tempTax.Add(taxAmt);
                        }
                    }
                    
                    // display sold info
                    double highestPrice = tempPrice.Max();
                    double highestTax = tempTax.Max();

                    UserInterface.Message($"You have sold the property for ${highestPrice} with tax ${highestTax}.");

                    // remove the sold property
                    var removeProperty = allBids.Find((obj) => obj.getUserBid == highestPrice);
                    allBids.Remove(removeProperty);
                    tempProp.Remove(removeProperty.getPropertyItem);
                }
                
            }

        }


        // submenu / account menu contains all options methods
        bool finishAccountMenu;
        void quitAccountMenu()
        {
            finishAccountMenu = true;
        }

        private void accountMenu()
        {
            while (!finishAccountMenu)
            {
                Menu accountMenu = new Menu();
                accountMenu.Add("Register new land for sale", RegisterNewLand);
                accountMenu.Add("Register new house for sale", RegisterNewHouse);
                accountMenu.Add("List my properties", DisplayMyProerty);
                accountMenu.Add("Search for a property for sale", SearchProperty);
                accountMenu.Add("Choose a property and place your bid", PlaceBid);
                accountMenu.Add("View current bids", ViewBids);
                accountMenu.Add("Sell property to the highest price", SellProperty);
                accountMenu.Add("Logout", Logout);
                accountMenu.Display();
            }
        }

        // US3. user logout
        public static void Logout()
        {
            MainUI back = new MainUI();
            back.RunMainMenu();


        }
    }
}
