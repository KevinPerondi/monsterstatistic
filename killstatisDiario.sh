#!/bin/bash
echo off

#start api
python apizinha.py 

#crawling data...
while true
do
	rm otp
	wget -O otp "https://www.tibia.com/community/?subtopic=killstatistics&world=Belobra"
	python pythonHtmlParser.py
	sleep 86400
done
