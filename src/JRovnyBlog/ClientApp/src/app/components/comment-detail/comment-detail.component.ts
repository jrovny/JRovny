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

  getElapsedTime(): string {
    // Get number of minutes
    let seconds =
      (Date.now() - new Date(this.comment.createdDate).getTime()) / 1000;
    let minutes = seconds / 60;
    let hours = minutes / 60;
    let days = hours / 24;

    // Within the minute
    if (seconds < 60) {
      return 'Just now';
    }

    // Within the hour
    if (minutes < 60) {
      return `${Math.round(minutes)}m ago`;
    }

    // Within the day
    if (hours < 24) {
      return `${Math.round(hours)}h ago`;
    }

    // Within 3 weeks
    if (days < 21) {
      return `${Math.round(days)}d ago`;
    }

    return `${Math.round(days) / 7}w ago`;
  }
}
