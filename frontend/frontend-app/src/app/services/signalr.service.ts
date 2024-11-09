import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })
  export class SignalRService {
    private hubConnection: HubConnection;
    private jsonsUpdatedSubject = new Subject<any>();
  
    constructor() {
      console.log("SignalRService constructor: Initialisation du service SignalR");
  
      this.hubConnection = new HubConnectionBuilder()
        .withUrl("http://localhost:5000/jsonHub", {
          withCredentials: true // Important pour gérer les cookies/session
        })
        .build();
  
      console.log("SignalRService: Connexion SignalR construite, tentative de démarrer...");
  
      this.hubConnection.on("ReceiveJsonUpdate", (jsons) => {
        console.log("SignalRService: Received JSONs: ", jsons);
        this.jsonsUpdatedSubject.next(jsons);
      });
  
      // Tentative de connexion à SignalR
      this.hubConnection
        .start()
        .then(() => {
          console.log("SignalR connection established successfully");
        })
        .catch(err => {
          console.error("SignalR connection error during startup: ", err);
          alert("Erreur de connexion SignalR: " + err);  // Alerte si la connexion échoue
          setTimeout(() => this.startConnection(), 5000); // Tentative de reconnexion après 5 secondes
        });
    }
  
    getJsonUpdates() {
      return this.jsonsUpdatedSubject.asObservable();
    }
  
    private startConnection() {
      console.log("SignalRService: Tentative de reconnexion...");
  
      this.hubConnection
        .start()
        .then(() => {
          console.log("SignalR connection established successfully after retry");
        })
        .catch(err => {
          console.error("SignalR connection error on retry: ", err);
          setTimeout(() => this.startConnection(), 5000); // Tentative de reconnexion après 5 secondes
        });
    }
  }
  