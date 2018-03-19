const { app, BrowserWindow} = require('electron');
const path = require('path');

let mainWindow = undefined;

var ipc = require('electron').ipcMain;

var PORT = 33333;
var HOST = '127.0.0.1';

var dgram = require('dgram');
var server = dgram.createSocket('udp4');

var previousTimestamp = 0;

server.on('listening', function () {
    var address = server.address();
    console.log('UDP Server listening on ' + address.address + ":" + address.port);
});

server.on('message', function (message, remote) {
    parseMessage(message);
});

function parseMessage(message){
  var messageObj = JSON.parse(message);
  if(messageObj.timestamp>previousTimestamp){
    if(mainWindow !== undefined){
      mainWindow.webContents.send('gaze-pos', messageObj);
    }
    previousTimestamp = messageObj.timestamp;
  }
}

server.bind(PORT, HOST);

app.on('ready', () => {
  mainWindow = new BrowserWindow();
  mainWindow.loadURL(path.join('file://', __dirname, 'window.html'));
});

