import java.security.MessageDigest;
import java.util.HashMap;
import java.util.*;
import java.util.Date;

public class SingleSingOnVlex 
{

    public String remote_auth(HashMap<String,String> hm)throws Exception
    {
        MessageDigest md = MessageDigest.getInstance("MD5");
        Number current = System.currentTimeMillis()/1000;

        if (hm.get("timestamp") == null){
            hm.put("timestamp",current.toString());
        };
        
        String keys[] = {"name", "email", "account_id", "token","timestamp"};
        String buffer = new String();

        for(int i=0;i<keys.length;i++){        
             buffer = buffer.concat(hm.get(keys[i]).toString());
         }

        byte[] dataBytes = buffer.getBytes();
 
        md.update(dataBytes, 0, dataBytes.length);

        byte[] mdbytes = md.digest();
 
        StringBuffer sb = new StringBuffer();
        for (int i = 0; i < mdbytes.length; i++) {
          sb.append(Integer.toString((mdbytes[i] & 0xff) + 0x100, 16).substring(1));
        }
        hm.put("hash",sb.toString());
        hm.remove("token");

        String remote_auth_url = "http://vlex.com/session/remote_auth?";

        Object params[] =  hm.keySet().toArray();
        
        for(int i=0;i<params.length;i++){            
            remote_auth_url += params[i].toString()+"="+hm.get(params[i]).toString();
            if (i < params.length-1) {
               remote_auth_url +="&";}
        }

        return remote_auth_url;
    }


}