import {
  createSelector,
  createFeatureSelector,
  ActionReducerMap,
} from '@ngrx/store';

import * as fromPost from './app.reducer';

export interface State {
  posts: fromPost.State;
}

export const reducers: ActionReducerMap<State> = {
  posts: fromPost.reducer,
};

export const selectPostState = createFeatureSelector<fromPost.State>(
  'appState'
);
export const selectPostIds = createSelector(
  selectPostState,
  fromPost.selectPostIds
);
export const selectPostEntities = createSelector(
  selectPostState,
  fromPost.selectPostEntities
);
export const selectAllPosts = createSelector(
  selectPostState,
  fromPost.selectAllPosts
);
export const selectCurrentPostId = createSelector(
  selectPostState,
  fromPost.getSelectedPostId
);
export const selectCurrentPost = createSelector(
  selectPostEntities,
  selectCurrentPostId,
  (posts, id) => posts[id]
);
