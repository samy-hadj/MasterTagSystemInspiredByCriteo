import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-tag-editor',
  templateUrl: './tag-editor.component.html',
  styleUrls: ['./tag-editor.component.css']
})
export class TagEditorComponent {
  @Input() jsonContent: string = '';   // JSON initial
  @Output() jsonChange = new EventEmitter<string>();  // Émet les changements du JSON

  onJsonChange() {
    this.jsonChange.emit(this.jsonContent);  // Émet les changements vers le parent
  }

  saveChanges() {
    alert('Modifications enregistrées!');
  }
}
