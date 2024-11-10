import { Component, OnInit, ViewChild } from '@angular/core';
import { TagService } from './services/tag.service';
import { SearchBarComponent } from './components/search-bar/search-bar.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Master Tag System';
  jsonContent: string = '';  // Contenu JSON à partager
  parsedJson: any;           // JSON analysé à passer à TreeView
  allTags: any[] = [];       // Liste de tous les JSONs
  selectedTagId: string = ''; // ID du tag sélectionné

  @ViewChild(SearchBarComponent) searchBar: SearchBarComponent | undefined;

  constructor(private tagService: TagService) {}

  ngOnInit() {
    this.loadTags();
    this.loadTagData();
  }

  // Fonction pour charger la liste des tags
  loadTags() {
    this.tagService.getAllTags().subscribe(data => {
      this.allTags = data;  // Stocke tous les JSONs dans la variable allTags
      console.log('Liste des JSONs:', this.allTags);
      // Notifie SearchBar pour qu'il mette à jour ses données
      if (this.searchBar) {
        this.searchBar.updateJsonList(this.allTags);
      }
    }, error => {
      console.error('Erreur de récupération des données:', error);
    });
  }

  // Fonction pour récupérer les données pour TreeView et Editor
  loadTagData() {
    // Commence avec aucun tag sélectionné
    // this.parsedJson = '';  // Réinitialise parsedJson
    // this.jsonContent = '';  // Formatage du JSON en chaîne
  }

  // Fonction pour mettre à jour jsonContent lorsque TagEditor modifie le JSON
  onJsonChange(newJsonContent: string) {
    this.jsonContent = newJsonContent;
    this.parsedJson = JSON.parse(newJsonContent);  // Mettre à jour parsedJson
  }

  // Fonction pour sélectionner un tag depuis la SearchBar
  onJsonSelected(json: any) {
    this.selectedTagId = json.id;  // Met à jour l'ID du tag sélectionné
    this.parsedJson = json;  // Met à jour le JSON sélectionné
    this.jsonContent = JSON.stringify(json, null, 2);  // Met à jour l'éditeur
  }

  // Fonction appelée lors d'une mise à jour réussie pour rafraîchir la liste des tags
  refreshTagList() {
    console.log("Mise à jour réussie, recharge la liste des tags...");
    this.loadTags();  // Recharge les tags depuis le backend
  }

  // Méthode appelée lorsqu'un JSON est sélectionné dans la InfoTable
  onJsonFromTableSelected(json: any) {
    this.onJsonSelected(json);  // Utilise la méthode existante pour mettre à jour les composants
  }
}
