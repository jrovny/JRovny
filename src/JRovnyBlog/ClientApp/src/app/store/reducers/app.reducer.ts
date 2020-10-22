import { createEntityAdapter, EntityAdapter, EntityState } from '@ngrx/entity';
import { Action, createReducer, on } from '@ngrx/store';
import { PostSummary } from 'src/app/models/post-summary';
import * as Actions from '../actions/app.actions';

export const appFeatureKey = 'app';

export interface State extends EntityState<PostSummary> {
  selectedPostId: number | null;
  loadingPosts: boolean;
  loadingSelectedPost: boolean;
}

export const adapter: EntityAdapter<PostSummary> = createEntityAdapter<
  PostSummary
>({
  selectId: (post) => post.postId,
  sortComparer: (p1, p2) => {
    return p2.postId - p1.postId;
  },
});

export const initialState: State = adapter.getInitialState({
  selectedPostId: null,
  loadingPosts: false,
  loadingSelectedPost: false,
});

const appReducer = createReducer(
  initialState,
  on(Actions.loadPosts, (state) => ({ ...state, loadingPosts: true })),
  on(Actions.loadPostsSuccess, (state, { data }) => {
    return adapter.setAll(data, { ...state, loadingPosts: false });
  }),
  on(Actions.loadPostsFailure, (state) => ({ ...state, loadingPosts: false })),
  on(Actions.loadPost, (state) => ({ ...state, loadingPost: true })),
  on(Actions.loadPostSuccess, (state, { payload }) => ({
    ...state,
    loadingPost: false,
    selectedPost: payload,
    selectedPostId: payload.postId,
  })),
  on(Actions.loadPostFailure, (state) => ({
    ...state,
    loadingPost: false,
    selectedPostId: null,
  })),
  on(Actions.clearPost, (state) => ({
    ...state,
    selectedPost: null,
    loadingPost: false,
    selectedPostId: null,
  }))
);

export const getSelectedPostId = (state: State) => state.selectedPostId;

const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = adapter.getSelectors();

export const selectPostIds = selectIds;
export const selectPostEntities = selectEntities;
export const selectAllPosts = selectAll;
export const selectPostTotal = selectTotal;

export function reducer(state: State | undefined, action: Action) {
  return appReducer(state, action);
}
