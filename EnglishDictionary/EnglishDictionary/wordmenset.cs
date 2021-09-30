using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EnglishDictionary
{
    class wordmenset : IComparable
    {



        // get the word and set to a string words
        public string words { get; set; }

        // get the word meaning store it in the list 
        public List<string> MEANS { get; set; }

        //to Display the meaning 
        public string WordAndMean { get;set;}



        //To shor the list in words order A-z
        public int CompareTo(object obj)
        {
            wordmenset wms = (wordmenset)obj;
            return words.CompareTo(wms.words);

            //throw new NotImplementedException();
        }



        // it will set to set to the meanings  to string List MEANS
        public void means(string mean)
        {
         if(mean !=null)
            {
                MEANS = new List<string> { mean };
                WordAndMean = " '" + words + "'" + " Word's Meaning :-  " + " '" + mean + "'";
            }
          else
            {
                WordAndMean = " '" + words + "'";
            }

        }

        //private void setwordmean()
        //{
        //    WordAndMean = words +"->"+ MEANS[0];
        //}
        
    }
}
