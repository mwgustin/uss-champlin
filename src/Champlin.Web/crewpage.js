const DEFAULT_CREW_IMAGE = "http://www.usschamplin.com/Images/bbcrewbiophoto.jpg";

var queryDict = {}
location.search.substr(1).split("&").forEach(function(item) {queryDict[item.split("=")[0]] = item.split("=")[1]})

if(!queryDict["crewUrl"]) {
    setNotFoundPage();
}

var request = new XMLHttpRequest();
request.open('GET', 'https://uss-champlin-api.azurewebsites.net/api/GetCrewItem?origUrl=http://www.usschamplin.com' + queryDict["crewUrl"] + '.html', true);

request.onload = function (){
    if(request.status >= 200 && request.status < 400) {
        var data = JSON.parse(this.response);
        updatePage(data);
    }
    else {
        setNotFoundPage();
    }
}
request.send();



function updatePage(data) {
    
    document.getElementById("crewImage").src = ((data.pictureUrl != null) ? data.pictureUrl : DEFAULT_CREW_IMAGE);

    document.getElementById("pageTitle").innerHTML = data.name + " - USS Champlin DD-601 Biographical Page";
    
    document.getElementById("crewName").innerHTML = data.name;
    document.getElementById("crewDOB").innerHTML = data.dateOfBirth;
    document.getElementById("crewServiceNumber").innerHTML = data.serviceNumber;
    document.getElementById("crewRank").innerHTML = data.rank;
    document.getElementById("crewDateOfEnlistment").innerHTML = data.dateOfEnlistment;
    document.getElementById("crewPlaceOfEnlistment").innerHTML = data.placeOfEnlistment;
    document.getElementById("crewDateOnShip").innerHTML = data.dateOnShip;
    document.getElementById("crewDateOffShip").innerHTML = data.dateOffShip;
    document.getElementById("crewDischarge").innerHTML = data.dateDischarged;
    document.getElementById("crewDateOfDeath").innerHTML = data.dateOfDeath;
    
    if(isDetailedBiography(data)) {
        document.getElementById("crewSpouse").innerHTML = data.spouse;
        document.getElementById("crewChildren").innerHTML = listToHtml(data.children);
        document.getElementById("crewGrandchildren").innerHTML = listToHtml(data.grandchildren);
        document.getElementById("crewGreatGrandchildren").innerHTML = listToHtml(data.greatGrandchildren);
        document.getElementById("crewHighSchool").innerHTML = listToHtml(data.highSchool);
        document.getElementById("crewCollege").innerHTML = listToHtml(data.college);
        document.getElementById("crewInterests").innerHTML = listToHtml(data.interests);
        document.getElementById("crewPostWarDetails").innerHTML = newLineToBr(data.postWarExperience);
    }
    else {
        document.getElementById("expandedDetailTable").innerHTML = "";
    }
}

function isDetailedBiography(data) {
    return (
        (data.spouse && data.spouse !== "")
        || (data.children && data.children.length > 0)
        || (data.grandchildren && data.grandchildren.length > 0)
        || (data.greatGrandchildren && data.greatGrandchildren.length > 0 )
        || (data.highSchool && data.highSchool.length > 0)
        || (data.college && data.college.length > 0)
        || (data.interests && data.interests.length > 0)
        || (data.postWarExperience && data.postWarExperience !== ""));
}

function listToHtml(list) {
    var returnStr = "";
    if(list) {
        list.forEach(item => {
            returnStr += item + "<br \\>";
        });
    }
    return returnStr;
}


function newLineToBr(text) {
    var result = text.replace(/\n/g, "<br \\>");
    return result;
}

function setNotFoundPage() {
    document.getElementById("mainBio").innerHTML = "<br \\><br \\><br \\><h1 style=\"text-align: center;\">Not Found</h1>";
    document.getElementById("expandedDetailTable").innerHTML = "";
}
