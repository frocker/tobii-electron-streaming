 # tobii-electron-streaming
Stream gaze position from Tobii's C# SDK to a node/electron application
Access the input stream using the C# SDK & streams the output over UDP on localhost, to your node or electron app.

1.  - Run TobiiElectronServer.exe, leaving the folder in your app directory.
    Or:
    - Create a C# project using the included .7z directory
    This will initiate the Tobii SDK, and act as the server to send the gaze input stream
    
2. Use the included electron app template to recieve the eye gaze stream.
   
   This acts as the client.
    
Planned features:
- Will add a stream for headposition.
