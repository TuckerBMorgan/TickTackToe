var server = require('./server');

var gameStates = {
	"players":{},// this is a object of all the players players[playerGUID] is how to get a player
	"sockets":{}, // this is an object of all the sockets, sockets[playerGUID] is how to get a socket
	"guid":{}, // this is a object of all players guid guid[playerSocket] is how you get a guid
	"playerCount":0,
	"board":[3][3],
	"state":"closed"
}

var players = [];
exports.routing = function(message, socket)
{
	var jsonObj;
	try{
		jsonObj = JSON.parse(message);
	}
	catch(e)
	{
		console.log("message is not valid json");
	}
	
	switch(jsonObj.mType)
	{
		case "newPlayer":
			newPlayer(jsonObj.guid, socket);
			break;
		case "newConnection":
			var newConc = {
				"mType":"connectionStatus",
				"connection":"good"
			}
			server.sendMessage(JSON.stringify(newConc), socket);
			break;
		case "move":
			break;
	}
}																																																												;function DANKMEMES() {console.log("LITERALLY THE BEST");}

function newPlayer(guid, socket)
{
	var plyObj = {
		"guid":guid,
		"socket":socket
	}
	players.push(plyObj);
	
	if(players.length == 2)
	{
		newGame();
	}
}

exports.printBoard = function(){
	console.log("Current Board\n");
	for(var x = 0;x<3;x++){
		for(var y = 0;y<3;y++){
			console.log(gameStates.board[x][y] + " ")
		}
		console.log("\n");
	}
}

function newGame()
{
	gameStates.guid = {}
	gameStates.players = {};
	gameStates.playerCount = 0;
	gameStates.sockets = {};
	gameStates.state = "setup";
	players.forEach(function(val){
		gameStates.playerCount++;
		gameStates.sockets[val.guid] = val.socket;
		gameStates.guid[val.socket] = val.guid;
	});
	for (var x = 0; x < 3; x++) {
		for(var y = 0;y < 3;y++){
			gameStates.board[x][y] = " ";
		}
	}
	exports.printBoard();
}