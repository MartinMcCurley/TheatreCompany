// Store the initialization function to be called when maps script loads
window.mapsCallback = function() {
    // Check if the map container exists
    const mapElement = document.getElementById('map');
    if (!mapElement) return;

    // Define your theater's location
    const theaterLocation = {
        lat: 55.8642, // Replace with your actual latitude
        lng: -4.2518  // Replace with your actual longitude
    };

    try {
        // Create the map
        const map = new google.maps.Map(mapElement, {
            zoom: 15,
            center: theaterLocation,
            styles: [
                {
                    "featureType": "all",
                    "elementType": "all",
                    "stylers": [
                        { "saturation": -100 },
                        { "gamma": 0.5 }
                    ]
                }
            ]
        });

        // Add a marker for the theater
        const marker = new google.maps.Marker({
            position: theaterLocation,
            map: map,
            title: 'Theatre Company'
        });
    } catch (error) {
        console.error('Error initializing map:', error);
        handleMapError();
    }
};

// Handle errors
function handleMapError() {
    const mapElement = document.getElementById('map');
    if (mapElement) {
        mapElement.innerHTML = '<div class="map-error">Unable to load map. Please try again later.</div>';
    }
}

// Add error handling to the window object
window.gm_authFailure = handleMapError; 