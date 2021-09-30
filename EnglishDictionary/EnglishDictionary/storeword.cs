using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EnglishDictionary
{
    class soterword 
    {
        // To check the input 
        bool input = true;

        
        //this will get word and  meaning  set  assign to the object in the list  
        public void store(string word, string mean)
        {
            //if user didn't input word this message box will execute 
            if (word == "")
            {
                input = false;
                message("Erro", "instert word pleas");
            }

            //if there is  a word this will execute 
            if (input == true)
            {
                if(mean == "")
                {
                    mean = null;
                }
                // this will cteate an object   
                wordmenset wms = new wordmenset();

                //adding the object to list 
                listwms.Dlist.Add(wms); 
                
                //loop will check every obj in list and set word and meaning 
                for (int i = 0; i < listwms.Dlist.Count; i++)
                {
                    //it check word alredy exsist
                    if (listwms.Dlist[i].words == word)
                    {
                        DialogResult DR = MessageBox.Show("word alredy exsist whant to replace the word ? ", "WARNING" , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (DR == DialogResult.Yes)
                        {
                            listwms.Dlist[i].words = word;
                        }
                        else if (DR == DialogResult.No)
                        {
                            //do nothing 
                        }
                    }
                    //it will check exsisting word has a meaning
                    if (listwms.Dlist[i].words == word && listwms.Dlist[i].MEANS != null && listwms.Dlist[i].MEANS.Contains(mean) == true)
                    {
                        DialogResult DR = MessageBox.Show("Meaning alredy  Exsist Do whant to replace it ? ", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (DR == DialogResult.Yes)
                        {
                            // INdexfo mesn will find the indext of the meaning i whant to repalace 
                            listwms.Dlist[i].MEANS[listwms.Dlist[i].MEANS.IndexOf(mean)] = mean;
                        }
                        else if (DR == DialogResult.No)
                        {
                            //Do nothing 
                        }
                        break;
                    }
                    //if a word without  meaning exsist
                    if(listwms.Dlist[i].words == word && listwms.Dlist[i].MEANS == null)
                    {
                        listwms.Dlist[i].means(mean);
                        break;
                    }
                    // if it's Diferent meaning 
                    if (listwms.Dlist[i].words == word && listwms.Dlist[i].MEANS.Contains(mean) == false)
                    {
                     
                      if(mean != null)
                        { 
                          listwms.Dlist[i].MEANS.Add(mean);
                          string newset = listwms.Dlist[i].WordAndMean + " , '" + mean + " '";
                          listwms.Dlist[i].WordAndMean = newset;
                        }

                      break;
                    }
                    //it will set new word and meaning
                    if (listwms.Dlist[i].words == null && listwms.Dlist[i].MEANS == null )
                    {
                        listwms.Dlist[i].words = word;
                        listwms.Dlist[i].means(mean);
                    }

                }
            }

            // it will check if ther is a  emty object 
            for (int i = listwms.Dlist.Count - 1; i >= 0; i--)
            {
               if (listwms.Dlist[i].words == null)
               {
                    listwms.Dlist.RemoveAt(i);
                }
            }
        }
       
        //To load the information fron jason file to List
        public void jsonload(String word, string mean) 
        {
            //if there is  a word this will execute 
            if (input == true)
            {
                // this will cteate an obj and add to the list 
                wordmenset wms = new wordmenset();
                listwms.Dlist.Add(wms);

                //loop will check every obj in list and set word and meaning 
                for (int i = 0; i < listwms.Dlist.Count; i++)
                { 
                    //it will set new word and meaning
                    if (listwms.Dlist[i].words == null && listwms.Dlist[i].MEANS == null)
                    {
                        listwms.Dlist[i].words = word;
                        listwms.Dlist[i].means(mean);
                    }

                }
            }
            // it will check if ther is a object 
            for (int i = listwms.Dlist.Count - 1; i >= 0; i--)
            {
                if (listwms.Dlist[i].words == null)
                {
                    listwms.Dlist.RemoveAt(i);
                }
            }
        }
        
        //To Delete all the words and meanig 
        public void DELETALL()
        {
            DialogResult DR = MessageBox.Show( " ALL MEANING AND WORD WILL BE LOST DO YOU WANT TO COUNTINUCE ? ", "WORNIG!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == DialogResult.Yes)
            {
                listwms.Dlist.Clear();
               //for (int i = listwms.Dlist.Count - 1; i >= 0; i--)
               // {
               //     listwms.Dlist.RemoveAt(i);
               // }
            }
            if(DR == DialogResult.No)
            {
            
            }
        }
      

        public void message(string title, string message)
        {
            MessageBox.Show(message, title);
        }
    }
}









