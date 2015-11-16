var net = require('net');
var control = require('./control');

var server = net.createServer(function(socket){
	socket.on('data', function(data){
		console.log(data);
		control.routing(data, socket);		
	})
});

exports.sendMessage = function(message, socket)
{
	console.log(message);
	socket.write(message + "\n\n");
}

server.listen(1337, '127.0.0.1');