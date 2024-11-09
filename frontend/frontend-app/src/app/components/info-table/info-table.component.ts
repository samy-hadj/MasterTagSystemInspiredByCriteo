import { Component, OnInit } from '@angular/core';
import { TagService } from '../../services/tag.service';
import { TagModel } from '../../models/tagModel.model';

@Component({
  selector: 'app-info-table',
  templateUrl: './info-table.component.html',
  styleUrls: ['./info-table.component.css']
})
export class InfoTableComponent implements OnInit {
  jsonList: TagModel[] = [];

  constructor(private tagService: TagService) {}

  ngOnInit(): void {
    console.log('InfoTableComponent initialized');
    this.loadInitialData();

    // Écouter les mises à jour en temps réel
    this.tagService.getRealTimeJsonData().subscribe(
      (jsonData: TagModel) => {
        console.log('Received real-time JSON data:', jsonData);
        
        // Ajouter l'objet jsonData à la liste en haut
        this.jsonList.unshift(jsonData);  // Ajout en début de liste
        
        // Limiter la taille de la liste à 5 éléments maximum
        if (this.jsonList.length > 5) {
          this.jsonList.pop();  // Retirer le dernier élément si la taille dépasse 5
        }

        console.log('Updated JSON list:', this.jsonList);
      },
      (error) => {
        console.error('Error receiving real-time JSON data:', error);
      }
    );    
  }

  private loadInitialData(): void {
    console.log('Loading initial data');
    this.tagService.getTagData().subscribe(
      (data: TagModel[]) => {
        console.log('Initial data loaded:', data);
        this.jsonList = data;
        
        // Limiter la taille de la liste à 5 éléments maximum après chargement initial
        if (this.jsonList.length > 5) {
          this.jsonList = this.jsonList.slice(0, 5);  // Garder seulement les 5 premiers
        }
      },
      (error) => {
        console.error('Error loading initial data:', error);
      },
      () => {
        console.log('Initial data loading completed');
      }
    );
  }
}
