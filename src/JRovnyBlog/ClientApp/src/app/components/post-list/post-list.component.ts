import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { PostSummary } from 'src/app/models/post-summary';
import { loadPosts } from 'src/app/store/actions/app.actions';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.scss'],
})
export class PostListComponent implements OnInit {
  loading$: Observable<boolean>;
  blogPosts$: Observable<PostSummary[]>;

  constructor(
    private store: Store<{
      appState: { posts: PostSummary[]; loadingPosts: boolean };
    }>
  ) {}

  ngOnInit(): void {
    this.loading$ = this.store.pipe(
      select((state) => state.appState.loadingPosts)
    );
    this.blogPosts$ = this.store.pipe(select((state) => state.appState.posts));
    this.store.dispatch(loadPosts());
  }
}
