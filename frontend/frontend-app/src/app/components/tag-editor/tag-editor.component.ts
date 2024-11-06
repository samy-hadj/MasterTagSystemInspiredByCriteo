import { Component } from '@angular/core';

@Component({
  selector: 'app-tag-editor',
  templateUrl: './tag-editor.component.html',
  styleUrls: ['./tag-editor.component.css']
})
export class TagEditorComponent {
  jsonContent: string = '{\n  "Id": "ad_12345",\n  "DestinationUrl": "https://exemple.com/produit",\n  "TrackingData": "campaign_promo"\n}';
  parsedJson: any;

  constructor() {
    this.parseJson();
  }

  onJsonChange() {
    this.parseJson();
  }

  parseJson() {
    try {
      this.parsedJson = JSON.parse(this.jsonContent);
    } catch {
      this.parsedJson = "JSON invalide";
    }
  }

  saveChanges() {
    alert('Modifications enregistrées!');
  }
}
