// tag.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tag } from '../models/tag.model';

@Injectable({
  providedIn: 'root'
})

export class TagService {
  private apiUrl = 'http://localhost:5000/api/tag/validate';

  constructor(private http: HttpClient) {}

  // Accepts a Tag object and sends it directly in the POST request
  validateTag(tag: Tag): Observable<any> {
    console.log("Sending request to:", this.apiUrl, "with payload:", tag);
    return this.http.post<any>(this.apiUrl, tag); // Send `tag` directly
  }
}
