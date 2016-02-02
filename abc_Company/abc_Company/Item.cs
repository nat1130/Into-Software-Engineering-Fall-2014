using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_Company
{
    public class Item
    {
        private int id_;
        private string name_;
        private string description_; 
        private float cost_;
        private int quantity_;
        private string image_;
        private int dateAdded_; // need to write
        private int datePurchased_; // need to write

        public Item()
        {
            id_ = 0;
            name_ = null;
            description_ = null;
            cost_ = 0;
            quantity_=0;
            image_ = null;
            dateAdded_ = 0;
            datePurchased_ = 0;
        }
        // get methods
        public void setId(int id)
        {
            id_ = id;
        }
        public void setName(string name)
        {
            name_ = name;
        }
        public void setDescription(string description)
        {
            description_ = description;
        }
        public void setCost(float cost)
        {
            cost_ = cost;
        }
        public void setQuantity(int quantity)
        {
            quantity_ = quantity;
        }
        public void setImage(string image)
        {
            image_ = image;
        }
        public void setDateAdded(int dateAdded)
        {
            dateAdded_ = dateAdded;
        }
        public void setDatePurchased(int datePurchased)
        {
            datePurchased_ = datePurchased;
        }

        // set methods;
        public int getId()
        {
            return id_;
        }
        public string getName()
        {
            return name_;
        }
        public string getDescription()
        {
            return description_;
        }
        public float getCost()
        {
            return cost_;
        }
        public int getQuantity()
        {
            return quantity_;
        }
        public string getImage()
        {
            return image_;
        }
        public int getDateAdded()
        {
            return dateAdded_;
        }
        public int getDatePurchased()
        {
            return datePurchased_;
        }
    }
}
