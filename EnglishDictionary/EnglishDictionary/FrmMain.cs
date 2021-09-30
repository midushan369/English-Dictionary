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

            TxtMeaning.Clear();
            TxtWord.Clear();

            // Check if the word already exists
            // If it exists, check if the meaning already exists
            // If the meaning already exists, replace it after confirmation
            // If the meaning doesn't exist, add it
            // If the word doesn't exist, add it
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
           


            //findign the matching the word form List
            for (int i = listwms.Dlist.Count - 1; i >= 0; i--)
            {
                LbDictionaryData.DataSource = null;
                if (listwms.Dlist[i].words.Contains(textBox1.Text) == true)
                {
                    LbDictionaryData.Items.Clear();
                    LbDictionaryData.Items.Add("word -> " +listwms.Dlist[i].words);
                    LbDictionaryData.Items.Add("_____Meanings_____");
                    foreach (string s in listwms.Dlist[i].MEANS)
                    {
                        LbDictionaryData.Items.Add(s);
                    }
                    serchword = "word -> " +listwms.Dlist[i].words;
                    break;
                }
            }
            DlistofWords.ResetBindings(false);

            textBox1.Clear();
            // Search for the matching words
            // If there are results display them in the list box
        }

        private void BtnDeleteSelected_Click(object sender, EventArgs e)
        {
            //only if somthing selectded will be deleat 
            if (LbDictionaryData.SelectedItem != null)
            {
                DialogResult DR = MessageBox.Show(" WORD AND MEANING WILL BE LOST,WANT TO DELETE ? ", "warrning!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
               if (DR == DialogResult.Yes)
               {
                 
                 try
                 {
                    //try theis delete code
                    listwms.Dlist.Remove((wordmenset)LbDictionaryData.SelectedItem);
                    DlistofWords.ResetBindings(false);
                 }

                 catch
                 {
                    string deletemean = (string)LbDictionaryData.SelectedItem;
                    //serching the word
                    //if erro occur try this
                    for (int i = listwms.Dlist.Count - 1; i >= 0; i--)
                    {
                            if (listwms.Dlist[i].MEANS.Contains(deletemean) == true)
                            {
                                listwms.Dlist[i].MEANS.Remove(deletemean);
                                LbDictionaryData.Items.Remove(deletemean);

                                //after deleting seting WordAndMean
                                for (int l = 0; l <=  listwms.Dlist[i].MEANS.Count; l++)
                                {

                                    if(l == 0 && listwms.Dlist[i].MEANS != null)
                                    {
                                        try // after deleting the last meaning can't add a meaning  to get arund the erro  put an try catch
                                        {
                                         listwms.Dlist[i].WordAndMean = "'" + listwms.Dlist[i].words + "' Word's Meaning  :- " + " '" + listwms.Dlist[i].MEANS[0] + " '";
                                        } 
                                        catch
                                        {
                                            listwms.Dlist[i].WordAndMean = "'" + listwms.Dlist[i].words + "' Word's Meaning :- ";
                                        }
                                       
                                    }
                                    if(l>0 && l < listwms.Dlist[i].MEANS.Count)
                                    { 
                                        listwms.Dlist[i].WordAndMean = listwms.Dlist[i].WordAndMean + ", '" + listwms.Dlist[i].MEANS[l] + " '";
                                    }
                                }
                                

                            }
                            //deleteing word after serching 
                            if(serchword == deletemean)
                            {
                                listwms.Dlist.RemoveAt(i);
                                LbDictionaryData.Items.Clear();
                                MessageBox.Show("working");
                            }
                            
                           
                         
                            
                    }  
                   
                 }

               }
               else if (DR == DialogResult.No)
               {
                //do thothing 
                }
                
            }
            else
            {
              MessageBox.Show("Nothing selected", "Erro");
            }
            
            // Ask for confirmation; Warn for unrecoverability
            // If confirmed delete the word
        }

        private void BtnListAll_Click(object sender, EventArgs e)
        {
            // Iteratively populate the list box in the ascending order of words
            //Sorting the list in ascending order of word
            listwms.Dlist.Sort();
            //listing all the things form list 
            LbDictionaryData.DataSource = DlistofWords;
            LbDictionaryData.DisplayMember = "WordAndMean";
            DlistofWords.ResetBindings(false);
            
            
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

        //this method add after changeing user dot't have to wait for timer to tick(mainly for devoping purpose)
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            loadData LD = new loadData();
            LD.fileupdate();
        }

    }

}
