import { EntityState } from '@ngrx/entity';
import { Action, createReducer, on } from '@ngrx/store';
import { PostDetail } from 'src/app/models/post-detail';
import { PostSummary } from 'src/app/models/post-summary';
import * as Actions from '../actions/app.actions';

export const appFeatureKey = 'app';

export interface AppState {
  loadingPosts: boolean;
  posts: PostSummary[];
  loadingPost: boolean;
  selectedPost?: PostDetail;
}

export const initialState: AppState = {
  loadingPosts: false,
  posts: [],
  loadingPost: false,
};

export interface State extends EntityState<PostSummary> {
  selectedPostId: number | null;
}

const appReducer = createReducer(
  initialState,
  on(Actions.loadPosts, (state) => ({ ...state, loadingPosts: true })),
  on(Actions.loadPostsSuccess, (state, { data }) => ({
    ...state,
    loadingPosts: false,
    posts: data,
  })),
  on(Actions.loadPostsFailure, (state) => ({ ...state, loadingPosts: false })),
  on(Actions.loadPost, (state) => ({ ...state, loadingPost: true })),
  on(Actions.loadPostSuccess, (state, { payload }) => ({
    ...state,
    loadingPost: false,
    selectedPost: payload,
  })),
  on(Actions.loadPostFailure, (state) => ({ ...state, loadingPost: false })),
  on(Actions.clearPost, (state) => ({
    ...state,
    selectedPost: null,
    loadingPost: false,
  }))
);

export function reducer(state: AppState | undefined, action: Action) {
  return appReducer(state, action);
}
