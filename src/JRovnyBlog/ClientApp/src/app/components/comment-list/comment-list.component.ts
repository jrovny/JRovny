import { NestedTreeControl } from '@angular/cdk/tree';
import { ViewChild } from '@angular/core';
import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Comment } from 'src/app/models/comment';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.scss'],
})
export class CommentListComponent implements OnInit {
  @Input() comments: Comment[];
  treeControl = new NestedTreeControl<Comment>((c) => c.children);
  @ViewChild('tree') tree;
  dataSource = new MatTreeNestedDataSource<Comment>();

  constructor() {}

  ngOnInit(): void {
    this.dataSource.data = this.comments;
    this.treeControl.dataNodes = this.comments;

    this.loadComments();
  }

  loadComments() {}

  ngAfterViewInit() {
    this.tree.treeControl.expandAll();
  }

  hasChild = (_: number, node: Comment) =>
    !!node.children && node.children.length > 0;

  hasNoContent = (_: number, _nodeData: Comment) => _nodeData.commentId === 0;
}
