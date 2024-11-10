from kafka import KafkaProducer
import json
import time
import random
import string

# Callback en cas de succès d'envoi de message
def on_send_success(record_metadata):
    print(f"Message envoyé avec succès à partition {record_metadata.partition}, offset {record_metadata.offset}.")

# Callback en cas d'erreur d'envoi de message
def on_send_error(excp):
    print(f"Erreur lors de l'envoi du message : {excp}")

# Création du producteur Kafka avec sérialisation JSON
producer = KafkaProducer(
    bootstrap_servers='localhost:9092',
    value_serializer=lambda v: json.dumps(v).encode('utf-8')
)

# Fonction pour générer des données JSON avec un ID aléatoire
def generate_data():
    random_id = ''.join(random.choices(string.ascii_letters + string.digits, k=16))
    return {
        "id": random_id,
        "destinationUrl": "https://example.com",
        "trackingData": "some tracking data"
    }

# Envoi de données toutes les 4 secondes avec des logs de succès/erreur
while True:
    data = generate_data()
    print(f"Envoi du message : {data}")

    # Envoi du message au topic Kafka
    future = producer.send('json-requests', value=data)
    future.add_callback(on_send_success).add_errback(on_send_error)  # Ajoute les callbacks pour le succès et l'erreur
    
    # Forcer l'envoi immédiat
    producer.flush()
    
    time.sleep(5)  # Envoi toutes les 4 secondes
