export interface Comment {
  commentId: number;
  parentCommentId?: number;
  content: string;
  userName: string;
  emailHash?: string;
  createdDate: Date;
  children?: Comment[];
}
