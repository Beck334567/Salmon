var map, marker, latitude, longitude;

function initMap() {
    navigator.geolocation.getCurrentPosition((position) => {
        latitude = position.coords.latitude;
        longitude = position.coords.longitude;

        map = new google.maps.Map(document.getElementById('map'),
            {
                zoom: 15,
                center: { lat: latitude, lng: longitude }
            });

        var locations = document.getElementById("location");
        locations.innerHTML = '<p">Latitude is : ' + position.coords.latitude + '° <br>Longitude is : ' + position.coords.longitude + '°</p>';

        marker = new google.maps.Marker({
            position: { lat: latitude, lng: longitude },
            map: map
        });
    });
}

function GetDistance() {
    let origins = { Latitude: latitude, Longitude: longitude };
    let destinations = { Latitude: 25.04862, Longitude: 121.5441894 };
    var getdistanceItems =
    {
        origins: origins,
        destinations: destinations
    };
    $.ajax({
        type: "post",
        url: "Index?handler=GetDistance",
        data: { requestDistance: getdistanceItems },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("X-CSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (result) {
            console.log(result);
        }
    });
};

function searchItem(isRandomSelect) {
    var location = { Latitude: latitude, Longitude: longitude };
    var searchItems =
    {
        UserRatingsTotal: document.getElementById('searchUserRatingsTotal').value,
        Name: document.getElementById('searchName').value,
        Radius: document.getElementById('searchRadius').value,
        Location: location, Rating: document.getElementById('searchRating').value,
        IsOpenNow: document.getElementById('searchIsOpenNow').checked
    };

    $.ajax({
        type: "post",
        url: "Index?handler=SearchItem",
        data: { requestPlace: searchItems, isRandomSelect: isRandomSelect },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("X-CSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (result) {
            var searchResults = document.getElementById("searchResult");
            var info = "";
            if (result.length === 0) {
                alert("沒有查詢到項目");
            }

            result.forEach(function (item) {
                info += `Name : ${item.name}<br>
                             Rating : ${item.rating}<br>
                             UserRatingsTotal : ${item.userRatingsTotal}<br>
                             Open : ${item.openingHours.isOpenNow}<br>
                             <span style="display:none">PlaceId : ${item.placeId}</span>
                             Vicinity : ${item.vicinity}  <input type="button" id="buttonDetail" value="SearchOnMap" onclick="searchDetail('${item.placeId}')"><br><br>`;
                if (isRandomSelect) {
                    refreshMap(item.geometry.location.latitude, item.geometry.location.longitude);
                }
            });

            searchResults.innerHTML = '<p>' + info + '</p>';
           
        }
    });
}

function searchDetail(placeId) {
    console.log(placeId);
    $.ajax({
        type: "post",
        url: "Index?handler=SearchDetail",
        data: { placeId: placeId },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("X-CSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (result) {
            console.log(result);
            refreshMap(result.geometry.location.latitude, result.geometry.location.longitude);
        }
    });
}

function refreshMap(newLatitude, newLongitude) {
    map = new google.maps.Map(document.getElementById('map'),
        {
            zoom: 14,
            center: { lat: parseFloat(newLatitude), lng: parseFloat(newLongitude) }
        });

    var output = document.getElementById("newLocation");
    output.innerHTML = '<p>New Latitude is : ' + parseFloat(newLatitude) + '° <br>New Longitude is : ' + parseFloat(newLongitude) + '°</p>';

    marker = new google.maps.Marker({
        position: { lat: parseFloat(newLatitude), lng: parseFloat(newLongitude) },
        map: map
    });
}