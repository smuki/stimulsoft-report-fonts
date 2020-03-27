# stimulsoft-report-fonts
Stimulsoft.Report fonts problem


This is a case for testing PDF embedded in Chinese font. 

The expected result is that Chinese is displayed normally and the file is not large


windows test step

check out  and run build-and-run-under-windows.bat


docker test step


1. build a docker 
docker build -t testreport:v1 .

2. run docker
docker run -it -v $(pwd)/:/rpt testreport:v1

3.build and run test sample
   1. cd /rpt 
   2. dotnet build .
   3. dotnet run .


