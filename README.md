# Plugin.YoutubeLibrary
A Xamarin Library for handling Youtube Api requests

## Folders Used
**Api**: This contains the code for handling the api calls <br>
**util**: Random helpers/functions used <br>
**Youtube**: The youtube service <br>



## Code Snippet

``` c#
using YoutubeLibrary.youtube;
public MainWindow()
{
  InitializeComponent();

  string ApiKey = "";
  string Accesstoken = "";
  YoutubeClient youtube = new YoutubeClient(ApiKey);
  YoutubeClient youtube2 = new YoutubeClient(ApiKey, Accesstoken);

  var playlistsJson = youtube.playlistsList.getPlaylist(new string[] { "snippet", "contentDetails" }, true);

}
``` 


