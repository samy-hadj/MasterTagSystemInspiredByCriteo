import { Component } from '@angular/core';
import { TagService } from '../../services/tag.service'; // Assurez-vous que le chemin est correct
import { Tag } from '../../models/tag.model';

@Component({
  selector: 'app-tag-validator',
  templateUrl: './tag-validator.component.html',
  styleUrls: ['./tag-validator.component.css']
})
export class TagValidatorComponent {
  tag: Tag = { Id: '', DestinationUrl: '', TrackingData: '' };
  validationResult: boolean | null = null; // pour stocker le résultat de la validation

  constructor(private tagService: TagService) {}

  validate() {
    this.tagService.validateTag(this.tag).subscribe(
      response => {
        this.validationResult = response.isValid; // Assurez-vous que le backend renvoie un champ 'isValid'
      },
      error => {
        console.error('Erreur lors de la validation du tag:', error);
        this.validationResult = false; // Optionnel : gérer l'erreur
      }
    );
  }
}
