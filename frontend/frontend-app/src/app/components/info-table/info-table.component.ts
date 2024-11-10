import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { TagService } from '../../services/tag.service';
import { TagModel } from '../../models/tagModel.model';

@Component({
  selector: 'app-info-table',
  templateUrl: './info-table.component.html',
  styleUrls: ['./info-table.component.css']
})
export class InfoTableComponent implements OnInit {
  @Input() jsonList: TagModel[] = [];
  @Output() jsonSelected = new EventEmitter<TagModel>();  // Nouvel événement de type TagModel

  constructor(private tagService: TagService) {}

  ngOnInit(): void {
    // Écouter les mises à jour en temps réel
    this.tagService.getRealTimeJsonData().subscribe(
      (jsonData: TagModel) => {
        console.log('Received real-time JSON data:', jsonData);
        this.jsonList.unshift(jsonData); // Ajouter en début de liste sans remplacer les autres éléments
        console.log('Updated JSON list:', this.jsonList);
      },
      (error) => {
        console.error('Error receiving real-time JSON data:', error);
      }
    );
  }

  // Sélectionner un JSON
  selectJson(json: TagModel): void {
    this.jsonSelected.emit(json);  // Émettre l'événement avec le JSON sélectionné
  }

  // Formater les données de suivi en JSON si elles sont en format objet
  formatTrackingData(data: any): string {
    return typeof data === 'object' ? JSON.stringify(data) : data;
  }
}
