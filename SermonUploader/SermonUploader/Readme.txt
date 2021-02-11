Sermon Uploader Readme: 

This is a quick, small utility that runs ffmpeg to encode MP3s and their ID3 / metadata tags with the information entered into the app, 
upload those MP3s to AWS S3, upload the video file to Youtube, and email the links needed to the folks that add the sermon to the podcast. 

The utility expects a number of files in the same directory as the .exe to run, this includes: 
 - SermonUploader.exe.Config - a text file with the configuration information needed to create the MP3s, save logs, credentials  for sending email, the emails to send to, and details to upload to S3 - if you delete this and run the utility it will recreate the file and prompt you for the needed info
 - podcast.jpg - which is the image file to embed in the MP3s for the podcast
 - speakers.config - a text files of the format (Newline)Speaker name, speaker title(Newline) - which provides autocomplete minister names to help the user, the first list is assumed to be the main minister
 - youtubeAPIKey.json - a file from Youtube that has the API key for permission to the church's youtube channel, if this changes you'll need to login to the main google account, create a new API key for youtube, download this json and replace it here
 - ffmpeg.exe - a ffmpeg .exe to convert the mp4 to mp3s
 - lame_enc.dll - the library for encoding MP3s that ffmpeg is using