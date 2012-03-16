
function addCategory(category){
	var newOption = new Option(category);
	newOption.value = category;
	newOption.innerHTML = category;
	$("#selectCategory").append(newOption);
}

function addPlace(place){
	$("#placesList").append("<li data-theme='c'><a onclick='toggleGetTicketScreen(" + place.qid + ")'>" + place.name + "</a></li>");
	$("#placesList").listview("refresh");
	
}

function initCategories(categories){
	if (categories==null){
		categories = ["Banks","Shops"];
	}
	for (var i in categories){
		addCategory(categories[i]);
	}
}

function initPlaces(jsonStr){
	if (jsonStr==null){
		jsonStr = '[{name: "Laka Dizingoff", qid:"1"}, {name: "Laka Even Gvirol", qid:"2"}, {name: "Laka Jerusalem", qid:"3"}]';
	}
	places = eval('(' + jsonStr + ')');
	
	for (var i in places){
		addPlace(places[i]);
	}
}

function setCategories(){
	$.get("http://qapp.apphb.com/categories", function(data){
        	initCategories(data);
        	});
}

function setPlaces(){
	$.get("http://qapp.apphb.com/merchants", function(data){
    	initPlaces(data);
    	});
}

function getQDetails(qID){
	//$.get/post
	var retVal = eval('({name:"Laka Dizingoff", nextnumber:"3", nextticket:"2", waittime:"32"})');
	return retVal;

	
	
}

function toggleGetTicketScreen(qID){
	//alert(placeName);
	qObj = getQDetails(qID);
	$("#mainPage").hide();
	$('#placeName').text(qObj.name);
	$('#nextNumber').text(qObj.nextnumber);
	$('#nextTicket').text(qObj.nextticket);
	$('#avgWaitTime').text(qObj.waittime);
	
	$("#getticket").fadeIn();
	
//	alert(placeName);
	
}


function getTicketFromServer(){
	$.post("http://qapp.apphb.com/queuetickets", { userid: "123123",
		queueid: "queues/125e809c-3c1b-4be5-8cd4-6e5c5cd1093a"},
			   function(data) {
			 //    alert("Recived: " + data);
			   });
}

var qtime = 30;
var interval;
function gotTicket(qID){
	$("#getticket").hide();
//	alert("gotTicketForLine " + qID);
	$("#dialogDiv").fadeIn();
//	getTicketFromServer();
	//Android.startWebPuller();
	//setTimeout(a, arg2);
	updateTime();
	interval = window.setInterval("updateTime()", 1000);
	
}


function updateTime(){
//	console.log("started");
	qtime--;
	if(qtime == 0){
		window.clearInterval(interval);
		$("#lblNext").css("display", "block");
	}
	var timeStr = "0:" + qtime.toString(); 
	$("#dtime").text(timeStr)
	
	
}

