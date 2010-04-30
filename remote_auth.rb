require 'rubygems'
require 'digest/md5'      # For the hexdigest function
require 'active_support'  # For the .to_query function

module Vlex
  class RemoteAuth
    class << self
      # Set default remote authorization path
      REMOTE_AUTH_URL = "http://vlex.com/account/login_remote_auth"

      def remote_auth(params)
        # Add timestamp
        params[:timestamp] = Time.now.to_i

        # Check that required parameters are present and concatenate them
        seed = ""
        keys = [:name, :email, :account_id, :token, :timestamp]
        keys.each do |key|
          raise ArgumentError.new "Missing parameter :#{key}" if params[key].nil?
          seed << params[key].to_s
        end
            
        # Add hash from the concatenated data
        params[:hash] = Digest::MD5.hexdigest(seed)

        # Generate the query and return the URL
        "#{REMOTE_AUTH_URL}?#{params.to_query}"
      end
    end
  end
end

# Example:
# data = {:account_id => "1234", :name => "tester", :token => "TOKEN", :email => "test@vlex.com"}
# puts Vlex::RemoteAuth::remote_auth data
