/* var js = document.createElement('script');
js.src = 'jquery.min.js';
js.type = 'text/javascript';
document.getElementsByTagName('head')[0].appendChild(js);

var script = document.createElement('script');
script.src = 'https://www.gstatic.com/firebasejs/8.2.1/firebase.js';
script.type = 'text/javascript';
document.getElementsByTagName('head')[0].appendChild(script);

var script2 = document.createElement('script');
script2.src = 'config.js';
script2.type = 'text/javascript';
document.getElementsByTagName('head')[0].appendChild(script2); */

var firebase = require('./config.js');

var storage = firebase.storage();
var storageRef= storage.ref();

$('#List').find('tbody').html('');

var i =0;
storageRef.child('fthTest-users/').listAll().then(function(result){
	
	// Id List
	var list=[];
		result.prefixes.forEach(function(idRef){
		// bucket, fullPath, name, parent, root, storage,child
	   // console.log(idRef.name.toString());
		list.push(idRef.name.toString());
	});
	return list;
});



storageRef.child('fthTest-users/jsTest/').listAll().then(function(result){
		
	   result.items.forEach(function(imageRef,row){
	// console.log("Image reference"+imageRef.metadata.name);
	
	imageRef.getDownloadURL().then(function(url){
	//console.log(url);
	
	let new_html='';
	new_html+='<tr>';
	new_html+='<td>';
	new_html+=row;
	new_html+='</td>';
	new_html+='<td>';
	new_html+='<img src="'+url+'" width="100px" style="float:right">';
	new_html+='</td>';
	new_html+='</tr>';
	$('#List').find('tbody').append(new_html);
	
	});
   });
});