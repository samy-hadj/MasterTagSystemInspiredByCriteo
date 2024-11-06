import kafka
print(kafka.__file__)
from kafka import KafkaProducer

import json
import time

# Configuration du producteur Kafka
producer = KafkaProducer(bootstrap_servers='localhost:9092',
                         value_serializer=lambda v: json.dumps(v).encode('utf-8'))

# Simulation d'envoi de JSON en masse
for i in range(1000):  # Exemple : envoie de 1000 messages
    data = {"id": i, "value": f"message-{i}"}
    producer.send('json-requests', value=data)
    time.sleep(0.01)  # Délai de 10 ms pour simuler un débit élevé
