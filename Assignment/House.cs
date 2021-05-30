using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    public class House : Property
    {
        private string description;


        public House(string address, int postcode, string description, User owner) : base(address, postcode, owner)
        {
            this.description = description;
        }
        public string getDescription { get { return description; } }

        public override string ToString()
        {
            return "House: " + "Ad: " + getAddress + " Postcode: " + getPostcode + " Info: " + getDescription;
        }

        /// <summary>
        /// This method is to create/register a new house
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static House Create(User owner)
        {
            Console.WriteLine("Register your house");
            string addressHouse = UserInterface.GetInput("Address");
            int postcodeHouse = UserInterface.GetInteger("Postcode");
            string descriptionHouse = UserInterface.GetInput("Description");

            House NewHouse = new House(addressHouse, postcodeHouse, descriptionHouse, owner);

            Console.WriteLine("Address: {0}, postcode: {1} registered successfully", NewHouse.getAddress, NewHouse.getPostcode);
            Console.WriteLine();
            return NewHouse;
        }

        /// <summary>
        /// This tax method is based on the tax method in Property class
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public override double Tax(double price)
        {
            const double taxRate = 0.1;
            double taxHouse = taxRate * price;
            return taxHouse;

        }
    }
}
