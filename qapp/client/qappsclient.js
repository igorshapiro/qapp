
function addCategory(category){
	var newOption = new Option(category);
	newOption.value = category;
	newOption.innerHTML = category;
	$("#selectCategory").append(newOption);
}

function addPlace(place){
	$("#placesList").append("<li><a>" + place.name + "</a></li>");
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

function initPlaces(places){
	if (places==null){
		var jsonStr = '[{name: "place1"}, {name: "place2"}, {name: "place3"}]';
		places = eval('(' + jsonStr + ')');
	}
	
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
    	initCategories(data);
    	});
}