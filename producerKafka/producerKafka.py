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

# Fonction pour générer des données JSON complexes avec des valeurs variées
def generate_data():
    # ID aléatoire
    random_id = ''.join(random.choices(string.ascii_letters + string.digits, k=16))

    # Probabilités d'invalidité de certaines valeurs
    destination_url = "https://example.com" if random.random() > 0.2 else "invalid_url"
    tracking_data = "some tracking data" if random.random() > 0.3 else ""
    click_count = random.randint(0, 1000) if random.random() > 0.5 else None
    session_id = ''.join(random.choices(string.ascii_letters + string.digits, k=8)) if random.random() > 0.3 else None
    referrer = "https://referrer.com" if random.random() > 0.6 else None

    # Ajout d'un champ 'details' avec des sous-éléments
    details = {
        "userInfo": {
            "age": random.randint(18, 65),
            "location": random.choice(["USA", "France", "Germany", "India"]),
            "preferences": {
                "theme": random.choice(["dark", "light"]),
                "language": random.choice(["English", "French", "German"])
            }
        },
        "activity": {
            "lastLogin": f"{random.randint(1, 12)}/{random.randint(1, 28)}/2024",
            "pagesVisited": random.randint(1, 100),
            "actions": ["click", "scroll", "navigate"][random.randint(0, 2)]
        }
    }

    # Données JSON complexes
    return {
        "id": random_id,
        "destinationUrl": destination_url,
        "trackingData": tracking_data,
        "clickCount": click_count,
        "sessionId": session_id,
        "referrer": referrer,
        "details": details  # Ajout du champ avec des enfants
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
    
    time.sleep(3)  # Envoi toutes les 5 secondes
