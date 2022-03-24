# Plugin.YoutubeLibrary
A Xamarin Library for handling YouTube API requests

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
## Retrieve playlists
To get the playlists calls the **youtube.playlistsList.getPlaylist(String[] part, bool Mine)** methods. This is an overloaded methood that takes in the following : the parts, Mine, parameter(optional); 

``` c#
public Main()
{
   YoutubeClient youtubeClient = new YoutubeClient(ApiKey, token);
   PlaylistResponse Playlists = await youtubeClient.playlistsList.getPlaylistAsync(new string[] { "snippet" }, true);
}
``` 

## Api Requests
An example of Api requests and the corresponding parts of the code

> POST https://youtube.googleapis.com/youtube/v3/playlists?part=snippet&part=contentDetails&prettyPrint=true&key=[API_KEY] HTTP/1.1 </br>
> A single parameter is:
> part=snippet
> Combination of multiple parameters
> part=snippet&part=contentDetails&prettyPrint=true
> **Adding a parameter**
 ``` c#
new Parameter("prettyPrint", "true");
``` 
## A request with parameters
```  c#
PlaylistResponse Playlists = await youtubeClient.playlistsList.getPlaylistAsync(new string[] { "snippet" }, true, new Parameter("prettyPrint", "true"));
``` 


## Insert playlists

``` c#
public MainWindow()
{
  var playlistsJson = youtube.playlistsList.getPlaylist(new string[] { "snippet", "contentDetails" }, true);
}
``` 
