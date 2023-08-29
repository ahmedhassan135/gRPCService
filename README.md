# gRPCService

This project creates and sends an RPC call to the server with a message containing and ID and Number. The server determines
whether or not a number is a prime number or not. The server then returns the result back to the requesting client.

This project has 2 components,
  1. Server: A stand alone server application that listens for client request
  2. Client: A dll library that facilitates communication with the server

To send requests to the server, the test application references the client library and uses the functionality to send a 
message to the server. 

The server maintaines a dictionary that keeps track of all the prime numbers that have been requested and the amount of times they have been requested. 
The Top 10 most requested prime numbers are displayed by the server on the console periodically. This is referenced as the 'Leaderboard'. It uses a priority queue with a sorted set to achieve this functionality. 

On the client side, in the client library, a message request is sent to the server. In case of a timeout the client will attempt 2 retries. If the request is still unsuccessful, an exception will be thrown.
The client can display the round trip time for each message. This can be enabled or disabled since I/O can be a costly operation.

# Scaling
For handling requests the scaling is pretty straight forward, the client is currently accepting a list of servers. Using the ID of each message the server to which it should be sent to can be determined (using hash or by simply using (modulus) % num_of_servers). The servers and clients are capable of individually handling requests and responses.
Maintaining a central leaderboard can be some what tricky, I propose that one server be elected as a coordinator which polls all other servers for their respective leaderboards and the compiles them to display a clusterwide top ten encountered prime numbers.


Note:
The users of this project can use their own end user applications and not necessarily use the sample test application provided
To use custom test application it must have a reference attached to the client.dll (binary from the client project)
Note: This application due to the client.dll attached has a dependency for the libraries, Google.Proto, Grpc.core and Grpc.tools, make sure those are installed as well


# Steps to run
  1. Compile the Server (GrpcServer.Service) project
  2. Compile the Client project
  3. Compile the Sample Test Application
  4. Run the Server
  5. Run the sample test application
