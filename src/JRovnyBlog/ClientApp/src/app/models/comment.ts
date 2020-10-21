export interface Comment {
  commentId: number;
  parentCommentId?: number;
  content: string;
  userName: string;
  createdDate: Date;
  children?: Comment[];
}
