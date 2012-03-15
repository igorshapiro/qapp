function getCategories(){  
	$.get("http://qapp.apphb.com/categories", function(data){
        	alert("Data Loaded: " + data);
        	});
}
