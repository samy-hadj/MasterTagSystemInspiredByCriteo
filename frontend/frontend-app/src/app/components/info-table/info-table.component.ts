import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-info-table',
  templateUrl: './info-table.component.html',
  styleUrls: ['./info-table.component.css']
})
export class InfoTableComponent {
  // Liste JSON par défaut
  @Input() jsonList: any[] = [
    { id: 1, name: 'Sample JSON 1', data: '{ "key": "value1" }', createdAt: new Date() },
    { id: 2, name: 'Sample JSON 2', data: '{ "key": "value2" }', createdAt: new Date() },
    { id: 3, name: 'Sample JSON 3', data: '{ "key": "value3" }', createdAt: new Date() }
  ];
}
