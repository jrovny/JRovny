import { NestedTreeControl } from '@angular/cdk/tree';
import { OnDestroy, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { select, Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { Comment } from 'src/app/models/comment';
import { PostDetail } from 'src/app/models/post-detail';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.scss'],
})
export class CommentListComponent implements OnInit, OnDestroy {
  treeControl = new NestedTreeControl<Comment>((c) => c.children);
  @ViewChild('tree') tree;
  dataSource = new MatTreeNestedDataSource<Comment>();
  comments: Comment[] = [];
  subscription$: Subscription;
  commentTree: Comment[] = [];

  constructor(
    private store: Store<{ appState: { selectedPost: PostDetail } }>
  ) {}

  ngOnInit(): void {
    this.subscription$ = this.store
      .pipe(select((state) => state.appState.selectedPost.comments))
      .subscribe((comments) => {
        this.comments = comments;
      });
    this.loadComments();

    this.dataSource.data = this.commentTree;
    this.treeControl.dataNodes = this.commentTree;
  }

  ngOnDestroy() {
    this.subscription$.unsubscribe();
  }

  ngAfterViewInit() {
    this.tree.treeControl.expandAll();
  }

  hasChild = (_: number, node: Comment) =>
    !!node.children && node.children.length > 0;

  hasNoContent = (_: number, _nodeData: Comment) => _nodeData.commentId === 0;

  loadComments() {
    this.comments.forEach((comment) => {
      var localComment: Comment = {
        commentId: comment.commentId,
        content: comment.content,
        createdDate: comment.createdDate,
        children: [],
        userName: comment.userName,
        parentCommentId: comment.parentCommentId,
        emailHash: comment.emailHash,
      };
      if (localComment.parentCommentId === 0) {
        this.commentTree.push(localComment);
      } else {
        this.add(this.commentTree, localComment);
      }
    });
  }

  add(comments: Comment[], comment: Comment) {
    comments.forEach((c) => {
      if (c.commentId === comment.parentCommentId) {
        c.children.push(comment);
      } else this.add(c.children, comment);
    });
  }
}
