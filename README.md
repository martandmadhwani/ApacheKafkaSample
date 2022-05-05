
# Introduction of Apache Kafka
Apache Kafka is a distributed event store and stream-processing platform. It is an open-source system developed by the Apache Software Foundation written in Java and Scala. The project aims to provide a unified, high-throughput, low-latency platform for handling real-time data feeds.

# Introduction of ZooKeeper
ZooKeeper is an open source Apache project that provides a centralized service for providing configuration information, naming, synchronization and group services over large clusters in distributed systems. The goal is to make these systems easier to manage with improved, more reliable propagation of changes.

# Why use apache kafka vs RabbitMq vs [Azure Service Bus]
Data Flow : 
RabbitMQ uses a distinct, bounded data flow. Messages are created and sent by the producer and received by the consumer.
Apache Kafka uses an unbounded data flow, with the key-value pairs continuously streaming to the assigned topic

Data Usage:
RabbitMQ is best for transactional data, such as order formation and placement, and user requests. 
Kafka works best with operational data like process operations, auditing and logging statistics, and system activity.

Messaging:
RabbitMQ sends messages to users. These messages are removed from the queue once they are processed and acknowledged. 
Kafka is a log. It uses continuous messages, which stay in the queue until the retention time expires.

Design Model:
RabbitMQ employs the smart broker/dumb consumer model. The broker consistently delivers messages to consumers and keeps track of their status. Kafka uses the dumb broker/smart consumer model. Kafka does not monitor the messages each user has read. Rather, it retains unread messages only, preserving all messages for a set amount of time. Consumers must monitor their position in each log.

Topology:
RabbitMQ uses the exchange queue topology sending messages to an exchange where they are in turn routed to various queue bindings for the consumer’s use.
Kafka employs the publish/subscribe topology, sending messages across the stream to the correct topics, and then consumed by users in the different authorized groups.

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
'' .\bin\windows\zookeeper-server-start.bat .\config\zookeeper.properties ''

It will start zookeeper on port 2181

-6 open cmd and run below command to start apache kafka
	'' .\bin\windows\kafka-server-start.bat .\config\server.properties ''

It will start apache kafka

# Create a topic

'' .\bin\windows\kafka-topics.bat --create --bootstrap-server localhost:9092 --replication-factor 1 --partitions 1 --topic TestTopic ''

# List the topics

'' .\bin\windows\kafka-topics.bat --list --bootstrap-server localhost:9092 ''

# Produce message on the topics

'' .\bin\windows\kafka-console-producer.bat --broker-list localhost:9092 --topic TestTopic ''

It will product sub console to broadcase message

# Consuming the message on the topics

'' .\bin\windows\kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic TestTopic --from-beginning ''


# Sample Kafka Net Core Application Producer

- Add nuget package Confluent.Kafka [latest 1.8.2]
- In appsetting.json add producer
'' "Producer":{ "bootstrapservers":"localhost:9092"} ''

- In startup file configure producer
'' var producerConfig = new ProducerConfig();

 Configuration.Bind("producer", producerConfig);
        
 services.AddSingleton<ProducerConfig>(producerConfig); ''
 
# Sample Kafka Net Core Application Consumer Console Application

- Create asp.net core console application
- Add nuget package Confluent.Kafka [latest 1.8.2]
- In Program.cs file put below code
'' var config = new ConsumerConfig()
            {
                GroupId= "gid-consumers",
                BootstrapServers="localhost:9092"
            };

            using (var consumer = new ConsumerBuilder<Null,string>(config).Build())
            {
                consumer.Subscribe("TestTopic");
                while(true)
                {
                    var cr = consumer.Consume();
                    Console.WriteLine(cr.Message.Value);
                }
            } '' 
