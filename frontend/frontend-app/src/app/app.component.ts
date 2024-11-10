import { Component, OnInit } from '@angular/core';
import { TagService } from './services/tag.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Master Tag System';
  jsonContent: string = '';  // Contenu JSON à partager
  parsedJson: any;           // JSON analysé à passer à TreeView

  constructor(private tagService: TagService) {}

  ngOnInit() {
    // Récupérer les données via le service TagService
    this.tagService.getTagData().subscribe(data => {
      this.parsedJson = data[1];
      console.log('Données JSON analysées:', this.parsedJson);
      this.jsonContent = JSON.stringify(this.parsedJson, null, 2);  // Formatage du JSON en chaîne
    }, error => {
      console.error('Erreur de récupération des données:', error);
    });
  }

  // Fonction pour mettre à jour jsonContent lorsque TagEditor modifie le JSON
  onJsonChange(newJsonContent: string) {
    this.jsonContent = newJsonContent;
    this.parsedJson = JSON.parse(newJsonContent);  // Mettre à jour parsedJson
  }
}
