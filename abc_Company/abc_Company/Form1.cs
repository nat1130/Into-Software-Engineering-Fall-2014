using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace abc_Company
{
    public partial class Form1 : Form
    {
        List<PictureBox> images = new List<PictureBox>();
        List<Control> idLabels = new List<Control>();
        List<Button> viewItemButtons = new List<Button>();
        List<Control> itemNameLabels = new List<Control>();
        List<Control> descriptionLabels = new List<Control>();
        List<Control> dollarLabels = new List<Control>();
        List<Control> costLabels = new List<Control>();
        List<Item> qSearch = new List<Item>();
        Database database = new Database();
        string currentUser = null;

        public Form1()
        {
            InitializeComponent();
            //timmersInitialized();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            
            //Taking user to first tab
            tabControl1.SelectedTab = tabPage1;

            //Adding Items to Lists
            addingItemsToList_s();
            makeItemsInvistble();
            makeNextAndPreviousInvisible();

            // Making order History Button Invisible
            orderHistoryButton.Visible = false;

            viewItemTimePicker.Visible = false;
            viewItemIdLabel.Visible = false;
            ///////////////////////////////////////////
            
        }

        //Resetting objects to default properties
        protected void resetObjects()
        {
            for (int index = 0; index < 8; index++)
            {
                idLabels[index].Text = null;
                images[index].Image = null; 
                itemNameLabels[index].Text = null;
                descriptionLabels[index].Text = null;
                costLabels[index].Text = null;
            }

            pageCounter = 0; currentPage = 1;

            makeItemsInvistble();
        }

        // Setting thing visiblity of objects
        protected void makeNextAndPreviousVisible()
        {
            nextLinkLabel.Visible = true;
            previousLinkLabel.Visible = true;
        }
        protected void makeNextAndPreviousInvisible()
        {
            nextLinkLabel.Visible = false;
            previousLinkLabel.Visible = false;
        }
        protected void makeItemsInvistble()
        {
            for (int index = 0; index < 8; index++)
            {
                idLabels[index].Visible = false;
                images[index].Visible = false;
                viewItemButtons[index].Visible = false;
                itemNameLabels[index].Visible = false;
                descriptionLabels[index].Visible = false;
                dollarLabels[index].Visible = false;
                costLabels[index].Visible = false;
            }
        }
        protected void makeItemsVistble()
        {
            for (int index = 0; index < 8; index++)
            {
                images[index].Visible = true;
                viewItemButtons[index].Visible = true;
                itemNameLabels[index].Visible = true;
                descriptionLabels[index].Visible = true;
                dollarLabels[index].Visible = true;
                costLabels[index].Visible = true;
            }
        }
        // Adding objects into specified List
        protected void addingItemsToList_s()
        {
            //Adding Buttons to list
            viewItemButtons.Add(button1);
            viewItemButtons.Add(button2);
            viewItemButtons.Add(button3);
            viewItemButtons.Add(button4);
            viewItemButtons.Add(button5);
            viewItemButtons.Add(button6);
            viewItemButtons.Add(button7);
            viewItemButtons.Add(button8);
            //Item Name labels to list
            itemNameLabels.Add(name0Label);
            itemNameLabels.Add(name1Label);
            itemNameLabels.Add(name2Label);
            itemNameLabels.Add(name3Label);
            itemNameLabels.Add(name4Label);
            itemNameLabels.Add(name5Label);
            itemNameLabels.Add(name6Label);
            itemNameLabels.Add(name7Label);
            //Item Description Label to list
            descriptionLabels.Add(description0Label);
            descriptionLabels.Add(description1Label);
            descriptionLabels.Add(description2Label);
            descriptionLabels.Add(description3Label);
            descriptionLabels.Add(description4Label);
            descriptionLabels.Add(description5Label);
            descriptionLabels.Add(description6Label);
            descriptionLabels.Add(description7Label);
            //Dollar Labels to list
            dollarLabels.Add(dollar0Label);
            dollarLabels.Add(dollar1Label);
            dollarLabels.Add(dollar2Label);
            dollarLabels.Add(dollar3Label);
            dollarLabels.Add(dollar4Label);
            dollarLabels.Add(dollar5Label);
            dollarLabels.Add(dollar6Label);
            dollarLabels.Add(dollar7Label);
            //Cost Labels
            costLabels.Add(price0Label);
            costLabels.Add(price1Label);
            costLabels.Add(price2Label);
            costLabels.Add(price3Label);
            costLabels.Add(price4Label);
            costLabels.Add(price5Label);
            costLabels.Add(price6Label);
            costLabels.Add(price7Label);
            //ID Lables to list
            idLabels.Add(searchID0Label);
            idLabels.Add(searchID1Label);
            idLabels.Add(searchID2Label);
            idLabels.Add(searchID3Label);
            idLabels.Add(searchID4Label);
            idLabels.Add(searchID5Label);
            idLabels.Add(searchID6Label);
            idLabels.Add(searchID7Label);
            //Picture Box to list
            images.Add(picBox0);
            images.Add(picBox1);
            images.Add(picBox2);
            images.Add(picBox3);
            images.Add(picBox4);
            images.Add(picBox5);
            images.Add(picBox6);
            images.Add(picBox7);
            
        }

        // Checking for Input
        protected bool orderHistSubmitCheckInput()
        {
            bool result = false;
            if (orderHistNumsComboBox.SelectedIndex == 0)
            {
                MessageBox.Show("Choose Order Number");
            }
            else
            {
                //MessageBox.Show("Information Available");
                result = true;
            }
            return result;
        }
        protected bool pymtSubmitItemCheckInput()
        {
            bool result = false;

            if (pymtFirstNameTextBox.Text == "" || pymtFirstNameTextBox.Text == null)
            {
                MessageBox.Show("Invalid Name");
            }
            else if (pymtLastNameTextBox.Text == "" || pymtLastNameTextBox.Text == null)
            {
                MessageBox.Show("Invalid Last Name");
            }
            else if (pymtCardNumMaskTextBox.MaskFull == false)
            {
                MessageBox.Show("Invalid Card Number");
            }
            else if (pymtMonthDateComboBox.SelectedItem == null)
            {
                MessageBox.Show("Month no Chosen");
            }
            else if (pymtYearDateComboBox.SelectedItem == null)
            {
                MessageBox.Show("Date not Chosen");
            }
            else if (pymtSecuCodeMaskedTextBox.MaskFull == false)
            {
                MessageBox.Show("Invalid Security Code");
            }
            else if (pymtCardTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Card Type not Chosen");
            }
            else
            {
                //MessageBox.Show("Order Submited");
                result = true;
            }
            
            return result;
        }
        protected bool adminSerchItemCheckInput()
        {
            bool result = false;
            if ((adminIDTextBox.Text == "" || adminIDTextBox.Text == null) &&
                (adminNameTextBox.Text == "" || adminNameTextBox.Text == null))
            {
                MessageBox.Show("Invalid Search");
            }
            else
            {
                result = true;
                //MessageBox.Show("Information Processed");
            }
            return result;
        }
        protected bool adminUpdateItemCheckInput()
        {
            bool result = false;
            if (adminIDTextBox.Text == "" || adminIDTextBox.Text == null)
            {
                MessageBox.Show("Invalid ID");
            }
            else if (adminNameTextBox.Text == "" || adminNameTextBox.Text == null)
            {
                MessageBox.Show("Invalid Name");
            }
            else if (adminDescriTextBox.Text == "" || adminDescriTextBox.Text == null)
            {
                MessageBox.Show("Invalid Description");
            }
            else if (adminCostTextBox.Text == "" || adminCostTextBox.Text == null)
            {
                MessageBox.Show("Invalid Cost");
            }
            else if (adminQtyTextBox.Text == "" || adminQtyTextBox.Text == null)
            {
                MessageBox.Show("Invalid Quantity");
            }
            else if (adminImageTextBox.Text == "" || adminImageTextBox.Text == null)
            {
                MessageBox.Show("Invalid Image Name");
            }
            else
            {
                result = true;
                //MessageBox.Show("Information Processed");
            }
            return result;
        }
        protected bool adminNewItemCheckInput()
        {
            bool result = false;
            //if (adminIDTextBox.Text == "" || adminIDTextBox.Text == null)
            //{
                //MessageBox.Show("Invalid ID");
            //} else
            if (adminNameTextBox.Text == "" || adminNameTextBox.Text == null)
            {
                MessageBox.Show("Invalid Name");
            }
            else if (adminDescriTextBox.Text == "" || adminDescriTextBox.Text == null)
            {
                MessageBox.Show("Invalid Description");
            }
            else if (adminCostTextBox.Text == "" || adminCostTextBox.Text == null)
            {
                MessageBox.Show("Invalid Cost");
            }
            else if (adminQtyTextBox.Text == "" || adminQtyTextBox.Text == null)
            {
                MessageBox.Show("Invalid Quantity");
            }
            else if (adminImageTextBox.Text == "" || adminImageTextBox.Text == null)
            {
                MessageBox.Show("Invalid Image Name");
            }
            else
            {
                result = true;
                MessageBox.Show("Information Processed");
            }
            return result;
        }
        protected bool newUserRegisterCheckInput()
        {
            CheckEmail util = new CheckEmail();

            bool result = false;
            if (newUserEmailTextBox.Text == "" || newUserEmailTextBox.Text == null)
            {
                MessageBox.Show("Invalid Email");
            }
            else if (newUserNameTextBox.Text == "" || newUserNameTextBox.Text == null)
            {
                MessageBox.Show("Invalid Username");
            }
            else if (newUserPasswordTextBox.Text == "" || newUserPasswordTextBox.Text == null)
            {
                MessageBox.Show("Invalid Password");
            }
            else if (newUserAddressTextBox.Text == "" || newUserAddressTextBox.Text == null)
            {
                MessageBox.Show("Invalid Address");
            }
            else if (newUserCityTextBox.Text == "" || newUserCityTextBox.Text == null)
            {
                MessageBox.Show("Invalid City");
            }
            else if (newUserStateTextBox.MaskCompleted == false || newUserStateTextBox.Text == null)
            {
                MessageBox.Show("Invalid State");
            }
            else if (newUserZipTextBox.MaskFull == false || !newUserZipTextBox.MaskCompleted)
            {
                MessageBox.Show("Invalid Zip");
            }
            else
            {
                if (util.IsValidEmail(newUserEmailTextBox.Text))
                {
                    result = true;
                }
                else
                {
                    MessageBox.Show("Please Enter A Valid Email");
                    newUserEmailTextBox.Clear();
                    newUserPasswordTextBox.Clear();
                }
            }

            return result;
        }
        protected bool usernNameSignInCheckInput()
        {
            bool result = false;

            if (userNameLoginTextBox.Text == "" || userNameLoginTextBox.Text == null &&
                userNamePasswordTextBox.Text == "" || userNamePasswordTextBox.Text == null)
                MessageBox.Show("Please enter a valid username and password");
            else
                result = true;
 
            return result;
        }
        protected bool searchButtonCheckInput() 
        {
            bool result = false;
            if (searchTextBox.Text == null || searchTextBox.Text == "")
                MessageBox.Show("Please enter a valid Search");
            else
                result = true;

            return result;
        }

        private string getOrderNumber()
        {
            string orderNumber;
            int num2, num3, num4;
            Random rnd = new Random();
            num2 = rnd.Next(0, 9);
            num3 = rnd.Next(0, 9);
            num4 = rnd.Next(0, 9);
            orderNumber = "1" + num2 + num3 + num4;
            return orderNumber;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            if (searchButtonCheckInput() == true)
                launchSearch();         
        }

        protected void launchSearch()
        {
            resetObjects();

            database.searchQuery(qSearch, searchTextBox.Text);
            if (qSearch.Count < 1)
            {
                MessageBox.Show("No Results Found");
                searchTextBox.Clear();
                makeNextAndPreviousInvisible();
            }
            else
            {
                if (qSearch.Count > 8)
                    makeNextAndPreviousVisible();

                // setting the count for total items in the list 
                int listSize = 0;
                if (qSearch.Count > 8)
                    listSize = 8;
                else
                {
                    listSize = qSearch.Count;
                    makeNextAndPreviousInvisible();
                }

                for (int index = 0; index < listSize; index++)
                {
                    itemNameLabels[index].Text = qSearch[index].getName();
                    descriptionLabels[index].Text = qSearch[index].getDescription();

                    costLabels[index].Text = "" + qSearch[index].getCost();
                    idLabels[index].Text = "" + qSearch[index].getId();

                    // Setting Image
                    images[index].SizeMode = PictureBoxSizeMode.StretchImage;
                    // Checking if Image is Available 
                    try { images[index].Image = Image.FromFile("images\\" + qSearch[index].getImage() + ".jpg"); }
                    catch { images[index].Image = Image.FromFile("images\\coming_soon.jpg"); }

                    images[index].Visible = true;
                    viewItemButtons[index].Visible = true;
                    itemNameLabels[index].Visible = true;
                    descriptionLabels[index].Visible = true;
                    dollarLabels[index].Visible = true;
                    costLabels[index].Visible = true;
                }
            }
        }
        int pageCounter = 0, currentPage = 1;

        private void nextLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pageCounter += 8;
            int count = 0; currentPage++;
            int index = pageCounter;

            makeItemsInvistble();
            

            while (index < qSearch.Count() && count < 8)
            {
                itemNameLabels[count].Text = qSearch[index].getName();
                descriptionLabels[count].Text = qSearch[index].getDescription();

                costLabels[count].Text = "" + qSearch[index].getCost();
                idLabels[count].Text = "" + qSearch[index].getId();
                // Setting Image
                images[count].SizeMode = PictureBoxSizeMode.StretchImage;
                // Checking if Image is Available 
                try { images[count].Image = Image.FromFile("images\\" + qSearch[index].getImage() + ".jpg"); }
                catch { images[count].Image = Image.FromFile("images\\coming_soon.jpg"); }

                // Setting Cart Label to Visible
                itemNameLabels[count].Visible = true;
                descriptionLabels[count].Visible = true;
                images[count].Visible = true;
                viewItemButtons[count].Visible = true;
                dollarLabels[count].Visible = true;
                costLabels[count].Visible = true;

                index++; count++;
            }

            if (index == qSearch.Count())
                nextLinkLabel.Visible = false;

        }

        private void previousLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(pageCounter> 7)
                pageCounter -= 8;
            int count = 0; currentPage++;
            int index = pageCounter;

            makeItemsInvistble();
            if (index < qSearch.Count())
                nextLinkLabel.Visible = true;
            while (index < qSearch.Count() && count < 8)
            {

                itemNameLabels[count].Text = qSearch[index].getName();
                descriptionLabels[count].Text = qSearch[index].getDescription();

                costLabels[count].Text = "" + qSearch[index].getCost();
                idLabels[count].Text = "" + qSearch[index].getId();
                // Setting Image
                images[count].SizeMode = PictureBoxSizeMode.StretchImage;
                // Checking if Image is Available 
                try { images[count].Image = Image.FromFile("images\\" + qSearch[index].getImage() + ".jpg"); }
                catch { images[count].Image = Image.FromFile("images\\coming_soon.jpg"); }

                // Setting Cart Label to Visible
                itemNameLabels[count].Visible = true;
                descriptionLabels[count].Visible = true;
                images[count].Visible = true;
                viewItemButtons[count].Visible = true;
                dollarLabels[count].Visible = true;
                costLabels[count].Visible = true;

                index++; count++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            viewItem(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            viewItem(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            viewItem(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            viewItem(3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            viewItem(4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            viewItem(5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            viewItem(6);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            viewItem(7);
        }

        /*private void tabPage2_Click(object sender, EventArgs e)
        {
            //viewItem(6);
        }*/

        protected void viewItem(int sucker)
        {
            tabControl1.SelectedTab = tabPage6;

            viewItemPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            viewItemPictureBox.Image = images[sucker].Image;

            viewItemIdLabel.Text = idLabels[sucker].Text;
            viewItemDetailsLabel.Text = String.Format("{0}", itemNameLabels[sucker].Text);
            viewItemDescriptionLabel.Text = descriptionLabels[sucker].Text;
            viewItemCostLabel.Text = costLabels[sucker].Text;
        }

        private void viewItemBuyButton_Click(object sender, EventArgs e)
        {
            
            if (viewItemNumeric.Value > 0)
            {
                float cost = float.Parse(viewItemNumeric.Value.ToString()) * float.Parse(viewItemCostLabel.Text);
                string currentDate = "" + viewItemTimePicker.Value.Year + viewItemTimePicker.Value.Month + viewItemTimePicker.Value.Day;

                cartDataGridView.Rows.Insert(cartDataGridView.RowCount - 1, viewItemDetailsLabel.Text, viewItemNumeric.Value.ToString(), cost, null ,viewItemIdLabel.Text, currentDate );

                clearViewItemTab();
            }
            else
            {
                MessageBox.Show("Item Not Added To Cart");
                clearViewItemTab();
            }
            tabControl1.SelectedTab = tabPage2;
        }
        protected void clearViewItemTab()
        {
            viewItemIdLabel.Text = "ID";
            viewItemDetailsLabel.Text = "Name & Description";
            viewItemCostLabel.Text = "Cost";
            viewItemNumeric.Text = "0";
        }
        private void shoppingCartButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage7;
        }
        private void cartDeleteButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in cartDataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells[3].Value))
                {
                    cartDataGridView.Rows.RemoveAt(row.Index);
                }
            }
        }

        private void cartSubmitButton_Click(object sender, EventArgs e)
        {
            if (cartDataGridView.Rows.Count < 2)
            {
                MessageBox.Show("Cart Empty \nOrder Not Submited");
                tabControl1.SelectedTab = tabPage2;
            }
            else if (pymtUserNameLabel.Text == "Not Signed In" || pymtUserNameLabel.Text == null)
            {
                MessageBox.Show("Sign In First");
                tabControl1.SelectedTab = tabPage3;
            }
            else
                tabControl1.SelectedTab = tabPage8;
        }

        private void pymtSubmitButton_Click(object sender, EventArgs e)
        {
            resetObjects();
            searchTextBox.Clear();

            if (pymtSubmitItemCheckInput() == true)
            {
                tabControl1.SelectedTab = tabPage9;
 
                processingOrder();
                cartDataGridView.Rows.Clear();
            }
        }

        private void processingOrder()
        {
            List<OrderHistory> myHist = new List<OrderHistory>();
            OrderHistory myItem = new OrderHistory();

            string item, quantity, price, id, date;
            string orderNumber = getOrderNumber();

            for (int RowIndex = 0; RowIndex < cartDataGridView.RowCount - 1; RowIndex++) 
            {
                item = cartDataGridView.Rows[RowIndex].Cells["itemColumn"].Value.ToString();
                quantity = cartDataGridView.Rows[RowIndex].Cells["qtyColumn"].Value.ToString();
                price = cartDataGridView.Rows[RowIndex].Cells["costColumn"].Value.ToString();
                id = cartDataGridView.Rows[RowIndex].Cells["idColumn"].Value.ToString();
                date = cartDataGridView.Rows[RowIndex].Cells["dateColumn"].Value.ToString();

                myItem.setUserName(item);
                myItem.setItemQuantity(int.Parse(quantity));
                myItem.setCost(float.Parse(price));

                myHist.Add(myItem);
                database.orderHistoryInsert(currentUser, orderNumber, id, price, quantity, "processing", date);
            }

            orderHistNumsComboBox.Items.Clear();
            mailFunction(orderNumber, myHist,  pymtCardNumMaskTextBox.Text.Substring(15)); 
            clearPymtObjects();
            gettingPastOrders();
            orderHistNumsComboBox.SelectedIndex = -1;
            orderHistNumsComboBox.Text = "Select Order Number";

            MessageBox.Show("Order " + orderNumber + " Submited");
            makeNextAndPreviousInvisible();
            tabControl1.SelectedTab = tabPage2;
        }

        void clearPymtObjects()
        {
            pymtFirstNameTextBox.Clear();
            pymtLastNameTextBox.Clear();
            pymtCardNumMaskTextBox.Clear();
            pymtMonthDateComboBox.SelectedIndex = -1;
            pymtYearDateComboBox.SelectedIndex = -1;
            pymtSecuCodeMaskedTextBox.Clear();
            pymtCardTypeComboBox.SelectedIndex = -1;
        }
        List<OrderHistory> orderHistory = new List<OrderHistory>();
        private void orderHistSearchButton_Click(object sender, EventArgs e)
        {
            //orderHistNumsComboBox.Items.Clear();
            //gettingPastOrders();

            if (orderHistNumsComboBox.SelectedIndex != -1)
            {
                orderHistDataGridView.Rows.Clear();
                gettingDesiredIdInfo();
                orderHistNumsComboBox.SelectedIndex = -1;
                orderHistNumsComboBox.Text = "Select Order Number";
                
            }
        }
        private void gettingPastOrders()
        {
            orderHistNumsComboBox.Items.Clear();
            orderHistNumsComboBox.Text = "Select Order Number";

            List<int> orderNums = new List<int>();
            database.getOrderNums(orderNums, currentUser);
            for (int index = 0; index < orderNums.Count; index++)
            {
                orderHistNumsComboBox.Items.Add(orderNums[index]);
            }
        }

        private void singInEventButton_Click(object sender, EventArgs e)
        {
            //Enabaling objects when a user has loged off
            if (currentUser != null )
            {
                orderHistoryButton.Visible = false;
                singInEventButton.Text = "Sign In";
                pymtUserNameLabel.Text = "Not Signed In";
                orderHistUsernameLabel.Text = "Username";
                MessageBox.Show(currentUser + " Logged Off");
                currentUser = null;
                signInButton.Enabled = true;
                newUserGudeLinkLabel.Enabled = true;
                removingPastOrdersFromComboBox();
                removingItemsFromDataGridViews();
            }

            tabControl1.SelectedTab = tabPage3;

        }
        private void removingPastOrdersFromComboBox()
        {
            for (int index = 0; index < orderHistory.Count; index++)
            {
                orderHistNumsComboBox.Items.Remove(index);
            }

            orderHistory.Clear();
        }
        private void removingItemsFromDataGridViews()
        {
            cartDataGridView.Rows.Clear();
            orderHistDataGridView.Rows.Clear();
        }

        private void signInButton_Click(object sender, EventArgs e)
        {
            if(usernNameSignInCheckInput() == true)
            {
                gettingUserLogin();
            }
        }
        private void gettingUserLogin()
        {
            currentUser = database.userLoginQuery(userNameLoginTextBox.Text, userNamePasswordTextBox.Text);

            //Disabling certain objects once user has loged on
            if (currentUser == "abc_admin" )
            {
                singInEventButton.Text = "Log out " + currentUser;
                pymtUserNameLabel.Text = currentUser;
                orderHistUsernameLabel.Text = currentUser;
                orderHistoryButton.Visible = true;
                signInButton.Enabled = false;
                newUserGudeLinkLabel.Enabled = false;
                tabControl1.SelectedTab = tabPage5;
            }
            else if (currentUser != "Invalid User")
            {
                singInEventButton.Text = "Log out " + currentUser;
                pymtUserNameLabel.Text = currentUser;
                orderHistUsernameLabel.Text = currentUser;
                orderHistoryButton.Visible = true;
                signInButton.Enabled = false;
                newUserGudeLinkLabel.Enabled = false;
                tabControl1.SelectedTab = tabPage1;
            }
            else
                MessageBox.Show(currentUser + "\nTry Again");

            userNameLoginTextBox.Text = "";
            userNamePasswordTextBox.Text = "";
        }

        private void newUserGudeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        private void newUserRegisterButton_Click(object sender, EventArgs e)
        {
            if (newUserRegisterCheckInput() == true)
            {
                //Getting new user information
                string newUserName = newUserNameTextBox.Text,
                       newUserPassword = hash(newUserPasswordTextBox.Text),
                       newUserEmail = newUserEmailTextBox.Text,
                       newUserAddress = newUserAddressTextBox.Text,
                       newUserCity = newUserCityTextBox.Text,
                       newUserState = newUserStateTextBox.Text;
                int newUserZip = int.Parse(zipCode(newUserZipTextBox.Text));

                // Adding the new User into the database
                if (database.checkingIfUserExistQuery(newUserName) == newUserName)
                {
                    MessageBox.Show("User Alread Exist");
                }
                else
                    database.newUserInsert(newUserName, newUserPassword, newUserEmail, newUserAddress, newUserCity, newUserState, newUserZip);

                newUserNameTextBox.Clear();
                newUserPasswordTextBox.Clear();
                newUserEmailTextBox.Clear();
                newUserAddressTextBox.Clear();
                newUserCityTextBox.Clear();
                newUserStateTextBox.Clear();
                newUserZipTextBox.Clear();
                tabControl1.SelectedTab = tabPage2;
            }
        }

        protected string hash(string userPassword)
        {
            string hesh = userPassword;
            SHA512 shaHash = new SHA512Managed();
            byte[] hash = shaHash.ComputeHash(Encoding.UTF8.GetBytes(hesh));
            shaHash.Clear();

            return  Convert.ToBase64String(hash);
        }
        string zipCode(string textInput)
        {
            string zipInput = "";
            for (int index = 0; index < textInput.Length; index++)
            {
                switch (textInput[index])
                {
                    case '-': break;
                    default: zipInput += textInput[index]; break;
                };

            }

            return zipInput;
        }
        private void orderHistoryButton_Click(object sender, EventArgs e)
        {
            orderHistNumsComboBox.Items.Clear();
            gettingPastOrders();
            tabControl1.SelectedTab = tabPage9;
  
        }

        protected void gettingDesiredIdInfo()
        {
            string itemSelected = orderHistNumsComboBox.Text, idName;
            database.orderHistoryQuery(orderHistory, currentUser, itemSelected);

            for (int index = 0; index < orderHistory.Count; index++)
            {
                idName = database.getIdName(orderHistory[index].getItemId().ToString());
                orderHistDataGridView.Rows.Insert(orderHistDataGridView.RowCount - 1, idName, orderHistory[index].getDate(),
                                                  orderHistory[index].getCost(), orderHistory[index].getItemQuantity(), "Processed");
       
            }
        }

        private void adminSearchButton_Click(object sender, EventArgs e)
        {
            Item adminItem = new Item();

            if (adminSerchItemCheckInput() == true)
            {
                database.adminSearch(adminItem, adminNameTextBox.Text);
                
                adminNameTextBox.Text = adminItem.getName();
                adminIDTextBox.Text = adminItem.getId().ToString();
                adminDescriTextBox.Text = adminItem.getDescription();
                adminCostTextBox.Text = "" + adminItem.getCost();
                adminQtyTextBox.Text = "" + adminItem.getQuantity();
                adminImageTextBox.Text = adminItem.getImage();
            }
        }

        private void adminUpdateButton_Click(object sender, EventArgs e)
        {
            if (adminUpdateItemCheckInput() == true)
            {
                string currentDate = "" + adminDateTimePicker.Value.Year + adminDateTimePicker.Value.Month + adminDateTimePicker.Value.Day;
                database.updateInventory(adminNameTextBox.Text, adminDescriTextBox.Text,
                                         adminCostTextBox.Text, adminQtyTextBox.Text,
                                         adminImageTextBox.Text, currentDate, adminIDTextBox.Text);
                
                clearingAdminInformation();
            }
        }

        private void adminAddButton_Click(object sender, EventArgs e)
        {
            string currentDate = "" + adminDateTimePicker.Value.Year + adminDateTimePicker.Value.Month + adminDateTimePicker.Value.Day;

            if (adminNewItemCheckInput() == true)
            {
                database.newitemInsert(adminNameTextBox.Text, adminDescriTextBox.Text, adminCostTextBox.Text, adminQtyTextBox.Text, adminImageTextBox.Text, currentDate);

                clearingAdminInformation();
            }
        }

        private void clearingAdminInformation()
        {
            adminIDTextBox.Clear();
            adminNameTextBox.Clear();
            adminDescriTextBox.Clear();
            adminCostTextBox.Clear();
            adminQtyTextBox.Clear();
            adminImageTextBox.Clear();
        }

        private void cartBackToSearch_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void pymtGoBackToSearchButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void orderHistGoBackToSearchButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void mailFunction(string orderNumber, List<OrderHistory> currentOrder, string ccInfo)
        {
            string getEmail = database.getEmailQuery(currentUser);
            try
            {
                string theBody = String.Format("Greetings {0},\n\nYou ordered:", currentUser);
                float totalCost = 0;
                foreach (var i in currentOrder)
                {
                    theBody += String.Format("\nName: {0}\nQuantity: {1}\nCost: ${2}\n", i.getUserName(), i.getItemQuantity(), i.getCost());
                    totalCost += i.getCost();
                }
                theBody += String.Format("\nTotal Cost: ${0}\nCharged to card ending in: {1}\n\nPlease refer to order number {2} to check its status.\n\nThank you for your business!", totalCost, ccInfo, orderNumber);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("abccompany362@gmail.com");
                mail.To.Add(getEmail);
                mail.Subject = String.Format("ABC Company - Order# {0}", orderNumber);
                mail.Body = theBody;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("abccompany362@gmail.com", "dickbutt");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                //MessageBox.Show("mail Send");
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.ToString());
               MessageBox.Show("Error sending email.");
            }
        }

        private void timmersInitialized()
        {
            timer1.Start();
            timer2.Start();
            timer3.Start();
            timer4.Start();
            timer5.Start();
            timer6.Start();
            timer7.Start();
        }

        int count1 = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (count1 == 0)
            {
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox3.Image = Image.FromFile("images\\featured.jpg");
                timer1.Stop();
                count1++;
            }
            else if (count1 == 1)
            {
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox3.Image = Image.FromFile("images\\wow.jpg");
                count1++;
            }
            else
            {
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox3.Image = Image.FromFile("images\\featured.jpg");
                count1 = 0;
            }
            
        }

        int count2 = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (count2 == 0)
            {
                pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox6.Image = Image.FromFile("images\\COMING-SOON.jpg");
                timer2.Stop();
                count2++;
            }
            else if (count2 == 1)
            {
                pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox6.Image = Image.FromFile("images\\hooray.jpg");
                count2++;
            }
            else
            {
                pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox6.Image = Image.FromFile("images\\COMING-SOON.jpg");
                count2 = 0;
            }
        }

        int count3 = 0;
        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Stop();
            /*if (count3 == 0)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Image.FromFile("images\\date.jpg");
                count3++;
            }
            else if (count3 == 1)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Image.FromFile("images\\percent.jpg");
                count3++;
            }
            else
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Image.FromFile("images\\date.jpg");
                count3 = 0;
            }*/
            
        }

        int count4 = 0;
        private void timer4_Tick(object sender, EventArgs e)
        {
            if (count4 == 0)
            {
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox4.Image = Image.FromFile("toCome\\IggyAzalea_TheNewClassic.jpg");
                count4++;
            }
            else if (count4 == 1)
            {
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox4.Image = Image.FromFile("toCome\\Marron 5 V.jpg");
                count4++;
            }
            else
            {
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox4.Image = Image.FromFile("toCome\\taylor-swift-1989.jpg");
                count4 = 0;
            }
        }

        private void signInGoBackButon_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void adminClearButton_Click(object sender, EventArgs e)
        {
            clearingAdminInformation();
        }
        
    }
}
