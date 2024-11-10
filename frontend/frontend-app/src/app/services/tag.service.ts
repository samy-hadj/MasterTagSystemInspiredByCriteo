import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { TagModel } from '../models/tagModel.model';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  private apiUrl = 'http://localhost:5000/api/tag/validate';
  private hubUrl = 'http://localhost:5000/jsonHub';

  private connection!: signalR.HubConnection;
  private jsonDataSubject = new Subject<any>();
  
  private tagDataUrl = './assets/tagData.json'; // URL de votre fichier JSON local
  private allJsonDataUrl = 'http://localhost:5000/api/tag/tags'; // URL pour récupérer tous les JSONs
  private updateTagUrl = 'http://localhost:5000/api/tag/update'; // URL pour mettre à jour un tag


  constructor(private http: HttpClient) {
    this.setupSignalRConnection();
  }

  validateTag(tag: TagModel): Observable<any> {
    return this.http.post<any>(this.apiUrl, tag);
  }

  getRealTimeJsonData(): Observable<any> {
    return this.jsonDataSubject.asObservable();
  }

  // Fonction pour récupérer un seul JSON
  getTagData(): Observable<TagModel[]> {
    return this.http.get<TagModel[]>(this.tagDataUrl);
  }

  // Fonction pour récupérer tous les JSONs disponibles
  getAllTags(): Observable<TagModel[]> {
    return this.http.get<TagModel[]>(this.allJsonDataUrl);
  }

  // Nouvelle fonction pour mettre à jour un tag
  updateTag(id: string, tag: TagModel): Observable<any> {
    return this.http.put<any>(`${this.updateTagUrl}/${id}`, tag);  // Utilisation de PUT ou PATCH selon le cas
  }

  private setupSignalRConnection(): void {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(this.hubUrl, { withCredentials: true })
      .build();

    this.connection.start().then(() => {
      console.log("Connected to SignalR hub");

      this.connection.on("ReceiveJsonUpdate", (jsonData) => {
        this.jsonDataSubject.next(jsonData);
      });
    }).catch(err => console.error("SignalR connection error:", err));
  }
}
