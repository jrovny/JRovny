import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import { PostSummary } from 'src/app/models/post-summary';
import { environment } from '../../../environments/environment';


// export interface State {
//   posts: PostSummary[]
// }

// const initialState: State = {
//   posts: []
// }

// export const reducers: ActionReducerMap<State> = {

// };


// export const metaReducers: MetaReducer<State>[] = !environment.production ? [] : [];
