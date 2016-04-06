@echo off
set /p BucketName= "Enter existing S3 bucket name (e.g. videos): "
set /p QueueName= "Enter existing SQS queue name (e.g. encodings): "

setx CLICKBERRY_BucketName %BucketName% /m
setx CLICKBERRY_QueueName %QueueName% /m
