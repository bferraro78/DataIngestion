# Linkfire Data Ingestion Project

Hello! Thank you for taking the time to review my DataIngestion project, and I am look forward to discussing it in the future!
I went with a rather simple approach, with one "Driver" Console App and one .NET Core WebApi Service. The both directories have solution files for all of your Visual Studio code viewing needs. The WebApi will need to be running when the console app is run. The Console app will return the local ElasticIndex URl as well as a Kibana Dashboard local URL (allowing you to query the albums index both programtically and using a UI). 

Although I chose to manually import the Data from the google drive given in the Task Description (https://drive.google.com/drive/folders/1RkUWkw9W0bijf7GOgV4ceiFppEpeXWGv - due to time constraints), I saw that there is a GoogleDrive API that uses OAUTH to access and download files from a drive. I implore you to take a look at another one of my casual projects where I implement an API to retrieve stock data using OAUTH to authenticate (https://github.com/bferraro78/ReversalBars/tree/master/TDAPI). I chose to extract the data from the SQL Table files programmatically and load them into local storage asynchronously. I represent the data as Dictionaries and preform LINQ expressions to map data to the Album object before injecting the created albums into a local Elastic Index - "albums". I saw a few third party Services that may have been useful (Adding data to a Sql Databse, Data Ingest Pipeline such as AzureEventHub, a ETL Service AWS Glue), however, I chose to do it locally to show my design and coding skills. 

I would love to discuss where in my project I would have implmented a Google Drive Download Service and what other thirds party solutions I could use to make my solution less resource intensive.

Feel free to reach out if you need anything / have any questions!
Thanks 

### Prerequisites:
- Docker
#### Optional
- Seq

##### Seq Logging
- Provides simple dashboard for applicaiton logging. 
![Seq Logging Dashboard](https://github.com/bferraro78/DataIngestion/blob/master/ReadmeMarkup/Seq.PNG)


## Local Startup Instructions
1. Install Docker
2. (Optional) Install Seq - https://datalust.co/download. Navigate to http://localhost:5341/ to see logging dashboard.
3. Manually insert data files into \MediaData\MediaData\Data (There are crurrently placeholder documents there with DB structure lines in place. The files were too big to add to the git repo).
4. Open powershell and run `docker-compose up -d` in the `\DataIngestion` (make sure the docker.compose file is present).
<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; - This starts up the local Elastic Index and corresponding Kibana dashboard as two docker images in a docker container
5. Open powershell in `\DataIngestion\MediaData\MediaData` and run the command `dotnet run`
6. Open another powershell to `\DataIngestion\Driver` and run the command `dotnet run`
7. After the Driver finishes running, the Elastic Index Url and Kibana Dash board are written in the Console. Navigate to kibana 'albums' index to view and query all added album objects. 
