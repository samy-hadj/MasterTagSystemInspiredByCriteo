import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { TreeDataService } from '../../services/tree-data.service';

interface TreeNode {
  name: string;
  children?: TreeNode[];
}

interface FlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

@Component({
  selector: 'app-tree-view',
  templateUrl: './tree-view.component.html',
  styleUrls: ['./tree-view.component.css']
})
export class TreeViewComponent implements OnInit, OnChanges {
  @Input() jsonData: any; // Prend la donnée transmise depuis le parent

  private transformer = (node: TreeNode, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
    };
  };

  treeControl = new FlatTreeControl<FlatNode>(
    node => node.level,
    node => node.expandable
  );

  treeFlattener = new MatTreeFlattener(
    this.transformer,
    node => node.level,
    node => node.expandable,
    node => node.children
  );

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  constructor(private treeDataService: TreeDataService) {}

  ngOnInit() {
    if (this.jsonData) {
      this.dataSource.data = this.jsonData;  // Charger les données JSON dans l'arbre
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['jsonData']) {  // Utilisation des crochets pour accéder à la propriété jsonData
      if (this.jsonData) {
        this.dataSource.data = this.jsonData;
      }
    }
  }

  hasChild = (_: number, node: FlatNode) => node.expandable;
}
