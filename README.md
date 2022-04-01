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
# Playlists
## Retrieve playlists
To get the playlists calls the **youtubeClient.playlistsList.getPlaylistAsync(String[] part, bool Mine)** methods. This is an overloaded methood that takes in the following : the parts, Mine, parameter(optional); 

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
> A single parameter is: </br>
> part=snippet </br>
> Combination of multiple parameters </br>
> part=snippet&part=contentDetails&prettyPrint=true </br>

**Adding a parameter** </br>
Optionally you could add an array of  parameters to the function e.g 
 ``` c#
 Parameter[] parameter = {new Parameter("prettyPrint", "true"), new Parameter("alt", "json")}; 
``` 
And then you could do
 ``` c#
Parameter[] parameter = {new Parameter("prettyPrint", "true"), new Parameter("alt", "json")};
PlaylistResponse Playlists = await youtubeClient.playlistsList.getPlaylistAsync(new string[] { "snippet" }, true, parameter);
 ```
## Delete playlists
``` c#
string id="playlist_id";
var response = await youtubeClient.playlistsList.deletePlaylistAsync(id);
}
``` 

## Insert playlists
To insert a playlist you call the overloaded insertPlaylistAsync Methods
``` c#
PlaylistResponse playlist = await youtubeClient.playlistsList.insertPlaylistAsync("playlistTitle", "Description", new string[] { "snippet" });

``` 
You could optionally add parameters 
``` c#
PlaylistResponse playlist = = await youtubeClient.playlistsList.insertPlaylistAsync("playlistTitle", "Description", new string[] { "snippet", }, parameter );
}
``` 
## Adding a body
Bodies are json objects, the Body_Item class is used to acheive this Json structure<br>
Body_Item class can store a single value 
``` c#
var b= new Body_Item("itemcount", "1")
//The equilavent
``` 
``` yaml
{
 "itemCount": 1
}
``` 
**Or multiple Body_Item classes**
``` c#
Body_Item contentDetails = new Body_Item("contentDetails");
contentDetails.values.Add(new Body_Item("itemcount", "1"));
contentDetails.values.Add(new Body_Item("itemcount", "2"));
``` 
```yaml
"contentDetails": {
    "itemCount": 1,
    "itemCount": 2
  }
  */
``` 
With this you are able to form Json objects</br>
Example: The code equivalent of this Json 
```yaml
{
  "contentDetails": {
    "itemCount": 1
  },
  "snippet": {
    "title": "playlistsTitle",
    "description": "playlistsDescription",
    "localized": {
      "title": "local",
      "description": "localDescription"
    }
  }
}
```
``` c#
 Body_Item snippetItems = new Body_Item("snippet");
            snippetItems.values.Add(new Body_Item("title", "playlistTitle"));
            snippetItems.values.Add(new Body_Item("description", "playlistsDescription"));
            snippetItems.values.Add(new Body_Item("localized", new List<Body_Item> { new Body_Item("title", "local"), new Body_Item("description", "LocalDescription") }));

            Body_Item contentDetails = new Body_Item("contentDetails");
            contentDetails.values.Add(new Body_Item("itemcount", "1"));
 var body = new List<Body_Item> { contentDetails, snippetItems}        
``` 

