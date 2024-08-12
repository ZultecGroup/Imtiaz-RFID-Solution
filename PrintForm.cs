using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Drawing.Printing;
using System.Collections.Concurrent;

using System.Threading;

namespace ZulLabel
{
    public partial class PrintForm : Form
    {
        private RequestLog _logger =new RequestLog();
        public PrintForm()
        {
            InitializeComponent();
        }
        #region declarations
        string connectionString = ConfigurationManager.AppSettings["str"];
        string label = "";
        DataTable dataTableForPrint = new DataTable();
        DataTable dataTableForBulkEan = new DataTable();
        DataTable dataTableSystemSettings = new DataTable();
        LoaderFrm load = new LoaderFrm();
        string sessionID = "";


        Dictionary<string, Dictionary<string, int>> articles = new Dictionary<string, Dictionary<string, int>>();
       
        #endregion
        private void PrintForm_Load(object sender, EventArgs e)
        {// Assuming you have a TabControl named "tabControl1"
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ItemSize = new System.Drawing.Size(250, 30); // Adjust width and height as needed
            tabControl2.SizeMode = TabSizeMode.Fixed;
            tabControl2.ItemSize = new System.Drawing.Size(250, 30); // Adjust width and height as needed


            dataTableForPrint.Columns.Add("EAN number", typeof(string));
            dataTableForPrint.Columns.Add("ModelandColor", typeof(string));
            dataTableForPrint.Columns.Add("Size", typeof(string));
            dataTableForPrint.Columns.Add("Description", typeof(string));
            dataTableForPrint.Columns.Add("Price", typeof(string));

            dataTableForBulkEan.Columns.Add("Code", typeof(string));
            dataTableForBulkEan.Columns.Add("Quantity", typeof(int));
            loadSingleFormParameters();

            loadSystemConfiguration();
            loadSystemBulkConfiguration();

            loadLabel();
        }

        #region system setting and other stuffs
        public void loadSystemConfiguration()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL command to select all records from the table
                    string selectCommandText = $"SELECT * FROM tbl_SystemSetting where rowName='single'";

                    // Create a DataTable to store the results
                    DataTable dataTable = new DataTable();

