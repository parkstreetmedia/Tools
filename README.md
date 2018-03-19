#Software 
All the various bits of software used to keep the radio room running. None of these are a paragon of programming, but they are
little bits of code to make our lives easier - and are straight-forward enough to fix when something changes (as these are all built around 
our processes). Perhaps some part of these will be useful to you, or perhaps you want to improve any of these basic tools, scripts really.

AirSupply - Alright, this has nothing to do with media, but it is a simple interface to control a c-50a air handler. It also pushes events from an EMS SQL database to AWS Dynamo and controls the air based on EMS events.  

EC2Control - Start/stops a single EC2 server that runs Wowza for streaming - we are just so cheap we don't want to leave it running all the time, we don't need to auto-scale. 

RRHDMIControl - Registers hotkeys in Windows, sits in the system tray, and when triggered switches the HDMI input of our HDMI switcher via serial. This enables an android remote or bluetooth keyboard to change inputs to our projector/streaming.

SermonUploader - Takes a mp4 video clip (or most any container supported by ffmpeg), creates mp3s for upload to our website, podcast, and uploads the video to YouTube. Also has a little GUI for uploading a YouTub video via the API so you don't need to give anyone the username/password to let folks add videos. 

ServicePPTCreator - Creates PowerPoint presentations... It does this based off our PDF bulletin (processing it in a huuuge hack-y set of string comparisons), creates a presentation based off our Planning Center Online service, again hack-y, but less so given the structured nature of PCO data items, converts HTML lyrics from our hymnal online to PowerPoint, converts bulk or singular Propresenter6 (or 5 probably) files to PowerPoint, text conversion only, and takes a scripture reference and creates Powerpoint slides. And before you ask, no, we can't give you the NIV sqlite DB this uses :-) we don't want to get sued by Rupert Murdoch, that and you know, honesty. 

