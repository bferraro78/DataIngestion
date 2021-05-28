# Linkfire Data Ingestion Project

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
5. Open the MediaData solution and run in IIS Express OR in `\DataIngestion\MediaData\MediaData` run the command `dotnet core`
6. Open the Driver Solution and start the console application
7. After the Driver finishes running, the Elastic Index Url and Kibana Dash board are written in the Console. Navigate to kibana 'albums' index to view and query all added album objects. 
