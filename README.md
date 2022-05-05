# Apache Kafka Sample with installation steps
apache kafka installation steps

** Download and setup apache kafka from web url **
- 1.https://www.apache.org/dyn/closer.cgi?path=/kafka/3.1.0/kafka_2.12-3.1.0.tgz
- 2. Extract tgz folder
- 3. rename folder to kafka and remove postfix version text and copy folder to c:\ drive
-  3a - bin directory contains windows and linux commands to run apache kafka [bat and sh files]
   3b - config folder holding all configuration files to configure apache kafka
   3c - libs folder contains all supporting libs.
- 4. Update server.properties file in config folder and update log path as below

#Change server.properties file
log.dirs=/tmp/kafka-logs
change it to
log.dirs=c:/kafka/kafka-logs

#Change zookeeper.properties file

dataDir=/tmp/zookeeper
change it to
dataDir=c:/kafka/zookeeper

- 5 Run command in cmd
''' .\bin\windows\zookeeper-server-start.bat .\config\zookeeper.properties '''

It will start zookeeper on port 2181

-6 open cmd and run below command to start apache kafka
	''' .\bin\windows\kafka-server-start.bat .\config\server.properties '''

It will start apache kafka

# Create a topic

''' .\bin\windows\kafka-topics.bat --create --bootstrap-server localhost:9092 --replication-factor 1 --partitions 1 --topic TestTopic '''

# List the topics

''' .\bin\windows\kafka-topics.bat --list --bootstrap-server localhost:9092 '''

# Product message on the topics

''' .\bin\windows\kafka-console-producer.bat --broker-list localhost:9092 --topic TestTopic '''

It will product sub console to broadcase message

# Consuming the message on the topics

''' .\bin\windows\kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic TestTopic --from-beginning '''