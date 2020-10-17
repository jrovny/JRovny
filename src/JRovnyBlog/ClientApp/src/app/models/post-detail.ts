import { Tag } from './tag';

export interface PostDetail {
    postId: number,
    title: string,
    content: string,
    slug: string,
    viewCount: number,
    commentCount: number,
    upvoteCount: number,
    published: boolean,
    createdDate: Date,
    modifiedDate: Date,
    userId: number,
    comments: any,
    tags: Tag[]
}