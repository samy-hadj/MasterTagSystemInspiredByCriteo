import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tag } from '../models/tag.model';

@Injectable({
  providedIn: 'root'
})

export class TagService {
  private apiUrl = 'http://localhost:5000/api/tag/validate';
  private tagDataUrl = './assets/tagData.json';  // Chemin vers ton fichier JSON pour les données de tag

  constructor(private http: HttpClient) {}

  // Fonction pour valider un tag avec un POST
  validateTag(tag: Tag): Observable<any> {
    console.log("Sending request to:", this.apiUrl, "with payload:", tag);
    return this.http.post<any>(this.apiUrl, tag); // Send `tag` directly
  }

  // Nouvelle fonction pour récupérer les données de tag en GET
  getTagData(): Observable<any> {
    return this.http.get<any>(this.tagDataUrl);  // Assurez-vous que le fichier JSON est dans le dossier assets
  }
}
