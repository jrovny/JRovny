import { Component, Input, OnInit } from '@angular/core';
import { Comment } from 'src/app/models/comment';

@Component({
  selector: 'app-comment-detail',
  templateUrl: './comment-detail.component.html',
  styleUrls: ['./comment-detail.component.scss'],
})
export class CommentDetailComponent implements OnInit {
  @Input() comment: Comment;

  constructor() {}

  ngOnInit(): void {}

  isRootNode() {
    // return this.comment.parentCommentId === 0;
    return false;
  }
}
