echo off
rm otp
wget -O otp "https://www.tibia.com/community/?subtopic=killstatistics&world=Belobra"
python pythonHtmlParser.py