                    // Execute the SQL command and fill the DataTable
                    using (SqlCommand command = new SqlCommand(selectCommandText, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                    singleprintURL.Text = dataTable.Rows[0][2].ToString();
                    printedURL.Text = dataTable.Rows[0][3].ToString();
                    txtUsername.Text = dataTable.Rows[0][4].ToString();
                    txtPassword.Text = dataTable.Rows[0][5].ToString();
                    priceURL.Text = dataTable.Rows[0][10].ToString();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void loadSystemBulkConfiguration()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL command to select all records from the table
                    string selectCommandText = $"SELECT * FROM tbl_SystemSetting where rowName='bulk'";

                    // Create a DataTable to store the results
                    DataTable dataTable = new DataTable();

                    // Execute the SQL command and fill the DataTable
                    using (SqlCommand command = new SqlCommand(selectCommandText, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                    txtbulkprintURL.Text = dataTable.Rows[0][2].ToString();
                    txtbulkrecievedURL.Text = dataTable.Rows[0][3].ToString();
                    txtbulkPassword.Text = dataTable.Rows[0][5].ToString();
                    txtbulkUsername.Text = dataTable.Rows[0][4].ToString();
                    txtbulkClient.Text = dataTable.Rows[0][6].ToString();
                    txtbulkID.Text = dataTable.Rows[0][7].ToString();
                    txtbulksiteID.Text = dataTable.Rows[0][8].ToString();
                    txtbulkvariantsURL.Text = dataTable.Rows[0][9].ToString();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void loadLabel()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL command to select all records from the table
                    string selectCommandText = $"SELECT * FROM tbl_Label";

                    // Create a DataTable to store the results
                    DataTable dataTable = new DataTable();

                    // Execute the SQL command and fill the DataTable
                    using (SqlCommand command = new SqlCommand(selectCommandText, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                    txtLabel.Text = dataTable.Rows[0][1].ToString();
                    label = dataTable.Rows[0][1].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public void loadSingleFormParameters()

        {
            dataTableSystemSettings.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // SQL query to select all rows from the table
                    string query = "SELECT * FROM tbl_SystemSetting";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        // Fill the DataTable with data from the SQL query
                        adapter.Fill(dataTableSystemSettings);
                    }
                    connection.Close();
                }
                if (dataTableSystemSettings.Rows.Count < 0)
                {
                    MessageBox.Show("No setting found");

                }
                // loadSingleData(barcode.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton213_Click(object sender, EventArgs e)
        {
            pages.SetPage("Print");

            dataTableForPrint.Clear();
            //dataTableForPrint.Columns.Add("EAN number", typeof(string));
            //dataTableForPrint.Columns.Add("Color", typeof(string));
            //dataTableForPrint.Columns.Add("Size", typeof(string));
            //dataTableForPrint.Columns.Add("Description", typeof(string));


            loadSystemConfiguration();
            loadSingleFormParameters();
            loadSystemBulkConfiguration();
            loadLabel();



        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            loadSingleFormParameters();
            loadSystemBulkConfiguration();

            pages.SetPage("System Configuration");
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            loadLabel();
            pages.SetPage("Label");
        }

        private void pages_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }



        private void labelSave_Click(object sender, EventArgs e)
        {
           
        }

        private void sysConfigSave_Click(object sender, EventArgs e)
        {
        }

        private void sysConfigSave_Click_1(object sender, EventArgs e)
        {
            string truncateCommandText = $"Delete from [tbl_SystemSetting] where rowName='single'";
            string insertCommandText = "INSERT INTO [tbl_SystemSetting] (Epc_Reservation,printed,Username,Password,rowName,variantsURL,priceURL) VALUES (@Value1,@Value2,@Value3,@Value4,@value5,@Value6,@value7)";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Delete previous record
                    using (SqlCommand truncateCommand = new SqlCommand(truncateCommandText, connection))
                    {
                        int rowsAffected = truncateCommand.ExecuteNonQuery();
                    }

                    // Insert new record
                    using (SqlCommand insertCommand = new SqlCommand(insertCommandText, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Value1", singleprintURL.Text);
                        insertCommand.Parameters.AddWithValue("@Value2", printedURL.Text);
                        insertCommand.Parameters.AddWithValue("@Value3", txtUsername.Text);
                        insertCommand.Parameters.AddWithValue("@Value4", txtPassword.Text);
                        insertCommand.Parameters.AddWithValue("@Value5", "single");
                        insertCommand.Parameters.AddWithValue("@Value6", txtbulkvariantsURL.Text);
                        insertCommand.Parameters.AddWithValue("@Value7", priceURL.Text);
                        int rowsAffected = insertCommand.ExecuteNonQuery();

                    }
                    connection.Close();

                    bunifuSnackbar1.Show(this,
                    "Single Print System Configuration updated",
                    Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success,
                     2000, "",
                     Bunifu.UI.WinForms.BunifuSnackbar.Positions.TopRight,
                     Bunifu.UI.WinForms.BunifuSnackbar.Hosts.FormOwner);


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            string truncateCommandText2 = $"Delete from [tbl_SystemSetting] where rowName='bulk'";
            string insertCommandText2 = "INSERT INTO [tbl_SystemSetting] (Epc_Reservation,printed,Username,Password,rowName,Client,Client_id,siteID,variantsURL) VALUES (@Value1,@Value2,@Value3,@Value4,@value5,@Value6,@Value7,@value8,@value9)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Delete previous record
                    using (SqlCommand truncateCommand2 = new SqlCommand(truncateCommandText2, connection))
                    {
                        int rowsAffected = truncateCommand2.ExecuteNonQuery();
                    }

                    // Insert new record
                    using (SqlCommand insertCommand = new SqlCommand(insertCommandText2, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Value1", txtbulkprintURL.Text);
                        insertCommand.Parameters.AddWithValue("@Value2", txtbulkrecievedURL.Text);
                        insertCommand.Parameters.AddWithValue("@Value3", txtbulkUsername.Text);
                        insertCommand.Parameters.AddWithValue("@Value4", txtbulkPassword.Text);
                        insertCommand.Parameters.AddWithValue("@Value5", "bulk");
                        insertCommand.Parameters.AddWithValue("@Value6", txtbulkClient.Text);
                        insertCommand.Parameters.AddWithValue("@Value7", txtbulkID.Text);
                        insertCommand.Parameters.AddWithValue("@Value8", txtbulksiteID.Text);
                        insertCommand.Parameters.AddWithValue("@Value9", txtbulkvariantsURL.Text);
                        int rowsAffected = insertCommand.ExecuteNonQuery();

                    }
                    connection.Close();

                    bunifuSnackbar1.Show(this,
                    "Bulk System Configuration updated",
                    Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success,
                     2000, "",
                     Bunifu.UI.WinForms.BunifuSnackbar.Positions.TopRight,
                     Bunifu.UI.WinForms.BunifuSnackbar.Hosts.FormOwner);


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void bulkSysConfig_Click(object sender, EventArgs e)
        {

            string truncateCommandText = $"Delete from [tbl_SystemSetting] where rowName='single'";
            string insertCommandText = "INSERT INTO [tbl_SystemSetting] (Epc_Reservation,printed,Username,Password,rowName,variantsURL,priceURL) VALUES (@Value1,@Value2,@Value3,@Value4,@value5,@Value6,@value7)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Delete previous record
                    using (SqlCommand truncateCommand = new SqlCommand(truncateCommandText, connection))
                    {
                        int rowsAffected = truncateCommand.ExecuteNonQuery();
                    }

                    // Insert new record
                    using (SqlCommand insertCommand = new SqlCommand(insertCommandText, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Value1", singleprintURL.Text);
                        insertCommand.Parameters.AddWithValue("@Value2", printedURL.Text);
                        insertCommand.Parameters.AddWithValue("@Value3", txtUsername.Text);
                        insertCommand.Parameters.AddWithValue("@Value4", txtPassword.Text);
                        insertCommand.Parameters.AddWithValue("@Value5", "single");
                        insertCommand.Parameters.AddWithValue("@Value6", txtbulkvariantsURL.Text);
                        insertCommand.Parameters.AddWithValue("@Value7", priceURL.Text);

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                    }
                    connection.Close();

                    bunifuSnackbar1.Show(this,
                    "Single Print System Configuration updated",
                    Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success,
                     2000, "",
                     Bunifu.UI.WinForms.BunifuSnackbar.Positions.TopRight,
                     Bunifu.UI.WinForms.BunifuSnackbar.Hosts.FormOwner);


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            string truncateCommandText2 = $"Delete from [tbl_SystemSetting] where rowName='bulk'";
            string insertCommandText2 = "INSERT INTO [tbl_SystemSetting] (Epc_Reservation,printed,Username,Password,rowName,Client,Client_id,siteID,variantsURL) VALUES (@Value1,@Value2,@Value3,@Value4,@value5,@Value6,@Value7,@value8,@value9)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Delete previous record
                    using (SqlCommand truncateCommand2 = new SqlCommand(truncateCommandText2, connection))
                    {
                        int rowsAffected = truncateCommand2.ExecuteNonQuery();
                    }

                    // Insert new record
                    using (SqlCommand insertCommand = new SqlCommand(insertCommandText2, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Value1", txtbulkprintURL.Text);
                        insertCommand.Parameters.AddWithValue("@Value2", txtbulkrecievedURL.Text);
                        insertCommand.Parameters.AddWithValue("@Value3", txtbulkUsername.Text);
                        insertCommand.Parameters.AddWithValue("@Value4", txtbulkPassword.Text);
                        insertCommand.Parameters.AddWithValue("@Value5", "bulk");
                        insertCommand.Parameters.AddWithValue("@Value6", txtbulkClient.Text);
                        insertCommand.Parameters.AddWithValue("@Value7", txtbulkID.Text);
                        insertCommand.Parameters.AddWithValue("@Value8", txtbulksiteID.Text);
                        insertCommand.Parameters.AddWithValue("@Value9", txtbulkvariantsURL.Text);
                        int rowsAffected = insertCommand.ExecuteNonQuery();

                    }
                    connection.Close();

                    bunifuSnackbar1.Show(this,
                    "Bulk System Configuration updated",
                    Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success,
                     2000, "",
                     Bunifu.UI.WinForms.BunifuSnackbar.Positions.TopRight,
                     Bunifu.UI.WinForms.BunifuSnackbar.Hosts.FormOwner);


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static string ConvertToBase64(string text)
        {
            // Convert the text to a byte array
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            // Convert the byte array to Base64 string
            string base64Text = Convert.ToBase64String(bytes);

            return base64Text;
        }

        #endregion

        #region single print

        public async Task<string> checkEANorBarcode(string barcode)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, dataTableSystemSettings.Rows[0][9].ToString());
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Basic " + ConvertToBase64(dataTableSystemSettings.Rows[0][4] + ":" + dataTableSystemSettings.Rows[0][5]));
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new KeyValuePair<string, string>("version", "1.0"));
            collection.Add(new KeyValuePair<string, string>("params", "{\"barcode\": \"" + barcode + "\"}"));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;


            int maxTries = 5;
            TimeSpan baseDelay = TimeSpan.FromSeconds(10);
            int tries = 0;

            while (tries < maxTries)
            {
                try
                {
                    var response = await client.SendAsync(request);
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    //JObject responseObject = JObject.Parse(jsonResponse);
                    int statusCode = (int)response.StatusCode;
                 

                    if (statusCode >= 500 && statusCode < 600)
                    {
                        // Perform exponential backoff
                        TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, tries) * baseDelay.TotalSeconds);
                        _logger.Log(sessionID, statusCode + $"Attempt {tries + 1}: Waiting for {delay.TotalSeconds} seconds before retrying...", DateTime.Now.ToString(), "", request.ToString(), barcode);

                        await Task.Delay(delay);

                        tries++;
                    }
                    else if (statusCode == 200)
                    {
                        JObject responseObject = JObject.Parse(jsonResponse);
                        // Successful response
                        string firstGtin = (string)responseObject["data"]["gtins"][0];
                        //_logger.Log(sessionID, barcode + " | the EAN received is= " + firstGtin, DateTime.Now.ToString());
                        string price = await getPrice(firstGtin, barcode);
                        dataTableForPrint.Rows.Add(firstGtin, (string)responseObject["data"]["groups"]["ModelColor"]["description"], (string)responseObject["data"]["characteristic_02"], (string)responseObject["data"]["groups"]["Family"]["description"],price);
                        return firstGtin;
                    }
                    else if (statusCode == 401 || statusCode == 403)
                    {
                        load.Hide();
                        _logger.Log(sessionID, barcode + " | " + jsonResponse, DateTime.Now.ToString(), "", request.ToString(), barcode);

                        MessageBox.Show(jsonResponse);
                        return "error";
                    }
                    else
                    {
                        JObject responseObject = JObject.Parse(jsonResponse);
                        // Log error and return "notfound"
                        _logger.Log(sessionID, responseObject.ToString(), DateTime.Now.ToString(), "", request.ToString(), barcode);
                        // detailRichbox.Text += Environment.NewLine + Environment.NewLine + barcode + " " + (string)responseObject["errors"][0].ToString();
                        load.Hide();
                        return "notfound";
                    }
                }
                catch (Exception ex)
                {
                    load.Hide();
                    // Log the error
                    _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString(), "", request.ToString(), barcode);
                    break;

                    //// Perform exponential backoff
                    //TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, tries) * baseDelay.TotalSeconds);
                    //await Task.Delay(delay);

                    //tries++;
                }
            }

            if (tries >= maxTries)
            {
                load.Hide();
                // Log that maximum retries exceeded
                _logger.Log(sessionID, "Maximum retries exceeded", DateTime.Now.ToString(), "", request.ToString(), barcode);
                MessageBox.Show("Maximum retries exceeded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "ok";
        }

        public async Task<string> getPrice(string gtin,string barcode)
        {
            var client = new HttpClient();
            string urlPrice = dataTableSystemSettings.Rows[0][10].ToString().Replace("|barcode|", gtin);

            var url = urlPrice + "?version=1.0";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Basic " + ConvertToBase64(dataTableSystemSettings.Rows[0][4] + ":" + dataTableSystemSettings.Rows[0][5]));


            int maxTries = 5;
            TimeSpan baseDelay = TimeSpan.FromSeconds(10);
            int tries = 0;

            while (tries < maxTries)
            {
                try
                {
                    var response = await client.SendAsync(request);
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    //JObject responseObject = JObject.Parse(jsonResponse);
                    int statusCode = (int)response.StatusCode;


                    if (statusCode >= 500 && statusCode < 600)
                    {
                        // Perform exponential backoff
                        TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, tries) * baseDelay.TotalSeconds);
                        _logger.Log(sessionID, statusCode + $"Attempt {tries + 1}: Waiting for {delay.TotalSeconds} seconds before retrying...", DateTime.Now.ToString(), gtin, urlPrice,barcode);

                        await Task.Delay(delay);

                        tries++;
                    }
                    else if (statusCode == 200)
                    {
                        load.Hide();
                        JObject responseObject = JObject.Parse(jsonResponse);
                        string price = (string)responseObject["data"]["dynamic_attributes"]["RelativeMarketValue"];
                        return price;

                    }
                    else if (statusCode == 401 || statusCode == 403)
                    {
                        load.Hide();
                        _logger.Log(sessionID, barcode + " | " + jsonResponse, DateTime.Now.ToString(), gtin, urlPrice, barcode);

                        MessageBox.Show(jsonResponse);
                        return "error";
                    }
                    else
                    {
                        JObject responseObject = JObject.Parse(jsonResponse);
                        // Log error and return "notfound"
                        _logger.Log(sessionID, responseObject.ToString(), DateTime.Now.ToString(), gtin, urlPrice, barcode);
                        // detailRichbox.Text += Environment.NewLine + Environment.NewLine + barcode + " " + (string)responseObject["errors"][0].ToString();
                        load.Hide();
                        return "notfound";
                    }
                }
                catch (Exception ex)
                {
                    // Log the error
                    _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString(), gtin, urlPrice, barcode);
                    break;

                    //// Perform exponential backoff
                    //TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, tries) * baseDelay.TotalSeconds);
                    //await Task.Delay(delay);

                    //tries++;
                }
            }

            if (tries >= maxTries)
            {
                // Log that maximum retries exceeded
                _logger.Log(sessionID, "Maximum retries exceeded", DateTime.Now.ToString(), gtin, urlPrice, barcode);
                MessageBox.Show("Maximum retries exceeded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return "";


        }

        private async void btnPrint_Click(object sender, EventArgs e)
        {
            dataTableForBulkEan.Clear();
            dataTableForPrint.Clear();
            
            load.Show();
            sessionID = DateTime.Now.ToString("yyyyMMddHHmmss");
            loadSingleFormParameters();

            loadSystemConfiguration();
            loadSystemBulkConfiguration();
            loadLabel();

            string ean = await checkEANorBarcode(barcode.Text);
            if (ean == "notfound" || ean=="error" || ean=="ok")
            {
                MessageBox.Show(ean);
                return;
            }
            string epc = dataTableSystemSettings.Rows[0][2].ToString().Replace("|barcode|", ean);
            for (int i = 0; i < int.Parse(txtQuantity.Text); i++)
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, epc);

                    // Adding headers
                    request.Headers.Add("Accept", "application/json");
                    request.Headers.Add("Authorization", "Basic " + ConvertToBase64(dataTableSystemSettings.Rows[0][4] + ":" + dataTableSystemSettings.Rows[0][5]));

                    // Adding form data
                    var formData = new List<KeyValuePair<string, string>>();
                    formData.Add(new KeyValuePair<string, string>("version", "1.0"));

                    // Convert form data to content
                    var content = new FormUrlEncodedContent(formData);
                    request.Content = content;

                    int maxTries = 5;
                    TimeSpan baseDelay = TimeSpan.FromSeconds(10);
                    int tries = 0;

                    while (tries < maxTries)
                    {
                        try
                        {
                            // Send request and get response
                            var response = await client.SendAsync(request);
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            JObject responseObject = JObject.Parse(jsonResponse);
                            int statusCode = (int)response.StatusCode;

                            if (statusCode >= 500 && statusCode < 600)
                            {
                                // Perform exponential backoff
                                TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, tries) * baseDelay.TotalSeconds);
                                await Task.Delay(delay);

                                tries++;
                            }
                            else if (statusCode != 200)
                            {
                                dataTableForPrint.Clear();
                                _logger.Log(sessionID, (string)responseObject["errors"][0].ToString(), DateTime.Now.ToString(), ean, epc, barcode.Text);

                                // No need for exponential backoff for non-5xx errors
                                break;
                            }
                            else
                            {
                                string finalepc = (string)responseObject["data"]["epcs"][0];
                                // _logger.Log(sessionID, ean + " | " + "EPC received | " + finalepc, DateTime.Now.ToString());

                                response.EnsureSuccessStatusCode();

                                string zpl = label.Replace("#model", dataTableForPrint.Rows[0][1].ToString());
                                zpl = zpl.Replace("#desc", dataTableForPrint.Rows[0][3].ToString());
                                zpl = zpl.Replace("#epc", dataTableForPrint.Rows[0][0].ToString());
                                zpl = zpl.Replace("#forrfid", finalepc);
                                zpl = zpl.Replace("#size", dataTableForPrint.Rows[0][2].ToString());
                                zpl = zpl.Replace("#price", dataTableForPrint.Rows[0][4].ToString());
                                string forQR = "https://fashion.sa/products/";
                                forQR = forQR + dataTableForPrint.Rows[0][1].ToString().Split('-')[0] + "/";
                                forQR = forQR + dataTableForPrint.Rows[0][0].ToString();
                                zpl = zpl.Replace("#qr", forQR);
                                //string zplData = "^XA^FO50,50^ADN,36,20^FDHello, World!^FS^XZ"; // Replace with your ZPL data

                                // Create a PrintDocument

                                PrintDocument printDocument = new PrintDocument();
                                RawPrinterHelper.SendStringToPrinter(printDocument.PrinterSettings.PrinterName, zpl);

                                postAcknowledgeAsync(finalepc, barcode.Text);

                                break; // Exit the retry loop if request is successful
                            }

                        }
                        catch (Exception ex)
                        {
                            dataTableForPrint.Clear();
                            // Log the error
                            _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString(), ean, epc, barcode.Text);

                            break;
                            //// Perform exponential backoff
                            //TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, tries) * baseDelay.TotalSeconds);
                            //await Task.Delay(delay);

                            //tries++;
                        }
                    }

                    if (tries >= maxTries)
                    {
                        dataTableForPrint.Clear();
                        // Log that maximum retries exceeded
                        _logger.Log(sessionID, "Maximum retries exceeded", DateTime.Now.ToString(), ean, epc, barcode.Text);

                        MessageBox.Show("Maximum retries exceeded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            dataTableForPrint.Clear();

            load.Hide();
            }
        public async void postAcknowledgeAsync(string finalepc,string barcode)
        {
            string recievedEpc = dataTableSystemSettings.Rows[0][3].ToString().Replace("|epcs|", finalepc);

            try
            {
               
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, recievedEpc);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Basic " + ConvertToBase64(dataTableSystemSettings.Rows[0][4] + ":" + dataTableSystemSettings.Rows[0][5]));
                List<KeyValuePair<string, string>> collection = new List<KeyValuePair<string, string>>();

                collection.Add(new KeyValuePair<string, string>("version", "1.0"));

                var content = new FormUrlEncodedContent(collection);
                request.Content = content;
                var response = await client.SendAsync(request);
                string jsonResponse = await response.Content.ReadAsStringAsync();
                JObject responseObject = JObject.Parse(jsonResponse);
                int statusCode = (int)response.StatusCode;

                if (statusCode != 200)
                {
                    _logger.Log(sessionID, responseObject.ToString(), DateTime.Now.ToString(), finalepc, recievedEpc, barcode);

                }
                else
                {

                    //detailRichbox.Text += Environment.NewLine + recievedEpc + " Printed.";

                    // _logger.Log(sessionID, responseObject.ToString(), DateTime.Now.ToString(),finalepc, recievedEpc,barcode);
                    bunifuSnackbar1.Show(this, "Printed and acknowledged",
                Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success,
                 2000, "",
                 Bunifu.UI.WinForms.BunifuSnackbar.Positions.TopRight,
                 Bunifu.UI.WinForms.BunifuSnackbar.Hosts.FormOwner);
                    //MessageBox.Show("Printed and acknowledged");
                }
            }
            catch(Exception ex)
            {
                _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString(), finalepc, recievedEpc, barcode);
            }
        }

        #endregion

        #region bulk printing

        private async void bulkPrintBtn_Click(object sender, EventArgs e)
        {
            
            loadLabel();
            LoaderFrm load = new LoaderFrm();
            load.Show();
           
            // Convert the dictionary to JSON
            DateTime currentDateTime = DateTime.Now;

            // Format date and time as string in YYYYMMDDHHMMSS format
            string formattedDateTime = currentDateTime.ToString("yyyyMMddHHmmss");
            var payload = new RequestParameter.bulkPrint
            {
                client = dataTableSystemSettings.Rows[1][6].ToString(),
                reference = "PO" + formattedDateTime,
                supplier = new RequestParameter.Supplier
                {
                    id = dataTableSystemSettings.Rows[1][7].ToString(),
                    site_id = dataTableSystemSettings.Rows[1][8].ToString()
                },
                articles = articles.ToDictionary(
                    kvp => kvp.Key,
                    kvp => new RequestParameter.Article { count = kvp.Value["count"] }
                )
            };
           
            string jsonPayload = JsonConvert.SerializeObject(payload);


            var client = new HttpClient();

            // Set request headers
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ConvertToBase64(dataTableSystemSettings.Rows[1][4] + ":" + dataTableSystemSettings.Rows[1][5]));

            // Create HttpRequestMessage with POST method and URL
            var request = new HttpRequestMessage(HttpMethod.Post, dataTableSystemSettings.Rows[1][2].ToString());

            // Add content to the request
            request.Content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");


            HttpResponseMessage response = client.SendAsync(request).GetAwaiter().GetResult();
            //var response = await client.SendAsync(request);
            int statusCode = (int)response.StatusCode;
            int maxTries = 5;
            TimeSpan baseDelay = TimeSpan.FromSeconds(10);
            int tries = 0;
           
            while (tries < maxTries)
            {
                try
                {
                    if (statusCode >= 500 && statusCode < 600)
                    {
                        // Console.WriteLine($"Attempt {tries + 1}: Waiting for {delay.TotalSeconds} seconds before retrying...");

                        // Perform exponential backoff
                        TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, tries) * baseDelay.TotalSeconds);
                        string articlesString = String.Join(", ", articles.Keys);
                        string barcodes = txtQue.Text;
                        _logger.Log(sessionID, statusCode + $"Attempt {tries + 1}: Waiting for {delay.TotalSeconds} seconds before retrying...", DateTime.Now.ToString(), articlesString, dataTableSystemSettings.Rows[1][2].ToString(), barcodes);
                        detailRichbox.Text += Environment.NewLine + statusCode + $"Attempt {tries + 1}: Waiting for {delay.TotalSeconds} seconds before retrying...";

                        await Task.Delay(delay);

                        tries++;
                    }

                    else if (statusCode == 200)
                    {
                        // Read the response content as string and print
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        JObject responseObj = JObject.Parse(jsonResponse);

                        JObject sgtinsArticles = (JObject)responseObj["data"]["sgtins_articles"];
                        //string sgtinsArticlesJson = sgtinsArticles.ToString();
                        JObject jsonObject = JObject.Parse(sgtinsArticles.ToString());

                        List<string> epcsList = new List<string>();
                        List<string> epcsList2 = new List<string>();
                        // Loop through each property in the JObject
                        // Create a dictionary to store the index of each EAN in dataTableForBulkEan
                        var eanIndexMap = new Dictionary<string, int>();
                        for (int i = 0; i < dataTableForBulkEan.Rows.Count; i++)
                        {
                            string ean = dataTableForBulkEan.Rows[i]["Code"].ToString();
                            eanIndexMap[ean] = i;
                        }

                        // Create a new DataTable for sorted data
                        DataTable sortedDataTableForPrint = dataTableForPrint.Clone(); // Clone structure of dataTableForPrint

                        // Sort dataTableForPrint according to the order in dataTableForBulkEan
                        foreach (DataRow row in dataTableForBulkEan.Rows)
                        {
                            string ean = row["Code"].ToString();
                            if (eanIndexMap.ContainsKey(ean))
                            {
                                DataRow[] foundRows = dataTableForPrint.Select($"[EAN number] = '{ean}'");
                                if (foundRows.Length > 0)
                                {
                                    sortedDataTableForPrint.ImportRow(foundRows[0]);
                                }
                            }
                        }

                        // Replace the original dataTableForPrint with the sorted one
                        dataTableForPrint = sortedDataTableForPrint;
                        foreach (DataRow row in dataTableForPrint.Rows)
                        {
                            string ean = row["EAN number"].ToString();
                            if (sgtinsArticles.ContainsKey(ean))
                            {
                                JArray epcsArray = (JArray)sgtinsArticles[ean]["epcs"];
                                foreach (var epc in epcsArray)
                                {
                                    epcsList.Add(epc.ToString());
                                }
                            }
                        }

                        //// Print the result
                        //foreach (var epc in epcsList)
                        //{
                        //    Console.WriteLine(epc); // Output the EPCs in the order of EAN numbers from the DataTable
                        //}






                        //foreach (var property in jsonObject.Properties())
                        //{
                        //    // Get the 'epcs' array from the property value
                        //    JArray epcsArray = (JArray)property.Value["epcs"];

                        //    // Add each epc to the list
                        //    foreach (var epc in epcsArray)
                        //    {
                        //       // _logger.Log(sessionID,"EPC recieved "+epc, DateTime.Now.ToString());
                        //        epcsList.Add(epc.ToString());
                        //    }
                        //}
                        string epcsString = string.Join(", ", epcsList);
                        epcsList2 = epcsList.ToList();
                        // _logger.Log(sessionID, "EPC recieved " + epcsString, DateTime.Now.ToString());

                        // _logger.Log(sessionID, epcsList.Count.ToString() + " EPC recieved", DateTime.Now.ToString());

                        for (int i = 0; i < dataTableForBulkEan.Rows.Count; i++)
                        {
                            for (int j = 0; j < int.Parse(dataTableForBulkEan.Rows[i][1].ToString()); j++)
                            {
                                string zpl = label.Replace("#model", dataTableForPrint.Rows[i][1].ToString());
                                zpl = zpl.Replace("#desc", dataTableForPrint.Rows[i][3].ToString());
                                zpl = zpl.Replace("#epc", dataTableForPrint.Rows[i][0].ToString());

                                zpl = zpl.Replace("#forrfid", epcsList[0]);
                                zpl = zpl.Replace("#size", dataTableForPrint.Rows[i][2].ToString());
                                zpl = zpl.Replace("#price", dataTableForPrint.Rows[i][4].ToString());
                                string forQR = "https://fashion.sa/products/";
                                forQR = forQR + dataTableForPrint.Rows[i][1].ToString().Split('-')[0] + "/";
                                forQR = forQR + dataTableForPrint.Rows[i][0].ToString();
                                zpl = zpl.Replace("#qr", forQR);

                              


                                //string zpl = label.Replace("@name@", dataTableForPrint.Rows[i][3].ToString());
                                //zpl = zpl.Replace("@EPC@", epcsList[0]);
                                //zpl = zpl.Replace("@label@", dataTableForPrint.Rows[i][2].ToString());
                                PrintDocument printDocument = new PrintDocument();
                                RawPrinterHelper.SendStringToPrinter(printDocument.PrinterSettings.PrinterName, zpl);

                                epcsList.RemoveAt(0);
                                string[] lines = txtQue.Lines;

                                //lines[0] = dataTableForPrint.Rows[i][0].ToString()+ " X "+ (dataTableForBulkEan.Rows[i][1]-j).ToString();
                                //lines[0] = dataTableForPrint.Rows[i][0].ToString() + " X " + (int.Parse((string)dataTableForBulkEan.Rows[i][1]) - j).ToString();
                                lines[0] = dataTableForPrint.Rows[i][0].ToString() + " X " + (Convert.ToInt32(dataTableForBulkEan.Rows[i][1]) - (j+1)).ToString();
                                txtQue.Lines = lines;

                                string aa = txtQue.Text;
                               }
                            if (txtQue.Text.Split('\n').Length >= 2)
                            {
                                // Remove the first line
                                txtQue.Text = txtQue.Text.Remove(0, txtQue.GetFirstCharIndexFromLine(1));
                                // Ensure the caret is at the beginning
                                txtQue.SelectionStart = 0;
                            }


                        }
                        //_logger.Log(sessionID,"All EPC printed", DateTime.Now.ToString());

                        // Convert back to JSON string
                        //string sgtinsArticlesJson = sgtinsArticles.ToString();
                        postAcknowledgeBulk(epcsList2);
                        load.Hide();
                        dataTableForPrint.Clear();
                        break;
                    }

                    else if (statusCode == 401 || statusCode == 403)
                    {
                        try
                        {
                            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            string articlesString = String.Join(", ", articles.Keys);
                            string barcodes = txtQue.Text;
                            _logger.Log(sessionID,jsonResponse, DateTime.Now.ToString(), articlesString, dataTableSystemSettings.Rows[1][2].ToString(), barcodes);
                           // detailRichbox.Text += Environment.NewLine + jsonResponse;
                            load.Hide();
                            dataTableForPrint.Clear();
                            MessageBox.Show(jsonResponse);
                        }
                        catch (Exception ex)
                        {
                          
                            dataTableForPrint.Clear();
                            string articlesString = String.Join(", ", articles.Keys);
                            string barcodes = txtQue.Text;
                            _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString(), articlesString, dataTableSystemSettings.Rows[1][2].ToString(), barcodes);
                            load.Hide();
                            MessageBox.Show("An error occurred");
                           // detailRichbox.Text += "An error occurred";

                        }
                        break;
                    }
                    else
                    {
                        try
                        {
                            dataTableForPrint.Clear();
                            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                            JObject responseObject = JObject.Parse(jsonResponse);
                            // Log error and return "notfound"
                            string articlesString = String.Join(", ", articles.Keys);
                            string barcodes = txtQue.Text;
                            _logger.Log(sessionID,responseObject.ToString(), DateTime.Now.ToString(), articlesString, dataTableSystemSettings.Rows[1][2].ToString(), barcodes);
                            load.Hide();
                            MessageBox.Show(responseObject.ToString());
                            //detailRichbox.Text += Environment.NewLine + Environment.NewLine + barcode + " " + (string)responseObject["errors"].ToString();
                        }
                        catch (Exception ex)
                        {
                            dataTableForPrint.Clear();
                            string articlesString = String.Join(", ", articles.Keys);
                            string barcodes = txtQue.Text;
                            _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString(), articlesString, dataTableSystemSettings.Rows[1][2].ToString(), barcodes);
                            detailRichbox.Text += "An error occurred";
                            load.Hide();
                            MessageBox.Show("An error occurred");
                        }
                        break;
                    }
                }
                catch (Exception ex)
                {
                    dataTableForPrint.Clear();
                    string articlesString = String.Join(", ", articles.Keys);
                    string barcodes = txtQue.Text;
                    _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString(), articlesString, dataTableSystemSettings.Rows[1][2].ToString(), barcodes);
                    load.Hide();
                    break;
                }
                
            }
            if (tries >= maxTries)
            {
                dataTableForPrint.Clear();
                // Log that maximum retries exceeded
                _logger.Log(sessionID, "Maximum retries exceeded", DateTime.Now.ToString(),"","","");
                if (dataTableForBulkEan.Rows.Count > 0)
                {
                    string alleansfailed = "";
                    for (int i = 0; i < dataTableForBulkEan.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            alleansfailed = dataTableForBulkEan.Rows[i][0].ToString();
                        }
                        else
                        {
                            alleansfailed += "," + dataTableForBulkEan.Rows[i][0].ToString();
                        }
                    }
                    _logger.Log(sessionID, "Ean(s) that are tried are "+alleansfailed, DateTime.Now.ToString(), "", "", "");
                }
                else
                {
                     // No rows in the DataTable
                }
                //detailRichbox.Text += "Maximum retries exceeded";
                load.Hide();
                MessageBox.Show("Maximum retries exceeded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        public async Task postAcknowledgeBulk(List<string> epcsList)
        {
            // Endpoint URL
            string variantsUrl = dataTableSystemSettings.Rows[1][3].ToString();

            // Your epcsList

            string epcsJson = JsonConvert.SerializeObject(epcsList);

            // Create HttpClient instance
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, variantsUrl);

            // Add headers
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Basic  " + ConvertToBase64(dataTableSystemSettings.Rows[0][4] + ":" + dataTableSystemSettings.Rows[0][5]));

            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new KeyValuePair<string, string>("version", "1.0"));
            collection.Add(new KeyValuePair<string, string>("params", "{\"delivery_ids\": [],\"epcs\":" + epcsJson + "}"));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = client.SendAsync(request).GetAwaiter().GetResult();
            int statusCode = (int)response.StatusCode;
            int maxTries = 5;
            TimeSpan baseDelay = TimeSpan.FromSeconds(10);
            int tries = 0;

            while (tries < maxTries)
            {
                try
                {
                    if (statusCode >= 500 && statusCode < 600)
                    {
                        // Console.WriteLine($"Attempt {tries + 1}: Waiting for {delay.TotalSeconds} seconds before retrying...");

                        // Perform exponential backoff
                        TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, tries) * baseDelay.TotalSeconds);
                        string epp = String.Join(", ", epcsList);

                        _logger.Log(sessionID, statusCode + $"Attempt {tries + 1}: Waiting for {delay.TotalSeconds} seconds before retrying...", DateTime.Now.ToString(), epp, dataTableSystemSettings.Rows[0][4] + ":" + dataTableSystemSettings.Rows[0][5], "");
                        detailRichbox.Text += Environment.NewLine + statusCode + $"Attempt {tries + 1}: Waiting for {delay.TotalSeconds} seconds before retrying...";

                        await Task.Delay(delay);

                        tries++;
                    }
                    else if (statusCode == 200)
                    {
                        string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        JObject responseObject = JObject.Parse(jsonResponse);
                        // _logger.Log(sessionID, jsonResponse.ToString(), DateTime.Now.ToString());
                        bunifuSnackbar1.Show(this,
                   epcsList.Count.ToString() + " Barcode(s) / Ean(s) Printed and acknowledged",
                   Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success,
                    2000, "",
                    Bunifu.UI.WinForms.BunifuSnackbar.Positions.TopRight,
                    Bunifu.UI.WinForms.BunifuSnackbar.Hosts.FormOwner);
                       // MessageBox.Show("Printed and acknowledged");
                        break;
                    }
                    else if (statusCode == 401 || statusCode == 403)
                    {
                        try
                        {
                            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            string epp = String.Join(", ", epcsList);

                            _logger.Log(sessionID, jsonResponse, DateTime.Now.ToString(), epp, dataTableSystemSettings.Rows[0][4] + ":" + dataTableSystemSettings.Rows[0][5], "");
                            detailRichbox.Text += Environment.NewLine + jsonResponse;
                            MessageBox.Show(jsonResponse);
                        }
                        catch (Exception ex)
                        {
                            string epp = String.Join(", ", epcsList);

                            _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString(), epp, dataTableSystemSettings.Rows[0][4] + ":" + dataTableSystemSettings.Rows[0][5],"");
                            MessageBox.Show("An error occurred");
                            // detailRichbox.Text += "An error occurred";

                        }
                        break;
                    }
                    else
                    {
                        try
                        {
                            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                            JObject responseObject = JObject.Parse(jsonResponse);
                            // Log error and return "notfound"
                            string epp = String.Join(", ", epcsList);

                            _logger.Log(sessionID, responseObject.ToString(), DateTime.Now.ToString(), epp, dataTableSystemSettings.Rows[0][4] + ":" + dataTableSystemSettings.Rows[0][5], "");
                            // MessageBox.Show(responseObject.ToString());
                            MessageBox.Show("An error occurred");
                            //detailRichbox.Text += Environment.NewLine + Environment.NewLine + barcode + " " + (string)responseObject["errors"].ToString();
                        }
                        catch (Exception ex)

                        {
                            string epp = String.Join(", ", epcsList);

                            _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString(), epp, dataTableSystemSettings.Rows[0][4] + ":" + dataTableSystemSettings.Rows[0][5], "");
                            detailRichbox.Text += "An error occurred";
                            MessageBox.Show("An error occurred");
                        }
                        break;
                    }
                }
                catch (Exception ex)
                {
                    _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString(), "", "", "");
                    MessageBox.Show("An error occurred");
                    break;
                }
               
            }
            if (tries >= maxTries)
            {
                // Log that maximum retries exceeded
                string epp = String.Join(", ", epcsList);

                _logger.Log(sessionID, "Maximum retries exceeded", DateTime.Now.ToString(),epp, dataTableSystemSettings.Rows[0][4] + ":" + dataTableSystemSettings.Rows[0][5], "");
                detailRichbox.Text += "Maximum retries exceeded";
                string epcsString = string.Join(", ", epcsList);
                MessageBox.Show("Maximum retries exceeded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void loadFromFile_Click(object sender, EventArgs e)
        {
            dataTableForBulkEan.Clear();
            dataTableForPrint.Clear();
            LoaderFrm load = new LoaderFrm();
            load.Show();
            txtQue.Text = "";
            dataTableForBulkEan.Clear();

            sessionID = DateTime.Now.ToString("yyyyMMddHHmmss");

            // File path
            string filePath = userFilePath.Text;

            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("The specified file does not exist.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    load.Close();
                    return; // Exit the method if the file does not exist
                }

                string[] lines = File.ReadAllLines(filePath);

                // Use a list to collect tasks and a list to collect results
                List<Task<(string ean, int quantity)>> tasks = new List<Task<(string ean, int quantity)>>();

                // Process each line in parallel
                foreach (var line in lines.Skip(1))
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        string[] parts = line.Split(';');
                        string ean = await checkEANorBarcode(parts[0]);
                        if (ean != "notfound" && ean != "ok" && parts.Length == 2)
                        {
                            return (ean, int.Parse(parts[1]));
                        }
                        return (ean, 0); // Default return value for invalid entries
                    }));
                }

                // Await all tasks to complete
                var results = await Task.WhenAll(tasks);

                // Filter valid results and update the DataTable and UI
                foreach (var item in results.Where(r => r.ean != "notfound" && r.ean != "ok"))
                {
                    dataTableForBulkEan.Rows.Add(item.ean, item.quantity);
                    txtQue.Text += $"{item.ean} X {item.quantity}{Environment.NewLine}";
                }

                // Loop through each row in the DataTable and add data to the dictionary
                foreach (DataRow row in dataTableForBulkEan.Rows)
                {
                    string code = row[0].ToString();
                    int quantity = int.Parse(row[1].ToString());

                    if (!articles.ContainsKey(code))
                    {
                        articles[code] = new Dictionary<string, int>();
                    }

                    articles[code]["count"] = quantity;
                }

                bunifuSnackbar1.Show(this,
                    $"{lines.Length - 1} Barcode(s) / Ean(s) Loaded from file",
                    Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success,
                    2000, "",
                    Bunifu.UI.WinForms.BunifuSnackbar.Positions.TopRight,
                    Bunifu.UI.WinForms.BunifuSnackbar.Hosts.FormOwner);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                load.Close();
            }
        }



        //public async Task checkEANorBarcodeforBulk(string barcode)
        //{
        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage(HttpMethod.Post, dataTableSystemSettings.Rows[0][9].ToString());
        //    request.Headers.Add("Accept", "application/json");
        //    request.Headers.Add("Authorization", "Basic " + ConvertToBase64(dataTableSystemSettings.Rows[0][4] + ":" + dataTableSystemSettings.Rows[0][5]));
        //    var collection = new List<KeyValuePair<string, string>>();
        //    collection.Add(new KeyValuePair<string, string>("version", "1.0"));
        //    collection.Add(new KeyValuePair<string, string>("params", "{\"barcode\": \"" + barcode + "\"}"));
        //    var content = new FormUrlEncodedContent(collection);
        //    request.Content = content;


        //    var response = client.SendAsync(request).GetAwaiter().GetResult();
        //    int statusCode = (int)response.StatusCode;
        //    int maxTries = 5;
        //    TimeSpan baseDelay = TimeSpan.FromSeconds(10);
        //    int tries = 0;

        //    while (tries < maxTries)
        //    {
        //        try
        //        {

        //            //JObject responseObject = JObject.Parse(jsonResponse);

        //            if (statusCode >= 500 && statusCode < 600)
        //            {
        //               // Console.WriteLine($"Attempt {tries + 1}: Waiting for {delay.TotalSeconds} seconds before retrying...");

        //                // Perform exponential backoff
        //                TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, tries) * baseDelay.TotalSeconds);
        //                _logger.Log(sessionID, statusCode + $"Attempt {tries + 1}: Waiting for {delay.TotalSeconds} seconds before retrying...", DateTime.Now.ToString());
        //                detailRichbox.Text += Environment.NewLine + statusCode + $"Attempt {tries + 1}: Waiting for {delay.TotalSeconds} seconds before retrying...";

        //                await Task.Delay(delay);

        //                tries++;
        //            }
        //            else if (statusCode == 200)
        //            {
        //                try
        //                {
        //                    string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //                    JObject responseObject = JObject.Parse(jsonResponse);

        //                    // Access the "gtins" array
        //                    string firstGtin = (string)responseObject["data"]["gtins"][0];
        //                    dataTableForPrint.Rows.Add(firstGtin, (string)responseObject["data"]["characteristic_01"], (string)responseObject["data"]["characteristic_02"], (string)responseObject["data"]["reference_description"]);
        //                    _logger.Log(sessionID, barcode + " | the EAN received is= " + firstGtin, DateTime.Now.ToString());
        //                    detailRichbox.Text += barcode + " | the EAN received is= " + firstGtin;
        //                    break;
        //                }
        //                catch (Exception ex)
        //                {
        //                    _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString());
        //                    MessageBox.Show("An error occurred");
        //                    detailRichbox.Text += "An error occurred";

        //                }
        //            }
        //            else if (statusCode == 401 || statusCode == 403)
        //            {
        //                try
        //                {
        //                    string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        //                    _logger.Log(sessionID, barcode + " | " + jsonResponse, DateTime.Now.ToString());
        //                    detailRichbox.Text += Environment.NewLine + barcode + " | " + jsonResponse;
        //                    MessageBox.Show(jsonResponse);
        //                }
        //                catch (Exception ex)
        //                {
        //                    _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString());
        //                    MessageBox.Show("An error occurred");
        //                    detailRichbox.Text += "An error occurred";

        //                }
        //            }
        //            else
        //            {
        //                try {
        //                    string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        //                    JObject responseObject = JObject.Parse(jsonResponse);
        //                    // Log error and return "notfound"
        //                    _logger.Log(sessionID, barcode + " | " + (string)responseObject["errors"][0].ToString(), DateTime.Now.ToString());
        //                    detailRichbox.Text += Environment.NewLine + Environment.NewLine + barcode + " " + (string)responseObject["errors"][0].ToString();
        //                }
        //                catch (Exception ex)
        //                {
        //                    _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString());
        //                    detailRichbox.Text += "An error occurred";
        //                    MessageBox.Show("An error occurred");
        //                }

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log the error
        //            _logger.Log(sessionID, "An error occurred: " + ex.Message, DateTime.Now.ToString());
        //            detailRichbox.Text += "An error occurred";

        //            break;

        //            //// Perform exponential backoff
        //            //TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, tries) * baseDelay.TotalSeconds);
        //            //await Task.Delay(delay);

        //            //tries++;
        //        }
        //    }

        //    if (tries >= maxTries)
        //    {
        //        // Log that maximum retries exceeded
        //        _logger.Log(sessionID, "Maximum retries exceeded", DateTime.Now.ToString());
        //        detailRichbox.Text += "Maximum retries exceeded";

        //        MessageBox.Show("Maximum retries exceeded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        //private async void loadFromFile_Click(object sender, EventArgs e)
        //{
        //    dataTableForBulkEan.Clear();
        //    dataTableForPrint.Clear();
        //    LoaderFrm load = new LoaderFrm();
        //    load.Show();
        //    txtQue.Text = "";
        //    dataTableForBulkEan.Clear();

        //    sessionID = DateTime.Now.ToString("yyyyMMddHHmmss");

        //    string filePath = userFilePath.Text;

        //    try
        //    {
        //        if (!File.Exists(filePath))
        //        {
        //            MessageBox.Show("The specified file does not exist.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            load.Close();
        //            return;
        //        }

        //        // Use StreamReader for asynchronous reading
        //        List<Task> tasks = new List<Task>();
        //        using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        //        using (var bufferedStream = new BufferedStream(fileStream))
        //        using (var streamReader = new StreamReader(bufferedStream))
        //        {
        //            string header = await streamReader.ReadLineAsync(); // Skip the header

        //            while (!streamReader.EndOfStream)
        //            {
        //                var line = await streamReader.ReadLineAsync();
        //                tasks.Add(ProcessLineAsync(line));
        //            }
        //        }

        //        await Task.WhenAll(tasks);

        //        // Loop through each row in the DataTable and add data to the dictionary
        //        Parallel.ForEach(dataTableForBulkEan.Rows.Cast<DataRow>(), row =>
        //        {
        //            string code = row[0].ToString();
        //            int quantity = int.Parse(row[1].ToString());

        //            if (!articles.ContainsKey(code))
        //            {
        //                articles[code] = new Dictionary<string, int>();
        //            }

        //            articles[code]["count"] = quantity;
        //        });

        //        bunifuSnackbar1.Show(this,
        //           (tasks.Count).ToString() + " Barcode(s) / Ean(s) Loaded from file",
        //           Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success,
        //            2000, "",
        //            Bunifu.UI.WinForms.BunifuSnackbar.Positions.TopRight,
        //            Bunifu.UI.WinForms.BunifuSnackbar.Hosts.FormOwner);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //    }
        //    finally
        //    {
        //        load.Close();
        //    }
        //}

        //private async Task ProcessLineAsync(string line)
        //{
        //    string[] parts = line.Split(';');
        //    string ean = await checkEANorBarcode(parts[0]);
        //    if (ean != "notfound" && parts.Length == 2)
        //    {
        //        lock (dataTableForBulkEan)
        //        {
        //            dataTableForBulkEan.Rows.Add(ean, int.Parse(parts[1]));
        //        }
        //        txtQue.Invoke((Action)(() => txtQue.Text += $"{ean} X {parts[1]}{Environment.NewLine}"));
        //    }
        //}
        #endregion





        private void detailRichbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void logsPage_Click(object sender, EventArgs e)
        {
            loadInitLogs();
        }

        public void loadInitLogs()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL command to select all records from the table
                    string selectCommandText = $"SELECT sessionid as 'Session ID',min(CreatedTime) as 'Transaction Time' FROM tbl_requestlogs group by SessionID order by SessionID desc";

                    // Create a DataTable to store the results
                    DataTable dataTable = new DataTable();

                    // Execute the SQL command and fill the DataTable
                    using (SqlCommand command = new SqlCommand(selectCommandText, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                    logInitGrid.DataSource = dataTable;
                    logInitGrid.CellBorderStyle = DataGridViewCellBorderStyle.Single;

                    // Set the border color for all cells
                    logInitGrid.GridColor = Color.Black;
                    //txtLabel.Text = dataTable.Rows[0][1].ToString();
                    //label = dataTable.Rows[0][1].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            pages.SetPage("Log");
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void logInitGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string session = "";
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Get the value of the cell that was double-clicked
                string cellValue = logInitGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                // Use the cell value as needed (e.g., store it in a string)
                session = cellValue;

                // Now you can use stringValue as needed
             //   Console.WriteLine("Value of the double-clicked cell: " + stringValue);
            }
          
            LogsFrm form2 = new LogsFrm(session);
            form2.Show();
        }

        private void PrintForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            Application.Exit();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            // Call the ShowDialog method to show the dialog.
            DialogResult result = openFileDialog.ShowDialog();

            // Process input if the user clicked OK.
            if (result == DialogResult.OK)
            {
                // Set the text box text to the selected file's path.
                userFilePath.Text = openFileDialog.FileName;
            }
        }

        private void userFilePath_TextChange(object sender, EventArgs e)
        {
            loadFromFile.Enabled = true;
        }

        private void labelSave_Click_1(object sender, EventArgs e)
        {
            string truncateCommandText = $"TRUNCATE TABLE tbl_Label";
            string insertCommandText = "INSERT INTO tbl_Label (LabelText) VALUES (@Value1)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Delete previous record
                    using (SqlCommand truncateCommand = new SqlCommand(truncateCommandText, connection))
                    {
                        int rowsAffected = truncateCommand.ExecuteNonQuery();
                    }

                    // Insert new record
                    using (SqlCommand insertCommand = new SqlCommand(insertCommandText, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Value1", txtLabel.Text);
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                    }
                    connection.Close();

                    bunifuSnackbar1.Show(this,
                    "Label Updated",
                    Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success,
                     2000, "",
                     Bunifu.UI.WinForms.BunifuSnackbar.Positions.TopRight,
                     Bunifu.UI.WinForms.BunifuSnackbar.Hosts.FormOwner);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}