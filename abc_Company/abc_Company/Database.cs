using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace abc_Company
{
    public class Database
    {
        public SQLiteConnection conn;
        public Database()
        {
            conn = new SQLiteConnection("Data Source=shopping.sl3;Version=3;");
            try
            {
                conn.Open();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Insert Functions for Database

        public void newUserInsert(string newUserName, string newUserPassword, string newUserEmail, string newUserAddress, string newUserCity, string newUserState, int newUserZip)
        {
            SQLiteCommand addNewUser = conn.CreateCommand();
            addNewUser.CommandText = @"INSERT INTO users (username, password, email, address, city, state, zip) VALUES (@username, @password, @email, @address, @city, @state, @zip)";
            addNewUser.Parameters.Add(new SQLiteParameter("@username", newUserName));
            addNewUser.Parameters.Add(new SQLiteParameter("@password", newUserPassword));
            addNewUser.Parameters.Add(new SQLiteParameter("@email", newUserEmail));
            addNewUser.Parameters.Add(new SQLiteParameter("@address", newUserAddress));
            addNewUser.Parameters.Add(new SQLiteParameter("@city", newUserCity));
            addNewUser.Parameters.Add(new SQLiteParameter("@state", newUserState));
            addNewUser.Parameters.Add(new SQLiteParameter("@zip", newUserZip));
            addNewUser.ExecuteNonQuery();
            addNewUser.Dispose();

            MessageBox.Show("User " + newUserName + " added");
        }

        public void newitemInsert(string name, string description, string cost, string quantity, string image, string dateAdded)
        {
            SQLiteCommand addNewItem = conn.CreateCommand();
            addNewItem.CommandText = @"INSERT INTO item (name, description, cost, quantity, image, date_added) VALUES (@name, @description, @cost, @quantity, @image, @dateAdded)";
            //addNewItem.Parameters.Add(new SQLiteParameter("@id", itemID));
            addNewItem.Parameters.Add(new SQLiteParameter("@name", name));
            addNewItem.Parameters.Add(new SQLiteParameter("@description", description));
            addNewItem.Parameters.Add(new SQLiteParameter("@cost", cost));
            addNewItem.Parameters.Add(new SQLiteParameter("@quantity", quantity));
            addNewItem.Parameters.Add(new SQLiteParameter("@image", image));
            addNewItem.Parameters.Add(new SQLiteParameter("@dateAdded", dateAdded));
            addNewItem.ExecuteNonQuery();
            addNewItem.Dispose();

            MessageBox.Show("Item Inserted");
        }
  
        public void orderHistoryInsert(string userName, string orderNumber, string itemId, string cost, string itemQuantity, string orderStatus, string date)
        {
            SQLiteCommand userOrderHist = conn.CreateCommand();
            userOrderHist.CommandText = @"INSERT INTO order_history (username, order_number, item_id, single_item_id, cost, item_quantity, date) VALUES (@username, @order_number, @item_id, @single_item_id, @cost, @item_quantity,@date)";
            //addNewItem.Parameters.Add(new SQLiteParameter("@id", itemID));
            userOrderHist.Parameters.Add(new SQLiteParameter("@username", userName));
            userOrderHist.Parameters.Add(new SQLiteParameter("@order_number", orderNumber));
            userOrderHist.Parameters.Add(new SQLiteParameter("@item_id", itemId));
            userOrderHist.Parameters.Add(new SQLiteParameter("@single_item_id", itemId));
            userOrderHist.Parameters.Add(new SQLiteParameter("@cost", cost));
            userOrderHist.Parameters.Add(new SQLiteParameter("@item_quantity", itemQuantity));
            //addNewItem.Parameters.Add(new SQLiteParameter("@order_status", orderStatus));
            userOrderHist.Parameters.Add(new SQLiteParameter("@date", date));
            userOrderHist.ExecuteNonQuery();
            userOrderHist.Dispose();
        }
        //--------------------------------------------------------------------------------------------------------------------
        // Queries 
        
        public void searchQuery(List<Item> qSearch, string searchObject) //passList
        {
            qSearch.Clear();
            SQLiteCommand searchQuery = conn.CreateCommand();
            searchQuery.CommandText = @"SELECT * FROM item WHERE name LIKE @searchText OR description LIKE @searchText";
            searchQuery.Parameters.Add(new SQLiteParameter("@searchText", "%" + searchObject + "%"));
            searchQuery.CommandType = System.Data.CommandType.Text;
            SQLiteDataReader reader = searchQuery.ExecuteReader();
            
            while (reader.Read()) //Loop through here to insert objects into list
            {
                // Creat a new Item object
                Item itemDeets = new Item();

                // getting results from search
                string idReader = reader["id"].ToString(),
                       nameReader = reader["name"].ToString(),
                       descriptionReader = reader["description"].ToString(),
                       costReader = reader["cost"].ToString(),
                       quantity = reader["quantity"].ToString(),
                       image = reader["image"].ToString(),
                       id = reader["id"].ToString();

                // Setting the results into the item
                itemDeets.setId(int.Parse(idReader));
                itemDeets.setName(nameReader);
                itemDeets.setDescription(descriptionReader);
                itemDeets.setCost(float.Parse(costReader));
                itemDeets.setQuantity(int.Parse(quantity));
                itemDeets.setImage(image);
                itemDeets.setId(int.Parse(id));
                
                // Adding the object to the List of Items
                qSearch.Add(itemDeets);
            }
            reader.Dispose();
            searchQuery.Dispose();
        }
        public Item adminSearch(Item item,string orderName)
        {
            SQLiteCommand adminSearch = conn.CreateCommand();
            adminSearch.CommandText = @"SELECT * FROM item WHERE name LIKE @name";
            adminSearch.Parameters.Add(new SQLiteParameter("@name", orderName + "%"));
            adminSearch.CommandType = System.Data.CommandType.Text;
            SQLiteDataReader reader = adminSearch.ExecuteReader();

            reader.Read();
            string idReader = reader["id"].ToString(),
                       nameReader = reader["name"].ToString(),
                       descriptionReader = reader["description"].ToString(),
                       costReader = reader["cost"].ToString(),
                       quantity = reader["quantity"].ToString(),
                       image = reader["image"].ToString();
                       //date = reader["date"].ToString() ;

            item.setId(int.Parse(idReader));
            item.setName(nameReader);
            item.setDescription(descriptionReader);
            item.setCost(float.Parse(costReader));
            item.setQuantity(int.Parse(quantity));
            item.setImage(image);
            //item.setDateAdded(int.Parse(date));
            reader.Dispose();
            adminSearch.Dispose();
            return item;
        }
        public void getOrderNums(List<int> orderNumbers, string userName)
        {
            orderNumbers.Clear();

            SQLiteCommand orderNumber = conn.CreateCommand();
            orderNumber.CommandText = @"SELECT DISTINCT order_number FROM order_history WHERE username = @username";
            orderNumber.Parameters.Add(new SQLiteParameter("@username", userName));
            orderNumber.CommandType = System.Data.CommandType.Text;
            SQLiteDataReader reader = orderNumber.ExecuteReader();

            while (reader.Read())
            {
                string orderNum = reader["order_number"].ToString();
                orderNumbers.Add(int.Parse(orderNum));
            }

            reader.Dispose();
            orderNumber.Dispose();
        }
        public void orderHistoryQuery( List<OrderHistory> orderHistory, string username, string orderNum)
        {
            orderHistory.Clear();
            SQLiteCommand orderQuery = conn.CreateCommand();
            orderQuery.CommandText = @"SELECT * FROM order_history WHERE order_number = @order_number AND username = @username";
            orderQuery.Parameters.Add(new SQLiteParameter("@username", username));
            orderQuery.Parameters.Add(new SQLiteParameter("@order_number", orderNum));
            orderQuery.CommandType = System.Data.CommandType.Text;
            SQLiteDataReader reader = orderQuery.ExecuteReader();

            while (reader.Read())
            {
                OrderHistory gettingHistory = new OrderHistory(); 
                string userName = reader["username"].ToString(),
                       orderNumber = reader["order_number"].ToString(),
                       itemId = reader["item_id"].ToString(),
                       cost = reader["cost"].ToString(),
                       itemQuantity = reader["item_quantity"].ToString(),
                       date = reader["date"].ToString();

                gettingHistory.setUserName(userName);
                gettingHistory.setOrderNumber(int.Parse(orderNumber));
                gettingHistory.setItemId(int.Parse(itemId));
                gettingHistory.setCost(float.Parse(cost));
                gettingHistory.setItemQuantity(int.Parse(itemQuantity));
                gettingHistory.setDate(int.Parse(date));

                orderHistory.Add(gettingHistory);
            }
            reader.Dispose();
            orderQuery.Dispose();
        }

        public string getIdName(string id)
        {
            SQLiteCommand getId = conn.CreateCommand();
            getId.CommandText = @"SELECT * FROM item WHERE id = @id";
            getId.Parameters.Add(new SQLiteParameter("@id", id));
            getId.CommandType = System.Data.CommandType.Text;
            SQLiteDataReader reader = getId.ExecuteReader();

            reader.Read();

            string nameReader = reader["name"].ToString();
            getId.Dispose();
            return nameReader;
        }

        public string getEmailQuery(string username)
        {
            SQLiteCommand getUserEmail = conn.CreateCommand();
            getUserEmail.CommandText = @"SELECT email FROM users WHERE username = @username";
            getUserEmail.Parameters.Add(new SQLiteParameter("@username", username));
            getUserEmail.CommandType = System.Data.CommandType.Text;
            SQLiteDataReader reader = getUserEmail.ExecuteReader();

            reader.Read();

            string nameReader = reader["email"].ToString();
            getUserEmail.Dispose();
            return nameReader;
        }

        
          
        // Login
        public string userLoginQuery(string username, string passwordHash)
        {
            string user;
            SHA512 shaHash = new SHA512Managed();
            byte[] hash = shaHash.ComputeHash(Encoding.UTF8.GetBytes(passwordHash));
            SQLiteCommand userLogin = conn.CreateCommand();
            userLogin.CommandText = @"SELECT username FROM users WHERE username = @user AND password = @passwordHash";
            userLogin.Parameters.Add(new SQLiteParameter("@user", username));
            userLogin.Parameters.Add(new SQLiteParameter("@passwordHash", Convert.ToBase64String(hash)));
            userLogin.CommandType = System.Data.CommandType.Text;
            SQLiteDataReader reader = userLogin.ExecuteReader();

            reader.Read();
            try
            { user = reader["username"].ToString(); }
            catch { user = "Invalid User";  }
            userLogin.Dispose();
            shaHash.Clear();
            return user;
        }

        // Checking If User already exist in the database
        public string checkingIfUserExistQuery(string username)
        {
            string user;
            SQLiteCommand userExistQuery = conn.CreateCommand();
            userExistQuery.CommandText = @"SELECT username FROM users WHERE username = @user";
            userExistQuery.Parameters.Add(new SQLiteParameter("@user", username));
            userExistQuery.CommandType = System.Data.CommandType.Text;
            SQLiteDataReader reader = userExistQuery.ExecuteReader();

            reader.Read();
            try
            { user = reader["username"].ToString(); }
            catch { user = "VALID"; }
            userExistQuery.Dispose();
            return user;
        }
        // Displaying new Items
        public void newItems(List<string> imageNames)
        {
            string image = "";
            SQLiteCommand newItemQuery = conn.CreateCommand();
            newItemQuery.CommandText = @"SELECT * FROM item ORDER BY date ASC LIMIT 10";
            newItemQuery.CommandType = System.Data.CommandType.Text;
            SQLiteDataReader reader = newItemQuery.ExecuteReader();

            while (reader.Read())
            {
                image = reader["image"].ToString();
                imageNames.Add(image);
            }

            newItemQuery.Dispose();
            reader.Dispose();
        }

        // Back up just in case database doesn't exist
        public void createDatabase()
        {
            SQLiteConnection.CreateFile("shopping.sl3");
            SQLiteConnection conn = new SQLiteConnection("Data Source=shopping.sl3;Version=3;");
            conn.Open();
            SQLiteCommand createDB = conn.CreateCommand();
            createDB.CommandText = @"CREATE TABLE [cart] ([username] TEXT, [itemid] INTEGER)";
            createDB.ExecuteNonQuery();
            createDB.CommandText = @"CREATE TABLE [item_single] ([id] INTEGER, [item_id] INTEGER, [item_name] TEXT, [username] TEXT, PRIMARY KEY(id))";
            createDB.ExecuteNonQuery();
            createDB.CommandText = @"CREATE TABLE [item] ([id] INTEGER, [name] TEXT, [description] TEXT, [cost] INTEGER, [quantity] INTEGER, [image] TEXT, [date_added] INTEGER, [date_purchased] INTEGER, PRIMARY KEY(id))";
            createDB.ExecuteNonQuery();
            createDB.CommandText = @"CREATE TABLE [users] ([username] TEXT, [password] TEXT, [email] TEXT, [address] TEXT, [city] TEXT, [state] TEXT, zip [TEXT], PRIMARY KEY(username))";
            createDB.ExecuteNonQuery();
            createDB.CommandText = @"CREATE TABLE [order_history] ([username] TEXT, [item_id] INTEGER, [item_quantity] INTEGER, [single_item_id] INTEGER, [order_number] INTEGER, [order_status] INTEGER, [date] INTEGER)";
            createDB.ExecuteNonQuery();
            createDB.Dispose();
            conn.Close();
            conn.Dispose();
        }
        // -------------------------------------------------------------------
        // Update Inventory
        public void updateInventory(string name, string description, string cost, string quantity, string image, string dateAdded, string id)
        {
            SQLiteCommand updateItem = conn.CreateCommand();
            updateItem.CommandText = @"UPDATE item SET name = @name, description = @description, cost = @cost, quantity = @quantity, image = @image, date_added = @dateAdded   WHERE id = @id";
            updateItem.Parameters.Add(new SQLiteParameter("@id", id));
            updateItem.Parameters.Add(new SQLiteParameter("@name", name));
            updateItem.Parameters.Add(new SQLiteParameter("@description", description));
            updateItem.Parameters.Add(new SQLiteParameter("@cost", cost));
            updateItem.Parameters.Add(new SQLiteParameter("@quantity", quantity));
            updateItem.Parameters.Add(new SQLiteParameter("@image", image));
            updateItem.Parameters.Add(new SQLiteParameter("@dateAdded", dateAdded));
            updateItem.ExecuteNonQuery();
            updateItem.Dispose();

            MessageBox.Show("Item sucesfully updated");
        }
        ~Database()
        {
            //conn.Dispose();
            //conn.Close();
        }
    }
}
