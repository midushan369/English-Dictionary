using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Windows.Forms;
using System.Text.Json.Serialization;

namespace EnglishDictionary
{
    class loadData
    {

        //serializelis the Dlist fron listwms class
        public void serializelistwms()
        {
            string fileName = "listwms_Dlist_object.json";
            string jsonString = JsonSerializer.Serialize(listwms.Dlist);
            File.WriteAllText(fileName, jsonString);
        }

        //crateing the text file 
        public void cratetext() 
        {
            string TxtjsonString = File.ReadAllText("listwms_Dlist_object.json");
            //stremWriter wil save the file will avalable at folder Debug 
            List<wordmenset> jwms = JsonSerializer.Deserialize<List<wordmenset>>(TxtjsonString);

                //writing the every object seperatli
                using (StreamWriter ws = new StreamWriter(@"datafile.dat.Text"))
                foreach (wordmenset o in jwms)
                {
                    string jsonString = JsonSerializer.Serialize(o);
                    ws.WriteLine(jsonString);
                }


            
        }
        
        //updateing the files
        public void fileupdate()
        {  
            
            
            //serualizing the list
            serializelistwms();

            //it Derislize the jsom file listwms_Dlist_object.json
            string fileName = "listwms_Dlist_object.json";
            string jsonString = File.ReadAllText(fileName);
            List<wordmenset> jwms = JsonSerializer.Deserialize<List<wordmenset>>(jsonString);
            
            
            //check the Derislize list with Exsisting list in the program
            if (jwms != listwms.Dlist)
            {   
                // if it not the same it will serialize the listwms.Dlist
                //replceing  the text file 
                cratetext();
            }
        }
    }
}
