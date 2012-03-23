using System.Security.Cryptography;
using System;
using System.Collections;
using System.Text;

public class RemoteAuth
{
   public static void Main(){

      // Definimos un hash con los parametros necesarios 
      Hashtable hash = new Hashtable();
      string tmp = [EMAIL];
      hash.Add("name",tmp); 
      hash.Add("email",tmp);
      hash.Add("account_id",[ACCOUNT_ID]);
      hash.Add("token",[TOKEN]);
      
      // Obtenemos los parametos para el request (name,email,account_id,hash,sha256,timestamp)  
      Hashtable postdata = RemoteAuth.remote_auth(hash);

      foreach (string key in postdata.Keys) {
        System.Console.Write(key);  
        System.Console.Write(": ");  
        System.Console.WriteLine(postdata[key]);  
      }      

      System.Console.WriteLine("https://vlex.com/session/remote_auth?");    
      foreach (string key in postdata.Keys) {
        System.Console.Write(key);  
        System.Console.Write("=");  
        System.Console.Write(postdata[key]);  
        System.Console.Write("&");  
      }      

   }
   
    public static Hashtable remote_auth(Hashtable ht){

        SHA256 sha = new SHA256Managed();
        string timestamp = RemoteAuth.GetCurrentMilli();
 
        ht.Add("timestamp",timestamp.ToString());
        
        string[] keys = new string[] {"name", "email", "account_id", "token","timestamp"};
        string buffer = string.Empty;

        for(int i=0;i<keys.Length;i++){        
             buffer += ht[keys[i]];
         }

        byte[] dataBytes = Encoding.Default.GetBytes(buffer);
  
        byte[] cryString = sha.ComputeHash(dataBytes); 
 
        string cryStringHexa = string.Empty;      
        foreach (byte x in cryString)  {
          cryStringHexa += string.Format("{0:x2}", x);
        }      

        ht.Add("hash",cryStringHexa);
        ht.Add("sha256","true");
        ht.Remove("token");

        return ht;
    }
   
    public static string GetCurrentMilli(){
      DateTime now = DateTime.Now;    
      DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
      TimeSpan diff = now.ToUniversalTime() - origin;
      return Math.Floor(diff.TotalSeconds).ToString();
    }
   
}