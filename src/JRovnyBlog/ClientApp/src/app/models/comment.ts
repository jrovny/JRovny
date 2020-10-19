export interface Comment {
    commentId: number,
    content: string,
    userName: string,
    createdDate: Date,
    children?: Comment[]
}
