//when browsing crew list, fetch data in background to warm up the azure function so crew fetches are quick.

var request = new XMLHttpRequest();
request.open('GET', 'https://uss-champlin-api.azurewebsites.net/api/GetAllCrew', true);

request.onload = function (){
    //pass
}
request.send();