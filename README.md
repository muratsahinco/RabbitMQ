# RabbitMQ
Test Projesidir.

1- erlang indir (https://www.erlang.org/downloads)
2- rabit mq indir (https://www.rabbitmq.com/install-windows.html)
3- C:\Program Files\RabbitMQ Server\rabbitmq_server-3.8.2\sbin> (cmd ile)
4- rabbitmq-plugins enable rabbitmq_management


İlgili kodu çalıştırdıktan sonra “Could not update enabled plugins file …” hatası alırsanız aşağıdaki işlemleri sırasıyla uygulayınız;

Bir komut istemcisini yönetici olarak çalıştırın ve C:\Program Files\RabbitMQ Server\rabbitmq_server-3.8.2\sbin> dizinini açınız.
rabbitmq-service remove komutunu çalıştırınız.
Ardından rabbitmq-service install komutunu çalıştırınız.
Ve en son tekrardan rabbitmq-plugins enable rabbitmq_management komutunu çalıştırınız.
