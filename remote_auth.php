<?php
/**
 * Returns a URL that grants access to vLex without explicitly signing in
 *
 * @since Unknown
 *
 * @param array $data : Containing ["name"] The name of the user - ["email"] 
 *   The email address of the user - ["account_id"] The corporate account id 
 *   of the company - ["token"] The corporate account token of the company
 * @return string : The URL granting access to vLex or NULL when errors occur
 */
function remote_auth($data) {
  // Set default remote authorization path
  $REMOTE_AUTH_URL = "http://vlex.com/session/remote_auth";

  // Sort $data checking that required parameters are present
  $keys = array("name", "email", "account_id", "token");
  foreach($keys as $key) {
    if(!isset($data[$key])) throw new Exception("Missing parameter: $key");
    $values[$key] = $data[$key];
  }

  // Add timestamp
  $values["timestamp"] = time();
  
  // Add hash from the concatenated data
  $values["hash"] = md5(implode($values));
  
  // Generate the query and return the URL
  $query = http_build_query($values); // name=yourname&amp;email=...
  return "$REMOTE_AUTH_URL?$query";
}

 // Example:
 // $my_data = array("account_id" => "1234", "name" => "tester", "token" => "TOKEN", "email" => "test@vlex.com");
 // echo(remote_auth($my_data)."\n");
?>
