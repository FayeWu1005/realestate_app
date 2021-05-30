using System;
using System.Collections.Generic;
using static Assignment.MainUI;

namespace Assignment
{
    public class Bid
    {
        // fields
        private User bidder;
        private Property propertyItem;
        private int bidPrice;

        // constructor
        public Bid(int bidPrice, User bidder, Property propertyItem)
        {
            this.bidPrice = bidPrice;
            this.bidder = bidder;
            this.propertyItem = propertyItem;
        }

        public int getUserBid { get { return bidPrice; } }
        public User getBidder { get; private set; }
        public Property getPropertyItem { get { return propertyItem; } }

        public override string ToString()
        {
            return bidder.getName + " (" + bidder.getEmail + ") " + "bid ($): " + bidPrice + " on address: " + propertyItem.getAddress + " with postcode: " + propertyItem.getPostcode;
        }

        // place bid method
        public static Bid placeNewBid(User bidder, Property propertyItem)
        {
            int bidAmt = UserInterface.GetInteger("Enter your bid ($)");

            Bid newBid = new Bid(bidAmt, bidder, propertyItem);
            return newBid;
        }

        

    }
}
