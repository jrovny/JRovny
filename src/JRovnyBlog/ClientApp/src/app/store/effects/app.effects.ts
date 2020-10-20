import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { map, mergeMap, catchError, exhaustMap } from 'rxjs/operators';
import { AppService } from 'src/app/services/app.service';
import {
  loadPost,
  loadPostFailure,
  loadPosts,
  loadPostsFailure,
  loadPostsSuccess,
  loadPostSuccess,
} from '../actions/app.actions';

@Injectable()
export class AppEffects {
  loadPosts$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadPosts),
      mergeMap(() =>
        this.appService.getBlogPostSummaries().pipe(
          map((posts) => loadPostsSuccess({ data: posts })),
          catchError(() => of(loadPostsFailure))
        )
      )
    )
  );

  loadPost$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadPost),
      mergeMap(({ payload }) =>
        this.appService.getBlogPostBySlug(payload).pipe(
          map((post) => loadPostSuccess({ payload: post })),
          catchError(() => of(loadPostFailure()))
        )
      )
    )
  );

  constructor(private actions$: Actions, private appService: AppService) {}
}
