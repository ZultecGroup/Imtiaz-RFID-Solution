
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZulLabel
{
    public partial class LogsFrm : Form
    {
        private string sessionID;
        string connectionString = ConfigurationManager.AppSettings["str"];

        public LogsFrm(string session)
        {
            InitializeComponent();
            sessionID = session;

        }
        DataTable dataTable = new DataTable();

        private void LogsFrm_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL command to select all records from the table
                string selectCommandText = $"SELECT SessionID as 'Session ID',EventDesc as 'Error Description',GTIN,Barcode,APICall as 'API call',CreatedTime as 'Transaction Time' FROM tbl_requestlogs where sessionID='" + sessionID + "' order by ID desc";

                // Create a DataTable to store the results

                // Execute the SQL command and fill the DataTable
                using (SqlCommand command = new SqlCommand(selectCommandText, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }

                detailLogGrid.DataSource = dataTable;
                //detailLogGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //detailLogGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //int totalWidth = detailLogGrid.Width;

                //// Calculate the width for each column based on percentages
                //int firstColumnWidth = (int)(totalWidth * 0.2);  // 20% of the total width
                //int secondColumnWidth = (int)(totalWidth * 0.6); // 60% of the total width
                //int thirdColumnWidth = (int)(totalWidth * 0.2);  // 20% of the total width

                //// Set the calculated widths to the columns
                //detailLogGrid.Columns[0].Width = firstColumnWidth;
                //detailLogGrid.Columns[1].Width = secondColumnWidth;
                //detailLogGrid.Columns[2].Width = thirdColumnWidth;
                foreach (DataGridViewColumn column in detailLogGrid.Columns)
                {
                    column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    
                }
                detailLogGrid.CellBorderStyle = DataGridViewCellBorderStyle.Single;

                // Set the border color for all cells
                detailLogGrid.GridColor = Color.Black;
            }
           

        }

        private void exportPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            string filePath = "D:\\ExportedData.pdf"; // Provide the desired path for the PDF file
            ExportDataTableToPDF(dataTable, filePath);
        }
        private void ExportDataTableToPDF(DataTable dataTable, string filePath)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);
            pdfTable.WidthPercentage = 100;

            // Add table headers
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(dataTable.Columns[i].ColumnName));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                pdfTable.AddCell(cell);
            }

            // Add table rows
            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                for (int column = 0; column < dataTable.Columns.Count; column++)
                {
                    pdfTable.AddCell(dataTable.Rows[row][column].ToString());
                }
            }

            document.Add(pdfTable);
            document.Close();
        }
    }
}
