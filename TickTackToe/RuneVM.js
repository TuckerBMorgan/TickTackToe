var server = require('./server');

exports.ExecuteRune = function(message, gameState){
	var keys = Object.keys(gameState.sockets);
	keys.forEach(function(element) {
		server.sendMessage(message, gameState.sockets[element]);		
	}, this);
}