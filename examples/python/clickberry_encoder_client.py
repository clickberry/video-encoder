"""
Example client for Clickberry Encoder https://github.com/clickberry/video-encoder
"""
import requests

URL = 'http://clickberry.encoder.domain/api/videos'
FILE = r'path/to/video.mp4'

with open(FILE, 'rb') as file_data:
    FILES = {'file': ('video', file_data, 'video/mp4')}
    REQUEST = requests.post(URL, files=FILES)
    print REQUEST.json()
