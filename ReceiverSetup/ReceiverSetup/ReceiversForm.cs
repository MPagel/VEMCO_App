using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReceiverSetup
{
    public partial class ReceiversForm : Form
    {
        ArrayList receiverList = new ArrayList(); //Replace with list of actual receivers

        public ReceiversForm()
        {
            InitializeComponent();


            //FOR TESTING------------------------------------------
            receiverList.Add(new Receivers("rec1", 0));
            receiverList.Add(new Receivers("rec2", 1));
            receiverList.Add(new Receivers("rec3", 0));
            receiverList.Add(new Receivers("rec4", 0));
            receiverList.Add(new Receivers("rec5", 0));
            receiverList.Add(new Receivers("rec6", 0));
            receiverList.Add(new Receivers("rec7", 0));
            receiverList.Add(new Receivers("rec8", 0));
            receiverList.Add(new Receivers("rec10", 0));
            receiverList.Add(new Receivers("rec11", 0));
            receiverList.Add(new Receivers("rec12", 0));
            receiverList.Add(new Receivers("rec13", 0));
            receiverList.Add(new Receivers("rec14", 0));

            //-------------------------------------------------------


            DataTable dt = new DataTable();
            dt.Columns.Add("Receivers");
            dt.Columns.Add("Status");


            //Here you would iterate through your list of receivers and add each receiver to DataTable 'dt'
            foreach (Receivers receiver in receiverList)
                dt.Rows.Add(receiver.receiver, receiver.status);
            

            //Tie ReceiversGridView's DataSource to 'dt'
            ReceiversGridView.DataSource = dt;          
        }
       

        //Event for user selecting a row
        private void ReceiversGridView_CellMouseClicked(object sender, DataGridViewCellMouseEventArgs e)
        {
            //If column headers not selected
            if (e.RowIndex != -1)
            {
                //If the selected receiver is off
                if (int.Parse(ReceiversGridView.SelectedRows[0].Cells[1].Value.ToString()) == 0) 
                    StatusToggleBtn.Text = "Turn On";
                else                    
                    StatusToggleBtn.Text = "Turn Off";
            }

            //This event also needs code that displays information in the 'DESCRIPTION' text box
            //


        }//End ReceiversGridView_CellMouseClicked()



        //Event when user toggles receiver on/off
        //This event is where you want to put the code in to actually turn on/off the receivers
        private void StatusToggleBtn_Clicked(object sender, EventArgs e)
        {
            //If the selected receiver is off
            if (int.Parse(ReceiversGridView.SelectedRows[0].Cells[1].Value.ToString()) == 0) 
            {
                //TurnReceiverOn();
                DescriptionTextBox.Text = "--- Receiver has been successfully turned on";


                ReceiversGridView.SelectedRows[0].DefaultCellStyle.SelectionBackColor = Color.Green;
                ReceiversGridView.SelectedRows[0].Cells[1].Style.SelectionForeColor = Color.Green;
                ReceiversGridView.SelectedRows[0].Cells[1].Value = 1;
                StatusToggleBtn.Text = "Turn Off";
            }
            else 
            {
                //TurnReceiverOff();
                DescriptionTextBox.Text = "--- Receiver has been successfully turned off";


                ReceiversGridView.SelectedRows[0].DefaultCellStyle.SelectionBackColor = Color.Red;
                ReceiversGridView.SelectedRows[0].Cells[1].Style.SelectionForeColor = Color.Red;
                ReceiversGridView.SelectedRows[0].Cells[1].Value = 0;
                StatusToggleBtn.Text = "Turn On";
            }

        }//End ReceiversGridView_CellMouseClicked()



        //Sets the color of all cells and width of columns on databinding completion
        private void ReceiversGridView_DataBindingCompleted(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ReceiversGridView.Columns[0].Width = 300;
            ReceiversGridView.Columns[1].Width = 48;

            foreach (DataGridViewRow row in ReceiversGridView.Rows)
            {
                if ((string)row.Cells[1].Value == "0")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.Red;
                    row.Cells[1].Style.SelectionForeColor = Color.Red;
                    row.Cells[1].Style.ForeColor = Color.Red;
                    row.Cells[1].Style.BackColor = Color.Red;
                }
                else if ((string)row.Cells[1].Value == "1")
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                    row.Cells[1].Style.SelectionForeColor = Color.Green;
                    row.Cells[1].Style.ForeColor = Color.Green;
                    row.Cells[1].Style.BackColor = Color.Green;
                }
            }

        }//End ReceiversGridView_DataBindingCompleted()

    }//End ReceiversForm Class
	


    //DUMMY CLASS FOR TESTING ---------------------------------------------------
	public class Receivers
    {
        public String receiver { get; private set; }
        public int status { get; private set; }

        public Receivers(String name, int receiverStatus)
        {
            receiver = name;
            status = receiverStatus;
        } 
    }
    //----------------------------------------------------------------------------



}//End Namespace
