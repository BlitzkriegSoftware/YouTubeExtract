#! /bin/bash

PLAYLIST=$1
APIKEY=$2

curl -i -G -d "playlistId=${PLAYLIST}&maxResults=50&part=snippet&key=${APIKEY}"  https://www.googleapis.com/youtube/v3/playlistItems
