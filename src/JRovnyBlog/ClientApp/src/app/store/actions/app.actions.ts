import { createAction, props } from '@ngrx/store';
import { PostDetail } from 'src/app/models/post-detail';
import { PostSummary } from 'src/app/models/post-summary';

export const loadPosts = createAction('[Post List] Load Posts');

export const loadPostsSuccess = createAction(
  '[Post List] Load Posts Success',
  props<{ data: PostSummary[] }>()
);

export const loadPostsFailure = createAction(
  '[Post List] Load Posts Failure',
  props<{ error: any }>()
);

export const loadPost = createAction(
  '[Post] Load Post',
  props<{ payload: string }>()
);

export const loadPostSuccess = createAction(
  '[Post] Load Post Success',
  props<{ payload: PostDetail }>()
);

export const loadPostFailure = createAction('[Post] Load Post Failure');

export const clearPost = createAction('[Post] Clear Post');
