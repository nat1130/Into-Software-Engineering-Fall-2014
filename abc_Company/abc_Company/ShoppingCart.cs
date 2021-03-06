﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appabc_Company
{
    public class ShoppingCart
    {
        private string username_;
        private int itemId_;
        private int itemQuantity_;
        private int singleItemId_;
        private int orderNumber_;
        private int orderStatus_;
        private int date_;

        public ShoppingCart()
        {
            username_ = null;
            itemId_ = 0;
            itemQuantity_ = 0;
            singleItemId_ = 0;
            orderNumber_ = 0;
            orderStatus_ = 0;
            date_ = 0;
        }

        // set Methods
        public void setUserName(string userName)
        {
            username_ = userName;
        }
        public void setItemId(int itemId)
        {
            itemId_ = itemId;
        }
        public void setItemQuantity(int itemQuantity)
        {
            itemQuantity_ = itemQuantity;
        }
        public void setSingleItemId(int singleItemId)
        {
            singleItemId_ = singleItemId;
        }
        public void setOrderNumber(int orderNumber)
        {
            orderNumber_ = orderNumber;
        }
        public void setOrderStatus(int orderStatus)
        {
            orderStatus_ = orderStatus;
        }
        public void setDate(int date)
        {
            date_ = date;
        }

        // get Methods
        public string getUserName()
        {
            return username_;
        }
        public int getItemId()
        {
            return itemId_;
        }
        public int getItemQuantity()
        {
            return itemQuantity_;
        }
        public int getSingleItemId()
        {
            return singleItemId_;
        }
        public int getOrderNumber()
        {
            return orderNumber_;
        }
        public int getOrderStatus()
        {
            return orderStatus_;
        }
        public int getDate()
        {
            return date_;
        }

    }
}
