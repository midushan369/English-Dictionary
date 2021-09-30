using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EnglishDictionary
{
    public partial class FrmMain : Form
    {
        BindingSource DlistofWords = new BindingSource();
        string serchword;
        soterword soterword = new soterword();
        

        public FrmMain()
        {
            InitializeComponent();


            // Load the existing data from the file
            DlistofWords.DataSource = listwms.Dlist;
            LbDictionaryData.DataSource = DlistofWords;
            LbDictionaryData.DisplayMember = "WordAndMean";


            //deserializeing and loading the data
            string fileName = "listwms_Dlist_object.json";
            string jsonString = File.ReadAllText(fileName);
            List<wordmenset> jwms = JsonSerializer.Deserialize<List<wordmenset>>(jsonString);
            for (int i = jwms.Count - 1; i >= 0; i--)
            {
                listwms.Dlist.Add(jwms[i]);
                // add the word amd all the Meanings that word has 
            }
            DlistofWords.ResetBindings(false);
       
        }

       
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string word = TxtWord.Text;
            string mean = TxtMeaning.Text;
            soterword.store(word, mean);
            DlistofWords.ResetBindings(false);

            loadData LD = new loadData();
           

            LbDictionaryData.DataSource = DlistofWords;
            LbDictionaryData.DisplayMember = "WordAndMean";
            DlistofWords.ResetBindings(false);

            // Check if the word already exists
            // If it exists, check if the meaning already exists
            // If the meaning already exists, replace it after confirmation
            // If the meaning doesn't exist, add it
            // If the word doesn't exist, add it
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            
            // LbDictionaryData.DisplayMember = "";
            //LbDictionaryData.Items.Add()



            for (int i = listwms.Dlist.Count - 1; i >= 0; i--)
            {
                LbDictionaryData.DataSource = null;
                if (listwms.Dlist[i].words.Contains(textBox1.Text) == true)
                {
                    LbDictionaryData.Items.Clear();
                    LbDictionaryData.Items.Add(listwms.Dlist[i].WordAndMean);
                    serchword = listwms.Dlist[i].WordAndMean;
                    break;
                }
            }


            DlistofWords.ResetBindings(false);


            // Search for the matching words
            // If there are results display them in the list box
        }

        private void BtnDeleteSelected_Click(object sender, EventArgs e)
        {
            try
            {
                listwms.Dlist.Remove((wordmenset)LbDictionaryData.SelectedItem);
                DlistofWords.ResetBindings(false);
            }
            
            catch 
            {
                //listwms.Dlist.Remove(listwms.Dlist.Contains());
                //serchword

                for (int i = listwms.Dlist.Count - 1; i >= 0; i--)
                {  
                     if(listwms.Dlist[i].WordAndMean.Contains(serchword) == true)
                     {
                         listwms.Dlist.RemoveAt(i);
                         LbDictionaryData.Items.Clear();
                    }
                } 
            }
            //loadData LD = new loadData();

            //soterword.remove(textBox1.Text);
            // Ask for confirmation; Warn for unrecoverability
            // If confirmed delete the word
        }

        private void BtnListAll_Click(object sender, EventArgs e)
        {
            // Iteratively populate the list box in the ascending order of words
            
            LbDictionaryData.DataSource = DlistofWords;
            LbDictionaryData.DisplayMember = "WordAndMean";
            DlistofWords.ResetBindings(false);
            listwms.Dlist.Sort();
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {

            DialogResult DR = MessageBox.Show(" WANT TO DELEAT ALL ? ", "warrning!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == DialogResult.Yes)
            {
                soterword.DELETALL();

            }
            else if (DR == DialogResult.No)
            {

            }
            DlistofWords.ResetBindings(false);
            //loadData LD = new loadData();
            
            // Ask for confirmation TWICE; Warn for unrecoverability
            // If confirmed delete the word
        }

        // Implement a timed task to automatically check if there are unsaved changes and save them if there are any
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            loadData LD = new loadData();
            LD.fileupdate();       
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            loadData LD = new loadData();
            LD.fileupdate();
        }

    }

}
