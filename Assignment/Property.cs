using System;
using System.Collections.Generic;

namespace Assignment
{
    public abstract class Property
    {
        // property fileds
        private string address;
        private int postcode;

        // has-a relationship
        private User owner;

        // property constructor
        public Property(string address, int postcode, User owner)
        {
            this.address = address;
            this.postcode = postcode;
            this.owner = owner;
        }

        // set property
        public string getAddress { get { return address; } }
        public int getPostcode { get { return postcode; } }

        public User getOwner { get { return owner; } }

        // methods
        public abstract double Tax(double maxAmt);


    }

}
