import { Component, Input, Output, EventEmitter } from '@angular/core';
import { TagService } from '../../services/tag.service';
import { TagModel } from '../../models/tagModel.model';

@Component({
  selector: 'app-tag-editor',
  templateUrl: './tag-editor.component.html',
  styleUrls: ['./tag-editor.component.css']
})
export class TagEditorComponent {
  @Input() jsonContent: string = '';
  @Input() tagId: string = '';
  @Output() jsonChange = new EventEmitter<string>();
  @Output() updateSuccess = new EventEmitter<void>();  // Émet lorsque l'update est réussi

  showConfirmation: boolean = false;
  showError: boolean = false;

  constructor(private tagService: TagService) {}

  onJsonChange() {
    this.jsonChange.emit(this.jsonContent);
  }

  saveChanges() {
    const updatedTag: TagModel = JSON.parse(this.jsonContent);
    this.tagService.updateTag(this.tagId, updatedTag).subscribe(
      (response) => {
        console.log('Tag mis à jour:', response);
        this.showConfirmation = true;
        this.showError = false;
        this.updateSuccess.emit();  // Émet un événement pour signaler la mise à jour réussie
        setTimeout(() => this.showConfirmation = false, 2000);
      },
      (error) => {
        console.error('Erreur de mise à jour:', error);
        this.showError = true;
        this.showConfirmation = false;
        setTimeout(() => this.showError = false, 2000);
      }
    );
  }
}
