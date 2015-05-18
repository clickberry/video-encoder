var request = require('request');
var fs = require('fs');

var apiUrl = 'http://localhost:51255/api/videos';
var videoPath = 'C:\\Users\\alexe_000\\Videos\\test.mp4';

var formData = {
    video: fs.createReadStream(videoPath),
};

request.post({url: apiUrl, formData: formData}, function (err, res, body) {
    if (err) {
        return console.error('Upload failed:', err);
    }
    if (201 != res.statusCode) {
        return console.error('Invalid response received:', res.statusCode);
    }

    console.log(body);
});
