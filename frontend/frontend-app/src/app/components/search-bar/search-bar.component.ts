import { Component, Output, EventEmitter } from '@angular/core';
import { TagService } from '../../services/tag.service';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent {
  searchQuery: string = '';
  jsons: any[] = [];  // Liste des JSONs
  filteredJsons: any[] = [];  // Liste filtrée des JSONs
  showDropdown: boolean = false; // Contrôle de l'affichage du dropdown

  @Output() jsonSelected = new EventEmitter<any>();  // Émet lorsque l'utilisateur sélectionne un JSON

  constructor(private tagService: TagService) {}

  ngOnInit() {
    this.tagService.getAllTags().subscribe((data) => {
      this.jsons = data;
      this.filteredJsons = data;  // Au départ, afficher tous les JSONs
    });
  }

  onSearchChange() {
    if (this.searchQuery.trim()) {
      this.showDropdown = true; // Affiche le dropdown si une recherche est effectuée
      this.filteredJsons = this.jsons.filter(json =>
        json.id.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    } else {
      this.showDropdown = false; // Cache le dropdown si la recherche est vide
      this.filteredJsons = []; // Vide la liste des résultats
    }
  }

  selectJson(json: any) {
    this.jsonSelected.emit(json);  // Émet le JSON sélectionné
    this.searchQuery = json.id;   // Affiche l'ID dans la barre de recherche
    this.showDropdown = false;    // Cache le dropdown après la sélection
  }

  // Méthode pour mettre à jour la liste des JSONs depuis le parent
  updateJsonList(newJsons: any[]) {
    this.jsons = newJsons;
    this.filteredJsons = newJsons;  // Met à jour la liste filtrée aussi
  }
}
