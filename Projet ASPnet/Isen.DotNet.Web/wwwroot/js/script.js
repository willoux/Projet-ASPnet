var map;

function initMap() {
	map = new google.maps.Map(document.getElementById('map'), {
		center: {
			lat: 43.467768,
			lng: 6.095036
		},
		zoom: 8
	});
}