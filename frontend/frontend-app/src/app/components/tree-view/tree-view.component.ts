import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';

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
export class TreeViewComponent implements OnChanges {
  @Input() jsonData: any;

  private transformer = (node: TreeNode, level: number) => ({
    expandable: !!node.children && node.children.length > 0,
    name: node.name,
    level: level,
  });

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

  ngOnChanges(changes: SimpleChanges) {
    if (changes['jsonData'] && changes['jsonData'].currentValue) {
      this.dataSource.data = this.buildTreeStructure(this.jsonData);
      console.log('Données JSON transformées en arborescence:', this.dataSource.data);
    }
  }

  buildTreeStructure(obj: any): TreeNode[] {
    const buildNode = (value: any, key: string = ''): TreeNode => {
      if (typeof value === 'object' && value !== null) {
        const children = Array.isArray(value)
          ? value.map((v, i) => buildNode(v, `Item ${i + 1}`))
          : Object.entries(value).map(([k, v]) => buildNode(v, k));
        return { name: key, children };
      } else {
        return { name: `${key}: ${value}` };
      }
    };

    // Directly return the JSON data as an array of TreeNode without an extra root node
    return Array.isArray(obj)
      ? obj.map((item, index) => buildNode(item, `Item ${index + 1}`))
      : Object.entries(obj).map(([key, value]) => buildNode(value, key));
  }

  hasChild = (_: number, node: FlatNode) => node.expandable;
}
