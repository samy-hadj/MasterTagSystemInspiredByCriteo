import { Component } from '@angular/core';
import { TagService } from '../../services/tag.service'; // Assurez-vous que le chemin est correct
import { Tag } from '../../models/tag.model'; // Assurez-vous que le chemin est correct

@Component({
  selector: 'app-tag-validator',
  templateUrl: './tag-validator.component.html',
  styleUrls: ['./tag-validator.component.css']
})
export class TagValidatorComponent {
  jsonContent: string = ''; // Contenu JSON à valider
  validationResult: string | null = null; // Résultat de la validation

  constructor(private tagService: TagService) {}

  validateTag() {
    try {
      const tagToValidate: Tag = JSON.parse(this.jsonContent);
      console.log("TAG TO VALIDATE: ", tagToValidate)


      // Vérifiez que tous les champs requis sont présents
      if (!tagToValidate.Id || !tagToValidate.DestinationUrl || !tagToValidate.TrackingData) {
        this.validationResult = 'Tous les champs sont requis.';
        return;
      }
  
      this.tagService.validateTag(tagToValidate).subscribe(
        (response) => {
          this.validationResult = response.isValid ? 'Valide' : 'Invalide';
        },
        (error) => {
          console.error('Erreur lors de la validation:', error);
          if (error.error && error.error.errors) {
            // Affichez les erreurs spécifiques
            const errorMessages = Object.entries(error.error.errors)
              .map(([key, value]) => `${key}: ${(value as string[]).join(', ')}`)
              .join('; ');
            this.validationResult = 'Erreurs de validation : ' + errorMessages;
          } else {
            this.validationResult = 'Erreur lors de la validation';
          }
        }
      );
    } catch (e) {
      this.validationResult = 'Erreur de parsing JSON : ' + e;
    }
  }
  
}
