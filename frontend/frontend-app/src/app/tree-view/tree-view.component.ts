import { Component, ViewContainerRef, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IntegralUITreeView } from '@lidorsystems/integralui-web/bin/integralui/components/integralui.treeview';

@Component({
  selector: 'app-tree-view',
  templateUrl: './tree-view.component.html',
  styleUrls: ['./tree-view.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class TreeViewComponent implements AfterViewInit {
    @ViewChild('application', { read: ViewContainerRef }) applicationRef: ViewContainerRef;
    @ViewChild('treeview') treeview: IntegralUITreeView;

    public items: Array<any> = [];

    public ctrlStyle: any = {
        general: {
            normal: 'trw-dfjson-normal'
        }
    };

    public treeFields: any = {
        id: 'itemId',
        expanded: 'isExpanded',
        pid: 'parentId',
        items: 'children',
        text: 'label'
    };

    constructor(private http: HttpClient) { }

    ngAfterViewInit() {
        // Charger les données dans l'arborescence à partir d'un fichier JSON
        this.loadFromJSON();
    }

    private loadFromJSON() {
        // Utilisation du service HTTP pour obtenir des données à partir d'un fichier JSON
        this.http.get<Array<any>>('./assets/file.json').subscribe((data) => {
            // Suspendre la mise à jour de la disposition de l'arborescence pour améliorer les performances
            this.treeview.suspendLayout();

            // Charger les données dans la vue arborescente
            this.treeview.loadData(data, null, this.treeFields, false);

            // Reprendre et mettre à jour la disposition de l'arborescence
            this.treeview.resumeLayout();
        });
    }
}
