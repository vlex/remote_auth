<html>

  <cfset account_id = "1234">
  <cfset token = "TOKEN">

  <cfset startTime = "{ts '1970-01-01 00:00:00'}">
  <cfset nowTime = now()>
  <cfset seconds = dateDiff('s',startTime,nowTime)>
  <cfset seconds = seconds + 10800>


  <cfset tmp= name & email & account_id & token & seconds>
  <cfset hash = LCase(Hash(tmp, "MD5"))>

  <cfset link = "http://vlex.com/session/remote_auth?name=" & name & "&email=" & email & "&account_id=" & account_id & "&timestamp=" & seconds & "&hash=" & hash> 

<body>
    <cflocation url=#link#>
</body>
</html>

