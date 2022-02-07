# Plugin.YoutubeLibrary
A Xamarin Library for handling Youtube Api requests

## Folders Used
### Api:
 This contains the code for handling the api calls 
### Util
Random helpers/functions used 
### Youtube
The youtube service 


## Create the service
The Plugin could be created with only the api key or both an api key and an access token. provididng an acces token allows for more functionality
``` c#
using YoutubeLibrary.youtube;
public MainWindow()
{
  string ApiKey = "API_KEY";
  string Accesstoken = "ACCESS_TOKEN";
  
  YoutubeClient youtube = new YoutubeClient(ApiKey);
  YoutubeClient youtube2 = new YoutubeClient(ApiKey, Accesstoken);
}
``` 
## Api Requests
An example of Api requests and the corresponding parts of the code
``` c#
POST https://youtube.googleapis.com/youtube/v3/playlists?part=snippet&part=contentDetails&prettyPrint=true&key=[API_KEY] HTTP/1.1
``` 
A parameter is
``` c#
part=snippet
``` 
Parameters are a combination of a multiple parameter
``` c#
part=snippet&part=contentDetails&prettyPrint=true
``` 
A body 
``` c#
Body
{
  "snippet": {
    "title": "NewVideo",
    "channelId": ""
  },
  "contentDetails": {
    "itemCount": 1
  }
}

``` 
## Retrieve playlists
To get the playlists calls the **youtube.playlistsList.getPlaylist(String[] parameters, bool Mine)** methods. This is an overloaded methood that takes in the following : the parts, Mine, body(optional); 

``` c#
public MainWindow()
{
  var playlistsJson = youtube.playlistsList.getPlaylist(new string[] { "snippet", "contentDetails" }, true);
  
}
``` 
## Insert playlists

``` c#
public MainWindow()
{
  var playlistsJson = youtube.playlistsList.getPlaylist(new string[] { "snippet", "contentDetails" }, true);
}
``` 

