import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tag } from '../models/tag.model'; // Assurez-vous que le chemin est correct

@Injectable({
  providedIn: 'root'
})
export class TagService {
  private apiUrl = 'http://localhost:5000/api/tag/validate'; // Point de terminaison correct

  constructor(private http: HttpClient) {}

  validateTag(tag: Tag): Observable<any> {
    return this.http.post<any>(this.apiUrl, tag); // Envoi de la requête POST avec le tag
  }
}
