using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment
{
    public class Land : Property
    {
        private int sqm;

        public Land(string address, int postcode, int sqm, User owner) : base(address, postcode, owner)
        {
            this.sqm = sqm;
        }

        public int getSqm { get { return sqm; } }

        public override string ToString()
        {
            return "Land: " + "Ad: " + getAddress + " Postcode: " + getPostcode + " Size(sqm): " + getSqm;
        }

        /// <summary>
        /// This method is to create/register a new land
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static Land Create(User owner)
        {
            Console.WriteLine("Register your land");
            string addressHouse = UserInterface.GetInput("Address");
            int postcodeHouse = UserInterface.GetInteger("Postcode");
            int sqm = UserInterface.GetInteger("Size (sqm)");

            Land NewLand = new Land(addressHouse, postcodeHouse, sqm, owner);

            Console.WriteLine("Address: {0}, postcode: {1}, size: {2} registered successfully", NewLand.getAddress, NewLand.getPostcode, NewLand.getSqm);
            Console.WriteLine();
            return NewLand;
        }

        /// <summary>
        /// This tax method is based on tax method in Property class
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public override double Tax(double price)
        {
            const double taxRate = 5.5;
            double taxLand = taxRate * sqm;
            return taxLand;

        }
    }

}
