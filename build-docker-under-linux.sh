docker build -t testreport:v1 .

docker run -it -v $(pwd)/:/rpt testreport:v1
