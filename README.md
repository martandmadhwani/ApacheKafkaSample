
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
- 1. https://www.apache.org/dyn/closer.cgi?path=/kafka/3.1.0/kafka_2.12-3.1.0.tgz
- 2. Extract tgz folder
- 3. rename folder to kafka and remove postfix version text and copy folder to c:\ drive
- 4. bin directory contains windows and linux commands to run apache kafka [bat and sh files]
- 5. config folder holding all configuration files to configure apache kafka
- 6. libs folder contains all supporting libs.
- 7. Update server.properties file in config folder and update log path as below

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
- In appsetting.json add producer cofiguration json "Producer":{ "bootstrapservers":"localhost:9092"}

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


# Kafka Cheat Sheet - [Useful Commands]

Display Topic Information

$ kafka-topics.sh --describe --zookeeper localhost:2181 --topic beacon
Topic:beacon	PartitionCount:6	ReplicationFactor:1	Configs:
	Topic: beacon	Partition: 0	Leader: 1	Replicas: 1	Isr: 1
	Topic: beacon	Partition: 1	Leader: 1	Replicas: 1	Isr: 1
Add Partitions to a Topic

$ kafka-topics.sh --alter --zookeeper localhost:2181 --topic beacon --partitions 3
WARNING: If partitions are increased for a topic that has a key, the partition logic or ordering of the messages will be affected
Adding partitions succeeded!
Change topic retention i.e set SLA

bin/kafka-topics.sh --zookeeper localhost:2181 --alter --topic mytopic --config retention.ms=28800000*
This set retention of 8-hours on messages coming to topic mytopic. After 8 hours message will be deleted.

Delete Topic

$ kafka-run-class.sh kafka.admin.DeleteTopicCommand --zookeeper localhost:2181 --topic test
kafka-topics.sh --create --zookeeper localhost:2181 --replication-factor 1 --partitions 3 --topic file_acquire_complete
kafka-topics.sh --create --zookeeper localhost:2181 --replication-factor 1 --partitions 3 --topic job_result
kafka-topics.sh --create --zookeeper localhost:2181 --replication-factor 1 --partitions 3 --topic trigger_match
kafka-topics.sh --create --zookeeper localhost:2181 --replication-factor 1 --partitions 3 --topic event_result
$ kafka-topics.sh --create --zookeeper localhost:2181 --replication-factor 1 --partitions 3 --topic job_result
Created topic "job_result".
$ kafka-topics.sh --list --zookeeper localhost:2181
event_result
file_acquire_complete
job_result
trigger_match
$ kafka-console-producer.sh --broker-list localhost:9092 --topic test
$ kafka-console-consumer.sh --zookeeper localhost:2181 --topic test --from-beginning
List existing topics
bin/kafka-topics.sh --zookeeper localhost:2181 --list

Purge a topic
bin/kafka-topics.sh --zookeeper localhost:2181 --alter --topic mytopic --config retention.ms=1000

... wait a minute ...

bin/kafka-topics.sh --zookeeper localhost:2181 --alter --topic mytopic --delete-config retention.ms

Delete a topic
bin/kafka-topics.sh --zookeeper localhost:2181 --delete --topic mytopic

Get the earliest offset still in a topic
bin/kafka-run-class.sh kafka.tools.GetOffsetShell --broker-list localhost:9092 --topic mytopic --time -2

Get the latest offset still in a topic
bin/kafka-run-class.sh kafka.tools.GetOffsetShell --broker-list localhost:9092 --topic mytopic --time -1

Consume messages with the console consumer
bin/kafka-console-consumer.sh --new-consumer --bootstrap-server localhost:9092 --topic mytopic --from-beginning

Get the consumer offsets for a topic
bin/kafka-consumer-offset-checker.sh --zookeeper=localhost:2181 --topic=mytopic --group=my_consumer_group

Read from __consumer_offsets
Add the following property to config/consumer.properties: exclude.internal.topics=false

bin/kafka-console-consumer.sh --consumer.config config/consumer.properties --from-beginning --topic __consumer_offsets --zookeeper localhost:2181 --formatter "kafka.coordinator.GroupMetadataManager\$OffsetsMessageFormatter"

Kafka Consumer Groups
List the consumer groups known to Kafka
bin/kafka-consumer-groups.sh --zookeeper localhost:2181 --list (old api)

bin/kafka-consumer-groups.sh --new-consumer --bootstrap-server localhost:9092 --list (new api)

View the details of a consumer group
bin/kafka-consumer-groups.sh --zookeeper localhost:2181 --describe --group <group name>

kafkacat
Getting the last five message of a topic
kafkacat -C -b localhost:9092 -t mytopic -p 0 -o -5 -e

Zookeeper
Starting the Zookeeper Shell
bin/zookeeper-shell.sh localhost:2181