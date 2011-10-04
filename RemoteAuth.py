#!/usr/bin/python
import md5
import time


def remote_auth(name,email):
  token = [token]
  account_id = [ACCOUNT ID]
  timestamp = int (time.time())
  st_tmp = name+email+account_id+token+str(timestamp)
  m = md5.new()
  m.update(st_tmp)
  hash = m.hexdigest() # Encriptamos los datos
  return 'http://vlex.com/session/remote_auth?name='+name+'&email='+email+'&account_id='+account_id+'&timestamp='+str(timestamp)+'&hash='+hash



# En este caso, name y email son estaticos. La idea es que se recuperen los valores correspondientes al usuario logado
# pseudocodigo -> name = SESION.current_user.email    email = SESION.current_user.email
# NOTA: Recomendamos utilizar el email del usuario como name tambien.
name = [USUARIO]
email = [E-MAIL]




# Generamos el link a vlex incluyendo el parametro hash 
url = remote_auth(name,email)


print "Content-Type: text/html\n\n"
print "<meta http-equiv=\"Refresh\" content=\"0;URL="+url+"\" />\n"

