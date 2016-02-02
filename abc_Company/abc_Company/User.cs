using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_Company
{
    public class User
    {
        public string username_;
        public string password_;
        public string email_;
        public string address_;
        public string city_;
        public string state_;
        public int zip_;

        public User()
        {
            username_ = null;
            password_ = null;
            email_ = null;
            address_ = null;
            city_ = null;
            state_ = null;
            zip_ = 0;
        }

        // set methods
        public void setUsernmae(string username)
        {
            username_ = username;
        }
        public void setPassword(string password)
        {
            password_ = password;
        }
        public void setEmail(string email)
        {
            email_ = email;
        }
        public void setAddress(string address)
        {
            address_ = address;
        }
        public void setCity(string city)
        {
            city_ = city;
        }
        public void setState(string state)
        {
            state_ = state;
        }
        public void setZIp(int zip)
        {
            zip_ = zip;
        }

        // get methods
        public string getUsernmae()
        {
            return username_;
        }
        public string getPassword()
        {
            return password_;
        }
        public string getEmail()
        {
            return email_;
        }
        public string getAddress()
        {
            return address_;
        }
        public string getCity()
        {
            return city_;
        }
        public string getState()
        {
            return state_;
        }
        public int getZIp()
        {
            return zip_;
        }
    }
}
