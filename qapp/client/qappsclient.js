
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

function gotTicket(qID){
	$("#getticket").hide();
	alert("gotTicketForLine " + qID);
	$("#mainPage").fadeIn();
	Android.startWebPuller();
}