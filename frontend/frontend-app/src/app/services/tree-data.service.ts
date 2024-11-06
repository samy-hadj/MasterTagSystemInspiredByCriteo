import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TreeDataService {

  constructor(private http: HttpClient) { }

  getTreeData(): Observable<any> {
    return this.http.get<any>('./assets/treeData.json');  // Assurez-vous que le fichier JSON est dans le dossier assets
  }
}
