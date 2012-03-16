
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
		jsonStr = '[{name: "place1", qid:"1"}, {name: "place2", qid:"2"}, {name: "place3", qid:"3"}]';
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
	var retVal = eval('({name:"laka", nextnumber:"3", nextticket:"12", waittime:"32"})');
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
		queueid: "queues/125e809c-3c1b-4be5-8cd4-6e5c5cd1093a",
		providerid:"merchants/29b5f252-c8e2-4db6-848e-c48e2caffa94"},
			   function(data) {
			     alert("Recived: " + data);
			   });
}

function gotTicket(qID){
	$("#getticket").hide();
	alert("gotTicketForLine " + qID);
	$("#mainPage").fadeIn();
	
	getTicketFromServer();
	Android.startWebPuller();
}

