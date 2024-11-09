# import kafka
# print(kafka.__file__)
# from kafka import KafkaProducer

# import json
# import time

# # Configuration du producteur Kafka
# producer = KafkaProducer(bootstrap_servers='localhost:9092',
#                          value_serializer=lambda v: json.dumps(v).encode('utf-8'))

# # Simulation d'envoi de JSON en masse
# for i in range(1000):  # Exemple : envoie de 1000 messages
#     data = {"id": i, "value": f"message-{i}"}
#     producer.send('json-requests', value=data)
#     time.sleep(0.01)  # Délai de 10 ms pour simuler un débit élevé


#FAKE PRODUCER:
import requests
import json
import time

# URL de l'API de ton backend
url = 'http://localhost:5000/api/tag/validate'  # Remplace par l'URL de ton backend C#

# Fonction pour générer des données JSON
def generate_data():
    return {"id": "123jDDDjlldohddlll44", "destinationUrl": "https://example.com", "trackingData": "some tracking dakkkta"}

# Envoi de JSON toutes les 2 secondes
while True:
    data = generate_data()
    print(f"Envoi de message : {data}")

    # Envoi de la requête POST au backend
    response = requests.post(url, json=data)

    # Affiche la réponse du backend
    if response.status_code == 200:
        print(f"Réponse du serveur : {response.json()}")
    else:
        print("Erreur dans l'envoi du message", response)

    time.sleep(4)  # Envoi toutes les 2 secondes
